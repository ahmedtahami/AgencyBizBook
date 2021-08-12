using AgencyBizBook.Entities;
using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Payment
        public ActionResult Index()
        {
            var modelList = db.Payments.ToList();
            return View(modelList);
        }
        public ActionResult AddExpense()
        {
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddExpense(ExpenseCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var desc = db.ExpenseCategories.Find(model.ExpenseCategoryId).Name;
                var payment = new Payment()
                {
                    Credit = 0,
                    Date = DateTime.Now,
                    Debit = model.Amount,
                    Description = desc + " " + model.Description,
                    Type = "Expense"
                };
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories.ToList(), model.ExpenseCategoryId);
            return View(model);
        }
    }
}