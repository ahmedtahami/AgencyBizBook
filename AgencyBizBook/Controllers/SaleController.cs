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
    public class SaleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Sale
        public ActionResult Index(bool currentMonth = false, bool today = false)
        {
            var modelList = new List<Sale>();
            if (currentMonth)
            {
                modelList = db.Sales.Where(p => p.Date.Month == DateTime.Now.Month).ToList();
            }
            else if (today)
            {
                modelList = db.Sales.Where(p => p.Date == DateTime.Now.Date).ToList();
            }
            else
            {
                modelList = db.Sales.ToList();
            }

            return View(modelList);
        }
        public ActionResult Create()
        {
            var products = (from p in db.Products
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
                    sale.TotalAmount += (item.Rate * item.Quantity);
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
                    Credit = 0,
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Debit = sale.TotalAmount,
                    Type = "Customer",
                    Description = "Sale",
                    SaleId = sale.Id
                };
                Payment payment1 = new Payment()
                {
                    Credit = sale.TotalAmount,
                    Debit = 0,
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Type = "Customer",
                    Description = "Sale Payment",
                };
                db.Payments.Add(payment);
                db.Payments.Add(payment1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            ViewBag.CustomerId = new SelectList(db.Users.ToList(), model.CustomerId);
            return View(model);
        }
        public JsonResult GetProductInfo(int id)
        {
            var availableStock = db.Stocks.Where(p => p.ProductId == id).FirstOrDefault().Quantity;

            return Json(new { ProductStock = availableStock }, JsonRequestBehavior.AllowGet);
        }
    }
}