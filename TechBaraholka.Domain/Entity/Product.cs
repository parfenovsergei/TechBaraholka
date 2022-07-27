using System;
using TechBaraholka.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace TechBaraholka.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public User Salesman { get; set; }
        public DateTime DateAdded { get; set; }
        public TypeProduct TypeProduct { get; set; }
    }
}
