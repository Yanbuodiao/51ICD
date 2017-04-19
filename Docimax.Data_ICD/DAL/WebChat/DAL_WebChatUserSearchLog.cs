using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Interface.WebChat;
using Docimax.Interface_ICD.Model.WebChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Data_ICD.DAL.WebChat
{
    public class DAL_WebChatUserSearchLog : IWebChatUserSearchLog
    {
        public void InsertUserSearchLog(WebChatUserSearchLog logModel)
        {
            using (var entity = new Entity_Write())
            {
                var newModel = new WebChat_UserSearchLog
                {
                    LogID = Guid.NewGuid().ToString("N"),
                    Openid = logModel.OpentId,
                    SearchFilter = logModel.SearchFilter,
                    SearchTime = DateTime.UtcNow,
                    ICDVersionID = logModel.ICDVersionID,
                    ICDVersionType = logModel.ICDVersionType,
                    SegmentResult = logModel.SegmentResult,
                };
                entity.WebChat_UserSearchLog.Add(newModel);
                entity.SaveChanges();
            }
        }
    }
}
