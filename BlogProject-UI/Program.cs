using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories;
using BlogProject.REPO.Utilities.Logging;
using BlogProject.REPO.Utilities.UnitOfWork;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.Mappers;
using BlogProject.SERVICE.Utilities.ILogging;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Identity;

namespace BlogProject_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Contexts
            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 3;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Logger
            builder.Services.AddScoped<ILogging, Logging>();

            //Repository
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUnitOfWorkService,  UnitOfWorkService>();

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(Mapping));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}