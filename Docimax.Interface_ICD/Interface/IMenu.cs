using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Interface
{
    public interface IMenu
    {
        /// <summary>
        /// 根据用户ID获取用户可看到的菜单列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>菜单列表</returns>
        List<ICDMenu> GetMenuListByUserID(string userID);
    }
}
