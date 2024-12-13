using ComputerStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ComputerStore.Models;

namespace ComputerStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");
            var configuration = builder.Configuration;

            
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                });

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.Password.RequireDigit = false; // Отключить требование цифр
                options.Password.RequireLowercase = false; // Отключить требование строчных букв
                options.Password.RequireUppercase = false; // Отключить требование заглавных букв
                options.Password.RequireNonAlphanumeric = false; // Отключить требование неалфавитных символов
                options.Password.RequiredLength = 3; // Минимальная длина пароля
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
