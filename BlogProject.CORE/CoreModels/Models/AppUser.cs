using BlogProject.CORE.CoreModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.CORE.CoreModels.Models
{
    public class AppUser : IdentityUser<string> , IBaseEntity
    {
        public string? Photo { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
