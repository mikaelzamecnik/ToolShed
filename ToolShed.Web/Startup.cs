using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToolShed.Web.DataAccess;
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

            // Change the format of the routing Urls
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));
            services.AddTransient<IProductRepository, ProductRepository>();

            // To add to session
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".ToolShed.Cart.Session";
                options.IdleTimeout = TimeSpan.FromDays(2);

            });



            // Make Core App work with Mvc
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            //app.UseAuthentication();
            app.UseStaticFiles();
            //Use session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(

                   name: null,
                   template: "produkter/{selectedCategory}/sida/{page:int}",
                   defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(

                   name: null,
                   template: "produkter/sida/{page:int}",
                   defaults: new { Controller = "Product", action = "List", page =1 });

                routes.MapRoute(

                   name: null,
                   template: "produkter/{selectedCategory}",
                   defaults: new { Controller = "Product", action = "List", page = 1 });

                routes.MapRoute(

                   name: null,
                   template: "",
                   defaults: new { Controller = "Product", action = "List", page = 1 });

                routes.MapRoute(

                    name: null,
                    template: "{controller}/{action}/{id?}");



            });



            Seed.FillIfEmpty(ctx);


        }
    }
}
