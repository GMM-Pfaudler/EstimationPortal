using System.Web;
using System.Web.Optimization;

namespace EstimationPortal
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/adminTheme").Include(
                       "~/Scripts/jquery-3.5.1.min.js",
                       "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
                       //"~/Content/vendor/chart.js/Chart.min.js",
                       "~/Content/vendor/jquery-easing/jquery.easing.min.js",
                       "~/Scripts/js/sb-admin-2.min.js",
                       //"~/Scripts/customeScript.js",
                       "~/Scripts/jquery.validate.js"));
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                  "~/Content/site.css",
                  "~/Content/bootstrap.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/Common").Include(
                  "~/Content/vendor/datatables/jquery.dataTables.min.js",
                  "~/Scripts/dataTables.buttons.min.js",
                  "~/Scripts/jszip.min.js",
                  "~/Scripts/vfs_fonts.js",
                  "~/Scripts/pdfmake.min.js",
                  "~/Scripts/buttons.html5.min.js",
                  "~/Scripts/buttons.colVis.min.js",
                  "~/Scripts/dataTables.responsive.min.js",
                  "~/Scripts/jquery-ui-1.11.4.min.js",
                  "~/Content/vendor/datatables/dataTables.bootstrap4.min.js",
                  "~/Scripts/bootstrap-multiselect.js",
                  "~/Scripts/responsive.bootstrap.min.js",
                  "~/Scripts/moment.min.js",
                  "~/Scripts/datetime-moment.js",
                  "~/Scripts/bootstrap-select.min.js",
                  "~/Scripts/js/select2.min.js",
                  "~/Scripts/timepicker.min.js"));


            bundles.Add(new StyleBundle("~/bundles/css").Include(
                     "~/Content/vendor/fontawesome-free/css/all.min.css",
                     "~/Content/css/sb-admin-2.css",
                     "~/Content/themes/base/jquery.ui.datepicker.css",
                     "~/Content/vendor/datatables/dataTables.bootstrap4.min.css",
                     "~/Content/css/buttons.dataTables.min.css",
                     "~/Scripts/searchPanes.dataTables.min.css",
                     "~/Scripts/select.dataTables.min.css",
                     "~/Scripts/fixedColumns.dataTables.min.css",
                     "~/Content/css/select2.min.css",
                     "~/Scripts/bootstrap-multiselect.css"));
        }
    }
}
