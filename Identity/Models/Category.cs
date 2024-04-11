using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class Category
    {
        [Key]
        public int? category_ID { get; set; }
        public string? category_name { get; set; }
    }
}
