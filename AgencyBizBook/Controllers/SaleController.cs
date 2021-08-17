using AgencyBizBook.Entities;
using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    public class SaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Sale
        public ActionResult Index()
        {
            var modelList = db.Sales.ToList();
            return View(modelList);
        }
        public ActionResult Create()
        {
            var products = (from p in db.Products
                            join s in db.Stocks
                            on p.Id equals s.ProductId
                            where s.Quantity > 0
                            select p).ToList();
            ViewBag.DriverId = new SelectList(db.Users.ToList(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Users.ToList(), "Id", "Name");
            ViewBag.ProductId = new SelectList(products, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(SaleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Sale sale = new Sale();
                sale.CustomerId = model.CustomerId;
                sale.DriverId = model.DriverId;
                sale.Date = DateTime.Now;
                sale.OutletAddress = "Gujranwala";
                sale.OutletCode = "123456";
                sale.OutletName = "ABC Outlet";
                sale.OutletPhoneNumber = "0321456789";
                sale.VehicleNumber = model.VehicleNumber;


                foreach (var item in model.Products)
                {
                    var productWeight = ((from p in db.Products where p.Id == item.ProductId select p).FirstOrDefault().NetWeight) * item.Quantity;
                    sale.TotalQuantity += item.Quantity;
                    sale.TotalAmount += item.Rate;
                    sale.TotalWeight += productWeight;

                    SaleDetail saleDetail = new SaleDetail()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        SaleId = sale.Id,
                    };
                    db.SaleDetails.Add(saleDetail);

                    var productStock = db.Stocks.Where(p => p.ProductId == item.ProductId).FirstOrDefault();
                    productStock.LastUpdated = DateTime.Now;
                    productStock.Quantity -= item.Quantity;
                    productStock.TotalWeight -= productWeight;
                    db.Entry(productStock).State = System.Data.Entity.EntityState.Modified;
                }
                db.Sales.Add(sale);
                Payment payment = new Payment()
                {
                    Debit = 0,
                    Date = DateTime.Now,
                    Credit = sale.TotalAmount,
                    Type = "Sale",
                    Description = "Sale",
                };
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            ViewBag.CustomerId = new SelectList(db.Users.ToList(), model.CustomerId);
            return View(model);
        }
    }
}