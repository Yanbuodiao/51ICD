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
    
    public partial class Dic_Organization
    {
        public int OrganizationID { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationName { get; set; }
        public string ShortName_CN { get; set; }
        public string ShortName_EN { get; set; }
        public string RegisterCode { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> AreaID { get; set; }
        public string DetailAddress1 { get; set; }
        public string DetailAddress2 { get; set; }
        public string DetailAddress3 { get; set; }
        public Nullable<int> CertificationFlag { get; set; }
        public string CreateUserID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastModifyID { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public Nullable<int> DeleteFlag { get; set; }
        public byte[] LastModifyStamp { get; set; }
    }
}
