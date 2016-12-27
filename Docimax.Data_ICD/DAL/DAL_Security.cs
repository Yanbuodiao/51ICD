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
                    CreateTime = model.LastSendTime,
                    LastModifyTime = model.LastSendTime,
                    SourceIP = model.SourceIP,
                    UserID = model.UserID,
                    UserPhoneNumber = model.PhoneNumber,
                };
                entity.Sec_Message.Add(newModel);
                entity.SaveChanges();
            }
        }
        public ICDExcuteResult<int> DymaticCodeVerify(string phoneNum, string sourceIP, int shouldDelaySecond)
        {
            using (var entity = new Entity_Read())
            {
                var beginTime = DateTime.Now.AddDays(-1);
                var all = entity.Sec_Message.Where(e => e.CreateTime > beginTime && (e.UserPhoneNumber == phoneNum || e.SourceIP == sourceIP)).ToList();
                if (all != null && all.Count > 0)
                {
                    if (all.Count(e => e.SourceIP == sourceIP) > 20)
                    {
                        return new ICDExcuteResult<int>
                        {
                            IsSuccess = false,
                            ErrorStr = "机器人？过段时间在试试吧",
                        };
                    }
                    if (all.Count(e => e.UserPhoneNumber == phoneNum) > 5)
                    {
                        return new ICDExcuteResult<int>
                        {
                            IsSuccess = false,
                            ErrorStr = "您今天注册的次数太多？过段时间在试试吧",
                        };
                    }
                    var lastCode = all.Where(e => e.UserPhoneNumber == phoneNum).OrderByDescending(t => t.SecurityMessage_ID).FirstOrDefault();
                    if (lastCode != null)
                    {
                        var spanTime = (int)(DateTime.Now - (lastCode.CreateTime ?? DateTime.Now)).TotalSeconds;
                        if (shouldDelaySecond > spanTime)
                        {
                            return new ICDExcuteResult<int>
                            {
                                IsSuccess = true,
                                TResult = shouldDelaySecond - spanTime,
                            };
                        }
                    }
                    return new ICDExcuteResult<int>
                    {
                        IsSuccess = true,
                        TResult = shouldDelaySecond,
                    };
                }
            }
            return new ICDExcuteResult<int>
            {
                IsSuccess = true,
                TResult = shouldDelaySecond,
            };
        }

        public bool IsPhoneNumUnicity(string uid, string phoneNum)
        {
            using (var entity = new Entity_Read())
            {
                var query = entity.AspNetUsers.Where(e => e.PhoneNumber == phoneNum);
                if (!string.IsNullOrWhiteSpace(uid))
                {
                    query = query.Where(e => e.Id != uid);
                }
                return query.FirstOrDefault() == null;
            }
        }
    }
}
