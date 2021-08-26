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
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        public ActionResult Index()
        {
            var model = new DashboardIndexViewModel();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"Select Sum(pay.Credit) Credit, Sum(pay.Debit) Debit 
                                From Payments pay;
                                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.TotalProducts = db.Products.Count();          
                        if (!dr.IsDBNull(dr.GetOrdinal("Credit")))
                            model.TotalCredit = dr.GetDouble(0);
                        if (!dr.IsDBNull(dr.GetOrdinal("Debit")))
                            model.TotalDebit = dr.GetDouble(1);
                        model.Balance = model.TotalDebit - model.TotalCredit;
                    }
                }
                dr.Close();
                conn.Close();
            }
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