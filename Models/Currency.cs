using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Manager.Models
{
    public class Currency
    {
       [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(75)]
         public string Description { get; set; }

        [ForeignKey("Currencies")]
        public int ExchangeCurrencyId { get; set; }
        public Currency Currencies { get; set; }

        [Column(TypeName ="smallmoney")]
        [Required]
        public decimal ExchangeRate { get; set; }


        
    }
}
