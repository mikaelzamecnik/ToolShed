using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Web.Infrastructure;
using ToolShed.Web.Models;
using ToolShed.Web.Repositories;
using ToolShed.Web.ViewModels;

namespace ToolShed.Web.Controllers
{
    public class CatalogController : Controller
    {

        private IProductRepository repo;

        public CatalogController(IProductRepository repository)
        {
            repo = repository;
        }
        public IActionResult Index()
        {
            return View(repo.Products);
        }

        public IActionResult Edit(int productId)
        {
            var product = repo.Products.FirstOrDefault(x => x.Id.Equals(productId));
            var vm = new EditProductViewModel
            {
             Categories=repo.Categories.ToSelectList(product),
             Product = product
            };
            return View(vm);

        }

        private bool SetSelected(Product product, Category x)
        {
            return (product.CategoryId == x.Id);
        }
        [HttpPost]
        public IActionResult Edit(EditProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                repo.SaveProduct(vm.Product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Heading = "Skapa ny produkt";
            var vm = new EditProductViewModel
            {
                Categories = repo.Categories.ToSelectList()
            };

            return View("Edit",vm);
        }

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var deleted = repo.DeleteProduct(productId);
            if(deleted != null)
            {
                //product was found and deleted
            }
            else
            {
                //Todo
                //product was not found show error
            }
            return RedirectToAction(nameof(Index));
        }
    }
}