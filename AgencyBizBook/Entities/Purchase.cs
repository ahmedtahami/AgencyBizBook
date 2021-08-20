using AgencyBizBook.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Entities
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Coke Invoice")]
        [MaxLength(50)]
        public string CokeInvoiceId { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Total Weight (ML)")]
        public double TotalWeight { get; set; }
        [Display(Name = "Total Quantity")]
        public double TotalQuantity { get; set; }
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }
        [Display(Name = "Shipment No.")]
        [MaxLength(50)]
        public string ShipmentNumber { get; set; }
        [Display(Name = "Driver")]
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual ApplicationUser Driver { get; set; }

    }
}