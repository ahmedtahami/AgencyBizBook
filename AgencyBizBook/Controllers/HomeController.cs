using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        public async Task<ActionResult> Index()
        {
            var model = new DashboardIndexViewModel();
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    var query = "select count(p.Id) Products, sum(pay.Credit) Credit, sum(pay.Debit) Debit from Products p, Payments pay";
            //    using (SqlCommand command = new SqlCommand(query, connection))
            //    {
            //        connection.Open();
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.HasRows)
            //            {
            //                while (await reader.ReadAsync())
            //                {
            //                    model.TotalCredit = reader.GetDouble(reader.GetOrdinal("Credit"));
            //                    model.TotalDebit = reader.GetDouble(reader.GetOrdinal("Debit"));
            //                    model.TotalProducts = reader.GetInt32(reader.GetOrdinal("Products"));
            //                    model.Balance = model.TotalDebit - model.TotalCredit;

            //                }
            //                connection.Close();
            //            }
            //        }
            //    }
            //}
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}