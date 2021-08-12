using AgencyBizBook.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencyBizBook.Entities
{
    public class LabilityStockOut
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int LabilityId { get; set; }
        [ForeignKey("LabilityId")]
        public virtual Lability Lability { get; set; }
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual ApplicationUser Driver { get; set; }
    }
}