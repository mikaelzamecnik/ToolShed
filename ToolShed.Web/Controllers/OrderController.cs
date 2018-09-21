using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolShed.Web.Models;
using ToolShed.Web.Repositories;

namespace ToolShed.Web.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository orderRepo;
        private Cart cart;

        public OrderController(IOrderRepository orderRepository, Cart cartService)
        {
            orderRepo = orderRepository;
            cart = cartService;

        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.CartRows.Any())
            {
                ModelState.AddModelError("", "Din kundvagn är tom");
            }
            if (ModelState.IsValid)
            {
                order.Rows = cart.CartRows.ToArray();
                orderRepo.SaveOrder(order);
                return RedirectToAction(nameof(OrderCompleted));

            }
            else
            {
                return View(order);
            }
        }

        public IActionResult OrderCompleted()
        {
            cart.EmptyCart();
            return View();
        }
    }
}