using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TechBaraholka.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите имя")]
        [MinLength(3, ErrorMessage = "Минимальная длина имени 3 символа")]
        [MaxLength(25, ErrorMessage = "Максимальная длина имени 30 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [MinLength(3, ErrorMessage = "Минимальная длина фамилии 3 символа")]
        [MaxLength(25, ErrorMessage = "Максимальная длина фамилии 30 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Укажите Email")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        //[Remote(action: "CheckEmail", controller: "Account", ErrorMessage = "Email уже используется")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Задайте пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Укажите адрес")]
        public string Address { get; set; }
    }
}
