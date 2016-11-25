using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Model;
using System.Collections.Generic;

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
        /// <summary>
        /// 获取所有可用的服务及其可用Menu
        /// </summary>
        /// <returns></returns>
        List<ServiceMenuModel> GetAvailableServiceMenu();
        /// <summary>
        /// 根据用户ID获取用户可得的Service（可提供的和可使用的）
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        UserAvailableServiceModel GetUserAvailableService(string userID);

        /// <summary>
        /// 根据授权编号获取签名和加密相关Key的名称或地址
        /// </summary>
        /// <param name="org_code">组织编号</param>
        /// <returns></returns>
        OrganizationModel GetOrgModelByAUTHCode(string org_code);

        Dictionary<int, string> GetICDVersion(ICDTypeEnum icdType);
    }
}
