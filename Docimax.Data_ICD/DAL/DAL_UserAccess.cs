using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_UserAccess : IUserAccess
    {
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
                    BankCertificationFlag = (CertificateState)(model.BankCertificationFlag ?? 0),
                    HasPassword = true,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
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
        public ICDExcuteResult<int> ApplyIdentityVerify(VerifyUserModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == model.UserID);
                    if (user == null)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未获得当前用户信息", };
                    }
                    if (user.CertificationFlag == null)
                    {
                        user.CertificationFlag = CertificateState.未申请.GetHashCode();
                    }
                    switch ((CertificateState)user.CertificationFlag)//以下状态不允许再次发起申请
                    {
                        case CertificateState.发起认证申请:
                        case CertificateState.平台人员二次审核通过:
                        case CertificateState.平台人员一次审核通过:
                        case CertificateState.认证通过:
                        case CertificateState.冻结:
                            transaction.Rollback();
                            return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "已经发起过实名认证申请", };
                    }
                    var userLog = new User_ChangeAuditLog
                    {
                        CreateTime = DateTime.Now,
                        UserID = model.UserID,
                        Operatedatetime = DateTime.Now,
                        OperateUserID = model.UserID,
                        OperateTarget = UserChangeTargetType.实名认证.GetHashCode(),
                        OperateType = model.CertificateFlag.GetHashCode(),
                        OriginValue = user.CertificationFlag.ToString(),
                        NewValue = model.CertificateFlag.GetHashCode().ToString(),
                        LastModifyTime = DateTime.Now,
                    };
                    entity.User_ChangeAuditLog.Add(userLog);
                    user.CertificationFlag = model.CertificateFlag.GetHashCode();
                    user.RealName = model.RealName;
                    user.IDCardNo = model.IDCardNo;
                    user.LastModtifyTime = DateTime.Now;
                    user.LastModityUserID = model.UserID;
                    foreach (var item in model.FileList)
                    {
                        var userAttach = new User_Attach
                        {
                            UserID = model.UserID,
                            AttachType = item.AttachType,
                            FileName = item.FileName,
                            AttachURL = item.FileURL,
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
                    return new ICDExcuteResult<int> { IsSuccess = true };
                }
            }
        }
        public ICDExcuteResult<int> ApplyBankCardVerify(VerifyUserModel model)
        {
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == model.UserID);
                    if (user == null)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未获得当前用户信息", };
                    }
                    if (user.BankCertificationFlag == null)
                    {
                        user.BankCertificationFlag = CertificateState.未申请.GetHashCode();
                    }
                    switch ((CertificateState)user.BankCertificationFlag)//以下状态不允许再次发起申请
                    {
                        case CertificateState.发起认证申请:
                        case CertificateState.平台人员二次审核通过:
                        case CertificateState.平台人员一次审核通过:
                        case CertificateState.认证通过:
                        case CertificateState.冻结:
                            return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "已经发起过实名认证申请", };
                    }
                    var userLog = new User_ChangeAuditLog
                    {
                        CreateTime = DateTime.Now,
                        UserID = model.UserID,
                        Operatedatetime = DateTime.Now,
                        OperateUserID = model.UserID,
                        OperateTarget = UserChangeTargetType.银行卡认证.GetHashCode(),
                        OperateType = model.BankCertificationFlag.GetHashCode(),
                        OriginValue = user.BankCertificationFlag.ToString(),
                        NewValue = model.BankCertificationFlag.GetHashCode().ToString(),
                        LastModifyTime = DateTime.Now,
                    };
                    entity.User_ChangeAuditLog.Add(userLog);
                    user.BankCertificationFlag = model.BankCertificationFlag.GetHashCode();
                    user.BankCardNO = model.BankCardNO;
                    user.LastModtifyTime = DateTime.Now;
                    user.LastModityUserID = model.UserID;
                    foreach (var item in model.FileList)
                    {
                        var userAttach = new User_Attach
                        {
                            UserID = model.UserID,
                            AttachType = item.AttachType,
                            FileName = item.FileName,
                            AttachURL = item.FileURL,
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
                    return new ICDExcuteResult<int> { IsSuccess = true };
                }
            }
        }

        public ICDTimePagedList<UserCertificationSearch, VerifyUserModel> GetUserVerifyList(ICDTimePagedList<UserCertificationSearch, VerifyUserModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var stateInt = queryModel.SearchModel.CertificateStatus.GetHashCode();
                var query = entity.AspNetUsers.Where(e => (stateInt == 0 ? true : e.CertificationFlag == stateInt) &&
                    string.IsNullOrEmpty(queryModel.TextFilter) ? true : (e.UserName.StartsWith(queryModel.TextFilter))).OrderBy(e => e.CertificationFlag).Skip((queryModel.PageIndex - 1) * queryModel.PageSize)
                    .Take(queryModel.PageSize)
                    .ToList();
                var initInt = CertificateState.未申请.GetHashCode();
                var resultQuery = query.Select(e => new VerifyUserModel
                {
                    UserID = e.Id,
                    UserName = e.UserName,
                    RealName = e.RealName,
                    CertificateFlag = (CertificateState)(e.CertificationFlag ?? initInt),
                }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
                return queryModel;
            }
        }

        public ICDTimePagedList<UserCertificationSearch, VerifyUserModel> GetBankCardVerifyList(ICDTimePagedList<UserCertificationSearch, VerifyUserModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var stateInt = queryModel.SearchModel.CertificateStatus.GetHashCode();
                var query = entity.AspNetUsers.Where(e => (stateInt == 0 ? true : e.BankCertificationFlag == stateInt) &&
                    string.IsNullOrEmpty(queryModel.TextFilter) ? true : (e.UserName.StartsWith(queryModel.TextFilter))).OrderBy(e => e.CertificationFlag).Skip((queryModel.PageIndex - 1) * queryModel.PageSize)
                    .Take(queryModel.PageSize)
                    .ToList();
                var initInt = CertificateState.未申请.GetHashCode();
                var resultQuery = query.Select(e => new VerifyUserModel
                {
                    UserID = e.Id,
                    UserName = e.UserName,
                    BankCardNO = e.BankCardNO,
                    BankCertificationFlag = (CertificateState)(e.BankCertificationFlag ?? initInt),
                }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
                return queryModel;
            }
        }

        public VerifyUserModel GetVerifyIdentityModel(string userID)
        {
            using (var entity = new Entity_Read())
            {
                var e = entity.AspNetUsers.FirstOrDefault(t => t.Id == userID);
                if (e == null)
                {
                    return new VerifyUserModel();
                }
                var fileList = entity.User_Attach.Where(t => t.UserID == userID && t.DeleteFlag != 1).ToList()
                    .Select(p => new ICDFile
                    {
                        AttachType = p.AttachType ?? 0,
                        FileName = p.FileName,
                        ContentType = p.ContentType,
                        FileURL = p.AttachURL,
                    }).ToList();
                var initInt = CertificateState.未申请.GetHashCode();
                return new VerifyUserModel
                {
                    UserID = e.Id,
                    UserName = e.UserName,
                    RealName = e.RealName,
                    IDCardNo = e.IDCardNo,
                    BankCardNO = e.BankCardNO,
                    CertificateFlag = (CertificateState)(e.CertificationFlag ?? initInt),
                    BankCertificationFlag = (CertificateState)(e.BankCertificationFlag ?? initInt),
                    LastModifyStamp = Convert.ToBase64String(e.LastModifyStamp),
                    FileList = fileList,
                };
            }
        }

        public ICDExcuteResult<int> AuditIdentityVerify(VerifyUserModel model)
        {
            if (model == null)
            {
                return new ICDExcuteResult<int>
                {
                    IsSuccess = false,
                    ErrorStr = "未找到相关用户"
                };
            }
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == model.UserID);
                    if (user == null)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未获得申请用户信息", };
                    }
                    if (Convert.ToBase64String(user.LastModifyStamp) != model.LastModifyStamp)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "用户信息已经发生变化", };
                    }
                    var userLog = new User_ChangeAuditLog
                    {
                        CreateTime = DateTime.Now,
                        UserID = model.UserID,
                        Operatedatetime = DateTime.Now,
                        OperateUserID = model.LastModifyUserID,
                        OperateTarget = UserChangeTargetType.实名认证.GetHashCode(),
                        OperateType = model.CertificateFlag.GetHashCode(),
                        OriginValue = user.CertificationFlag.ToString(),
                        NewValue = model.CertificateFlag.GetHashCode().ToString(),
                        LastModifyTime = DateTime.Now,
                    };
                    entity.User_ChangeAuditLog.Add(userLog);
                    user.CertificationFlag = model.CertificateFlag.GetHashCode();
                    user.LastModtifyTime = DateTime.Now;
                    user.LastModityUserID = model.LastModifyUserID;
                    entity.SaveChanges();
                    transaction.Commit();
                    return new ICDExcuteResult<int> { IsSuccess = true };
                }
            }
        }

        public ICDExcuteResult<int> AuditBankCard(VerifyUserModel model)
        {
            if (model == null)
            {
                return new ICDExcuteResult<int>
                {
                    IsSuccess = false,
                    ErrorStr = "未找到相关用户"
                };
            }
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var user = entity.AspNetUsers.FirstOrDefault(e => e.Id == model.UserID);
                    if (user == null)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "未获得申请用户信息", };
                    }
                    if (Convert.ToBase64String(user.LastModifyStamp) != model.LastModifyStamp)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "用户信息已经发生变化", };
                    }
                    var userLog = new User_ChangeAuditLog
                    {
                        CreateTime = DateTime.Now,
                        UserID = model.UserID,
                        Operatedatetime = DateTime.Now,
                        OperateUserID = model.LastModifyUserID,
                        OperateTarget = UserChangeTargetType.银行卡认证.GetHashCode(),
                        OperateType = model.BankCertificationFlag.GetHashCode(),
                        OriginValue = user.BankCertificationFlag.ToString(),
                        NewValue = model.BankCertificationFlag.GetHashCode().ToString(),
                        LastModifyTime = DateTime.Now,
                    };
                    entity.User_ChangeAuditLog.Add(userLog);
                    user.BankCertificationFlag = model.BankCertificationFlag.GetHashCode();
                    user.LastModtifyTime = DateTime.Now;
                    user.LastModityUserID = model.LastModifyUserID;
                    entity.SaveChanges();
                    transaction.Commit();
                    return new ICDExcuteResult<int> { IsSuccess = true };
                }
            }
        }

        public ICDExcuteResult<int> ApplyServiceProvider(UserServiceModel model)
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
                                case CertificateState.认证通过:
                                case CertificateState.冻结:
                                    return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "已经发起过服务认证申请" };
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
                            FileName = e.FileName,
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
                    return new ICDExcuteResult<int> { IsSuccess = true };
                }
            }
        }

        public ICDTimePagedList<UserServiceSearch, UserServiceModel> GetUserServiceList(ICDTimePagedList<UserServiceSearch, UserServiceModel> queryModel)
        {
            using (var entity = new Entity_Read())
            {
                var stateInt = queryModel.SearchModel.CertificateStatus.GetHashCode();
                var query = (from us in entity.User_Service_Provider.Where(e => (stateInt == 0 ? true : e.CertificationStatus == stateInt))
                             join u in entity.AspNetUsers.Where(t => string.IsNullOrEmpty(queryModel.TextFilter) ? true : (t.UserName.StartsWith(queryModel.TextFilter))) on us.UserID equals u.Id
                             join s in entity.Dic_Service.Where(e => e.DeleteFlag != 1) on us.ServiceID equals s.ServiceID
                             orderby us.ApplyTime
                             select new
                             {
                                 us.UserID,
                                 us.ServiceID,
                                 us.User_ServiceID,
                                 u.UserName,
                                 us.CertificationStatus,
                                 s.ServiceName,
                                 us.ApplyTime,
                             }).Skip((queryModel.PageIndex - 1) * queryModel.PageSize)
                    .Take(queryModel.PageSize)
                    .ToList();

                var initInt = CertificateState.未申请.GetHashCode();
                var resultQuery = query.Select(e => new UserServiceModel
                {
                    UserID = e.UserID,
                    UserName = e.UserName,
                    ApplyTime = e.ApplyTime ?? DateTime.Now,
                    CertificateFlag = (CertificateState)(e.CertificationStatus ?? initInt),
                    User_ServiceID = e.User_ServiceID,
                    Service = new ServiceModel
                    {
                        ServiceID = e.ServiceID ?? 0,
                        ServiceName = e.ServiceName,
                    },
                }).ToList();
                queryModel.TotalRecords = query.Count();
                queryModel.Content = resultQuery;
                return queryModel;
            }
        }

        public UserServiceModel GetUserService(int user_serviceID)
        {
            using (var entity = new Entity_Read())
            {
                var e = (from us in entity.User_Service_Provider.Where(t => t.User_ServiceID == user_serviceID)
                         join u in entity.AspNetUsers on us.UserID equals u.Id
                         join s in entity.Dic_Service on us.ServiceID equals s.ServiceID
                         orderby us.ApplyTime
                         select new
                         {
                             us.UserID,
                             us.ServiceID,
                             us.User_ServiceID,
                             us.CertificationStatus,
                             us.ApplyTime,
                             us.LastModifyStamp,
                             u.UserName,
                             u.CertificationFlag,
                             s.ServiceName,
                         }).FirstOrDefault();
                if (e == null)
                {
                    return null;
                }
                var initInt = CertificateState.未申请.GetHashCode();
                var result = new UserServiceModel
                {
                    UserID = e.UserID,
                    UserName = e.UserName,
                    ApplyTime = e.ApplyTime ?? DateTime.Now,
                    CertificateFlag = (CertificateState)(e.CertificationStatus ?? initInt),
                    UserCertificateFlag = (CertificateState)(e.CertificationFlag ?? initInt),
                    User_ServiceID = e.User_ServiceID,
                    LastModifyStamp = Convert.ToBase64String(e.LastModifyStamp),
                    Service = new ServiceModel
                    {
                        ServiceID = e.ServiceID ?? 0,
                        ServiceName = e.ServiceName,
                    },
                };
                var serviceClaims = entity.User_Service_Claim.Where(t => t.User_ServiceID == user_serviceID && t.DeleteFlag != 1)
                    .ToList().Select(c => new ServiceClaimModel
                    {
                        ClaimType = (ServiceClaimType)c.ClaimType,
                        ClaimValue = c.ClaimValue,
                        ClaimOpereateType = (ServiceClaimOpereateType)c.ClaimOperateType,
                    }).ToList();
                var codeVersions = serviceClaims.Where(c => new List<ServiceClaimType> {
                    ServiceClaimType.编码操作和手术版本,
                    ServiceClaimType.编码诊断版本 }.Contains(c.ClaimType)).Select(r => int.Parse(r.ClaimValue)).ToList();
                var versions = entity.BaseDic_ICD_Version.Where(c => codeVersions.Contains(c.ICD_VersionID)).Select(f => new { f.ICD_VersionID, f.ICD_VersionName }).ToList();
                serviceClaims.ForEach(t =>
                {
                    t.ClaimValue = versions.FirstOrDefault(p => p.ICD_VersionID == int.Parse(t.ClaimValue)).ICD_VersionName;
                });
                var serviceAttaches = entity.User_Service_Attach.Where(t => t.User_ServiceID == user_serviceID && t.DeleteFlag != 1)
                    .ToList().Select(c => new ServiceAttachModel
                    {
                        ContentType = c.ContentType,
                        FileURL = c.AttachURL,
                        FileName = c.FileName,
                        AttachType = (ServiceAttachType)c.AttachType,
                    }).ToList();
                result.Service.ServiceClaims = serviceClaims;
                result.Service.ServiceAttaches = serviceAttaches;
                return result;
            }
        }
        public ICDExcuteResult<int> AuditServiceProvider(UserServiceModel model)
        {
            if (model == null)
            {
                return new ICDExcuteResult<int>
                {
                    IsSuccess = false,
                    ErrorStr = "未找到用户的服务申请"
                };
            }
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    var us = entity.User_Service_Provider.FirstOrDefault(e => e.User_ServiceID == model.User_ServiceID);
                    if (us == null)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int>
                        {
                            IsSuccess = false,
                            ErrorStr = "未找到用户的服务申请"
                        };
                    }
                    if (Convert.ToBase64String(us.LastModifyStamp) != model.LastModifyStamp)
                    {
                        transaction.Rollback();
                        return new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "用户的服务申请信息已经发生变化", };
                    }
                    us.LastModifyUserID = model.LastModifyUserID;
                    us.LastModityTime = model.LastModifyTime;
                    us.CertificationStatus = model.CertificateFlag.GetHashCode();
                    if (model.CertificateFlag == CertificateState.认证通过 ||
                        model.CertificateFlag == CertificateState.平台人员一次审核通过 ||
                        model.CertificateFlag == CertificateState.平台人员二次审核通过)
                    {
                        us.PlatformAuditTime = DateTime.Now;
                        us.PlatformAuditUserID = model.LastModifyUserID;
                    }
                    else
                    {
                        us.PlatformAbendTime = DateTime.Now;
                        us.PlatformAbendUserID = model.LastModifyUserID;
                    }
                    entity.SaveChanges();
                    transaction.Commit();
                    return new ICDExcuteResult<int> { IsSuccess = true };
                }
            }
        }
        public ICDExcuteResult<string> CreateOrLogin(string phoneNum, string hospitalName)
        {
            using (var entity = new Entity_Write())
            {
                using (var transaction = entity.Database.BeginTransaction())
                {
                    try
                    {
                        var user = entity.AspNetUsers.FirstOrDefault(e => e.PhoneNumber == phoneNum);
                        if (user == null)
                        {
                            var newID = Guid.NewGuid().ToString();
                            var securityStamp = Guid.NewGuid().ToString();
                            user = new AspNetUsers
                            {
                                UserName = phoneNum,
                                PhoneNumber = phoneNum,
                                HospitalName = hospitalName,
                                PhoneNumberConfirmed = true,
                                SecurityStamp = securityStamp,
                                Id = newID,
                            };
                            entity.AspNetUsers.Add(user);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(hospitalName) && user.HospitalName != hospitalName)
                            {
                                user.HospitalName = hospitalName;
                            }
                            if (!user.LockoutEnabled)
                            {
                                if (!user.PhoneNumberConfirmed)
                                {
                                    user.PhoneNumberConfirmed = true;
                                }
                            }
                            else
                            {
                                return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "账户已经被锁定，请联系管理员" };
                            }
                        }
                        entity.SaveChanges();
                        transaction.Commit();
                        return new ICDExcuteResult<string> { IsSuccess = true, TResult = user.Id };
                    }
                    catch (Exception ex)
                    {
                        //todo  记录错误日志
                        transaction.Rollback();
                        return new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "系统罢工了，请稍后再试" };
                    }
                }
            }
        }
    }
}
