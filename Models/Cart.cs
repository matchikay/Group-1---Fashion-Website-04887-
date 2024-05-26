using System.ComponentModel.DataAnnotations;

namespace LaFlor.Models
{
    public class Cart
    {
        [Key]
        public int cart_id { get; set; }
        [Required]
        public int cart_qty { get; set; }
        public int cart_total { get; set; }
        public string? cart_status { get; set; }
        public int flower_id { get; set; }
        public int customer_id { get; set; }

    }
}
