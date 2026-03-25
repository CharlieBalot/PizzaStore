using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaData.Models
{
    public class Orders
    {
        [Key]
        [Required]
        public int order_id { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }        
    }
}
