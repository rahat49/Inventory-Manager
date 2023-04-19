using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Manager.Models
{
    public class Currencies
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        [Required]
        [StringLength(75)]
        public string Description { get; set; }

        //[ForeignKey("Currency")]
        //public int ExchangeCurrencyId { get; set; }
        //public Currencies Currency { get; set; }

        [Column(TypeName = "smallmoney")]
        [Required]
        public decimal ExchangeRate { get; set; }

    }
}
