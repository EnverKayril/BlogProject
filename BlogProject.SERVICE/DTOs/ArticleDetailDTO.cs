using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class ArticleDetailDTO
    {
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string UserProfileImage { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public CommentCreateDTO NewComment { get; set; }
    }
}
