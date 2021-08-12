using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Models
{
    public class PurchaseCreateViewModel
    {
        public string CokeInvoiceId { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalAmount { get; set; }
        public string ShipmentNumber { get; set; }
        public string DriverId { get; set; }
        public List<ProductPurchaseCreateViewModel> Products { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
    }
    public class ProductPurchaseCreateViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
    }
}