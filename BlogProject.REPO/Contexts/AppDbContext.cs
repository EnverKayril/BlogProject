﻿using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogProject.REPO.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, Role, string, AppUserClaim, AppUserRole, AppUserLogin, RoleClaim, AppUserToken>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new ArticleConfig());
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new AppUserClaimConfig());
            builder.ApplyConfiguration(new AppUserLoginConfig());
            builder.ApplyConfiguration(new AppUserRoleConfig());
            builder.ApplyConfiguration(new AppUserTokenConfig());
            builder.ApplyConfiguration(new RoleClaimConfig());
            builder.ApplyConfiguration(new RoleConfig());
        }
    }
}
