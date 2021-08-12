using AgencyBizBook.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencyBizBook.Entities
{
    public class StockOut
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public DateTime LastUpdated { get; set; }
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual ApplicationUser Driver { get; set; }
    }
}