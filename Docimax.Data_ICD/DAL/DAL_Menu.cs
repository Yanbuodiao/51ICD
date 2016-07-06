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
                }

            }
            return new List<ICDMenu> { 
                new ICDMenu{ DisplayNanme="主页",},
                new ICDMenu{},
                new ICDMenu{},};
        }
    }
}
