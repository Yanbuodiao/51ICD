﻿using Docimax.CodeOrder.Data;
using Docimax.CodeOrder.Interface;
using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Message;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.UploadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_Code_Order : ICode_Order
    {
        public CodeOrderModel GetCodeOrderDetail(string userID, int codeOrderID)
        {
            using (Entity_Read entity = new Entity_Read())
            {
                var codeModel = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == codeOrderID);
                if (codeModel == null)
                {
                    return null;
                }
                if (checkUserCanViewOrder(userID, codeModel))
                {
                    var result = getCodeOrderTemplate((e => e.OrganizationID == codeModel.ORGID),
                        (e => e.ServiceID == codeModel.ServiceID),
                        (e => e.SubORGID == codeModel.ORGSubID));
                    if (codeModel != null)
                    {
                        result.CodeOrderID = codeOrderID;
                        result.PickedUserID = codeModel.PickedUserID;
                        result.CaseNum = codeModel.CaseNum;
                        result.PlatformOrderCode = codeModel.PlatformOrderCode;
                        result.CreateTime = codeModel.Createtime;
                        result.OrderStatus = (ICDOrderState)codeModel.OrderStatus;
                        result.LastModifyStamp = Convert.ToBase64String(codeModel.LastModifyStamp);
                    }
                    var dbDiagnosis = entity.Code_Order_Diagnosis.Where(e => e.CodeOrderID == codeOrderID)
                        .ToList().Select(e => new DiagnosisCodeResult
                        {
                            CodeOrderID = codeOrderID,
                            Diagnosis = e.ClinicalDiagnosis,
                            ICDCode = e.ICD_Code,
                            ICDName = e.ICD_Content,
                            DisplayText = string.Format("{0}-{1}", e.ICD_Code, e.ICD_Content),
                            Index = e.DiagnosisIndex ?? 0,
                        }).ToList();
                    var dbOperate = entity.Code_Order_Operate.Where(e => e.CodeOrderID == codeOrderID)
                        .ToList().Select(e => new OperationCodeResult
                        {
                            CodeOrderID = codeOrderID,
                            OperationName = e.ClinicalOperate,
                            ICDCode = e.ICDCode,
                            ICDName = e.ICDContent,
                            DisplayText = string.Format("{0}-{1}", e.ICDCode, e.ICDContent),
                            Index = e.OperateIndex ?? 0,
                        }).ToList();
                    result.OperationCodeResultList = dbOperate;
                    result.DiagnosisCodeResultList = dbDiagnosis;
                    var codeItems = entity.Code_Order_UploadedItem.Where(e => e.CodeOrderID == codeOrderID && e.DeleteFlag != 1)
                        .ToList().Select(p => new UploadedItemModel
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
                    return result;
                }
                return null;
            }
        }

        public CodeOrderModel GetNewCodeOrder(string userID, string serviceName)
        {
            using (var entity = new Entity_Read())
            {
                var serviceAuditStatusInt = CertificateState.认证通过.GetHashCode();
                var orgModel = (from u in entity.AspNetUsers.Where(e => e.Id == userID)
                                join o in entity.Dic_Organization on u.ORGID equals o.OrganizationID
                                select new { u.ORGID, u.SubORGID, o.OrganizationName, o.OrganizationCode }).FirstOrDefault();
                if (orgModel == null)
                {
                    return null;
                }
                return getCodeOrderTemplate((e => e.OrganizationID == orgModel.ORGID),
                         (e => e.ServiceName == serviceName),
                         (e => e.SubORGID == orgModel.SubORGID));
            }
        }

        public ICDExcuteResult<int> SaveNewCodeOrder(CodeOrderModel newCodeOrder, bool isSubmit)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    if (newCodeOrder == null)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "订单实体不能为空" };
                    }
                    var stateInt = isSubmit ? ICDOrderState.待抢单.GetHashCode() : ICDOrderState.新创建.GetHashCode();
                    try
                    {
                        var model = entity.Code_Order.FirstOrDefault(e => (newCodeOrder.CodeOrderID > 0) ? e.CodeOrderID == newCodeOrder.CodeOrderID : e.CaseNum == newCodeOrder.CaseNum && e.ORGID == newCodeOrder.ORGID && e.ORGSubID == newCodeOrder.ORGSubID);
                        if (model != null)
                        {
                            model.LastModifyTime = newCodeOrder.LastModifyTime;
                            model.LastModifyUserID = newCodeOrder.LastModifyUserID;
                            if (model.OrderStatus <= stateInt)
                            {
                                model.OrderStatus = stateInt;
                            }
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
                            newCodeOrder.CodeOrderID = model.CodeOrderID;
                        }
                        codeOrderSaveUploadedItem(newCodeOrder, entity, model);
                        entity.SaveChanges();
                        trasanction.Commit();
                    }
                    catch (Exception ex)
                    {
                        trasanction.Rollback();
                        //todo 记录错误日志
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "系统正忙，稍候再试" };
                    }
                    return new ICDExcuteResult<int> { IsSuccess = true, TResult = newCodeOrder.CodeOrderID };
                }
            }
        }

        public ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> GetUpLoadedCodeOrderList(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> queryModel)
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
                                 o.OrderType,
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
                        OrderType = (OrderTypeEnum)(e.OrderType ?? 0),
                    }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
            }
            return queryModel;
        }

        public ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> GetUnClaimOrderList(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var serviceName = "ICD编码";
                var stateInt = queryModel.SearchModel.OrderState.GetHashCode();
                var query = (from org in entity.Dic_Organization
                             join o in entity.Code_Order on org.OrganizationID equals o.ORGID
                             join sc in entity.ORG_Service_Config on o.ORGID equals sc.ORGID
                             join s in entity.Dic_Service.Where(e => e.ServiceName == serviceName) on sc.ServiceID equals s.ServiceID
                             where o.OrderStatus == stateInt &&
                             (sc.IsAssigned != 1 || (entity.ORG_Service_Assign.Any(e => e.UserID == queryModel.SearchModel.UserID && e.Enable == true && e.ORG_ID == o.ORGID)))
                             select new
                             {
                                 o.PlatformOrderCode,
                                 org.OrganizationName,
                                 o.OrderStatus,
                                 o.OrderType,
                                 o.Createtime,
                                 o.ORGID,
                                 o.ORGSubID,
                                 o.CodeOrderID,
                                 o.LastModifyStamp,
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
                        OrderType = (OrderTypeEnum)(e.OrderType ?? 0),
                        LastModifyStamp = Convert.ToBase64String(e.LastModifyStamp),
                    }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
            }
            return queryModel;
        }

        public ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> GetMyCodeOrderList(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var endDate = queryModel.EndDate.AddDays(1);
                var stateInt = queryModel.SearchModel.OrderState.GetHashCode();
                var query = (from o in entity.Code_Order.Where(e => e.PickedUserID == queryModel.SearchModel.UserID)
                             join or in entity.Dic_Organization on o.ORGID equals or.OrganizationID
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
                                 o.OrderType,
                                 o.ORGSubID,
                                 or.OrganizationName,
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
                        ORGName = e.OrganizationName,
                        OrderStatus = (ICDOrderState)(e.OrderStatus ?? 1000),
                        OrderType = (OrderTypeEnum)(e.OrderType ?? 0),
                    }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
            }
            return queryModel;

        }

        public ICDExcuteResult<int> ClaimCodeOrder(string userID, string stamp, int codeOrderID)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    var originStateInt = ICDOrderState.待抢单.GetHashCode();
                    var newStateInt = ICDOrderState.已接单.GetHashCode();
                    var model = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == codeOrderID);
                    if (model == null)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未找到相应的订单" };
                    }
                    if (model.OrderStatus != originStateInt || Convert.ToBase64String(model.LastModifyStamp) != stamp)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "很遗憾，您没有抢到" };
                    }
                    model.LastModifyUserID = userID;
                    model.LastModifyTime = DateTime.Now;
                    model.OrderStatus = newStateInt;
                    model.PickedUserID = userID;
                    model.PickedTime = DateTime.Now;
                    entity.SaveChanges();
                    trasanction.Commit();
                }
                return new ICDExcuteResult<int> { IsSuccess = true, TResult = codeOrderID };
            }
        }

        public ICDExcuteResult<int> SaveCodeResult(CodeOrderModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    var orderModel = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == model.CodeOrderID);
                    if (orderModel == null)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未找到相应的订单" };
                    }
                    if (Convert.ToBase64String(orderModel.LastModifyStamp) != model.LastModifyStamp)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "很遗憾，已经有人在您前面提交" };
                    }
                    if (orderModel.OrderStatus < model.OrderStatus.GetHashCode())
                    {
                        orderModel.OrderStatus = model.OrderStatus.GetHashCode();
                    }
                    orderModel.LastModifyUserID = model.LastModifyUserID;
                    orderModel.LastModifyTime = DateTime.Now;

                    #region 诊断处理

                    uploadDiagnosis(model.CodeOrderID, model.DiagnosisCodeResultList, entity);

                    #endregion

                    #region 手术操作处理

                    uploadOperate(model.CodeOrderID, model.OperationCodeResultList, entity);

                    #endregion

                    entity.SaveChanges();
                    trasanction.Commit();
                }
            }
            return new ICDExcuteResult<int> { IsSuccess = true, TResult = model.CodeOrderID };
        }

        /// <summary>
        /// 保存编码结果
        /// </summary>
        /// <param name="model">页面提交的编码结果</param>
        /// <returns>保存结果</returns>
        public ICDExcuteResult<int> SaveCodeResult(MedicalRecordBase model)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    var orderModel = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == model.CodeOrderID);
                    if (orderModel == null)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未找到相应的订单" };
                    }
                    if (Convert.ToBase64String(orderModel.LastModifyStamp) != model.LastModifyStamp)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "很遗憾，已经有人在您前面提交" };
                    }
                    if (orderModel.OrderStatus < model.OrderStatus.GetHashCode())
                    {
                        orderModel.OrderStatus = model.OrderStatus.GetHashCode();
                    }
                    orderModel.LastModifyUserID = model.LastModifyUserID;
                    orderModel.LastModifyTime = DateTime.Now;

                    #region 诊断处理

                    uploadDiagnosis(model.CodeOrderID, model.DiagnosisCodeResultList, entity);

                    #endregion

                    #region 手术操作处理

                    uploadOperate(model.CodeOrderID, model.OperationCodeResultList, entity);

                    #endregion

                    entity.SaveChanges();
                    trasanction.Commit();
                }
            }
            return new ICDExcuteResult<int> { IsSuccess = true, TResult = model.CodeOrderID };
        }

        public bool IsCodeOrderExistByMecicalRecord(MedicalRecordBase mr)
        {
            using (var entity = new Entity_Read())
            {
                return entity.Code_Order.Any(e => (e.AdmissionTimes ?? 0) == mr.AdmissionTimes &&
                    e.CaseNum == mr.MedicalRecordNO &&
                    e.OutTime == mr.DischargeDate);
            }
        }

        public ExcuteResult SaveCodeOrder(MedicalRecordBase mr, string authCode, string medicalRecordPath)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    if (entity.Code_Order.Any(e => e.CaseNum == mr.MedicalRecordNO && e.AdmissionTimes == mr.AdmissionTimes && e.OutTime == mr.DischargeDate))
                    {
                        return new ExcuteResult { IsSuccess = false, ErrorStr = MessageStr.MedicalRecordRepeat };
                    }
                    var stateInt = ICDOrderState.待抢单.GetHashCode();
                    ICodeNum codeNumBuilder = new CodeNum_Access();
                    var org = entity.ORG_Service_Config.FirstOrDefault(e => e.AuthCode == authCode);//todo  编码版本冗余存入订单表
                    var model = new Code_Order
                    {
                        AdmissionTimes = mr.AdmissionTimes,
                        CaseNum = mr.MedicalRecordNO,
                        OutTime = mr.DischargeDate,
                        OrderType = mr.OrderType.GetHashCode(),
                        MedicalRecordPath = medicalRecordPath,
                        PlatformOrderCode = codeNumBuilder.InitCodeNum("IC"),
                        AUTHCode = authCode,
                        ORGID = org == null ? 0 : org.ORGID,
                        Diagnosis_ICD_VersionID = org == null ? 0 : org.Diagnosis_ICD_VersionID,
                        Operation_ICD_VersionID = org == null ? 0 : org.Operation_ICD_VersionID,
                        OrderStatus = stateInt,
                        Createtime = DateTime.Now,
                        LastModifyTime = DateTime.Now,
                    };
                    try
                    {
                        entity.Code_Order.Add(model);
                        entity.SaveChanges();
                        trasanction.Commit();
                        return new ExcuteResult { IsSuccess = true };
                    }
                    catch (Exception ex)
                    {
                        //todo  记录异常记录
                        trasanction.Rollback();
                        return new ExcuteResult { IsSuccess = false, ErrorStr = MessageStr.InnerError };
                    }
                }
            }
        }

        public CodeOrderInterfaceModel GetCodeOrder(int codeOrderID)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == codeOrderID);
                var diagnosises = entity.Code_Order_Diagnosis.Where(e => e.CodeOrderID == codeOrderID).Select(e => new
                {
                });
                if (model == null)
                {
                    return null;
                }
                return new CodeOrderInterfaceModel
                {
                    CodeOrderID = codeOrderID,
                    PlatformOrderCode = model.PlatformOrderCode,
                    CaseNum = model.CaseNum,
                    MedicalRecordPath = model.MedicalRecordPath,
                    Diagnosis_ICD_VersionID = model.Diagnosis_ICD_VersionID ?? 0,
                    Operation_ICD_VersionID = model.Operation_ICD_VersionID ?? 0,
                    PickedID = model.PickedUserID,
                    LastModifyStamp = Convert.ToBase64String(model.LastModifyStamp),
                };
            }
        }
        public bool CanCodingMR(int codeOrderID, string uid)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == codeOrderID);
                if (model == null)
                {
                    return false;
                }
                var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == uid);
                if (user == null)
                {
                    return false;
                }
                var certificateState = CertificateState.认证通过.GetHashCode();
                if (model.PickedUserID == uid && user.CertificationFlag == certificateState && user.BankCertificationFlag == certificateState)
                {
                    return true;
                }
                //todo  后期需校验编码服务的认证状态
                return false;
            }
        }

        public bool CanViewingMR(int codeOrderID, string uid)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == codeOrderID);
                if (model == null)
                {
                    return false;
                }
                var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == uid);
                if (user == null)
                {
                    return false;
                }
                if (user.ORGID == model.ORGID && ((user.SubORGID ?? 0) == (model.ORGSubID ?? 0)))
                {
                    return true;
                }
                var certificateState = CertificateState.认证通过.GetHashCode();
                if (model.PickedUserID == uid && user.CertificationFlag == certificateState && user.BankCertificationFlag == certificateState)
                {
                    return true;
                }
                //todo  后期需校验编码服务的认证状态
                return false;
            }
        }

        public ICDExcuteResult<int> SaveInterfaceOrdeCode(MedicalRecord_Data model, ICDOrderState newState)
        {
            using (var entity = new Entity_Write())
            {
                using (var trasanction = entity.Database.BeginTransaction())
                {
                    var orderModel = entity.Code_Order.FirstOrDefault(e => e.CodeOrderID == model.CodeOrderID);
                    if (orderModel == null)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未找到相应的订单" };
                    }
                    if (Convert.ToBase64String(orderModel.LastModifyStamp) != model.LastModifyStamp)
                    {
                        trasanction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "很遗憾，已经有人在您前面提交" };
                    }
                    orderModel.LastModifyUserID = model.LastModifyUserID;
                    orderModel.LastModifyTime = DateTime.Now;

                    #region 诊断处理

                    uploadDiagnosis(model.CodeOrderID, model.DiagnosisCodeResultList, entity);

                    #endregion

                    #region 手术操作处理

                    uploadOperate(model.CodeOrderID, model.OperationCodeResultList, entity);

                    #endregion

                    return new ICDExcuteResult<int> { IsSuccess = true };
                }
            }
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
        private CodeOrderModel getCodeOrderTemplate(Expression<Func<Dic_Organization, bool>> org_Conditions,
            Expression<Func<Dic_Service, bool>> s_Conditions,
            Expression<Func<ORG_SubOrganization, bool>> sub_Conditions)
        {
            using (var entity = new Entity_Read())
            {
                var successStatusInt = CertificateState.认证通过.GetHashCode();
                var orgModel = (from org in entity.Dic_Organization.Where(e => e.DeleteFlag != 1).Where(org_Conditions)
                                join sub in entity.ORG_SubOrganization.Where(e => e.DeleteFlag != 1).Where(sub_Conditions)
                                     on org.OrganizationID equals sub.ORGID into newSubOrganization
                                from p in newSubOrganization.DefaultIfEmpty()
                                select new { org.OrganizationID, SubORGID = p != null ? p.SubORGID : 0, org.OrganizationName, org.OrganizationCode }).FirstOrDefault();
                if (orgModel == null)
                {
                    return null;
                }
                var query = (from org_Ser in entity.ORG_Service_Config.Where(e => e.ServiceAuditStatus == successStatusInt && e.EndTime > DateTime.Now)
                             join ser in entity.Dic_Service.Where(s_Conditions).Where(e => e.DeleteFlag != 1) on org_Ser.ServiceID equals ser.ServiceID
                             join org_Ser_item in entity.ORG_Service_Item.Where(e => e.DeleteFlag != 1) on org_Ser.ORGID equals org_Ser_item.ORGID
                             join upload in entity.Dic_Item on org_Ser_item.ItemID equals upload.ItemID
                             where orgModel.OrganizationID == org_Ser.ORGID && ((org_Ser_item.Sub_ORGID ?? 0) == orgModel.SubORGID)
                             select upload).ToList().Select(e => new ItemModel
                             {
                                 ParentID = e.ParentID ?? 0,
                                 ItemDescription = e.ItemDescription,
                                 ItemID = e.ItemID,
                                 ItemIndex = e.ItemIndex,
                                 ItemName = e.ItemName,
                             }).ToList();
                var result = query.Where(e => e.ParentID == 0).OrderBy(t => t.ItemIndex).ToList();
                result.ForEach(e => e.ChildrenList = getChildrenItemList(e.ItemID, query));
                return new CodeOrderModel
                {
                    ORGID = orgModel.OrganizationID,
                    ORGName = orgModel.OrganizationName,
                    ORGSubID = orgModel.SubORGID,
                    ORGCode = orgModel.OrganizationCode,
                    ItemList = result
                };
            }
        }

        private bool checkUserCanViewOrder(string userID, Code_Order order)
        {
            if (order.PickedUserID == userID)
            {
                return true;
            }
            using (var entity = new Entity_Read())
            {
                var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == userID);
                if (user != null)
                {
                    return user.ORGID == order.ORGID && ((user.SubORGID ?? 0) == (order.ORGSubID ?? 0));
                }
                return false;
            }
        }
        private void uploadDiagnosis(int codeOrderID, List<DiagnosisCodeResult> diagnosisList, Entity_Write entity)
        {
            var dbDiagnosis = entity.Code_Order_Diagnosis.Where(e => e.CodeOrderID == codeOrderID).ToList();
            dbDiagnosis.ForEach(e =>
            {
                var diagnosis = diagnosisList.FirstOrDefault(t => t.ICDCode == e.ICD_Code);
                if (diagnosis == null)//数据库有  新上传的没有  数据库中删除
                {
                    entity.Code_Order_Diagnosis.Remove(e);
                }
                else
                {
                    e.ClinicalDiagnosis = diagnosis.Diagnosis;
                    e.DiagnosisIndex = diagnosis.Index;
                    e.LastModifyTime = diagnosis.LastModifyTime;
                }
            });
            diagnosisList.Where(p => !string.IsNullOrWhiteSpace(p.ICDName)).ToList().ForEach(e =>
            {
                var diagnosis = dbDiagnosis.FirstOrDefault(t => t.ICD_Code == e.ICDCode);
                if (diagnosis == null)//新上传的有 数据库没有   数据库增加
                {
                    var newDiagnosis = new Code_Order_Diagnosis
                    {
                        CodeOrderID = codeOrderID,
                        CreateTime = e.CreateTime,
                        CreateUserID = e.CreateUserID,
                        DiagnosisIndex = e.Index,
                        ClinicalDiagnosis = e.Diagnosis,
                        ICD_Code = e.ICDCode,
                        ICD_Content = e.ICDName,
                        LastModifyTime = e.LastModifyTime,
                        LastModifyUserID = e.LastModifyUserID,
                    };
                    entity.Code_Order_Diagnosis.Add(newDiagnosis);
                }
            });
        }
        private void uploadOperate(int codeOrderID, List<OperationCodeResult> operateList, Entity_Write entity)
        {
            var dbOperate = entity.Code_Order_Operate.Where(e => e.CodeOrderID == codeOrderID).ToList();
            dbOperate.ForEach(e =>
            {
                var operate = operateList.FirstOrDefault(t => t.ICDCode == e.ICDCode);
                if (operate == null)//数据库有  新上传的没有  数据库中删除
                {
                    entity.Code_Order_Operate.Remove(e);
                }
                else
                {
                    e.ClinicalOperate = operate.OperationName;
                    e.OperateIndex = operate.Index;
                    e.LastModifyUserID = operate.LastModifyUserID;
                    e.LastModifyTime = operate.LastModifyTime;
                }
            });
            operateList.Where(p => !string.IsNullOrWhiteSpace(p.ICDName)).ToList().ForEach(e =>
            {
                var operate = dbOperate.FirstOrDefault(t => t.ICDCode == e.ICDCode);
                if (operate == null)//新上传的有 数据库没有   数据库增加
                {
                    var newOperate = new Code_Order_Operate
                    {
                        CodeOrderID = codeOrderID,
                        CreateTime = e.CreateTime,
                        CreateUserID = e.CreateUserID,
                        OperateIndex = e.Index,
                        ICDCode = e.ICDCode,
                        ICDContent = e.ICDName,
                        ClinicalOperate = e.OperationName,
                        LastModifyTime = e.LastModifyTime,
                        LastModifyUserID = e.LastModifyUserID,
                    };
                    entity.Code_Order_Operate.Add(newOperate);
                }
            });
        }

        #endregion
    }
}
