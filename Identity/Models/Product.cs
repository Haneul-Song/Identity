using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Models
{
    public class Product
    {
        [Key]
        public int product_ID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string name { get; set; }
        public int? year { get; set; }
        public int? num_parts { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal price { get; set; }
        public string? img_link { get; set; }
        public string? primary_color { get; set; }
        public string? secondary_color { get; set; }
        public string? description { get; set; }

    }
}
