using System.ComponentModel.DataAnnotations;

namespace TechBaraholka.Domain.Enum
{
    public enum TypeProduct
    {
        [Display(Name = "Телевизор")]
        TV = 0,
        [Display(Name = "Смартфон")]
        SmartPhone = 1,
        [Display(Name = "Ноутбук")]
        Laptop = 2,
        [Display(Name = "Товар для дома")]
        HomeAppliances = 3,
        [Display(Name = "Аксессуар для компьютера")]
        Accessory = 4,
    }
}
