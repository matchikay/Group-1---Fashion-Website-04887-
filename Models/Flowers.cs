using System.ComponentModel.DataAnnotations;

namespace LaFlor.Models
{
    public class Flowers
    {
        [Key]
        public int? flower_id { get; set; }
        [Required]
        public string? flower_name { get; set; }
        public int? flower_price { get; set; }
        public string? flower_details { get; set; }
        public string? flower_image { get; set; }

    }
}
