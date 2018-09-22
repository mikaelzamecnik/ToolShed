using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.Models;

namespace ToolShed.Web.ViewModels
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }

        public List<SelectListItem> Categories { get; set; }


    }
}
