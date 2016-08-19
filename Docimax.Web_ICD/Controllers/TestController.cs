using Docimax.Common;
using Docimax.Common_ICD.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Docimax.Web_ICD.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestShowFile()
        {
            var url = "51icd-sysblob/user-attach/160726-113115739.jpg";
            return File(FileHelper.GetFile(url), "image/jpeg");
        }

        public ActionResult TypeaheadTest()
        {
            return View();
        }

        public JsonResult GetCities(string q = null)
        {
            return Json(!string.IsNullOrEmpty(q) ? Cities.Where(c => c.CityName.Contains(q)).ToList() : Cities, JsonRequestBehavior.AllowGet);
        }

        public IList<City> Cities
        {
            get
            {
                IList<City> cities = new List<City>();
                cities.Add(new City { Id = 1, ProvinceName = "广东省", CityName = "广州市" });
                cities.Add(new City { Id = 2, ProvinceName = "山东省", CityName = "济南市" });
                cities.Add(new City { Id = 3, ProvinceName = "陕西省", CityName = "西安市" });
                cities.Add(new City { Id = 4, ProvinceName = "青海省", CityName = "西宁市" });
                cities.Add(new City { Id = 5, ProvinceName = "广西省", CityName = "南宁市" });
                cities.Add(new City { Id = 6, ProvinceName = "江苏省", CityName = "南京市" });
                return cities;
            }
        }

        public class City
        {
            public int Id { get; set; }
            public string ProvinceName { get; set; }
            public string CityName { get; set; }
        }

        public ActionResult TestRandom()
        {
            var testStr = PlatformPwdHelper.CreatRadomPwd();
            //var key="hMme7#lfW4pMP&eV";

            //var key = "hMme7#l#d4pMP&eVYN8m5d0qcscfKrPD";

            //var encryptStr = EncryptUtils.EncryptByAES(testStr, key);
            //var decryptStr = EncryptUtils.DecryptByAES(encryptStr, key);
            //return Content(string.Format(@"{0}<br>{1}<br>{2}", testStr, encryptStr, decryptStr));
            return Content(testStr);
        }
    }
}