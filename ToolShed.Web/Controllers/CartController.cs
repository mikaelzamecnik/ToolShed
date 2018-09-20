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
        private const string customerCartKey = "customer_cart";

        public CartController(IProductRepository repository)
        {
            repo = repository;
        }

        public IActionResult Index(string returnUrl)
        {
            var vm = new CartViewModel
            {
                Cart = GetCustomerCart(),
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var p = repo.Products.FirstOrDefault(x => x.Id.Equals(id));
            if(p != null)
            {
                var cart = GetCustomerCart();
                cart.AddProduct(p, 1);
                SaveCustomerCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        private void SaveCustomerCart(Cart cart)
        {
            HttpContext.Session.SetJson(customerCartKey, cart);
        }

        private Cart GetCustomerCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>(customerCartKey) ?? new Cart();
            return cart;
        }
    }
}