using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Entities
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Vehicle Number")]
        [MaxLength(20)]
        public string VehicleNumber { get; set; }
        [Display(Name = "Outlet Code")]
        [MaxLength(30)]
        public string OutletCode { get; set; }
        [Display(Name = "Outlet Name")]
        [MaxLength(30)]
        public string OutletName { get; set; }
        [Display(Name = "Outlet Address")]
        [MaxLength(50)]
        public string OutletAddress { get; set; }
        [Display(Name = "Outlet Phone Number")]
        [MaxLength(20)]
        public string OutletPhoneNumber { get; set; }
        [Display(Name = "Driver")]
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual ApplicationUser Driver { get; set; }
        [Display(Name = "Customer")]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        [Display(Name = "Total Weight (ML)")]
        public double TotalWeight { get; set; }
        [Display(Name = "Total Quantity")]
        public double TotalQuantity { get; set; }
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}