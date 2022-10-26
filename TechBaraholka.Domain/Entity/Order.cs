using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBaraholka.Domain.Entity
{
    public class Order
    {
        public int Id { get; set; }
        public int Sum { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public DateTime OrderDate { get; set; }
    }
}
