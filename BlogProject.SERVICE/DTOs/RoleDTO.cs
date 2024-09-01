using BlogProject.CORE.CoreModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class RoleDTO
    {
        [DisplayName("Rol Id")]
        public string Id { get; set; }
        [DisplayName("Rol Adı")]
        [Required(ErrorMessage = "Rol Adı boş geçilmemelidir.")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "Açıklama boş geçilmemelidir.")]
        [MaxLength(250)]
        public string Description { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [DisplayName("Düzenlenme Tarihi")]
        public DateTime? UpdateDate { get; set; }
        [DisplayName("Durum")]
        public EntityStatus Status { get; set; }
    }
}
