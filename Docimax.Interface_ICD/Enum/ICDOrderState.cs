﻿
namespace Docimax.Interface_ICD.Enum
{
    /// <summary>
    /// ICD 编码订单状态
    /// </summary>
    public enum ICDOrderState
    {
        新创建 = 1000,//订单初始化，保存相关信息
        待抢单 = 2000,//信息保存完毕订单创建完毕，可以被编码员抢单进行编码
        已接单 = 3000,//编码员抢单完成，单子进入编码员的待编码订单中
        编码中 = 3200,//编码员开始编码，保存了部分信息
        待补充信息 = 3500,//编码员在编码过程中发现需要补充相关信息
        补充完成 = 3600,//医院上传补充完相关信息后置状态如此
        审核未通过 = 5555,
        抽检未通过 = 6555,
        验收未通过 = 7555,
        编码完成 = 10000,//编码员编码完成后，置状态如此
        审核通过 = 15000,//预留 平台审核通过后 一期没有普审功能，编码完成的订单自动置为验收通过；编码结果通过审核，实际状态为验收通过；原则上验收通过的订单经过15-30天后再进行结算
        抽检通过 = 16000,//预留 平台抽审通过后 一期没有抽审功能，编码完成的订单抽审通过后，置状态如此
        验收通过 = 17000,//预留 医院验收 一期没有医院验收功能，医院验收通过后，置状态如此
        结算中 = 18000,//开始结算的标记（生成了支付订单） 一个编码订单可能有多个支付订单
        开始支付 = 19000,/*开始进行真正支付流程的标识，支付订单提交到三方支付机构前（锁定数据）置状态如此。超过一定时间没有最终支付状态时，发起向第三方的查询，整体查询结果
                         *在三方没有找到相关订单，编码订单大状态置为结算中，记录相关日志；在第三方查询到相关订单，根据结果更新支付状态为支付中，支付成功或者支付失败。
                         *一期没有接入第三方支付机构时，导出代付单之前指状态如此
                        */
        提交支付失败 = 19555,//瞬时状态，提交到第三方失败或者导出代付单失败
        支付中 = 20000,/*提交到第三方支付机构成功或者代付单导出成功后，置状态如此；提交到第三方支付机构失败或者导出代付单失败时，记录日志，大状态回滚到结算中；
                       *超过一定期限后主动向第三方查询或者找财务确认：支付失败，记录失败，大状态回滚到结算中；支付中，记录查询日志；支付成功 正常流程
                      */
        支付失败 = 21555,//瞬时状态，第三方
        支付成功 = 30000,//第三方机构支付成功或者财务支付成功后，置状态如此；
        订单无效 = 55555,//订单不符合平台规范,平台人员关闭订单
    }
}
