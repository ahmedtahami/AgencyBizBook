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
        public string CokeInvoiceId { get; set; }
        public DateTime Date { get; set; }
        public double TotalWeight { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalAmount { get; set; }
        public string ShipmentNumber { get; set; }
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual ApplicationUser Driver { get; set; }

    }
}