using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Docimax.Common_ICD.Dictionary
{
    public static class ServiceList
    {
        private static IEnumerable<SelectListItem> availableServices;
        public static IEnumerable<SelectListItem> AvailableServices
        {
            get
            {
                if (availableServices == null)
                {
                    IService access = new DAL_Service();
                    var services = access.GetAllSerevice();
                    availableServices = services.Where(e => !e.DeleteFlag).Select(t => new SelectListItem
                    {
                        Text = t.ServiceName,
                        Value = t.ServiceID.ToString(),
                    });
                }
                return availableServices;
            }
        }
    }
}
