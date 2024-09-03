using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class AppUserCreateDTO
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        public string UserName { get; set; }
        [DisplayName("Parola")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("E-mail Adresi")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(13, ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        [MinLength(11, ErrorMessage = "{0} {1} karakterden az olmamalıdır.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DisplayName("Resim")]
        [DataType(DataType.Upload)]
        public IFormFile? PhotoFile { get; set; }
        public string? Photo { get; set; }
    }
}
