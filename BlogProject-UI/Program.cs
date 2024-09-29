using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.REPO.Utilities.UnitOfWork;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.Mappers;
using BlogProject.SERVICE.Services;
using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace BlogProject_UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("Uygulama baþlatýlýyor...");

                var builder = WebApplication.CreateBuilder(args);

                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString)
                           .UseLazyLoadingProxies());

                // Identity ayarlarý
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
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.AccessDeniedPath = new PathString("/Error/403");
                });

                var app = builder.Build();

                // Ortam kontrollü hata sayfalarý
                if (app.Environment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Error/500");
                    app.UseStatusCodePagesWithReExecute("/Error/{0}");
                }

                // Middleware sýralamasý
                app.UseSession();
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                // Admin Area Yönlendirme
                app.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                // Varsayýlan Yönlendirme
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Uygulama baþlatýlamadý.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
