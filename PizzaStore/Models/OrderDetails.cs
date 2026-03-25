using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaData.Models
{
    public class OrderDetails
    {
        [Key]
        [Required]
        public int order_details_id { get; set; }

        [Required]
        public int order_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string pizza_id { get; set; } = string.Empty;

        [Required]
        public int quantity { get; set; }
    }
}
