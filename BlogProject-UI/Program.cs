using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.REPO.Utilities.Logging;
using BlogProject.REPO.Utilities.UnitOfWork;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.Mappers;
using BlogProject.SERVICE.Services;
using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities;
using BlogProject.SERVICE.Utilities.ILogging;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogProject_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Veri Tabaný Baðlantýsý
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            // Identity Servisleri
            builder.Services.AddIdentity<AppUser, Role>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Logger Servisi
            builder.Services.AddScoped<ILogging, Logging>();

            // Repositories ve Servisler
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IAppUserService, AppUserService>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddScoped<UserManager<AppUser>>();
            builder.Services.AddTransient<ImageHelper>();

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(Mapping));

            // Session ve Razor Runtime Compilation
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddSession();

            // Cookie Ayarlarý (Remember Me ve Oturum Süresi)
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Authorization/Login");
                options.LogoutPath = new PathString("/Admin/Authorization/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "BlogProject",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                options.SlidingExpiration = true; // Kullanýcý aktifse cookie süresini uzat
                options.ExpireTimeSpan = TimeSpan.FromDays(7); // Oturum 7 gün hatýrlanacak
                options.AccessDeniedPath = new PathString("/Admin/Authorization/AccessDenied");
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "Admin",
                areaName: "Admin",
                pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
