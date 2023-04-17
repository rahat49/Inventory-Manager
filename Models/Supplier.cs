using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Manager.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        [StringLength(6)]
        public string Code { get; set; } = "";
        [Required]
        [StringLength(75)]
        public string Name { get; set; } = "";

        [Remote("IsEmailExists", "Supplier",AdditionalFields ="Id",ErrorMessage ="Email Id Already Exists")]
        [Required]
        [MaxLength(75)]
        [DataType(DataType.EmailAddress,ErrorMessage ="E-Mail is not valid")]
        public string Email { get; set; } = "";
        [MaxLength(75)]
        public string Address { get; set; } = "";
        [MaxLength(15)]
        [DisplayName("Phone No")]
        public string PhoneNo { get; set; } = "";

    }
}
