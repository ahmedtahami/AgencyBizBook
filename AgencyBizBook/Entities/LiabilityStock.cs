using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgencyBizBook.Entities
{
    public class LiabilityStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }
        [Display(Name = "Liability")]
        public int LiabilityId { get; set; }
        [ForeignKey("LiabilityId")]
        public virtual Liability Liability { get; set; }
    }
}