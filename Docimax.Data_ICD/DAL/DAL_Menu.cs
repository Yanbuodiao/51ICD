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
    public class DAL_Menu : IMenu
    {
        public List<ICDMenu> GetMenuListByUserID(string userID)
        {
            using (var entity = new Entity_Read())
            {
                var resultMenus = entity.Dic_Menu.Where(e => (e.RoleControl ?? 0) == 0);
                if (!string.IsNullOrWhiteSpace(userID))
                {
                    var serviceAuditStatusInt = CertificateState.认证通过.GetHashCode();
                    var requestInt = ServiceType.Request.GetHashCode();
                    var providerInt = ServiceType.Provider.GetHashCode();
                    var userModel = entity.AspNetUsers.FirstOrDefault(e => e.Id == userID && e.CertificationFlag == serviceAuditStatusInt);
                    if (userModel != null)
                    {
                        var requestMenuIDs = (from org in entity.Dic_Organization.Where(e => e.DeleteFlag != 1
                                                   && e.CertificationFlag == serviceAuditStatusInt && e.OrganizationID == userModel.ORGID)
                                              join org_service in entity.ORG_Service_Config.Where(e => e.ServiceAuditStatus == serviceAuditStatusInt
                                                   && e.DeleteFlag != 1) on org.OrganizationID equals org_service.ORGID
                                              join service_menu in entity.Dic_Service_Menu.Where(e => e.DeleteFlag != 1 && e.ServiceType == requestInt)
                                                   on org_service.ServiceID equals service_menu.ServiceID
                                              select service_menu.MenuID);
                        var userMenuIDs = (from userProvider in entity.User_Service_Provider.Where(e => e.DeleteFlag != 1
                                               && e.CertificationStatus == serviceAuditStatusInt && e.UserID == userID)
                                           join service_menu in entity.Dic_Service_Menu.Where(e => e.DeleteFlag != 1 && e.ServiceType == providerInt)
                                                on userProvider.ServiceID equals service_menu.ServiceID
                                           select service_menu.MenuID).Union(requestMenuIDs);
                        var userMenus=from menuID in userMenuIDs
                                      join menu in entity.Dic_Menu on menuID equals menu.MenuID
                                      select menu;
                        resultMenus = resultMenus.Union(userMenus);
                    }
                }
                var allResult = resultMenus.ToList().Select(e =>
                  new ICDMenu
                  {
                      MenuID = e.MenuID,
                      DisplayName = e.DisplayName,
                      AreaName = e.AreaName,
                      ControllerName = e.ControllerName,
                      ActionName = e.ActionName,
                      Index = e.MenuIndex ?? int.MaxValue,
                      ParentID = e.ParentMenuID ?? 0,
                  }).ToList();
                var result = allResult.Where(e => e.ParentID == 0).OrderBy(e => e.Index).ToList();
                result.ForEach(e => e.SonMenuList = GetMemu(e.MenuID, allResult));
                return result;
            }
        }

        private List<ICDMenu> GetMemu(int parentID, List<ICDMenu> allResultMenuList)
        {
            var result = allResultMenuList.Where(p => p.ParentID == parentID).OrderBy(e => e.Index).ToList();
            result.ForEach(e => e.SonMenuList = GetMemu(e.MenuID, allResultMenuList));
            return result;
        }
    }
}
