using System.Web;
using System.Web.Optimization;

namespace Softvision.All
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            //Scripts
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/article").Include(
                                    "~/Scripts/adminArticle.js",
                                    "~/Scripts/article.js",
                                    "~/Scripts/_articlePV.js"));

            bundles.Add(new ScriptBundle("~/bundles/articlePV").Include(
                        "~/Scripts/_articlePV.js"));

            bundles.Add(new ScriptBundle("~/bundles/generalValidation").Include(
                        "~/Scripts/generalValidation.js"));

            bundles.Add(new ScriptBundle("~/bundles/trueEditorPV").Include(
                        "~/Scripts/_trueEditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/question").Include(
                                    "~/Scripts/adminQuestion.js",
                                    "~/Scripts/question.js",
                                    "~/Scripts/_questionPV.js"));

            bundles.Add(new ScriptBundle("~/bundles/questionPV").Include(
                                    "~/Scripts/_questionPV.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                      "~/Scripts/user.js",
                      "~/Scripts/validation.js",
                      "~/Scripts/common.js"));


            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/Scripts/jquery-ui.js"));

            //Styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/main").Include(
                      "~/Content/annEditor.css",
                      "~/Content/main.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-ui").Include(
                      "~/Content/jquery-ui.css",
                      "~/Content/smoothness-jquery-ui.css",
                      "~/Content/jquery-ui.theme.css",
                      "~/Content/jquery-ui.structure.css"));

        }
    }
}
