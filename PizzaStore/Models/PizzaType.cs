using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PizzaData.Models
{
    public class PizzaType
    {
        [Key]
        [Required]
        [MaxLength(50)]
        public string pizza_type_id { get; set; } = string.Empty;

        [MaxLength(100)]
        public string name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string category { get; set; } = string.Empty;

        [MaxLength(150)]
        public string ingredients { get; set; } = string.Empty;
    }
}