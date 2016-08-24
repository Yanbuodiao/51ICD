using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Docimax.Web_ICD.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
        /// <summary>
        /// 用户身份证号
        /// </summary>
        public string IDCardNo { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNO { get; set; }
        /// <summary>
        /// 实名认证状态与枚举Docimax.Interface_ICD.Enum.CertificateState的枚举值相对应
        /// </summary>
        public int? CertificationFlag { get; set; }
        /// <summary>
        /// 所属组织ID
        /// </summary>
        public int? ORGID { get; set; }
        /// <summary>
        ///所属子组织ID
        /// </summary>
        public int? SubORGID { get; set; }
        /// <summary>
        /// 首次登陆
        /// </summary>
        public int? IsFirstLogin { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIP { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人用户ID
        /// </summary>
        public int? CreateUserID { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModtifyTime { get; set; }
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public int? LastModityUserID { get; set; }

       [Timestamp]
        public byte[] LastModifyStamp { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        { }
        public ApplicationRole(string name) : this() { this.Name = name; }
        public ApplicationRole(string name, string description) : this(name) { this.Description = description; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人用户ID
        /// </summary>
        public string CreateUserID { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModtifyTime { get; set; }
        /// <summary>
        /// 最后修改人ID
        /// </summary>
        public string LastModityUserID { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole { }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}