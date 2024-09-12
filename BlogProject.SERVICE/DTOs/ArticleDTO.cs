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
    public class ArticleDTO
    {
        [DisplayName("Makale Id")]
        public string Id { get; set; }
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        public string Title { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string Content { get; set; }
        [DisplayName("Blog Görseli")]
        public string? Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string AppUserId { get; set; }
        public string CategoryId { get; set; }
        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public EntityStatus Status { get; set; }
    }
}
