using BlogProject.CORE.CoreModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class CommentWithUserDTO
    {
        [DisplayName("Yorum Id")]
        public string? CommentId { get; set; }
        [DisplayName("Makale Id")]
        public string? ArticleId { get; set; }
        [DisplayName("Makale Başlığı")]
        public string? ArticleTitle { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string CommentContent { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [DisplayName("Değişiklik Tarihi")]
        public DateTime? UpdateDate { get; set; }
        [DisplayName("Onay")]
        public bool Approved { get; set; }
        [DisplayName("Durum")]
        public EntityStatus Status { get; set; }
    }
}
