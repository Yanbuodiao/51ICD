﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities_Write : DbContext
    {
        public Entities_Write()
            : base("name=Entities_Write")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BaseDic_Area> BaseDic_Area { get; set; }
        public virtual DbSet<BaseDic_City> BaseDic_City { get; set; }
        public virtual DbSet<BaseDic_ICD_Repository> BaseDic_ICD_Repository { get; set; }
        public virtual DbSet<BaseDic_ICD_Version> BaseDic_ICD_Version { get; set; }
        public virtual DbSet<BaseDic_Province> BaseDic_Province { get; set; }
        public virtual DbSet<Code_Builder> Code_Builder { get; set; }
        public virtual DbSet<Code_Builder_Item> Code_Builder_Item { get; set; }
        public virtual DbSet<Dic_Menu> Dic_Menu { get; set; }
        public virtual DbSet<Dic_Organization> Dic_Organization { get; set; }
        public virtual DbSet<Dic_Service> Dic_Service { get; set; }
        public virtual DbSet<Dic_Service_Claim> Dic_Service_Claim { get; set; }
        public virtual DbSet<Dic_Service_Menu> Dic_Service_Menu { get; set; }
        public virtual DbSet<Dic_Service_UploadItem> Dic_Service_UploadItem { get; set; }
        public virtual DbSet<Dic_UploadItem> Dic_UploadItem { get; set; }
        public virtual DbSet<ORG_Attach> ORG_Attach { get; set; }
        public virtual DbSet<ORG_Service_Config> ORG_Service_Config { get; set; }
        public virtual DbSet<ORG_Service_Provider> ORG_Service_Provider { get; set; }
        public virtual DbSet<ORG_Service_UploadItem> ORG_Service_UploadItem { get; set; }
        public virtual DbSet<ORG_SubOrganization> ORG_SubOrganization { get; set; }
        public virtual DbSet<Sec_Message> Sec_Message { get; set; }
        public virtual DbSet<User_Attach> User_Attach { get; set; }
        public virtual DbSet<User_Service_Attach> User_Service_Attach { get; set; }
        public virtual DbSet<User_Service_Provider> User_Service_Provider { get; set; }
    }
}
