using BlogProject.CORE.CoreModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.CORE.CoreModels.Models
{
    public class Comment : BaseEntity, IBaseEntity
    {
        public string Content { get; set; }
        public string AppUserId { get; set; }
        public string ArticleId { get; set; }
        public bool Approved { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Article Article { get; set; }
    }
}
