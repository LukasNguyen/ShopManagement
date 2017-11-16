using System.Web;
using System.Web.Optimization;
using SaleShop.Common;

namespace SaleShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").
                Include("~/Assets/client/js/jquery.min.js"));
            bundles.Add(new ScriptBundle("~/js/base").
                Include("~/Assets/admin/libs/jquery-ui/jquery-ui.js",
                "~/Assets/admin/libs/mustache/mustache.js",
                "~/Assets/admin/libs/numeral/numeral.js", 
                "~/Assets/admin/libs/jquery-validation/dist/jquery.validate.js",
                "~/Assets/admin/libs/jquery-validation/dist/additional-methods.js",
                "~/Assets/client/js/controllers/shoppingCart.js",
                "~/Assets/client/js/common.js"));

            bundles.Add(new StyleBundle("~/css/base").
                Include("~/Assets/client/css/bootstrap.css",new CssRewriteUrlTransform()).
                Include("~/Assets/client/libs/font-awesome-4.7.0/css/font-awesome.css",new CssRewriteUrlTransform()).
                Include("~/Assets/client/css/style.css",new CssRewriteUrlTransform()).
                Include("~/Assets/admin/libs/jquery-ui/themes/smoothness/jquery-ui.css",new CssRewriteUrlTransform()).
                Include("~/Assets/client/css/custom.css",new CssRewriteUrlTransform()));


            BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.GetByKey("EnableBundles"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }
    }
}
