﻿using System.Web;
using System.Web.Optimization;

namespace news
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery/jquery-2.2.4.min.js",
                       "~/Scripts/bootstrap/popper.min.js",
                       "~/Scripts/bootstrap/bootstrap.min.js",
                       "~/Scripts/plugins/plugins.js",
                       "~/Scripts/active.js",
                       "~/Scripts/google-map/map-active.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/style.css.map",
                      "~/Content/style.css"
));
        }
    }
}
