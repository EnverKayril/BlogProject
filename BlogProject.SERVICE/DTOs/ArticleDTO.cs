using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class ArticleDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public string Content { get; set; }
        [Required(ErrorMessage = "{0} alanı boş geçilmemelidir.")]
        public string Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public string AppUserId { get; set; }
        public string CategoryId { get; set; }
    }
}
