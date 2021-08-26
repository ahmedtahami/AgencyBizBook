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
    public class LiabilityController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Liability
        public ActionResult Index()
        {
            var modelList = db.Liabilities.ToList();
            return View(modelList);
        }
        public ActionResult LiabilityStock()
        {
            var modelList = db.LiabilityStocks.ToList();
            return View(modelList);
        }
        public ActionResult LiabilityTransactions()
        {
            var modelList = db.LiabilityTransactions.ToList();
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
                db.Liabilities.Add(liability);
                LiabilityStock liabilityStock = new LiabilityStock()
                {
                    LastUpdated = DateTime.Now,
                    LiabilityId = liability.Id,
                    Quantity = 0
                };
                db.LiabilityStocks.Add(liabilityStock);

                Payment payment = new Payment()
                {
                    Credit = liabilityStock.Quantity * liability.Price,
                    Debit = 0,
                    LiabilityStockId = liabilityStock.Id,
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Type = "Liability",
                    Description = liability.Name
                };
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(liability);
        }
        public ActionResult StockIn()
        {
            ViewBag.LiabilityId = new SelectList(db.Liabilities.ToList(), "Id", "Name");
            ViewBag.DriverId = new SelectList(db.Users.ToList(), "Id", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult StockIn(LiabilityTransaction model)
        {
            if (ModelState.IsValid)
            {
                var liabilityStock = db.LiabilityStocks.Where(p => p.LiabilityId == model.LiabilityId).FirstOrDefault();
                liabilityStock.Quantity += (int) model.In;
                liabilityStock.LastUpdated = DateTime.Now;
                db.Entry(liabilityStock).State = System.Data.Entity.EntityState.Modified;
                LiabilityTransaction transaction = new LiabilityTransaction()
                {
                    Date = DateTime.Now,
                    DriverId = model.DriverId,
                    LiabilityId = model.LiabilityId,
                    In = model.In,
                    Out = 0
                };
                db.LiabilityTransactions.Add(transaction);

                var payment = db.Payments.Where(p => p.LiabilityStockId == liabilityStock.Id).FirstOrDefault();
                payment.Credit = (double)((model.In) * (model.Liability.Price));
                payment.EntryDate = DateTime.Now;
                payment.LastUpdated = DateTime.Now;
                db.Entry(payment).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LiabilityStock");
            }
            ViewBag.LiabilityId = new SelectList(db.Liabilities.ToList(), model.LiabilityId);
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            return PartialView(model);
        }
        public ActionResult StockOut()
        {
            ViewBag.LiabilityId = new SelectList(db.Liabilities.ToList(), "Id", "Name");
            ViewBag.DriverId = new SelectList(db.Users.ToList(), "Id", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult StockOut(LiabilityTransaction model)
        {
            if (ModelState.IsValid)
            {
                var liabilityStock = db.LiabilityStocks.Where(p => p.LiabilityId == model.LiabilityId).FirstOrDefault();
                liabilityStock.Quantity -= (int)model.Out;
                liabilityStock.LastUpdated = DateTime.Now;
                db.Entry(liabilityStock).State = System.Data.Entity.EntityState.Modified;
                LiabilityTransaction transaction = new LiabilityTransaction()
                {
                    Date = DateTime.Now,
                    DriverId = model.DriverId,
                    LiabilityId = model.LiabilityId,
                    In = 0,
                    Out = model.Out
                };
                db.LiabilityTransactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("LiabilityStock");
            }
            ViewBag.LiabilityId = new SelectList(db.Liabilities.ToList(), model.LiabilityId);
            ViewBag.DriverId = new SelectList(db.Users.ToList(), model.DriverId);
            return PartialView(model);
        }
    }
}