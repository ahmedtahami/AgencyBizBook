using AgencyBizBook.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencyBizBook.Entities
{
    public class LiabilityTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Quantity")]
        public int? In { get; set; }
        [Display(Name = "Quantity")]
        public int? Out { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "Liability")]
        public int LiabilityId { get; set; }
        [ForeignKey("LiabilityId")]
        public virtual Liability Liability { get; set; }
        [Display(Name = "Driver")]
        public string DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual ApplicationUser Driver { get; set; }
    }
}