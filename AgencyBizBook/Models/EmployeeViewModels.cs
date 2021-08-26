using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class UsersIndexViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public DateTime JoinDate { get; set; }
    }
    public class EmployeeCreateViewModel
    {
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(30)]
        public string Address { get; set; }
        [MaxLength(13)]
        public string CNIC { get; set; }
        [Display(Name = "Employee Type")]
        public EmployeeTypes EmployeeType { get; set; }
    }
    public class CustomerCreateViewModel
    {
        [Display(Name = "First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [MaxLength(30)]
        public string Address { get; set; }
        [MaxLength(13)]
        public string CNIC { get; set; }
    }
    public enum EmployeeTypes
    {
        Driver = 1,
        Salesman = 2,
        Accountant = 3
    }
}