using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaData.Models
{
    public class Pizzas
    {
        [Key]
        [Required]
        [MaxLength(50)]
        public string pizza_id { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string pizza_type_id { get; set; } = string.Empty;

        [MaxLength(3)]
        public string size { get; set; } = string.Empty;

        [Required]
        [Precision(10, 2)]
        public decimal price { get; set; }
    }
}
