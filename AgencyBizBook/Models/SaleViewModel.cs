using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class SaleCreateViewModel
    {
        [Display(Name = "Vehicle Number")]
        [MaxLength(20)]
        public string VehicleNumber { get; set; }
        [Display(Name = "Customer")]
        public string CustomerId { get; set; }
        [Display(Name = "Driver")]
        public string DriverId { get; set; }
        public List<ProductSaleCreateViewModel> Products { get; set; }
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Available Stock")]
        public int AvailableStock { get; set; }
        public double Rate { get; set; }
    }
    public class ProductSaleCreateViewModel
    {
        [Display(Name = "Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
    }
}