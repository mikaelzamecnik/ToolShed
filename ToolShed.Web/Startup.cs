using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }


            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(

                    name: "default",
                    template: "{controller=Product}/{action=List}/{id=}");


            });

            Seed.FillIfEmpty(ctx);

           
        }
    }
}
