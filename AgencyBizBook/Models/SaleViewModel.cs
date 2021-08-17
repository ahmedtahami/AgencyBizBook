using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class SaleCreateViewModel
    {
        public string VehicleNumber { get; set; }
        public string CustomerId { get; set; }
        public string DriverId { get; set; }
        public List<ProductSaleCreateViewModel> Products { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
    }
    public class ProductSaleCreateViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
    }
}