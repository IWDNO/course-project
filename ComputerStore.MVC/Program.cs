using ComputerStore.Application;
using ComputerStore.Application.Implementations;
using ComputerStore.Application.Interfaces;
using ComputerStore.DataAccess;
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

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication().AddCookie();

            builder.Services.AddDbContext<ComputerStoreDBContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(nameof(ComputerStoreDBContext)));
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
