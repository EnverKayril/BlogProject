using BlogProject.CORE.CoreModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Configurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(
                new AppUser
                {
                    FirstName = "Enver",
                    NormalizedFirstName = "ENVER",
                    LastName = "Kayrıl",
                    NormalizedLastName = "KAYRIL",
                    UserName = "enverkayril",
                    NormalizedUserName = "ENVERKAYRIL",
                    PhoneNumber = "999999999",
                    Email = "enverkayril@gmail.com",
                    NormalizedEmail = "ENVERKAYRIL@GMAIL.COM",
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Kayril123"),
                    RoleId = 1,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                    PhoneNumberConfirmed = true,
                    EmailConfirmed = true,
                }
                );

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.FirstName).HasMaxLength(30).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(30).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasMany(u => u.Articles).WithOne(a => a.AppUser).HasForeignKey(u => u.AppUserId);
        }
    }
}
