using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docimax.Web_Test.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            //【1】获取 完整url （协议名+域名+虚拟目录名+文件名+参数）
            string url = Request.Url.ToString();
            //【2】获取 虚拟目录名+页面名+参数：
            string a = Request.RawUrl;
            //(或 string url=Request.Url.PathAndQuery;)
            //【3】获取 虚拟目录名+页面名：
            string b = Request.Url.AbsolutePath;
            //(或 string url= HttpContext.Current.Request.Path;)
            //【4】获取 域名：
            string c = Request.Url.Host;
            //【5】获取 参数：
            string d = Request.Url.Query;
            //【6】获取 端口：
            var e = Request.Url.Port;
            return View();
        }

        public ActionResult TreeViewTest()
        {
            return View();
        }
    }
}