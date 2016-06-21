using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docimax.Interface_ICD.Enum
{
    public enum ItemState
    {
        订单新建 = 100,//订单刚创建时初始状态
        订单取消 = 105,//需求方主动发起
        订单关闭 = 115,//订单有违基本法律和道德规范时，平台进行关闭
        订单申领 = 200,//服务提供方对该订单进行了领单操作，领单后限制时间内（暂定1小时）未“结果提交”会自动释放（无论是否已经开始编码）     
        信息待补充 = 340,//编码过程中，发现缺少病案内容项目，服务提供方发起要求补充相关信息的流程（此流程下计时停止）；需求方补充提交后，订单恢复申领状态并开始继续计时
        结果提交 = 400,//编码服务提供方编码完成后，置状态如此
        编码完成 = 1000,//编码结果通过服务需求方的审核（编码最终完成）

    }
}
