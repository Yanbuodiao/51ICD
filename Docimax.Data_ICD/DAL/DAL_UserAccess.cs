using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Linq;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_UserAccess : IUserAccess
    {
        public ICDExcuteResult<int> ApplyIdentityVerify(VerifyIdentityModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == model.UserID);
                    if (user == null)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { Result = false, ErrorStr = "未获得当前用户信息", };
                    }
                    if (user.CertificationFlag != null)
                    {
                        switch ((CertificateState)user.CertificationFlag)//以下状态不允许再次发起申请
                        {
                            case CertificateState.发起认证申请:
                            case CertificateState.平台人员二次审核通过:
                            case CertificateState.平台人员一次审核通过:
                            case CertificateState.认证成功:
                            case CertificateState.冻结:
                                return new ICDExcuteResult<int> { Result = false, ErrorStr = "已经发起过实名认证申请", };
                        }
                    }
                    user.CertificationFlag = model.CertificateFlag.GetHashCode();
                    user.RealName = model.RealName;
                    user.IDCardNo = model.IDCardNo;
                    user.BankCardNO = model.BankCardNO;
                    foreach (var item in model.FileList)
                    {
                        var userAttach = new User_Attach
                        {
                            UserID = model.UserID,
                            AttachType = item.AttachType,
                            ContentType = item.ContentType,
                            CreateTime = model.ApplyTime,
                            CreateUserID = model.UserID,
                            LastModifyUserID = model.UserID,
                            LastModityTime = model.ApplyTime,
                        };
                        entity.User_Attach.Add(userAttach);
                    }
                    entity.SaveChanges();
                    transaction.Commit();
                    return new ICDExcuteResult<int> { Result = true };
                }
            }
        }

        public UserInfoModel GetUserInfo(string userID)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.AspNetUsers.FirstOrDefault(e => e.Id == userID);
                var services = (from sp in entity.User_Service_Provider.Where(e => e.UserID == userID)
                                join s in entity.Dic_Service on sp.ServiceID equals s.ServiceID
                                select new
                                {
                                    s.ServiceID,
                                    s.ServiceName,
                                    s.Description,
                                    sp.CertificationStatus,
                                }).ToList();
                if (model == null)
                {
                    return new UserInfoModel();
                }
                var result = new UserInfoModel
                {
                    UserID = userID,
                    CertificationFlag = (CertificateState)(model.CertificationFlag ?? 0),
                    HasPassword = true,
                    PhoneNumber = model.PhoneNumber,
                    TwoFactor = model.TwoFactorEnabled,
                };
                if (services != null)
                {
                    result.Services = services.Select(e => new ServiceModel
                    {
                        ServiceID = e.ServiceID,
                        ServiceName = e.ServiceName,
                        Description = e.Description,
                        CertificateStatus = (CertificateState)(e.CertificationStatus ?? 0)
                    }).ToList();
                }
                return result;
            }
        }

        public ICDExcuteResult<int> ApplyServiceProvider(UserServiceApplyModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var userService = entity.User_Service_Provider.FirstOrDefault(e => e.UserID == model.UserID && e.ServiceID == model.Service.ServiceID);
                    if (userService != null)
                    {
                        if (userService.CertificationStatus != null)
                        {
                            switch ((CertificateState)userService.CertificationStatus)//以下状态不允许再次发起申请
                            {
                                case CertificateState.发起认证申请:
                                case CertificateState.平台人员二次审核通过:
                                case CertificateState.平台人员一次审核通过:
                                case CertificateState.认证成功:
                                case CertificateState.冻结:
                                    return new ICDExcuteResult<int> { Result = false, ErrorStr = "已经发起过服务认证申请" };
                            }
                        }
                    }
                    else
                    {
                        userService = new User_Service_Provider
                        {
                            UserID = model.UserID,
                            ServiceID = model.Service.ServiceID,
                            ApplyTime = DateTime.Now,
                            LastModityTime = DateTime.Now,
                            LastModifyUserID = model.UserID,
                            CreateTime = DateTime.Now,
                            CreateUserID = model.UserID,
                        };
                        entity.User_Service_Provider.Add(userService);
                        entity.SaveChanges();
                    }
                    userService.CertificationStatus = model.Service.CertificateStatus.GetHashCode();
                    var userServiceClaims = entity.User_Service_Claim.Where(t => t.User_ServiceID == userService.User_ServiceID).ToList();
                    model.Service.ServiceClaims.ForEach(e =>
                    {
                        var claim = userServiceClaims.FirstOrDefault(t => t.ClaimType == e.ClaimType.GetHashCode());
                        if (claim == null)
                        {
                            claim = new User_Service_Claim
                            {
                                CreateUserID = model.UserID,
                                CreateTime = DateTime.Now,
                                ClaimType = e.ClaimType.GetHashCode(),
                                User_ServiceID = userService.User_ServiceID
                            };
                            entity.User_Service_Claim.Add(claim);
                        }
                        claim.ClaimOperateType = e.ClaimOpereateType.GetHashCode();
                        claim.ClaimValue = e.ClaimValue;
                        claim.LastModifyTime = DateTime.Now;
                    });
                    model.Service.ServiceAttaches.ForEach(e =>
                    {
                        var userServiceAttach = new User_Service_Attach
                        {
                            User_ServiceID = userService.User_ServiceID,
                            AttachURL = e.FileURL,
                            AttachType = e.AttachType.GetHashCode(),
                            ContentType = e.ContentType,
                            CreateTime = DateTime.Now,
                            CreateUserID = model.UserID,
                            LastModifyUserID = model.UserID,
                            LastModityTime = DateTime.Now,
                        };
                        entity.User_Service_Attach.Add(userServiceAttach);
                    });
                    entity.SaveChanges();
                    transaction.Commit();
                    return new ICDExcuteResult<int> { Result = true };
                }
            }
        }

        public ICDPagedList<UserCertificationSearch, VerifyIdentityModel> GetUpLoadedCodeOrderList(ICDPagedList<UserCertificationSearch, VerifyIdentityModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var stateInt = queryModel.SearchModel.CertificateStatus.GetHashCode();
                var query = entity.AspNetUsers.Where(e => (stateInt == 0 ? true : e.CertificationFlag == stateInt) &&
                    string.IsNullOrEmpty(queryModel.TextFilter) ? true : (e.UserName.StartsWith(queryModel.TextFilter))).OrderBy(e => e.CertificationFlag).Skip((queryModel.Page - 1) * queryModel.PageSize)
                    .Take(queryModel.PageSize)
                    .ToList();
                var resultQuery = query.Select(e => new VerifyIdentityModel
                {
                    UserID = e.Id,
                    UserName = e.UserName,
                    RealName = e.RealName,
                    CertificateFlag = (CertificateState)e.CertificationFlag,
                }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
                return queryModel;
            }
        }
    }
}
