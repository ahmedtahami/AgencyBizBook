﻿using AgencyBizBook.Entities;
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
                    var productWeight = ((from p in db.Products where p.Id == item.ProductId select p).FirstOrDefault().NetWeight) * item.Quantity;
                    purchase.TotalQuantity += item.Quantity;
                    purchase.TotalAmount += item.Rate;
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
                    Credit = 0,
                    Date = DateTime.Now,
                    Debit = purchase.TotalAmount,
                    Type = "Purchase",
                    Description = "Purchase",
                };
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            ViewBag.ProductId = new SelectList(db.Products.ToList(), model.ProductId);
            return View(model);
        }
    }
}