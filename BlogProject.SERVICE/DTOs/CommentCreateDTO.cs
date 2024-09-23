using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogProject.CORE.CoreModels.Enums;

namespace BlogProject.SERVICE.DTOs
{
    public class CommentCreateDTO
    {
        public string? Id { get; set; }
        public string? AppUserId { get; set; }
        public string ArticleId { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string Content { get; set; }
        [DisplayName("Onay")]
        public bool Approved { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [DisplayName("Durum")]
        public EntityStatus Status { get; set; }

    }
}
