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
    public class InvestmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Investment
        public ActionResult Index()
        {
            var modelList = db.Investments.ToList();
            return View(modelList);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Investment model)
        {
            if (ModelState.IsValid)
            {
                Investment investment = new Investment()
                {
                    Credit = model.Credit,
                    Debit = model.Debit,
                    Date = DateTime.Now,
                    Description = model.Description,
                    Name = model.Name
                };
                db.Investments.Add(investment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id.Equals(null))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var model = db.Investments.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Investment model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}