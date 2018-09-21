using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.DataAccess;
using ToolShed.Web.Models;

namespace ToolShed.Web.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private ApplicationDbContext ctx;
        public OrderRepository(ApplicationDbContext context)
        {
            ctx = context;
        }
        public IEnumerable<Order> Orders => ctx.Orders.Include(o => o.Rows).ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            ctx.AttachRange(order.Rows.Select(x => x.Product));
            if (order.Id == 0)
            {
                ctx.Orders.Add(order);
            }
            ctx.SaveChanges();


        }


    }
}
