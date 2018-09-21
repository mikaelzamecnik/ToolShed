using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Web.Repositories;
using ToolShed.Web.ViewModels;

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

        public IActionResult List(string selectedCategory, int page = 1)
        {
            int categoryId = 0;
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                var cat = repo.Categories.FirstOrDefault(c => c.Name.Equals(selectedCategory));
                if (cat != null) categoryId = cat.Id;
            }

            //var ps = repo.Products.ToList();

            var toSkip = (page - 1) * PageLimit;
            var products = repo.Products
                //.Where(p=> selectedCategory == null || p.Category.Name.Equals(selectedCategory, StringComparison.InvariantCultureIgnoreCase ))
                .Where(p => categoryId == 0 || p.CategoryId.Equals(categoryId))
                .OrderBy(x => x.Id)
                .Skip(toSkip)
                .Take(PageLimit);

            var paging = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageLimit,
                TotalItems = selectedCategory == null ? repo.Products.Count() : repo.Products.Where(p => p.CategoryId == categoryId).Count()
            };

            var vm = new ProductListViewModel
            {
                Products = products,
                Pager = paging,
                SelectedCategory = selectedCategory
            };

            return View("~/Views/Product/List.cshtml", vm);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}