using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Docimax.Data_ICD.DAL
{
    public class DAL_Security : ISecurity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool NeedVerifyIP(SecurityIPModel Model)
        {
            using (var entity = new Entity_Write())
            {
                var a = entity.Database.BeginTransaction();
                var item = entity.Sec_Message.FirstOrDefault(e => e.SourceIP == Model.SourIP);
                if (item != null)
                {
                    if ((item.LastModifyTime ?? DateTime.Now).AddMinutes(3) <= Model.CreateTime)
                    {
                        //item.InValidateTime = item.InValidateTime ?? 0 + 1;
                    }
                }

            }

            return false;
        }

        public SecurityPhoneModel GetLastPhoneMessage(string phoneNumber)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.Sec_Message.Where(e => e.UserPhoneNumber == phoneNumber).OrderByDescending(e => e.SecurityMessage_ID).FirstOrDefault();
                if (model == null)
                {
                    return null;
                }
                return new SecurityPhoneModel
                {
                    LastSendTime = model.CreateTime ?? DateTime.Now,
                    SourceIP = model.SourceIP,
                    PhoneNumber = model.UserPhoneNumber,
                };
            }
        }

        public void SavePhoneMessage(SecurityPhoneModel model)
        {
            using (var entity = new Entity_Write())
            {
                var newModel = new Sec_Message
                {
                    CreateTime=model.LastSendTime,
                    LastModifyTime=model.LastSendTime,
                    SourceIP=model.SourceIP,
                    UserID=model.UserID,
                    UserPhoneNumber=model.PhoneNumber,                    
                };
                entity.Sec_Message.Add(newModel);
                entity.SaveChanges();
            }
        }
    }
}
