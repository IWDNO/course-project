using ComputerStore.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ComputerStoreDBContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(nameof(ComputerStoreDBContext)));
                });

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
