
using Docimax.Common;
using Docimax.Web_ICD.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_ICD.Controllers
{
    public class VerificationCodeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public ActionResult GetNewValidateCode(string validateKey)
        {
            var verrifyCodeModel = ValidateCodeHelper.CreateValidateCode(validateKey);
            return File(verrifyCodeModel.ValidateImage, @"image/jpeg");
        }
        [AllowAnonymous]
        public ActionResult VerifyValidateCode(string validateKey,string validateCode)
        {
            var verifyResult = ValidateCodeHelper.VerifyValidateCode(validateKey, validateCode);
            return Content(verifyResult.ToString());
        }
    }
}