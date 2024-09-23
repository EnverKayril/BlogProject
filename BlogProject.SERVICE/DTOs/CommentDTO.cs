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
    public class CommentDTO
    {
        [DisplayName("Yorum Id")]
        public string? Id { get; set; }
        [DisplayName("Makale Id")]
        public string? ArticleId { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string Content { get; set; }
        [DisplayName("Kullanıcı Id")]
        public string AppUserId { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [DisplayName("Değişiklik Tarihi")]
        public DateTime? UpdateDate { get; set; }
        [DisplayName("Onay")]
        public bool Approved { get; set; }
        [DisplayName("Durum")]
        public EntityStatus Status { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
    }
}
