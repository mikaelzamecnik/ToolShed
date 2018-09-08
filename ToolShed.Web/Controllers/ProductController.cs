using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Web.Repositories;

namespace ToolShed.Web.Controllers
{
    public class ProductController : Controller
    {

        private IProductRepository repo;
        public int PageLimit = 4;

        public ProductController(IProductRepository productRepository)
        {
            repo = productRepository;
        }

        public IActionResult List(int page = 1)
        {
            var toSkip = (page - 1) * PageLimit;
            var products = repo.Products.OrderBy(x => x.Id).Skip(toSkip).Take(PageLimit);

            return View(products);
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}