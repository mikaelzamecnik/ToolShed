using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.DataAccess;
using ToolShed.Web.Models;

namespace ToolShed.Web.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private ApplicationDbContext ctx;

        public ProductRepository(ApplicationDbContext context)
        {
            ctx = context;
        }

        public IEnumerable<Product> Products => ctx.Products;
        public IEnumerable<Category> Categories => ctx.Categories;

        public void SaveProduct(Product p)
        {
            if (p.Id == 0)
            {
                ctx.Products.Add(p);
            }
            else
            {
                var ctxProduct = ctx.Products.FirstOrDefault(x => x.Id.Equals(p.Id));
                if (ctxProduct != null)
                {
                    var ctxCategory = ctx.Categories.FirstOrDefault(x => x.Id.Equals(p.CategoryId));
                    if (ctxCategory != null)
                    {
                        ctxProduct.Name = p.Name;
                        ctxProduct.Description = p.Description;
                        ctxProduct.Price = p.Price;
                        ctxProduct.Category = ctxCategory;
                    }
                }
            }
            ctx.SaveChanges();
        }
        public Product DeleteProduct(int productId)
        {
            var ctxProduct = ctx.Products.FirstOrDefault(x => x.Id.Equals(productId));
            if(ctxProduct != null)
            {
                ctx.Products.Remove(ctxProduct);
                ctx.SaveChanges();
            }
            return ctxProduct;
        }

    }
}
