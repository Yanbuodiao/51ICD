using Docimax.CodeOrder.Data;
using Docimax.CodeOrder.Interface;
using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_Code_Order : ICode_Order
    {
        public CodeOrderModel GetCodeOrderDetail(string userID, int codeOrderID, string serviceName = "ICD编码服务")
        {
            var result = GetNewCodeOrder(userID, serviceName);
            using (var entity = new Entity_Read())
            {
                var codeModel = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == codeOrderID);
                if (codeModel != null)
                {
                    result.CaseNum = codeModel.CaseNum;
                    result.PlatformOrderCode = codeModel.PlatformOrderCode;
                    result.CreateTime = codeModel.Createtime;
                }
                var codeItems = entity.Code_Order_UploadedItem.Where(e => e.CodeOrderID == codeOrderID && e.DeleteFlag != 1).ToList().Select(p => new UploadedItemModel
                {
                    AttachFileName = p.AttachName,
                    AttachURL = p.AttachURL,
                    CodeOrderID = codeOrderID,
                    CodeOrderItemID = p.CodeOrderUploadedItemID,
                    ContentType = p.ContentType,
                    ItemID = p.ItemID ?? 0,
                    GIndex = p.GIndex ?? 0,
                }).ToList();
                result.ItemList.ForEach(e => e.UploadedItemList = buildUploadedItems(e, codeItems));
            }
            return result;
        }

        public CodeOrderModel GetNewCodeOrder(string userID, string serviceName)
        {
            using (var entity = new Entity_Read())
            {
                var serviceAuditStatusInt = CertificateState.认证成功.GetHashCode();
                var orgModel = (from u in entity.AspNetUsers.Where(e => e.Id == userID)
                                join o in entity.Dic_Organization on u.ORGID equals o.OrganizationID
                                select new { u.ORGID, u.SubORGID, o.OrganizationName, o.OrganizationCode }).FirstOrDefault();
                if (orgModel == null)
                {
                    return null;
                }
                var query = (from org_service in entity.ORG_Service_Config.Where(e => e.ServiceAuditStatus == serviceAuditStatusInt)
                             join org_service_upload in entity.ORG_Service_Item.Where(e => e.DeleteFlag != 1) on org_service.ORGID equals org_service_upload.ORGID
                             join service in entity.Dic_Service.Where(e => e.ServiceName == serviceName) on org_service_upload.ServiceID equals service.ServiceID
                             join upload in entity.Dic_Item on org_service_upload.ItemID equals upload.ItemID
                             where orgModel.ORGID == org_service.ORGID && ((org_service_upload.Sub_ORGID ?? 0) == (orgModel.SubORGID ?? 0))
                             select new
                             {
                                 upload.ItemID,
                                 upload.ItemIndex,
                                 upload.ItemName,
                                 ParentID = upload.ParentID ?? 0,
                                 upload.ItemDescription
                             }).ToList().Select(e => new ItemModel
                             {
                                 ParentID = e.ParentID,
                                 ItemDescription = e.ItemDescription,
                                 ItemID = e.ItemID,
                                 ItemIndex = e.ItemIndex,
                                 ItemName = e.ItemName,
                             }).ToList();
                var result = query.Where(e => e.ParentID == 0).OrderBy(t => t.ItemIndex).ToList();
                result.ForEach(e => e.ChildrenList = getChildrenItemList(e.ItemID, query));
                return new CodeOrderModel
                {
                    ORGID = orgModel.ORGID ?? 0,
                    ORGName = orgModel.OrganizationName,
                    ORGSubID = orgModel.SubORGID ?? 0,
                    ORGCode = orgModel.OrganizationCode,
                    ItemList = result
                };
            }
        }

        public ICDExcuteResult SaveNewCodeOrder(CodeOrderModel newCodeOrder, bool isSubmit)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    if (newCodeOrder == null)
                    {
                        return new ICDExcuteResult { Result = false, ErrorStr = "订单实体不能为空" };
                    }
                    var stateInt = isSubmit ? ICDOrderState.待抢单.GetHashCode() : ICDOrderState.新创建.GetHashCode();
                    try
                    {
                        var model = entity.Code_Order.FirstOrDefault(e => e.CaseNum == newCodeOrder.CaseNum && e.ORGID == newCodeOrder.ORGID && e.ORGSubID == newCodeOrder.ORGSubID);
                        if (model != null)
                        {
                            model.LastModifyTime = newCodeOrder.LastModifyTime;
                            model.LastModifyUserID = newCodeOrder.LastModifyUserID;
                            if (isSubmit) { model.OrderStatus = stateInt; }
                        }
                        else
                        {
                            ICodeNum codeNumBuilder = new CodeNum_Access();
                            model = new Code_Order
                            {
                                CaseNum = newCodeOrder.CaseNum,
                                Createtime = newCodeOrder.CreateTime,
                                CreateUserID = newCodeOrder.CreateUserID,
                                LastModifyTime = newCodeOrder.LastModifyTime,
                                LastModifyUserID = newCodeOrder.LastModifyUserID,
                                ServiceID = newCodeOrder.ServiceID,
                                ORGID = newCodeOrder.ORGID,
                                ORGSubID = newCodeOrder.ORGSubID,
                                PlatformOrderCode = codeNumBuilder.InitCodeNum("01"),
                                OrderStatus = stateInt,
                            };
                            entity.Code_Order.Add(model);
                            entity.SaveChanges();
                        }
                        codeOrderSaveUploadedItem(newCodeOrder, entity, model);
                        entity.SaveChanges();
                        trasanction.Commit();
                    }
                    catch (Exception ex)
                    {
                        trasanction.Rollback();
                        //todo 记录错误日志
                        return new ICDExcuteResult { Result = false, ErrorStr = "系统正忙，稍候再试" };
                    }
                    return new ICDExcuteResult { Result = true };
                }
            }
        }

        public ICDPagedList<CodeOrderSearchModel, CodeOrderModel> GetUpLoadedCodeOrderList(ICDPagedList<CodeOrderSearchModel, CodeOrderModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var endDate = queryModel.EndDate.AddDays(1);
                var stateInt = queryModel.SearchModel.OrderState.GetHashCode();
                var query = (from u in entity.AspNetUsers.Where(e => e.Id == queryModel.SearchModel.UserID)
                             join o in entity.Code_Order on u.ORGID equals o.ORGID
                             where o.Createtime >= queryModel.BeginDate &&
                             o.Createtime <= endDate &&
                             (stateInt == 0 ? true : o.OrderStatus == stateInt) &&
                             ((o.ORGSubID ?? 0) == (u.SubORGID ?? 0)) &&
                             (string.IsNullOrEmpty(queryModel.TextFilter) ? true : (o.CaseNum.Contains(queryModel.TextFilter) || o.PlatformOrderCode.Contains(queryModel.TextFilter)))
                             select new
                             {
                                 o.CaseNum,
                                 o.PlatformOrderCode,
                                 o.OrderStatus,
                                 o.Createtime,
                                 o.ORGID,
                                 o.ORGSubID,
                                 o.CodeOrderID
                             }).OrderByDescending(e => e.Createtime);
                var resultQuery = query.Skip((queryModel.Page - 1) * queryModel.PageSize)
                    .Take(queryModel.PageSize)
                    .ToList().Select(e => new CodeOrderModel
                    {
                        CodeOrderID = e.CodeOrderID,
                        CaseNum = e.CaseNum,
                        PlatformOrderCode = e.PlatformOrderCode,
                        CreateTime = e.Createtime ?? DateTime.Now,
                        ORGID = e.ORGID ?? 0,
                        OrderStatus = (ICDOrderState)(e.OrderStatus ?? 1000),
                    }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
            }
            return queryModel;
        }

        public ICDPagedList<CodeOrderSearchModel, CodeOrderModel> GetUnClaimOrderList(ICDPagedList<CodeOrderSearchModel, CodeOrderModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var stateInt = queryModel.SearchModel.OrderState.GetHashCode();
                var query = (from org in entity.Dic_Organization
                             join o in entity.Code_Order on org.OrganizationID equals o.ORGID
                             where o.OrderStatus == stateInt
                             select new
                             {
                                 o.PlatformOrderCode,
                                 org.OrganizationName,
                                 o.OrderStatus,
                                 o.Createtime,
                                 o.ORGID,
                                 o.ORGSubID,
                                 o.CodeOrderID
                             }).OrderByDescending(e => e.Createtime);
                var resultQuery = query.Skip((queryModel.Page - 1) * queryModel.PageSize)
                    .Take(queryModel.PageSize)
                    .ToList().Select(e => new CodeOrderModel
                    {
                        CodeOrderID = e.CodeOrderID,
                        PlatformOrderCode = e.PlatformOrderCode,
                        ORGName = e.OrganizationName,
                        CreateTime = e.Createtime ?? DateTime.Now,
                        ORGID = e.ORGID ?? 0,
                        OrderStatus = (ICDOrderState)(e.OrderStatus ?? 1000),
                    }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
            }
            return queryModel;
        }

        public ICDPagedList<CodeOrderSearchModel, CodeOrderModel> GetMyCodeOrderList(ICDPagedList<CodeOrderSearchModel, CodeOrderModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var endDate = queryModel.EndDate.AddDays(1);
                var stateInt = queryModel.SearchModel.OrderState.GetHashCode();
                var query = (from o in entity.Code_Order.Where(e => e.PickedUserID == queryModel.SearchModel.UserID)
                             where o.Createtime >= queryModel.BeginDate &&
                             o.Createtime <= endDate &&
                             (stateInt == 0 ? true : o.OrderStatus == stateInt) &&
                             (string.IsNullOrEmpty(queryModel.TextFilter) ? true : (o.CaseNum.Contains(queryModel.TextFilter) || o.PlatformOrderCode.Contains(queryModel.TextFilter)))
                             select new
                             {
                                 o.CaseNum,
                                 o.PlatformOrderCode,
                                 o.OrderStatus,
                                 o.Createtime,
                                 o.ORGID,
                                 o.ORGSubID,
                                 o.CodeOrderID
                             }).OrderByDescending(e => e.Createtime);
                var resultQuery = query.Skip((queryModel.Page - 1) * queryModel.PageSize)
                    .Take(queryModel.PageSize)
                    .ToList().Select(e => new CodeOrderModel
                    {
                        CodeOrderID = e.CodeOrderID,
                        CaseNum = e.CaseNum,
                        PlatformOrderCode = e.PlatformOrderCode,
                        CreateTime = e.Createtime ?? DateTime.Now,
                        ORGID = e.ORGID ?? 0,
                        OrderStatus = (ICDOrderState)(e.OrderStatus ?? 1000),
                    }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
            }
            return queryModel;

        }

        #region Private Function

        private List<ItemModel> getChildrenItemList(int parentID, List<ItemModel> allResultNeedUploadItems)
        {
            var result = allResultNeedUploadItems.Where(p => p.ParentID == parentID).OrderBy(e => e.ItemIndex).ToList();
            result.ForEach(e => e.ChildrenList = getChildrenItemList(e.ItemID, allResultNeedUploadItems));
            return result;
        }
        private List<UploadedItemModel> buildUploadedItems(ItemModel item, List<UploadedItemModel> allUploadedItemList)
        {
            if (item != null)
            {
                if (item.ChildrenList != null && item.ChildrenList.Count > 0)
                {
                    item.ChildrenList.ForEach(e => e.UploadedItemList = buildUploadedItems(e, allUploadedItemList));
                }
                return allUploadedItemList.Where(e => e.ItemID == item.ItemID).OrderBy(p => p.GIndex).ToList();
            }
            return null;
        }
        private static void codeOrderSaveUploadedItem(CodeOrderModel newCodeOrder, Entity_Write entity, Code_Order model)
        {
            foreach (var item in newCodeOrder.UploadedList)
            {
                var newUploadedItem = new Code_Order_UploadedItem
                {
                    AttachURL = item.AttachURL,
                    AttachName = item.AttachFileName,
                    CodeOrderID = model.CodeOrderID,
                    ContentType = item.ContentType,
                    CreateTime = item.CreateTime,
                    CreateUserID = item.CreateUserID,
                    GIndex = item.GIndex,
                    ItemID = item.ItemID,
                };
                entity.Code_Order_UploadedItem.Add(newUploadedItem);
            }
        }

        #endregion
    }
}
