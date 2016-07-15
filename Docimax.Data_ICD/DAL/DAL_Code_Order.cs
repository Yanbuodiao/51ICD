using Docimax.Data_ICD.Entity;
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
                var query = (from u in entity.AspNetUsers
                             join org_service in entity.ORG_Service_UploadItem on u.ORGID equals org_service.ORGID
                             join service in entity.Dic_Service.Where(e => e.ServiceName == serviceName) on org_service.ServiceID equals service.ServiceID
                             join upload in entity.Dic_UploadItem on org_service.UploadItemID equals upload.UploadItemID
                             select new
                             {
                                 upload.UploadItemID,
                                 upload.UploadItemIndex,
                                 upload.UploadItemName,
                                 ParentID = upload.ParentID ?? 0,
                                 upload.UploadItemDescription
                             }).ToList().Select(e => new UploadItemModel
                             {
                                 ParentID = e.ParentID,
                                 UploadItemDescription = e.UploadItemDescription,
                                 UploadItemID = e.UploadItemID,
                                 UploadItemIndex = e.UploadItemIndex,
                                 UploadItemName = e.UploadItemName,
                             }).ToList();
                var result = query.Where(e => e.ParentID == 0).OrderBy(t => t.UploadItemIndex).ToList();
                result.ForEach(e => e.ChildrenList = GetUploadItemList(e.UploadItemID,query));
                return new CodeOrderModel { UpLoadItemList = result };
            }
        }

        private List<UploadItemModel> GetUploadItemList(int parentID, List<UploadItemModel> allResultUploadItems)
        {
            var result = allResultUploadItems.Where(p => p.ParentID == parentID).OrderBy(e => e.UploadItemIndex).ToList();
            result.ForEach(e => e.ChildrenList = GetUploadItemList(e.UploadItemID, allResultUploadItems));
            return result;
        }
    }
}
