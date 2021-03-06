using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Database;
using Shop.Database.Cart;
using Shop.Domain.Cart;
using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using Shop.UI.Infrastructure;
using Shop.UI.ValidationContext;
using Stripe;
using System;

namespace Shop.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["DefaultConnection"]));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
           {
               options.LoginPath = "/Accounts/Login";
           });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                options.AddPolicy("Manager", policy => policy.RequireAssertion(
                    context => context.User.HasClaim("Role", "Manager") || context.User.HasClaim("Role", "Admin")));
                options.AddPolicy("User", policy => policy.RequireAssertion(
                    context => context.User.HasClaim("Role", "Manager") || context.User.HasClaim("Role", "Admin") || context.User.HasClaim("Role", "User")));
            });

            services.AddControllersWithViews();

            services
                .AddMvc()
                .AddRazorPagesOptions(options =>
               {
                   options.Conventions.AuthorizeFolder("/Admin", "Manager");
                   options.Conventions.AuthorizePage("/Admin/ConfigureUsers", "Admin");
               })
                .AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSession(options =>
           {
               options.Cookie.Name = "Cart";
               options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
           });


            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            services.AddApplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();


            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
