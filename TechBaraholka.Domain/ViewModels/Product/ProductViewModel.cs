using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechBaraholka.Domain.Enum;

namespace TechBaraholka.Domain.ViewModels.Product
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Укажите название товара")]
        [MinLength(3, ErrorMessage = "Минимальная длина названия товара 3 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина названия товара 50 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите описание товара")]
        [MaxLength(300, ErrorMessage = "Максимальная длина названия товара 300 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите цену товара")]
        [Range(typeof(int),"1","100000",ErrorMessage ="Недопустимая цена")]
        public int Price { get; set; }

        [Required(ErrorMessage = ("Укажите тип товара"))]
        public TypeProduct TypeProduct { get; set; }
    }
}
