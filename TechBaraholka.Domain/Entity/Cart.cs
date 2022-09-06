using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBaraholka.Domain.Entity
{
    public class Cart
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int Sum { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }

}
