using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Core.Services.implementations;
using Store.Data.Context;
using Store.Core.Services.Interfaces;

namespace Store
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region DataBase Context

            services.AddDbContext<StoreContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("StoreConnection"));
                }
            );
            #endregion
            #region Authentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);

            });

            #endregion
            #region IOC

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISecurityService, SecurityService>();

            #endregion

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPanel", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "AdminPanel"));
                options.AddPolicy("ManageUser", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "ManageUser"));
                options.AddPolicy("ManageRoles", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "ManageRoles"));
                options.AddPolicy("ManageProduct", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "ManageProduct"));

            });
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "Admin",
                        pattern: "{Area}/{controller}/{action}/{id?}");
                
                endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });

            });
        }
    }
}
