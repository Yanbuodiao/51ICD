//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Docimax.Data_ICD
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Service_Provider
    {
        public int User_ServiceID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> CertificationStatus { get; set; }
        public Nullable<System.DateTime> ApplyTime { get; set; }
        public Nullable<System.DateTime> PlatformAuditTime { get; set; }
        public Nullable<int> PlatformAuditUserID { get; set; }
        public Nullable<System.DateTime> PlatformAbendTime { get; set; }
        public Nullable<System.DateTime> PlatformAbendUserID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserID { get; set; }
        public Nullable<System.DateTime> LastModityTime { get; set; }
        public string LastModifyUserID { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public byte[] LastModifyStamp { get; set; }
    }
}
