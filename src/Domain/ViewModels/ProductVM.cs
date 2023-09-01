using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Shop.Domain
{
    public class ProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}