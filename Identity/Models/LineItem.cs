using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class LineItem
    {
        [Key]
        public int lineItem_ID { get; set; }
        [ForeignKey("Order")]
        public int transaction_ID { get; set; }
        public int product_ID { get; set; }
        public int? qty { get; set; }
        public int? rating { get; set; }

    }
}
