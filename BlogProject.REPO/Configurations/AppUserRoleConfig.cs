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
    public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(
                new AppUserRole
                {
                    RoleId = "1",
                    UserId = "1",
                },
                new AppUserRole
                {
                    RoleId = "2",
                    UserId = "2",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "3",
                },
                new AppUserRole
                {
                    RoleId = "4",
                    UserId = "4",
                },
                new AppUserRole
                {
                    RoleId = "4",
                    UserId = "5",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "6",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "7",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "8",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "9",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "10",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "11",
                },
                new AppUserRole
                {
                    RoleId = "3",
                    UserId = "12",
                });
        }
    }
}
