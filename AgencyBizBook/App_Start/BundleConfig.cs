using System.Web;
using System.Web.Optimization;

namespace AgencyBizBook
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            
            bundles.Add(new StyleBundle("~/Site/Styles").Include(
                      "~/assets/datatables/dataTables.bootstrap4.min.css",
                      "~/assets/datatables/buttons.bootstrap4.min.css",
                      "~/assets/datatables/responsive.bootstrap4.min.css",
                      "~/assets/morris/morris.css",
                      "~/assets/css/bootstrap.min.css",
                      "~/assets/css/metismenu.min.css",
                      "~/assets/css/icons.css",
                      "~/assets/css/style.css"
                ));
            bundles.Add(new ScriptBundle("~/Site/Scripts").Include(
                      "~/assets/js/jquery.min.js",
                      "~/assets/js/bootstrap.bundle.min.js",
                      "~/assets/js/metismenu.min.js",
                      "~/assets/js/jquery.slimscroll.js",
                      "~/assets/js/waves.min.js",
                      "~/assets/datatables/jquery.dataTables.min.js",
                      "~/assets/datatables/dataTables.bootstrap4.min.js",
                      "~/assets/datatables/dataTables.buttons.min.js",
                      "~/assets/datatables/buttons.bootstrap4.min.js",
                      "~/assets/datatables/jszip.min.js",
                      "~/assets/datatables/pdfmake.min.js",
                      "~/assets/datatables/vfs_fonts.js",
                      "~/assets/datatables/buttons.html5.min.js",
                      "~/assets/datatables/buttons.print.min.js",
                      "~/assets/datatables/buttons.colVis.min.js",
                      "~/assets/datatables/dataTables.responsive.min.js",
                      "~/assets/datatables/responsive.bootstrap4.min.js",
                      "~/assets/pages/datatables.init.js",
                      "~/assets/morris/morris.min.js",
                      "~/assets/raphael/raphael.min.js",
                      "~/assets/pages/dashboard.init.js",
                      "~/assets/js/app.js"
                ));
        }
    }
}
