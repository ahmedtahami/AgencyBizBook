using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class ExpenseCreateViewModel
    {
        public double Amount { get; set; }
        [Display(Name = "Expense Category")]
        public int ExpenseCategoryId { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
    }
    public class ExpenseIndexViewModel
    {
        public double Amount { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }
    }
    public class CashTranactionViewModel
    {
        [Display(Name = "User")]
        public string UserId { get; set; }
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        public double Amount { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [MaxLength(20)]
        public string Type { get; set; }
        public static List<Models.Type> types = new List<Models.Type>()
        {
                new Models.Type(){Name = "Sale Payment"},
                new Models.Type(){Name = "Sale Customer Payment"},
                new Models.Type(){Name = "Purchase Payment"},
                new Models.Type(){Name = "Purchase Customer Payment"},
                new Models.Type(){Name = "Expenses"},
                new Models.Type(){Name = "Supplier Payment"},
                new Models.Type(){Name = "Miscellaneous"},
        };
    }
    public class Type
    {
        public string Name { get; set; }
    }

}