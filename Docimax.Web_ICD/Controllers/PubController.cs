﻿using Docimax.Common;
using Docimax.Data_ICD.DAL;
using Docimax.Interface_ICD.Interface;
using Docimax.Interface_ICD.Model;
using Docimax.Web_ICD.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class PubController : Controller
    {
        private ApplicationUserManager _userManager;
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> SendPhoneVerifyCode(Telephone telephone)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ICDExcuteResult<int> { IsSuccess = false, ErrorStr = "请输入正确的手机号码" });
            }
            var sourceIp = HttpHelper.GetIPFromRequest(Request);
            var shouldDelaySecond = 150;
            var verifyResult = ValidateCodeHelper.PhoneIntercept(telephone.PhoneNumber, sourceIp, shouldDelaySecond);
            switch (verifyResult)
            {
                case -2:
                    return Json(new ICDExcuteResult<int>
                    {
                        IsSuccess = false,
                        ErrorStr = "机器人？过段时间在试试吧",
                    });
                case -1:
                    return Json(new ICDExcuteResult<int>
                    {
                        IsSuccess = false,
                        ErrorStr = "机器人？过段时间在试试吧",
                    });
                case 0:
                    var code = ValidateCodeHelper.CreatePhoneValidateCode(telephone.PhoneNumber, sourceIp);
                    if (UserManager.SmsService != null)
                    {
                        var message = new IdentityMessage
                        {
                            Destination = telephone.PhoneNumber,
                            Body = "您的验证码是：" + code.ValidateCode + "。请不要把验证码泄露给其他人。如非本人操作，可不用理会！",
                        };
                        await UserManager.SmsService.SendAsync(message);
                        ThreadPool.QueueUserWorkItem(a =>
                        {
                            ISecurity access = new DAL_Security();
                            access.SavePhoneMessage(new SecurityPhoneModel
                            {
                                LastSendTime = DateTime.Now,
                                PhoneNumber = telephone.PhoneNumber,
                                SourceIP = sourceIp,
                            });
                        });
                    }
                    return Json(new ICDExcuteResult<int>
                    {
                        IsSuccess = true,
                        TResult = shouldDelaySecond,
                    });
                default:
                    return Json(new ICDExcuteResult<int>
                    {
                        ErrorStr="没收到短信？请稍等，短信君正在路上",
                        IsSuccess = false,
                        TResult = verifyResult,
                    });
            }
        }
    }
}