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
        /// 根据订单号和当前登录用户获得订单详情
        /// </summary>
        /// <param name="userID">当前登录用户ID</param>
        /// <param name="codeOrderID">订单号</param>
        /// <param name="serviceName">订单所属服务名称</param>
        /// <returns>订单详情实体</returns>
        CodeOrderModel GetCodeOrderDetail(string userID, int codeOrderID, string serviceName = "ICD编码服务");
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
        /// <summary>
        /// 编码需求方根据一定的查询条件查询符合条件的订单列表
        /// </summary>
        /// <param name="queryModel">含查询条件和查询结果的实体</param>
        /// <returns>符合条件的订单列表</returns>
        ICDPagedList<CodeOrderSearchModel, CodeOrderModel> GetUpLoadedCodeOrderList(ICDPagedList<CodeOrderSearchModel, CodeOrderModel> queryModel);

        /// <summary>
        /// 获取待抢单的编码订单列表
        /// </summary>
        /// <param name="queryModel">含查询条件和查询结果的实体</param>
        /// <returns>符合条件的订单列表</returns>
        ICDPagedList<CodeOrderSearchModel, CodeOrderModel> GetUnClaimOrderList(ICDPagedList<CodeOrderSearchModel, CodeOrderModel> queryModel);

        /// <summary>
        /// 根据条件获取属于编码人的编码订单列表
        /// </summary>
        /// <param name="queryModel">查询条件</param>
        /// <returns>符合条件的订单列表</returns>
        ICDPagedList<CodeOrderSearchModel, CodeOrderModel> GetMyCodeOrderList(ICDPagedList<CodeOrderSearchModel, CodeOrderModel> queryModel);
    }
}
