using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class ExpenseCreateViewModel
    {
        public double Amount  { get; set; }
        public int ExpenseCategoryId  { get; set; }
        public string Description  { get; set; }
    }
    public class ExpenseIndexViewModel
    {
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}