using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Models
{
    public class ProductCategory
    {
        [Key]
        [ForeignKey("Product")]
        public int product_ID { get; set; }

        [ForeignKey("Category")]
        public string category_ID { get; set; }

        // Navigation properties
        //public Product Products { get; set; }
        //public Category Categories { get; set; }
    }
}
