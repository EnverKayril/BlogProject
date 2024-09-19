using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
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
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(50);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(50);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var adminUser = new AppUser
            {
                Id = "1",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "Admin.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            adminUser.PasswordHash = AppUserHelper.CreatePasswordHash(adminUser, "adminuser");

            var editorUser = new AppUser
            {
                Id = "2",
                UserName = "Editor",
                NormalizedUserName = "EDITOR",
                Email = "editor@mail.com",
                NormalizedEmail = "EDITOR@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "Admin.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            editorUser.PasswordHash = AppUserHelper.CreatePasswordHash(editorUser, "editoruser");

            var verifieduser1 = new AppUser
            {
                Id = "3",
                UserName = "Verifieduser1",
                NormalizedUserName = "VERIFIEDUSER1",
                Email = "verifieduser1@mail.com",
                NormalizedEmail = "VERIFIEDUSER1@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage1.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser1.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser1, "verifieduser1");

            var newuser1 = new AppUser
            {
                Id = "4",
                UserName = "Newuser1",
                NormalizedUserName = "NEWUSER1",
                Email = "newuse1r@mail.com",
                NormalizedEmail = "NEWUSER1@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "DefaultUser.jpg",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            newuser1.PasswordHash = AppUserHelper.CreatePasswordHash(newuser1, "newuser1");

            var newuser2 = new AppUser
            {
                Id = "5",
                UserName = "Newuser2",
                NormalizedUserName = "NEWUSER2",
                Email = "newuser2@mail.com",
                NormalizedEmail = "NEWUSER2@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "DefaultUser.jpg",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            newuser2.PasswordHash = AppUserHelper.CreatePasswordHash(newuser2, "newuser2");

            var verifieduser2 = new AppUser
            {
                Id = "6",
                UserName = "Verifieduser2",
                NormalizedUserName = "VERIFIEDUSER2",
                Email = "verifieduser2@mail.com",
                NormalizedEmail = "VERIFIEDUSER2@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage2.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser2.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser2, "verifieduser2");

            var verifieduser3 = new AppUser
            {
                Id = "7",
                UserName = "Verifieduser3",
                NormalizedUserName = "VERIFIEDUSER3",
                Email = "verifieduser3@mail.com",
                NormalizedEmail = "VERIFIEDUSER3@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage3.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser3.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser3, "verifieduser3");

            var verifieduser4 = new AppUser
            {
                Id = "8",
                UserName = "Verifieduser4",
                NormalizedUserName = "VERIFIEDUSER4",
                Email = "verifieduser4@mail.com",
                NormalizedEmail = "VERIFIEDUSER4@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage4.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser4.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser4, "verifieduser4");

            var verifieduser5 = new AppUser
            {
                Id = "9",
                UserName = "Verifieduser5",
                NormalizedUserName = "VERIFIEDUSER5",
                Email = "verifieduser5@mail.com",
                NormalizedEmail = "VERIFIEDUSER5@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage5.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser5.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser5, "verifieduser5");

            var verifieduser6 = new AppUser
            {
                Id = "10",
                UserName = "Verifieduser6",
                NormalizedUserName = "VERIFIEDUSER6",
                Email = "verifieduser6@mail.com",
                NormalizedEmail = "VERIFIEDUSER6@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage6.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser6.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser6, "verifieduser6");

            var verifieduser7 = new AppUser
            {
                Id = "11",
                UserName = "Verifieduser7",
                NormalizedUserName = "VERIFIEDUSER7",
                Email = "verifieduser7@mail.com",
                NormalizedEmail = "VERIFIEDUSER7@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage7.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser7.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser7, "verifieduser7");

            var verifieduser8 = new AppUser
            {
                Id = "12",
                UserName = "Verifieduser8",
                NormalizedUserName = "VERIFIEDUSER8",
                Email = "verifieduser8@mail.com",
                NormalizedEmail = "VERIFIEDUSER8@MAIL.COM",
                PhoneNumber = "1234567890",
                Photo = "UserImage8.jpg",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            verifieduser8.PasswordHash = AppUserHelper.CreatePasswordHash(verifieduser8, "verifieduser8");

            builder.HasData(adminUser, editorUser, verifieduser1, newuser1, newuser2, verifieduser2, verifieduser3, verifieduser4, verifieduser5, verifieduser6, verifieduser7, verifieduser8);
        }
    }
}
