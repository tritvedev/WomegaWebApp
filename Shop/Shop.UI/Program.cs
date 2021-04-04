using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                // All using block does is when we take services from a container, it disposes them in memory safely
                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    context.Database.EnsureCreated();

                    if (!context.Users.Any()) // if there are not many users
                    {
                        var adminUser = new IdentityUser()
                        {
                            UserName = "admin"
                        };

                        var managerUser = new IdentityUser()
                        {
                            UserName = "manager"
                        };

                        userManager.CreateAsync(adminUser, "admin").GetAwaiter().GetResult();       // creates a specific user in the backend with no password
                        userManager.CreateAsync(managerUser, "manager").GetAwaiter().GetResult();       // creates a specific user in the backend with no password

                        var adminClaim = new Claim("Role", "Admin");
                        var managerClaim = new Claim("Role", "Manager");

                        userManager.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                        userManager.AddClaimAsync(managerUser, adminClaim).GetAwaiter().GetResult();

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
