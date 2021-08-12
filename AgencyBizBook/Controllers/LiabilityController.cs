using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    public class LiabilityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Liability
        public ActionResult Index()
        {
            return View();
        }
    }
}