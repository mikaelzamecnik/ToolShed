﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.Models;

namespace ToolShed.Web.Repositories
{
    public interface IOrderRepository
    {

        IEnumerable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
