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
    public class ArticleCreateDTO
    {
        public string? CategoryId { get; set; }
        public string? AppUserId { get; set; }
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(100,ErrorMessage = "{0} {1} karakterden fazla olmamalıdır.")]
        public string Title { get; set; }
        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string Content { get; set; }
        public string? Thumbnail { get; set; }
        [DisplayName("Blog Görseli")]
        public IFormFile? PhotoFile { get; set; }
    }
}
