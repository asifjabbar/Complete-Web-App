using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangalabazaar_Entities;

namespace Banglabazaar.Web.HomeViewModel
{
    public class ProductsWidgetViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProducts { get; set; }
    }
}