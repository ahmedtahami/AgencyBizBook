using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    public class InvestmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Investment
        public ActionResult Index()
        {
            return View();
        }
    }
}