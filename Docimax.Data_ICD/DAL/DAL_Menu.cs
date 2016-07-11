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
            using (var entity = new Entities_Read())
            {
                var resultMenus = entity.Dic_Menu.Where(e => (e.RoleControl ?? 0) == 0).Select(p =>
                    new
                    {
                        p.MenuID,
                        p.DisplayName,
                        p.AreaName,
                        p.ControllerName,
                        p.ActionName,
                        p.MenuIndex,
                        p.ParentMenuID,
                    }).ToList();
                if (!string.IsNullOrWhiteSpace(userID))
                {
                    var serviceAuditStatusInt = (int)CertificateState.认证成功;
                    var userModel = entity.AspNetUsers.FirstOrDefault(e => e.Id == userID && e.CertificationFlag == serviceAuditStatusInt);
                    if (userModel != null)
                    {
                        var requestService = (from org in entity.Dic_Organization.Where(e => e.DeleteFlag != 1
                                                  && e.CertificationFlag == serviceAuditStatusInt && e.OrganizationID == userModel.ORGID)
                                             join org_service in entity.ORG_Service_Config.Where(e => e.ServiceAuditStatus == serviceAuditStatusInt
                                                  && e.DeleteFlag != 1) on org.OrganizationID equals org_service.ORGID
                                             select org_service.ServiceID ?? 0).ToList();
                        var allService = entity.User_Service_Provider.Where(e => e.DeleteFlag != 1 && e.CertificationStatus == serviceAuditStatusInt && e.UserID == userID).
                                              Select(e => e.ServiceID ?? 0).Concat(requestService).ToList();
                        resultMenus = resultMenus.Union(from p in entity.Dic_Menu.Where(e => e.DeleteFlag != 1 && (e.RoleControl ?? 0) != 0)
                                                        join service_menu in entity.Dic_Service_Menu.Where(e => e.DeleteFlag != 1)
                                                             on p.MenuID equals service_menu.MenuID
                                                        join service in entity.Dic_Service.Where(e => e.DeleteFlag != 1) on service_menu.ServiceID equals service.ServiceID
                                                        join s in allService on service.ServiceID equals s
                                                        select new
                                                        {
                                                            p.MenuID,
                                                            p.DisplayName,
                                                            p.AreaName,
                                                            p.ControllerName,
                                                            p.ActionName,
                                                            p.MenuIndex,
                                                            p.ParentMenuID,
                                                        }).ToList();
                    }
                }
                var allResult = resultMenus.Select(e =>
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
