
using Docimax.Interface_ICD.Enum;
namespace Docimax.Interface_ICD.Model
{
    /// <summary>
    /// 编码订单查询实体类
    /// </summary>
    public class CodeOrderSearchModel
    {
        public ICDOrderState OrderState { get; set; }
        public string UserID { get; set; }
    }
}
