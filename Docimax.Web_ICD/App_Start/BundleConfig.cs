﻿using System.Web;
using System.Web.Optimization;

namespace Docimax.Web_ICD
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));



            bundles.Add(new ScriptBundle("~/bundles/angularJS").Include(
                   "~/Scripts/angular.js"));


            bundles.Add(new ScriptBundle("~/bundles/sitePubJS").Include(
                               "~/Scripts/Site/Pub.js"));
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
