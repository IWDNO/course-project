using ComputerStore.Application;
using ComputerStore.Application.Implementations;
using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
using ComputerStore.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace ComputerStore.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddIdentity<UserEntity, RoleEntity>()
                .AddEntityFrameworkStores<ComputerStoreDBContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddDbContext<ComputerStoreDBContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(nameof(ComputerStoreDBContext)));
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Настройки пароля
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Настройки блокировки
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Настройки пользователя
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });



            builder.Services.AddScoped<ICategoriesRepository, EFCategoriesRepository>();
            builder.Services.AddScoped<IProductsRepository, EFProductsRepository>();
            builder.Services.AddScoped<ISuppliersRepository, EFSuppliersRepository>();
            builder.Services.AddScoped<IRolesRepository, EFRolesRepository>();
            builder.Services.AddScoped<ISalesRepository, EFSalesRepository>();
            builder.Services.AddScoped<ISaleItemsRepository, EFSaleItemsRepository>();
            builder.Services.AddScoped<IUsersRepository, EFUsersRepository>();

            builder.Services.AddScoped<DataManager>();

            builder.Services.AddControllersWithViews();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // Добавление аутентификации
            app.UseAuthorization();  // Добавление авторизации

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
