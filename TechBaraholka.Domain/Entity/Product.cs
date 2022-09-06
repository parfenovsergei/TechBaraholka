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
        public int Price { get; set; }
        //public int UserId { get; set; }
        public virtual User User { get; set; }
        //public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
       /* public int OrderId { get; set; }
        public virtual Order? Order { get; set; }*/
        public DateTime DateAdded { get; set; }
        public TypeProduct TypeProduct { get; set; }
        public string PicturePath { get; set; }
    }
}
