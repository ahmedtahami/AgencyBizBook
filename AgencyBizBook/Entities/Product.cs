using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgencyBizBook.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [Display(Name = "Sale Price")]
        public double SalePrice { get; set; }
        [Display(Name = "Purchase Price")]
        public double PurchasePrice { get; set; }
        [Display(Name = "Net. Weight (ML)")]
        public double NetWeight { get; set; }
    }
}