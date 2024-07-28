using BlogProject.CORE.CoreModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category {Name = "Yazılım" },
                new Category {Name = "Sistem" },
                new Category {Name = "Web Grafik" },
                new Category {Name = "Güvenlik" },
                new Category {Name = "Bulut Sistemler" }
                );

            builder.HasMany(c => c.Articles).WithOne(a => a.Category).HasForeignKey(a => a.CategoryId);
        }
    }
}
