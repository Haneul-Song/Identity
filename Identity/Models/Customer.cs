using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class Customer /*: IdentityUser*/
    {

        // Custom properties specific to your application
        [Key]
        public int? customer_ID { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public DateTime? birth_date { get; set; }
        public string? country_of_residence { get; set; }
        public string? gender { get; set; }
        public decimal? age { get; set; }

        // Navigation properties for domain-specific relationships
        // Assuming Order and Recommendation are other domain models in your application
        //public virtual ICollection<Order>? Orders { get; set; }
        //public virtual ICollection<CustomerRecommendation>? Recommendations { get; set; }
        
    }
}
