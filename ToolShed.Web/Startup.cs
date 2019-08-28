using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToolShed.Web.DataAccess;
using ToolShed.Web.Models;
using ToolShed.Web.Repositories;

namespace ToolShed.Web
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration conf)
        {
            _configuration = conf;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var conn = _configuration.GetConnectionString("ToolShedProducts");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));

            services.AddTransient<IProductRepository, ProductRepository>();


            //services.AddTransient<IPasswordValidator<IdentityUser>, CorporatePasswordValidator>();

            //services.AddTransient<IUserValidator<IdentityUser>, CorporateUserValidator>();

            //services.AddAutoMapper();


            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<ApplicationDbContext>().
                AddDefaultTokenProviders();

            services.AddTransient<IIdentitySeeder, IdentitySeeder>();



            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
            });




            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddScoped(f => CartSession.GetCart(f));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".ToolShed.Cart.Session";
                options.IdleTimeout = TimeSpan.FromDays(2);
            });

            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx, IIdentitySeeder identitySeeder)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/error.html");
            }

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                // -> /produkter/skruvdragare/sida/1
                routes.MapRoute(
                    name: null,
                    template: "produkter/{selectedCategory}/sida/{page:int}",
                    defaults: new { controller = "Product", action = "List" }
                );

                // -> /produkter/sida/2
                routes.MapRoute(
                    name: null,
                    template: "produkter/sida/{page:int}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );

                // -> /produkter/skruvdragare
                routes.MapRoute(
                    name: null,
                    template: "produkter/{selectedCategory}",
                    defaults: new { controller = "Product", action = "List", page = 1 }
                );
                // -> /
                routes.MapRoute(
                    name: null,
                    template: "",
                    //defaults: new { controller = "Product", action = "List", page = 1 }
                    defaults: new { controller = "Home", action = "Index", page = 1 }
                );

                routes.MapRoute(
                    name: null,
                    template: "{controller=Product}/{action=List}/{id?}"
                );
            });

            identitySeeder.CreateAdminAccountIfEmpty();

            Seed.FillIfEmpty(ctx);
        }
    }
}
