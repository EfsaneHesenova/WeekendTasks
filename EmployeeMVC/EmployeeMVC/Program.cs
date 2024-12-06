using EmployeeMVC.DAL;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.SignIn.RequireConfirmedEmail = false;

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>(); 

            builder.Services.AddDbContext<AppDbContext>(
            options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"))
            );
            var app = builder.Build();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );
            app.MapControllerRoute(
             name: "default",
             pattern: "{controller=Home}/{action=Index}/{id?}"
             );

            app.Run();
        }
    }
}
