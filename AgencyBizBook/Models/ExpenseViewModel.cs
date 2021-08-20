using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class ExpenseCreateViewModel
    {
        public double Amount  { get; set; }
        [Display(Name = "Expense Category")]
        public int ExpenseCategoryId  { get; set; }
        [MaxLength(50)]
        public string Description  { get; set; }
    }
    public class ExpenseIndexViewModel
    {
        public double Amount { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}