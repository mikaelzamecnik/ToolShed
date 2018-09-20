using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Web.Models;
using ToolShed.Web.Repositories;
using Newtonsoft.Json;
using ToolShed.Web.Infrastructure;

namespace ToolShed.Web.Controllers
{
    public class CartController : Controller
    {

        private IProductRepository repo;
        private Cart cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            repo = repository;
            cart = cartService;
        }

        public IActionResult Index(string returnUrl)
        {
            var vm = new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var p = repo.Products.FirstOrDefault(x => x.Id.Equals(id));
            if(p != null)
            {
                
                cart.AddProduct(p, 1);
                
            }

            return RedirectToAction(nameof(Index), new {returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {

            var p = repo.Products.FirstOrDefault(x => x.Id.Equals(id));
            if(p != null)
            {
                cart.RemoveCartRow(p);
            }
            return RedirectToAction(nameof(Index), new { returnUrl });

        }
    }
}