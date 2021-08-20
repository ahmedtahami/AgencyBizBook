using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class EmployeeIndexViewModel
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public DateTime JoinDate { get; set; }
    }
    public class EmployeeCreateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public EmployeeTypes EmployeeType { get; set; }
    }
    public enum EmployeeTypes
    {
        Driver = 1,
        Salesman = 2,
        Accountant = 3
    }
}