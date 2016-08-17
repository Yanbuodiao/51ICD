using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using System;
using Docimax.Data_ICD.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docimax.Interface_ICD.Enum;

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
                var claims = entity.Dic_Service_Claim.Where(e => e.ServiceID == serviceID).Select(t => new ServiceClaimModel
                {
                    ServiceID = serviceID,
                    ClaimType = (ServiceClaimType)t.ClaimType,
                }).ToList();
                return new ServiceModel
                {
                    ServiceID = model.ServiceID,
                    ServiceName = model.ServiceName,
                    Description = model.Description,
                    DeleteFlag = model.DeleteFlag == 1,
                    ServiceClaims = claims,
                };
            }
        }
    }
}
