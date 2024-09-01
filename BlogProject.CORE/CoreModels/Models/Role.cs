using BlogProject.CORE.CoreModels.BaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.CORE.CoreModels.Models
{
    public class Role : IdentityRole<string> , IBaseEntity
    {
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
