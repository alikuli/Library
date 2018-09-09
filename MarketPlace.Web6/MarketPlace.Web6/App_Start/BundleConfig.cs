using System.Web.Optimization;

namespace MarketPlace.Web6
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Note. This all picks up the 
            //jquery.validate and
            //jquery.validate.unobtrusive
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/Knockout").Include(
                        "~/Scripts/knockout-3.1.0.js.js",
                        "~/Scripts/knockout-3.1.0.debug.js",
                        "~/Scripts/knockout.mapping-latest.debug.js",
                        "~/Scripts/perpetuum.knockout.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/FormAjaxPlugIn").Include(
                        "~/Scripts/jquery.form.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/JqueryIntelisense").Include(
            "~/Scripts/jquery-{version}.intellisense.js"));

            bundles.Add(new ScriptBundle("~/bundles/JquerySlim").Include(
            "~/Scripts/jquery-{version}.slim.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                        "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Scripts/umd/popper.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-grid.css",
                      "~/Content/bootstrap-reboot.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/site.css",
                      "~/Content/PrintStyleSheet.css"));
        }
    }
}





//using System.Web.Optimization;

//namespace MarketPlace.Web6
//{
//    public class BundleConfig
//    {
//        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
//        public static void RegisterBundles(BundleCollection bundles)
//        {
//            //Note. This all picks up the 
//            //jquery.validate and
//            //jquery.validate.unobtrusive
//            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
//                        "~/Scripts/jquery-{version}.js"));



//            bundles.Add(new ScriptBundle("~/bundles/JqueryIntelisense").Include(
//            "~/Scripts/jquery-{version}.intellisense.js"));

//            bundles.Add(new ScriptBundle("~/bundles/JquerySlim").Include(
//            "~/Scripts/jquery-{version}.slim.js"));

//            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
//                        "~/Scripts/jquery.validate*"));

//            // Use the development version of Modernizr to develop with and learn from. Then, when you're
//            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

//            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
//                        "~/Scripts/modernizr-*"));

//            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
//                        "~/Scripts/respond.min.js"));

//            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
//                      "~/Scripts/umd/popper.js",
//                      "~/Scripts/bootstrap.js"));



//            bundles.Add(new StyleBundle("~/Content/css").Include(
//                      "~/Content/bootstrap.css",
//                      "~/Content/site.css"));
//        }
//    }
//}
