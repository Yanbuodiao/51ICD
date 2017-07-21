using Docimax.Interface_ICD.Enum;
using Docimax.Interface_ICD.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Docimax.Web_ICD.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "记住此浏览器?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
    public class UserNameModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} 最多包含{1}个字符。")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
    }
    public class LoginViewModel
    {

        [Display(Name = "用户名")]
        public string LoginUserName { get; set; }

        [Display(Name = "密   码")]
        public string LoginPassword { get; set; }

        [Display(Name = "手机号")]
        public string LoginPhoneNum { get; set; }

        [Display(Name = "验证码")]
        public string LoginDynamicPWD { get; set; }

        public int LoginType { get; set; }

        public bool LoginSuccess { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "手机号")]
        public string PhoneNum { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} 最多包含{1}个字符。")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 4)]
        [Display(Name = "医院名")]
        public string HosipitalName { get; set; }

        [Display(Name = "验证码")]
        public string DynamicPWD { get; set; }

        public bool NeedVarify { get; set; }
    }
    public class VerifyIdentityViewModel : BaseModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        [Required]
        [StringLength(18, ErrorMessage = "请输出正确的身份证号码。", MinimumLength = 15)]
        [Display(Name = "身份证号")]
        public string IDCardNo { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 2)]
        [Display(Name = "姓名")]
        public string RealName { get; set; }
        /// <summary>
        /// 发起申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// 认证状态
        /// </summary>
        public CertificateState CertificateFlag { get; set; }
        /// <summary>
        /// 附件信息
        /// </summary>
        public List<ICDFile> FileList { get; set; }
    }

    public class VerifyBankCardModel : BaseModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        [Required]
        [StringLength(23, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 19)]//算上四位间加的间隔
        [Display(Name = "银行卡号")]
        public string BankCardNO { get; set; }

        /// <summary>
        /// 发起申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 认证状态
        /// </summary>
        public CertificateState CertificateFlag { get; set; }

        /// <summary>
        /// 附件信息
        /// </summary>
        public List<ICDFile> FileList { get; set; }
    }
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "手机号")]
        public string PhoneNum { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} 最多包含{1}个字符。")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "验证码")]
        public string DynamicPWD { get; set; }
    }

    public class TelLoginViewModel : Telephone
    {
        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }

        [Required]
        [Display(Name = "收到的短信验证码")]
        public string DynamicPWD { get; set; }
        public bool LoginSuccess { get; set; }
    }

    public class TelRegisterViewModel : Telephone
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} 至少包含 {2} 个字符。", MinimumLength = 4)]
        [Display(Name = "所在医院")]
        public string HosipitalName { get; set; }

        [Required]
        [Display(Name = "收到的短信验证码")]
        public string DynamicPWD { get; set; }

        public bool RegisterSuccess { get; set; }
    }

    public class Telephone
    {
        [Required]
        [Phone]
        [Display(Name = "电话号码")]
        public string PhoneNumber { get; set; }
    }
}
