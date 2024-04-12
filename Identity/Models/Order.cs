using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class Order
    {
        [Key]
        public int transaction_ID { get; set; }
        public ICollection<CartLine> Lines { get; set; }
            = new List<CartLine>();

        [ForeignKey("customer_ID")]
        public string? customer_ID { get; set; }
        public DateTime? date { get; set; }
        public string? day_of_week { get; set; }
        public int? time { get; set; }
        public string? entry_mode { get; set; }
        public decimal amount { get; set; }
        public string? type_of_transaction { get; set; }
        public string? country_of_transaction { get; set; }
        public string? shipping_address { get; set; }
        public string? bank { get; set; }
        public string? type_of_card { get; set; }
        public int? fraud { get; set; }
        //public Customer? Customer { get; set; }
        //public ICollection<LineItem>? LineItems { get; set; }
    }
}
