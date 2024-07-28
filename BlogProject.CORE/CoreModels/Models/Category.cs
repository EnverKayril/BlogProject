using BlogProject.CORE.CoreModels.BaseModels;
using BlogProject.CORE.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.CORE.CoreModels.Models
{
    public class Category : BaseEntity, IBaseEntity
    {
        public string Name { get; set; }
        public string DetailUrl { get { return NormalizedUrl.TurkishToEnglish(Name); } }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
