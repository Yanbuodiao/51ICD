using Docimax.Common;
using Docimax.Common_ICD.Cache;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private SignInHelper _helper;
        public AccountController()
        {
        }
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private SignInHelper SignInHelper
        {
            get
            {
                if (_helper == null)
                {
                    _helper = new SignInHelper(UserManager, AuthenticationManager);
                }
                return _helper;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var result = await SignInHelper.PasswordSignIn(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    //case SignInStatus.InvalidEmail:
            //    //    var user = await UserManager.FindByNameAsync(model.Email);
            //    //    ViewBag.UserID = user.Id;
            //    //    return View("DisplayEmail");
            //    case SignInStatus.RequiresTwoFactorAuthentication:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "无效的登录.");
            return View(model);
            //}
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // 要求用户已通过使用用户名/密码或外部登录名登录
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInHelper.TwoFactorSignIn(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            var ip = HttpHelper.GetIPFromRequest(Request);
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    var ip = HttpHelper.GetIPFromRequest(Request);
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            UserName = model.UserName,
        //            PhoneNumber = model.PhoneNum,
        //            LockoutEnabled = false,
        //            HospitalName = model.HosipitalName,
        //        };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return await SendConfirmEmail(user.Id);
        //        }
        //        AddErrors(result);
        //    }
        //    model.NeedVarify = true;
        //    return View(model);
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            var ip = HttpHelper.GetIPFromRequest(Request);
            if (ModelState.IsValid)
            {
                var validateCodeResult = ValidateCodeHelper.VerifyPhoneDymaticCode(model.PhoneNum, model.DynamicPWD);
                if (!validateCodeResult)
                {
                    AddErrors(new IdentityResult("手机验证码不匹配或者已过期"));
                }
                else
                {
                    var user = new UserModel
                    {
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNum,
                        LockoutEnabled = false,
                        HospitalName = model.HosipitalName,
                        Password = model.Password,
                        LastLoginIP = ip,
                    };
                    IUserAccess access = new DAL_UserAccess();
                    var result = access.CreateUserByPhone(user);
                    if (!result.IsSuccess)
                    {
                        AddErrors(new IdentityResult(result.ErrorStr));
                    }
                    else
                    {
                        await signIn(user.UserName, result.TResult, user.PhoneNumber);
                        return RedirectToLocal(returnUrl);
                    }
                }
            }
            model.DynamicPWD = string.Empty;
            model.Password = string.Empty;
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendConfirmEmail(string userID)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userID, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(
                userID,
                "51ICD-激活账户",
                string.Format(@"<br/><br/>请通过单击 <a href={0}>这里</a> 激活您在51ICD注册的账户
                                       <br/><br/><br/>如果上述链接失效，请将下面地址复制到浏览器地址栏进行激活<br/><br/>{1}
                                       <br/><br/><br/>系统邮件，请勿回复", callbackUrl, callbackUrl));
            return RedirectToAction("ConfirmEmailNotify", "Account", userID);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmailNotify(string userID)
        {
            ViewBag.UserID = userID;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model,string returnUrl)
        {
            var ip = HttpHelper.GetIPFromRequest(Request);
            if (ModelState.IsValid)
            {
                var validateCodeResult = ValidateCodeHelper.VerifyPhoneDymaticCode(model.PhoneNum, model.DynamicPWD);
                if (!validateCodeResult)
                {
                    AddErrors(new IdentityResult("手机验证码不匹配或者已过期"));
                }
                else
                {
                    var user = new UserModel
                    {
                        UserName = model.UserName,
                        PhoneNumber = model.PhoneNum,
                        Password = model.Password,
                        LastLoginIP = ip,
                    };
                    IUserAccess access = new DAL_UserAccess();
                    var result = access.RestPasswordByPhone(user);
                    if (!result.IsSuccess)
                    {
                        AddErrors(new IdentityResult(result.ErrorStr));
                    }
                    else
                    {
                        await signIn(user.UserName, result.TResult, user.PhoneNumber);
                        return RedirectToLocal(returnUrl);
                    }
                }
            }
            model.DynamicPWD = string.Empty;
            model.Password = string.Empty;
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 请求重定向到外部登录提供程序
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // 生成令牌并发送该令牌
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            var result = await SignInHelper.ExternalSignIn(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresTwoFactorAuthentication:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // 从外部登录提供程序获取有关用户的信息
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult MenuList()
        {
            if (HttpRuntime.Cache["isIniliaze"] == null)
            {
                UserManager.Users.FirstOrDefault();
                HttpRuntime.Cache.Insert("isIniliaze", true, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
            return PartialView(ServiceMenu.GetMenuList(User.Identity.GetUserId()));
        }

        [AllowAnonymous]
        public PartialViewResult ModalLogin(string returnUrl)
        {
            return PartialView();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModalLogin(LoginViewModel model, string returnUrl)
        {
            returnUrl = HttpUtility.UrlDecode(returnUrl);

            if (model == null)
            {
                return Json(new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "数据丢失，请稍后再试" });
            }

            var ip = HttpHelper.GetIPFromRequest(Request);
            IUserAccess access = new DAL_UserAccess();
            if (model.LoginType == 0)//用户名密码登录
            {
                var userModel = new UserModel { UserName = model.LoginUserName, Password = model.LoginPassword };
                var loginResult = access.UserLogin(userModel);
                if (loginResult.IsSuccess)
                {
                    await signIn(model.LoginUserName, loginResult.TResult, userModel.PhoneNumber);
                    return Json(new ICDExcuteResult<string> { IsSuccess = true, TResult = returnUrl });
                }
                return Json(new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = loginResult.ErrorStr });
            }
            if (model.LoginType == 1)//短信验证码登录
            {
                var getResult = access.GetUserByPhoneNum(model.LoginPhoneNum);
                if (!getResult.IsSuccess)
                {
                    return Json(new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = getResult.ErrorStr });
                }
                var validateCodeResult = ValidateCodeHelper.VerifyPhoneDymaticCode(model.LoginPhoneNum, model.LoginDynamicPWD);
                if (!validateCodeResult)
                {
                    return Json(new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "动态密码不匹配或者已过期" });
                }
                else
                {
                    await signIn(getResult.TResult.UserName, getResult.TResult.UserID, model.LoginPhoneNum);
                    return Json(new ICDExcuteResult<string> { IsSuccess = true, TResult = returnUrl });
                }
            }
            return Json(new ICDExcuteResult<string> { IsSuccess = false, ErrorStr = "无效的登录" });
        }

        private async Task signIn(string userName, string Id, string phoneNum)
        {
            await SignInHelper.SignInAsync(new ApplicationUser
            {
                UserName = userName,
                Id = Id,
                PhoneNumber = phoneNum,
                PhoneNumberConfirmed = true,
            }, false, false);
        }

        #region 帮助程序

        private const string XsrfKey = "XsrfId";
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }
            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }
            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}