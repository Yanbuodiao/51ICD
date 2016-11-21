
namespace Docimax.Interface_ICD.Enum
{
    /// <summary>
    /// 订单类别
    /// </summary>
    public enum OrderTypeEnum
    {
        /// <summary>
        /// 图片上传的订单 （tif jpg）等等
        /// </summary>
        PicOrder = 0,
        /// <summary>
        /// 接口订单 通过调用接口生成的订单
        /// </summary>
        InterfaceOrder = 1,
    }
}
