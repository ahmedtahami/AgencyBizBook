using AgencyBizBook.Entities;
using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Stock
        public ActionResult Index()
        {
            var modelList = db.Stocks.ToList();
            return View(modelList);
        }
        public ActionResult StockIn()
        {
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "Id", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult StockIn(StockTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var weight = ((from p in db.Products where p.Id == model.ProductId select p).FirstOrDefault().NetWeight) * model.Quantity;
                var stock = (from s in db.Stocks where s.ProductId == model.ProductId select s).FirstOrDefault();
                stock.Quantity += model.Quantity;
                stock.TotalWeight += weight;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products.ToList(), model.ProductId);
            return PartialView(model);
        }
        public ActionResult StockOut()
        {
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "Id", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult StockOut(StockTransactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stock = (from s in db.Stocks where s.ProductId == model.ProductId select s).FirstOrDefault();
                if (stock.Quantity > 0 && model.Quantity <= stock.Quantity)
                {
                    var weight = ((from p in db.Products where p.Id == model.ProductId select p).FirstOrDefault().NetWeight) * model.Quantity;
                    stock.Quantity -= model.Quantity;
                    stock.TotalWeight -= weight;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Quantity", "Please Enter A Valid Quantity");
                    ViewBag.ProductId = new SelectList(db.Products.ToList(), "Id", "Name", model.ProductId);
                    return PartialView(model);
                }
            }
            ViewBag.ProductId = new SelectList(db.Products.ToList(), model.ProductId);
            return PartialView(model);
        }
        public ActionResult DriverStockLog()
        {
            var modelList = db.StockOut.ToList();
            return View(modelList);
        }
        public ActionResult DriverStockOut()
        {
            ViewBag.ProductId = new SelectList(db.Products.ToList(), "Id", "Name");
            ViewBag.DriverId = new SelectList(db.Users.ToList(), "Id", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult DriverStockOut(StockOut model)
        {
            if (ModelState.IsValid)
            {
                var stock = (from s in db.Stocks where s.ProductId == model.ProductId select s).FirstOrDefault();
                if (stock.Quantity > 0 && model.Quantity <= stock.Quantity)
                {
                    var weight = ((from p in db.Products where p.Id == model.ProductId select p).FirstOrDefault().NetWeight) * model.Quantity;
                    stock.Quantity -= model.Quantity;
                    stock.TotalWeight -= weight;
                    StockOut entity = new StockOut()
                    {
                        DriverId = model.DriverId,
                        LastUpdated = DateTime.Now,
                        ProductId = model.ProductId,
                        Quantity = model.Quantity,
                        TotalWeight = weight
                    };
                    db.StockOut.Add(entity);
                    db.SaveChanges();
                    return RedirectToAction("DriverStockLog");
                }
                else
                {
                    ModelState.AddModelError("Quantity", "Please Enter A Valid Quantity");
                    ViewBag.ProductId = new SelectList(db.Products.ToList(), "Id", "Name", model.ProductId);
                    ViewBag.DriverId = new SelectList(db.Users.ToList(), "Id", "Name", model.DriverId);
                    return PartialView(model);
                }
            }
            ViewBag.ProductId = new SelectList(db.Products.ToList(), model.ProductId);
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            return PartialView(model);
        }
    }
}