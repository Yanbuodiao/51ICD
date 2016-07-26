using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Interface
{
    public interface ICode_Order
    {
        /// <summary>
        /// 根据用户ID和用户使用的服务获取该服务下的订单模板（主要指含需要上传的文件列表）
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="serviceName">服务名称</param>
        /// <returns>订单模板（含需要上传的文件列表）</returns>
        CodeOrderModel GetNewCodeOrder(string userID, string serviceName);
        /// <summary>
        /// 创建新订单
        /// </summary>
        /// <param name="newCodeOrder">新订单实体类</param>
        /// <param name="isSubmit">是否直接提交 true：直接提交进入待抢单列表  false：部分上传文件 未真正提交</param>
        /// <returns>创建结果</returns>
        ICDExcuteResult SaveNewCodeOrder(CodeOrderModel newCodeOrder, bool isSubmit);
    }
}
