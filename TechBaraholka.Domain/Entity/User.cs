using System.Collections.Generic;
using TechBaraholka.Domain.Enum;

namespace TechBaraholka.Domain.Entity

{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string AvatarPath { get; set; }
        public int Balance { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual List<Product> Products { get; set; } = new List<Product>();
        //public virtual List<Order> Oreders { get; set; } = new List<Order>();
    }
}
