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
        public CodeOrderModel GetNewCodeOrder(string userID, string serviceName)
        {
            using (var entity = new Entity_Read())
            {
                var serviceAuditStatusInt = CertificateState.认证成功.GetHashCode();
                var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == userID);
                var query = (from org_service in entity.ORG_Service_Config.Where(e => e.ServiceAuditStatus == serviceAuditStatusInt)
                             join org_service_upload in entity.ORG_Service_Item.Where(e => e.DeleteFlag != 1) on org_service.ORGID equals org_service_upload.ORGID
                             join service in entity.Dic_Service.Where(e => e.ServiceName == serviceName) on org_service_upload.ServiceID equals service.ServiceID
                             join upload in entity.Dic_Item on org_service_upload.ItemID equals upload.ItemID
                             where user.ORGID == org_service.ORGID && ((org_service_upload.Sub_ORGID ?? 0) == (user.SubORGID ?? 0))
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
                result.ForEach(e => e.ChildrenList = GetUploadItemList(e.ItemID, query));
                return new CodeOrderModel
                {
                    ORGID = user.ORGID??0,
                    ORGSubID = user.SubORGID??0,
                    ItemList = result
                };
            }
        }

        private List<ItemModel> GetUploadItemList(int parentID, List<ItemModel> allResultUploadItems)
        {
            var result = allResultUploadItems.Where(p => p.ParentID == parentID).OrderBy(e => e.ItemIndex).ToList();
            result.ForEach(e => e.ChildrenList = GetUploadItemList(e.ItemID, allResultUploadItems));
            return result;
        }
    }
}
