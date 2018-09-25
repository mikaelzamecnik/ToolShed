﻿using System;
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


        IConfiguration Configuration;

        public Startup(IConfiguration conf)
        {
            Configuration = conf;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetConnectionString("ToolShedProducts");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IIdentitySeeder, IdentitySeeder>();
            // Change the format of the routing Urls
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddScoped(f => CartSession.GetCart(f));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            services.AddTransient<IProductRepository, ProductRepository>();


            // To add to session
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".ToolShed.Cart.Session";
                options.IdleTimeout = TimeSpan.FromDays(2);

            });


            // Adding OrderRepository
            services.AddTransient<IOrderRepository, OrderRepository>();
            // Make Core App work with Mvc
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx, IIdentitySeeder IdentitySeeder)
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
            //app.UseAuthentication();
            app.UseStaticFiles();
            app.UseAuthentication();
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

            IdentitySeeder.CreateAdminAccountIfEmpty();

            Seed.FillIfEmpty(ctx);


        }
    }
}
