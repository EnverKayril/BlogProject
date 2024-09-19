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
    }
}
