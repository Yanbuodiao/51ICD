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
    
    public partial class ORG_Service_Provider
    {
        public int ORG_ServiceID { get; set; }
        public Nullable<int> ORGID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> CertificationStatus { get; set; }
        public Nullable<System.DateTime> ApplyTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public Nullable<int> LastModifyUserID { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public byte[] LastModifyStamp { get; set; }
    }
}
