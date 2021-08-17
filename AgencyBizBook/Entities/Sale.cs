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
        public string VehicleNumber { get; set; }
        public string OutletCode { get; set; }
        public string OutletName { get; set; }
        public string OutletAddress { get; set; }
        public string OutletPhoneNumber { get; set; }
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual ApplicationUser Driver { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; }
        public double TotalWeight { get; set; }
        public double TotalQuantity { get; set; }
        public double TotalAmount { get; set; }
        public DateTime Date { get; set; }
    }
}