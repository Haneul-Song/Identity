using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class ProductRecommendation
    {

        [Key]
        public int product_id { get; set; }
        public string? product_name { get; set; }
        public string? recommendation_1 { get; set; }
        public string? recommendation_2 { get; set; }
        public string? recommendation_3 { get; set; }
        public string? recommendation_4 { get; set; }
        public string? recommendation_5 { get; set; }

    }
}
