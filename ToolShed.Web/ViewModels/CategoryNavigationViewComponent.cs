using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.Repositories;

namespace ToolShed.Web.ViewModels
{
    public class CategoryNavigationViewComponent: ViewComponent
    {

        private IProductRepository repo;

        public CategoryNavigationViewComponent(IProductRepository repository)
        {
            repo = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["SelectedCategory"];
            var categories = repo.Categories.Select(x => x.Name).OrderBy(c => c);
            return View(categories);
        }


    }
}
