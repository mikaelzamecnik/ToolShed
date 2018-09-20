using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.Web.Models
{
    public class CartRow
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
