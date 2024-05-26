using System.ComponentModel.DataAnnotations;

namespace LaFlor.Models
{
    public class Customer
    {
        [Key]
        public int? customer_id { get; set; } = default(int?);
        [Required]
        public string customer_username { get; set;}
        public string customer_email { get; set;}
        public string customer_password { get; set;}

    }
}
