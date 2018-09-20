using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.Infrastructure;

namespace ToolShed.Web.Models
{
    public class CartSession : Cart
    {
       [JsonIgnore] 

       public ISession Session { get; private set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            CartSession cart = session.GetJson<CartSession>(customerCartKey) ?? new CartSession();
            cart.Session = session;
            return cart;

        }

        public override void AddProduct(Product p , int quantity)
        {
            base.AddProduct(p, quantity);
            CommitToSession();
        }

        public override void RemoveCartRow(Product p)
        {
            base.RemoveCartRow(p);
            CommitToSession();
        }

        public override void EmptyCart()
        {
            base.EmptyCart();
            Session.Remove(customerCartKey);
        }

        private void CommitToSession()
        {
            Session.SetJson(customerCartKey, this);

        }


    }
}
