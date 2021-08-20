using AgencyBizBook.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class PurchaseCreateViewModel
    {
        [Display(Name = "Coke Invoice")]
        [MaxLength(50)]
        public string CokeInvoiceId { get; set; }
        [Display(Name = "Total Quantity")]
        public double TotalQuantity { get; set; }
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }
        [Display(Name = "Shipment No.")]
        [MaxLength(50)]
        public string ShipmentNumber { get; set; }
        [Display(Name = "Driver")]
        public string DriverId { get; set; }
        public List<ProductPurchaseCreateViewModel> Products { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
    }
    public class ProductPurchaseCreateViewModel
    {
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
    }
}