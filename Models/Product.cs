using InventoryManger.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Inventory_Manager.Models
    {
        public class Product
        {
            [Key]
            [StringLength(6)]
            public string Code { get; set; }
            [Required]
            [StringLength(75)]
            public string Name { get; set; }
            [Required]
            [StringLength(225)]
            public string Description { get; set; }
            [Required]
            [Column(TypeName = "smallmoney")]
            public decimal Cost { get; set; }
            [Required]
            [Column(TypeName = "smallmoney")]
            public decimal Price { get; set; }
            [Required]
            [ForeignKey("Units")]
            [DisplayName("Unit")]
            public int UnitId { get; set; }
            public Unit Units { get; set; }


            [ForeignKey("Brands")]
            [DisplayName("Brand")]
            public int BrandId { get; set; }
            public Brand Brands { get; set; }

            [ForeignKey("Categories")]
            [DisplayName("Category")]
            public int CategoryId { get; set; }
            public Category Categories { get; set; }

            [ForeignKey("ProductGroup")]
            [DisplayName("Product Group")]
            public int ProductGroupId { get; set; }
            public ProductGroup ProductGroup { get; set; }

            [ForeignKey("ProductProfile")]
            [DisplayName("Product Profile")]
            public int ProductProfileId { get; set; }
            public ProductProfile ProductProfile { get; set; }

        public string PhotoUrl { get; set; } = "noimg.png";
            [Display(Name="Product Photo")]
            [NotMapped]
            public IFormFile ProductPhote { get; set; }

        [NotMapped]
           public string BriefPhotoName { get; set; }

    }
}


