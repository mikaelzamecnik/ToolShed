using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("Pris")]
        public decimal Price { get; set; }
        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
