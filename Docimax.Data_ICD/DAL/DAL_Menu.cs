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
                var resultMenus = entity.Dic_Menu.Where(e => (e.RoleControl ?? 0) == 0);
                if (!string.IsNullOrWhiteSpace(userID))
                {
                    var serviceAuditStatusInt = (int)CertificateState.认证成功;
                    var requestMenus = from user in entity.AspNetUsers.Where(e => e.Id == userID && e.CertificationFlag == serviceAuditStatusInt)
                                       join org in entity.Dic_Organization.Where(e => e.DeleteFlag != 1) on user.ORGID equals org.OrganizationID
                                       join org_service in entity.ORG_Service_Config.Where(e => e.ServiceAuditStatus == serviceAuditStatusInt
                                            && e.DeleteFlag != 1) on org.OrganizationID equals org_service.ORGID
                                       join service in entity.Dic_Service.Where(e => e.DeleteFlag != 1) on org_service.ServiceID equals service.ServiceID
                                       join service_menu in entity.Dic_Service_Menu.Where(e => e.DeleteFlag != 1) on service.ServiceID equals
                                            service_menu.ServiceID
                                       join menu in entity.Dic_Menu.Where(e => e.DeleteFlag != 1) on service_menu.MenuID equals menu.MenuID
                                       select menu;
                    var providerMenus = from user in entity.AspNetUsers.Where(e => e.Id == userID && e.CertificationFlag == serviceAuditStatusInt)
                                        join userserviceProvider in entity.User_Service_Provider.Where(e => e.DeleteFlag != 1)
                                             on user.Id equals userserviceProvider.UserID
                                        join service in entity.Dic_Service.Where(e => e.DeleteFlag != 1)
                                             on userserviceProvider.ServiceID equals service.ServiceID
                                        join service_menu in entity.Dic_Service_Menu.Where(e => e.DeleteFlag != 1) on service.ServiceID equals
                                             service_menu.ServiceID
                                        join menu in entity.Dic_Menu.Where(e => e.DeleteFlag != 1) on service_menu.MenuID equals menu.MenuID
                                        select menu;
                }

            }
            return new List<ICDMenu> { 
                new ICDMenu{ DisplayNanme="主页",},
                new ICDMenu{},
                new ICDMenu{},};
        }
    }
}
