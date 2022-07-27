using System.ComponentModel.DataAnnotations;

namespace TechBaraholka.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "Пользователь")]
        User = 0,
        [Display(Name = "Админ")]
        Admin = 1,
    }
}
