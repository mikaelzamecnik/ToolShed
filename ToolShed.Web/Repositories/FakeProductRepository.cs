using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.Models;

namespace ToolShed.Web.Repositories
{
    public class FakeProductRepository:IProductRepository
    {
        public IEnumerable<Product> Products => new List<Product>
        {
            new Product {Name = "AEG KS55-2", Price = 1299, Description = "Lorem ipsum doler sit amet"},
            new Product {Name = "Bosch GKS 165", Price = 1275, Description = "Lorem ipsum doler sit amet"},
            new Product {Name = "Dewalt DEW550", Price = 1190, Description = "Lorem ipsum doler sit amet"},
            new Product {Name = "Festool HK 55 EQB-Plus", Price = 3695, Description = "Lorem ipsum doler sit amet"}




        };


    }
}
