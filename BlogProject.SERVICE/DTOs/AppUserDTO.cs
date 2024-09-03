using BlogProject.CORE.CoreModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class AppUserDTO
    {
        [DisplayName("Kullanıcı Id")]
        public string Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [DisplayName("Rol Id")]
        public string? RoleId { get; set; }
        [DisplayName("Email Onayı")]
        public bool? EmailConfirmed { get; set; }
        [DisplayName("Resim")]
        public string? Photo { get; set; }
    }
}
