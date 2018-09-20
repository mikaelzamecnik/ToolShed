using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.Web.Models
{
    public class Cart
    {
        private List<CartRow> _cartRows = new List<CartRow>();

        public virtual void AddProduct(Product p, int quantity)
        {

            var cartRow = _cartRows.Where(x => x.Product.Id.Equals(p.Id)).FirstOrDefault();
            if(cartRow == null)
            {
                _cartRows.Add(new CartRow
                {
                    Product = p,
                    Quantity = quantity
                });
            }
            else
            {
                cartRow.Quantity += quantity;
            }
        }

        public virtual void RemoveCartRow(Product p)
        {
            _cartRows.RemoveAll(x => x.Product.Id.Equals(p.Id));
        }

        public virtual decimal GetCartTotal()
        {
            var total = _cartRows.Sum(x => x.Product.Price * x.Quantity);
            return total;
        }

        public virtual void EmptyCart()
        {
            _cartRows.Clear();
        }

        public virtual IEnumerable<CartRow> CartRows => _cartRows;



    }
}
