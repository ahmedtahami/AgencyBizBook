using AgencyBizBook.Entities;
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
            var modelList = db.Labilities.ToList();
            return View(modelList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Liability liability)
        {
            if (ModelState.IsValid)
            {
                db.Labilities.Add(liability);
                db.SaveChanges();
            }
            return View(liability);
        }
    }
}