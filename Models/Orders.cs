using System.ComponentModel.DataAnnotations;

namespace LaFlor.Models
{
    public class Orders
    {
        [Key]
        public int order_id { get; set; }
        [Required]
        public string? order_name { get; set; }
        public string? order_number { get; set; }
        public string? order_email { get; set; }
        public string? order_payment_method { get; set; }
        public string? order_address1 { get; set; }
        public string? order_address2 { get; set; }
        public string? order_barangay { get; set; }
        public string? order_city { get; set; }
        public string? order_province { get; set; }
        public string? order_zip { get; set; }
        public string? order_placed_date { get; set; }
        public int? order_total { get; set; }
        public string? order_status { get; set; }
        public int? customer_id { get; set; }
    }
}



