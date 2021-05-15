using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shop.Database;
using Shop.Domain.Models;
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
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                    context.Database.EnsureCreated();

                    if (!context.Users.Any(x => x.UserName == "admin"))
                    {
                        var adminUser = new User()
                        {
                            UserName = "admin"
                        };

                        userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                        var adminClaim = new Claim("Role", "Admin");

                        userManager.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                    }
                    if (!context.Users.Any(x => x.UserName == "manager"))
                    {
                        var managerUser = new User()
                        {
                            UserName = "manager"
                        };

                        userManager.CreateAsync(managerUser, "password").GetAwaiter().GetResult();

                        var managerClaim = new Claim("Role", "Manager");

                        userManager.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();
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
