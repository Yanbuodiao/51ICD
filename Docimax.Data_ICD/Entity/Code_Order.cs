//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Docimax.Data_ICD.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Code_Order
    {
        public int CodeOrderID { get; set; }
        public string PlatformOrderCode { get; set; }
        public string CaseNum { get; set; }
        public Nullable<System.DateTime> OutTime { get; set; }
        public Nullable<int> AdmissionTimes { get; set; }
        public string MedicalRecordPath { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public string AUTHCode { get; set; }
        public Nullable<int> ORGID { get; set; }
        public Nullable<int> ORGSubID { get; set; }
        public Nullable<System.DateTime> PickedTime { get; set; }
        public string PickedUserID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> OrderType { get; set; }
        public Nullable<System.DateTime> Createtime { get; set; }
        public string CreateUserID { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifyUserID { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public byte[] LastModifyStamp { get; set; }
    }
}
