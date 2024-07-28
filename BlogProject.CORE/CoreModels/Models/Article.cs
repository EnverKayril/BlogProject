using BlogProject.CORE.CoreModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.CORE.CoreModels.Models
{
    public class Article : BaseEntity, IBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        // Referans
        public string CategoryId { get; set; }
        public string AppUserId { get; set; }

        // Navigation
        public virtual Category Category { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
