using AgencyBizBook.Entities;
using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PurchaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Purchase
        public ActionResult Index(bool currentMonth = false, bool today = false)
        {
            var modelList = new List<Purchase>();
            if (currentMonth)
            {
                modelList = db.Purchases.Where(p => p.Date.Month == DateTime.Now.Month).ToList();
            }
            else if (today)
            {
                modelList = db.Purchases.ToList();
            }
            else
            {
                modelList = db.Purchases.ToList();
            }
            
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
                    var productWeight = ((from p in db.Products where p.Id == item.ProductId select p).FirstOrDefault().NetWeight) * item.Quantity;
                    purchase.TotalQuantity += item.Quantity;
                    purchase.TotalAmount += (item.Rate * item.Quantity);
                    purchase.TotalWeight += productWeight;

                    PurchaseDetail purchaseDetail = new PurchaseDetail()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        PurchaseId = purchase.Id
                    };
                    db.PurchaseDetails.Add(purchaseDetail);

                    var productStock = db.Stocks.Where(p => p.ProductId == item.ProductId).FirstOrDefault();
                    if (productStock == null)
                    {
                        Stock stock = new Stock()
                        {
                            LastUpdated = DateTime.Now,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            TotalWeight = productWeight
                        };
                        db.Stocks.Add(stock);
                    }
                    else
                    {
                        productStock.LastUpdated = DateTime.Now;
                        productStock.Quantity += item.Quantity;
                        productStock.TotalWeight += productWeight;
                        db.Entry(productStock).State = System.Data.Entity.EntityState.Modified;
                    }
                    
                }
                db.Purchases.Add(purchase);
                Payment payment = new Payment()
                {
                    Credit = purchase.TotalAmount,
                    Debit = 0,
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Type = "Supplier",
                    Description = "Purchase",
                    PurchaseId = purchase.Id
                };
                Payment payment1 = new Payment()
                {
                    Credit = 0,
                    Debit = purchase.TotalAmount,
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Type = "Supplier",
                    Description = "Purchase Payment",
                };
                db.Payments.Add(payment);
                db.Payments.Add(payment1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            ViewBag.ProductId = new SelectList(db.Products.ToList(), model.ProductId);
            return View(model);
        }
    }
}