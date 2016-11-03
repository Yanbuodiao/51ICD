using Docimax.Data_ICD.Entity;
using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System.Collections.Generic;
using System.Linq;

namespace Docimax.Data_ICD.DAL
{
    public class DAL_Service : IService
    {
        public List<ServiceModel> GetAllSerevice()
        {
            using (var entity = new Entity_Read())
            {
                var allList = entity.Dic_Service.ToList();
                return allList.Select(e => new ServiceModel
                {
                    ServiceID = e.ServiceID,
                    ServiceName = e.ServiceName,
                    Description = e.Description,
                    DeleteFlag = e.DeleteFlag == 1,
                }).ToList();
            }
        }

        public ServiceModel GetServiceByID(int serviceID)
        {
            using (var entity = new Entity_Read())
            {
                var model = entity.Dic_Service.FirstOrDefault(e => e.ServiceID == serviceID);
                if (model == null)
                {
                    return new ServiceModel();
                }
                var claims = entity.Dic_Service_Claim.Where(e => e.ServiceID == serviceID).ToList().Select(t => new ServiceClaimModel
                {
                    ServiceID = serviceID,
                    ClaimType = (ServiceClaimType)t.ClaimType,
                }).ToList();
                var attachs = entity.Dic_Service_Attach.Where(e => e.ServiceID == serviceID).ToList().Select(t => new ServiceAttachModel
                    {
                        AttachType = (ServiceAttachType)t.AttachType,
                        ServiceID = serviceID,
                    }).ToList();
                return new ServiceModel
                {
                    ServiceID = model.ServiceID,
                    ServiceName = model.ServiceName,
                    Description = model.Description,
                    DeleteFlag = model.DeleteFlag == 1,
                    ServiceClaims = claims,
                    ServiceAttaches = attachs,
                };
            }
        }

        public List<ServiceMenuModel> GetAvailableServiceMenu()
        {
            using (var entity = new Entity_Read())
            {
                var query = (from s in entity.Dic_Service.Where(e => e.DeleteFlag != 1)
                             join sm in entity.Dic_Service_Menu.Where(e => e.DeleteFlag != 1) on s.ServiceID equals sm.ServiceID
                             join m in entity.Dic_Menu.Where(e => e.DeleteFlag != 1) on sm.MenuID equals m.MenuID
                             select new
                             {
                                 s.ServiceID,
                                 sm.ServiceType,
                                 m.MenuID,
                                 m.MenuIndex,
                                 m.ParentMenuID,
                                 m.ActionName,
                                 m.AreaName,
                                 m.ControllerName,
                                 m.DisplayName
                             }).ToList();
                var pubMenu = entity.Dic_Menu.Where(e => e.DeleteFlag != 1 && (e.RoleControl ?? 0) == 0).ToList().Select(p => new ICDMenu
                {
                    MenuID = p.MenuID,
                    ActionName = p.ActionName,
                    AreaName = p.AreaName,
                    ControllerName = p.ControllerName,
                    DisplayName = p.DisplayName,
                    ParentID = p.ParentMenuID ?? 0,
                    Index = p.MenuIndex ?? 0,
                }).ToList();
                var result = query.GroupBy(e => new { e.ServiceID, e.ServiceType }).Select(t => new ServiceMenuModel
                {
                    ServiceID = t.Key.ServiceID,
                    ServiceType = (ServiceType)(t.Key.ServiceType ?? 0),
                    MenuList = query.Where(c => c.ServiceID == t.Key.ServiceID
                        && c.ServiceType == t.Key.ServiceType).Select(p => new ICDMenu
                        {
                            MenuID = p.MenuID,
                            ActionName = p.ActionName,
                            AreaName = p.AreaName,
                            ControllerName = p.ControllerName,
                            DisplayName = p.DisplayName,
                            ParentID = p.ParentMenuID ?? 0,
                            Index = p.MenuIndex ?? 0,
                        }).ToList(),
                }).ToList();

                result.ForEach(e =>
                {
                    e.MenuList.ForEach(c => c.SonMenuList = buildChildMemu(c.MenuID, e.MenuList));
                    e.MenuList = e.MenuList.Where(t => t.ParentID == 0).ToList();
                });
                result.Add(new ServiceMenuModel
                {
                    ServiceID = -1,
                    ServiceType = ServiceType.Public,
                    MenuList = pubMenu,
                });
                return result;
            }
        }

        public UserAvailableServiceModel GetUserAvailableService(string userID)
        {
            using (var entity = new Entity_Read())
            {
                var certificationState = CertificateState.认证通过.GetHashCode();
                var requestService = (from u in entity.AspNetUsers.Where(e => e.CertificationFlag == certificationState && e.Id == userID)
                                      join os in entity.ORG_Service_Config.Where(e => e.ServiceAuditStatus == certificationState && e.DeleteFlag != 1) on u.ORGID equals os.ORGID
                                      select os.ServiceID).ToList().Select(c => new ServiceMenuModel
                                      {
                                          ServiceID = c ?? 0,
                                          ServiceType = ServiceType.Request
                                      }).ToList();
                var providerService = (from u in entity.AspNetUsers.Where(e => e.CertificationFlag == certificationState && e.Id == userID)
                                       join us in entity.User_Service_Provider.Where(e => e.CertificationStatus == certificationState && e.DeleteFlag != 1) on u.Id equals us.UserID
                                       select us.ServiceID).ToList().Select(c => new ServiceMenuModel
                                       {
                                           ServiceID = c ?? 0,
                                           ServiceType = ServiceType.Provider
                                       }).ToList();
                var allAvailable = requestService.Union(providerService).ToList();
                allAvailable.Add(new ServiceMenuModel { ServiceID = -1, ServiceType = ServiceType.Public });
                return new UserAvailableServiceModel
                {
                    UserID = userID,
                    AvaliableServices = allAvailable
                };
            }
        }

        public OrganizationModel GetOrgModelByCode(string org_code)
        {
            using (var entity = new Entity_Read())
            {
                var orgModel = entity.Dic_Organization.FirstOrDefault(e => e.OrganizationCode == org_code);
                if (orgModel != null)
                {
                    return new OrganizationModel
                    {
                        Org_Code = org_code,
                        EncryptKeyName = orgModel.EncryKey,
                        CheckSignPubKeyPath = orgModel.SignKey,
                        SignPriKeyPath = orgModel.SignPriKey,
                    };
                }
                return null;
            }
        }

        #region 私有方法
        private List<ICDMenu> buildChildMemu(int parentID, List<ICDMenu> allResultMenuList)
        {
            var result = allResultMenuList.Where(p => p.ParentID == parentID).OrderBy(e => e.Index).ToList();
            result.ForEach(e => e.SonMenuList = buildChildMemu(e.MenuID, allResultMenuList));
            return result;
        }

        #endregion
    }
}
