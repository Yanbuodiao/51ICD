using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using Docimax.Common;
using Docimax.Interface_ICD.Interface;
using Docimax.Data_ICD.DAL;

namespace Docimax.Common_ICD.Cache
{
    public class ServiceMenu
    {
        static string serviceMenuKey;

        static object lockObj = new object();

        static ServiceMenu()
        {
            if (string.IsNullOrEmpty(serviceMenuKey))
            {
                lock (lockObj)
                {
                    if (string.IsNullOrEmpty(serviceMenuKey))
                    {
                        serviceMenuKey = Guid.NewGuid().ToString();
                    }
                }
            }
        }
        static List<ServiceMenuModel> AvailableServiceMenuList
        {
            get
            {
                var availableServiceMenuList = HttpRuntime.Cache[serviceMenuKey] as List<ServiceMenuModel>;
                if (availableServiceMenuList == null)
                {
                    string path = HttpContext.Current.Server.MapPath("~/App_Data/AvalilableServiceMenuList.xml");
                    if (System.IO.File.Exists(path))//如果缓存文件存在,直接从缓存文件加载
                    {
                        availableServiceMenuList = XmlHelper.XmlDeserializeFromFile<List<ServiceMenuModel>>(path, Encoding.UTF8);
                    }
                    else//如果缓存文件不存在，从数据库中拿到初始数据,保存到缓存文件中，尤其注意：更新相关数据时，需要同时更新缓存文件
                    {
                        IService access = new DAL_Service();
                        availableServiceMenuList = access.GetAvailableServiceMenu();
                        XmlHelper.XmlSerializeToFile(availableServiceMenuList, path, Encoding.UTF8);
                    }
                    CacheDependency dep = new CacheDependency(path);
                    //添加到缓存中
                    HttpRuntime.Cache.Insert(serviceMenuKey,
                        availableServiceMenuList,
                        dep,
                        System.Web.Caching.Cache.NoAbsoluteExpiration,
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        CacheItemPriority.High,
                        (key, value, reason) =>
                        {
                            IService access = new DAL_Service();//失效的时候，从数据库拿到最新的数据持久化到文件中
                            availableServiceMenuList = access.GetAvailableServiceMenu();
                            XmlHelper.XmlSerializeToFile(availableServiceMenuList, path, Encoding.UTF8);
                        });
                }
                return availableServiceMenuList;
            }
        }

        public static List<ICDMenu> GetMenuList(string userID)
        {
            IService access = new DAL_Service();
            var userAvailable = access.GetUserAvailableService(userID);
            var result = AvailableServiceMenuList.Where(c => userAvailable.AvaliableServices.Any(t => t.ServiceID == c.ServiceID && t.ServiceType == c.ServiceType)).SelectMany(p => p.MenuList).OrderBy(e => e.Index).ToList();
            return result;
        }
    }
}
