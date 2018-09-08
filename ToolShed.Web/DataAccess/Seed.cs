using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.Models;


namespace ToolShed.Web.DataAccess
{
    public static class Seed
    {

        internal static void FillIfEmpty(ApplicationDbContext ctx)
        {
            if (!ctx.Categories.Any())
            {
                ctx.Categories.Add(new Category { Name = "Cirkelsågar" });
                ctx.Categories.Add(new Category { Name = "Skruvdragare" });
                ctx.SaveChanges();

            }
            

            if (!ctx.Products.Any())
            {
                var products = new List<Product>{
                    new Product {Name = "AEG KS55-2", Price = 1299, Description = "Lorem ipsum doler sit amet", CategoryId = 1},
                    new Product {Name = "Bosch GKS 165", Price = 1275, Description = "Lorem ipsum doler sit amet",CategoryId = 1},
                    new Product {Name = "Dewalt DEW550", Price = 1190, Description = "Lorem ipsum doler sit amet",CategoryId = 1},
                    new Product {Name = "Festool HK 55 EQB-Plus", Price = 3695, Description = "Lorem ipsum doler sit amet",CategoryId = 1},

                    new Product {Name = "Bosch GDR 10,8v", Price = 2365, Description = "Lorem ipsum doler sit amet", CategoryId = 2},
                    new Product {Name = "Bosch PSR 10,8 LI-2", Price = 1349, Description = "Lorem ipsum doler sit amet", CategoryId = 2},
                    new Product {Name = "Dewalt DCF83", Price = 3599, Description = "Lorem ipsum doler sit amet", CategoryId = 2},
                    new Product {Name = "Fein ASCM 12 C", Price = 3270, Description = "Lorem ipsum doler sit amet", CategoryId = 2}
                };

                ctx.Products.AddRange(products);
                ctx.SaveChanges();

            }
            
        }


    }
}
