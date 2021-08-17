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
            ViewBag.DriverId = new SelectList(db.Users.ToList(), "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Users.ToList(), "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "Id", "Name");
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
                    sale.TotalQuantity += item.Quantity;
                    sale.TotalAmount += item.Rate;
                    sale.TotalWeight += ((from p in db.Products where p.Id == item.ProductId select p).FirstOrDefault().NetWeight) * item.Quantity;

                    SaleDetail saleDetail = new SaleDetail()
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        SaleId = sale.Id,
                    };
                    db.SaleDetails.Add(saleDetail);
                }
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            ViewBag.CustomerId = new SelectList(db.Users.ToList(), model.CustomerId);
            return View(model);
        }
    }
}