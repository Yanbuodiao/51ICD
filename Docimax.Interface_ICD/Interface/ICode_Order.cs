using Docimax.Interface_ICD.Model;
using Docimax.Interface_ICD.Model.CodeOrder;
using Docimax.Interface_ICD.Model.UploadModel;
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
        /// <returns>订单详情实体</returns>
        CodeOrderModel GetCodeOrderDetail(string userID, int codeOrderID);

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
        ICDExcuteResult<int> SaveNewCodeOrder(CodeOrderModel newCodeOrder, bool isSubmit);

        /// <summary>
        /// 编码需求方根据一定的查询条件查询符合条件的订单列表
        /// </summary>
        /// <param name="queryModel">含查询条件和查询结果的实体</param>
        /// <returns>符合条件的订单列表</returns>
        ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> GetUpLoadedCodeOrderList(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> queryModel);

        /// <summary>
        /// 获取待抢单的编码订单列表
        /// </summary>
        /// <param name="queryModel">含查询条件和查询结果的实体</param>
        /// <returns>符合条件的订单列表</returns>
        ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> GetUnClaimOrderList(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> queryModel);

        /// <summary>
        /// 根据条件获取属于编码人的编码订单列表
        /// </summary>
        /// <param name="queryModel">查询条件</param>
        /// <returns>符合条件的订单列表</returns>
        ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> GetMyCodeOrderList(ICDTimePagedList<CodeOrderSearchModel, CodeOrderModel> queryModel);

        /// <summary>
        /// 领单操作
        /// </summary>
        /// <param name="userID">领单的用户ID</param>
        /// <param name="stamp">并发限制的票据</param>
        /// <param name="codeOrderID">订单号</param>
        /// <returns>领单结果</returns>
        ICDExcuteResult<int> ClaimCodeOrder(string userID, string stamp, int codeOrderID);

        /// <summary>
        /// 保存编码结果
        /// </summary>
        /// <param name="model">页面提交的编码结果</param>
        /// <returns>保存结果</returns>
        ICDExcuteResult<int> SaveCodeResult(CodeOrderModel model);

        /// <summary>
        /// 根据上传的病案信息（病案号  住院次  出院日期）判断是否已经存在相关的编码订单
        /// </summary>
        /// <param name="mr">病案信息</param>
        /// <returns>True 存在  false：不存在</returns>
        bool IsCodeOrderExistByMecicalRecord(MedicalRecordCoding mr);

        /// <summary>
        /// 保存从接口请求的编码订单
        /// </summary>
        /// <param name="mr">接口请求的病案内容</param>
        /// <param name="authCode">机构授权号</param>
        /// <param name="medicalRecordPath">病案详细内容存放地址</param>
        /// <returns>true：保存成功 false：保存失败</returns>
        ExcuteResult SaveCodeOrder(MedicalRecordCoding mr, string authCode, string medicalRecordPath);
        CodeOrderInterfaceModel GetCodeOrder(int codeOrderID);
            
    }
}
