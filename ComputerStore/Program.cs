using ComputerStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ComputerStore.Models;

namespace ComputerStore
{
    public class Program
    {
        public static async Task Main(string[] args)
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
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1; 
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using(var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Customer", "Seller", "Admin" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }


            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                string adminEmail = "admin@admin.com";
                string adminPassword = "Admin123";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new IdentityUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail
                    };
                    await userManager.CreateAsync(adminUser, adminPassword);
                    await userManager.AddToRolesAsync(adminUser, new[] { "Admin", "Customer", "Seller" });

                    var adminWorker = new WorkerEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin Worker",
                        IdentityUserId = adminUser.Id
                    };
                    context.Workers.Add(adminWorker);

                    var adminCustomer = new CustomerEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = $"Admin Customer",
                        IdentityUserId = adminUser.Id
                    };
                    context.Customers.Add(adminCustomer);
                }

                // Создание пользователей с ролью "Customer"
                for (int i = 1; i <= 5; i++)
                {
                    string customerEmail = $"user{i}@example.com";
                    string customerPassword = "123123";
                    if (await userManager.FindByEmailAsync(customerEmail) == null)
                    {
                        var customerUser = new IdentityUser
                        {
                            Email = customerEmail,
                            UserName = customerEmail
                        };
                        await userManager.CreateAsync(customerUser, customerPassword);
                        await userManager.AddToRoleAsync(customerUser, "Customer");

                        var customer = new CustomerEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = $"Customer {i}",
                            IdentityUserId = customerUser.Id
                        };
                        context.Customers.Add(customer);
                    }
                }

                // Создание пользователей с ролями "Seller" и "Customer"
                for (int i = 1; i <= 3; i++)
                {
                    string sellerEmail = $"seller{i}@example.com";
                    string sellerPassword = "123123";
                    if (await userManager.FindByEmailAsync(sellerEmail) == null)
                    {
                        var sellerUser = new IdentityUser
                        {
                            Email = sellerEmail,
                            UserName = sellerEmail
                        };
                        await userManager.CreateAsync(sellerUser, sellerPassword);
                        await userManager.AddToRolesAsync(sellerUser, new[] { "Seller", "Customer" });

                        // Создание WorkerEntity для продавца
                        var sellerWorker = new WorkerEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = $"Seller {i}",
                            IdentityUserId = sellerUser.Id
                        };
                        context.Workers.Add(sellerWorker);

                        // Создание CustomerEntity для продавца
                        var sellerCustomer = new CustomerEntity
                        {
                            Id = Guid.NewGuid(),
                            Name = $"Seller Customer {i}",
                            IdentityUserId = sellerUser.Id
                        };
                        context.Customers.Add(sellerCustomer);
                    }
                }
            }

            app.Run();
        }
    }
}
