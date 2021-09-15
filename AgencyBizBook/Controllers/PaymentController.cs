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
    [Authorize(Roles = "Admin")]
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Payment
        public ActionResult Index(bool customerLedger = false, bool supplierLedger = false, bool employeeLedger = false)
        {
            var modelList = new List<Payment>();
            if (customerLedger)
            {
                modelList = (from payments in db.Payments where payments.Type == "Customer" select payments).ToList();
            }
            else if (supplierLedger)
            {
                modelList = (from payments in db.Payments where payments.Type == "Supplier" select payments).ToList();
            }
            else if (employeeLedger)
            {
                modelList = (from payments in db.Payments where payments.Type == "Employee" select payments).ToList();
            }
            else
            {
                modelList = (from payments in db.Payments select payments).ToList();
            }
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
            return PartialView();
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
            return PartialView(model);
        }
        public ActionResult CashIn()
        {
            ViewBag.UserId = new SelectList(db.Users.ToList(), "Id", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult CashIn(CashTranactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payment payment = new Payment()
                {
                    Credit = model.Amount,
                    Description = model.Description,
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Type = model.Type,
                    UserId = model.UserId
                };
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users.ToList(), "Id", "Name", model.UserId);
            return PartialView(model);
        }
        public ActionResult CashOut()
        {
            ViewBag.UserId = new SelectList(db.Users.ToList(), "Id", "Name");
            return PartialView();
        }
        [HttpPost]
        public ActionResult CashOut(CashTranactionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Payment payment = new Payment()
                {
                    Debit = model.Amount,
                    Description = model.Description,
                    EntryDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Type = model.Type,
                    UserId = model.UserId
                };
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users.ToList(), "Id", "Name", model.UserId);
            return PartialView(model);
        }
    }
}