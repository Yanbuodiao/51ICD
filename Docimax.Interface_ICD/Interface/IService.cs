using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Interface
{
    public interface IService
    {
        /// <summary>
        /// 获取所有服务（含已经停用的服务）
        /// </summary>
        /// <returns></returns>
        List<ServiceModel> GetAllSerevice();

        /// <summary>
        /// 根据服务ID获取服务详情，含服务配置详情
        /// </summary>
        /// <param name="serviceID">服务ID</param>
        /// <returns></returns>
        ServiceModel GetServiceByID(int serviceID);
    }
}
