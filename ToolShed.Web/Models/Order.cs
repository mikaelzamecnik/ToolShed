using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.Web.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }
        [BindNever]
        public ICollection<CartRow> Rows { get; set; }

        [BindNever]
        public bool Shipped { get; set; }

        [Required(ErrorMessage ="Vänligen ange förnamn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Vänligen ange efternamn")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Vänligen ange gatuadress")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Vänligen ange postnummer")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Vänligen ange stad")]
        public string City { get; set; }
    }
}
