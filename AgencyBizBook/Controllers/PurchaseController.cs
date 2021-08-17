using AgencyBizBook.Entities;
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
            if (ModelState.IsValid)
            {
                Purchase purchase = new Purchase();
                purchase.CokeInvoiceId = model.CokeInvoiceId;
                purchase.Date = DateTime.Now;
                purchase.DriverId = model.DriverId;
                purchase.ShipmentNumber = model.ShipmentNumber;

                foreach (var item in model.Products)
                {
                    purchase.TotalQuantity += item.Quantity;
                    purchase.TotalAmount += item.Rate;
                    purchase.TotalWeight += ((from p in db.Products where p.Id == item.ProductId select p).FirstOrDefault().NetWeight) * item.Quantity;

                    PurchaseDetail purchaseDetail = new PurchaseDetail()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        PurchaseId = purchase.Id
                    };
                    db.PurchaseDetails.Add(purchaseDetail);
                }
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            ViewBag.ProductId = new SelectList(db.Products.ToList(), model.ProductId);
            return View(model);
        }
    }
}