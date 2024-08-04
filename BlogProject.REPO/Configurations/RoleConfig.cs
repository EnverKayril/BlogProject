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
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { Id = 1,Name = "Admin", Description = "Admin rolü tüm haklara sahiptir"}

                );

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).HasMaxLength(30).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(250).IsRequired();
            builder.HasMany(r => r.AppUsers).WithOne(u => u.Role).HasForeignKey(u => u.RoleId);
        }
    }
}
