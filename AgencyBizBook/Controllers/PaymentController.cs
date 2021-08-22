using AgencyBizBook.Entities;
using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
        public ActionResult Expenses(bool currentMonth = false, bool today = false)
        {
            var modelList = new List<ExpenseIndexViewModel>();
            if (currentMonth)
            {
                modelList = (from expense in db.Payments
                             where expense.Type == "Expense" && expense.EntryDate.Month == DateTime.Now.Month
                             select new ExpenseIndexViewModel()
                             {
                                 Amount = expense.Debit,
                                 EntryDate = expense.EntryDate,
                                 LastUpdated = expense.LastUpdated,
                                 Description = expense.Description
                             }).ToList();
            }
            else if (today)
            {
                var tempModelList = db.Payments.Where(p => p.Type == "Expense").ToList();
                foreach (var item in tempModelList)
                {
                    if (item.EntryDate.Date == DateTime.Now.Date)
                    {
                        var model = new ExpenseIndexViewModel()
                        {
                            Amount = item.Debit,
                            Description = item.Description,
                            EntryDate = item.EntryDate,
                            LastUpdated = item.LastUpdated
                        };
                        modelList.Add(model);
                    }
                }
            }
            else
            {
                modelList = (from expense in db.Payments
                             where expense.Type == "Expense"
                             select new ExpenseIndexViewModel()
                             {
                                 Amount = expense.Debit,
                                 EntryDate = expense.EntryDate,
                                 LastUpdated = expense.LastUpdated,
                                 Description = expense.Description
                             }).ToList();
            }
            
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
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Debit = model.Amount,
                    Description = desc + " " + model.Description,
                    Type = "Expense"
                };
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Expenses");
            }
            ViewBag.ExpenseCategoryId = new SelectList(db.ExpenseCategories.ToList(), model.ExpenseCategoryId);
            return View(model);
        }
        public ActionResult ExpenseCategories()
        {
            var modelList = db.ExpenseCategories.ToList();
            return View(modelList);
        }
        public ActionResult AddExpenseCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddExpenseCategory(ExpenseCategory model)
        {
            if (ModelState.IsValid)
            {
                db.ExpenseCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("ExpenseCategories");
            }
            return View(model);
        }
    }
}