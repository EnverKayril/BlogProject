using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }

        public string Token { get; set; }
        [DisplayName("Yeni Şifre Giriniz")]
        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Yeni Şifreyi Tekrar Giriniz")]
        [Required(ErrorMessage = "Şifre tekrarı boş geçilemez.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
