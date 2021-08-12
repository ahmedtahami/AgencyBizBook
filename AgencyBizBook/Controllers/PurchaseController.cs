using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    public class PurchaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Purchase
        public ActionResult Index()
        {
            var modelList = db.Purchases.ToList();
            return View(modelList);
        }
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Users.ToList(), "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(PurchaseCreateViewModel model)
        {
            return View();
        }
    }
}