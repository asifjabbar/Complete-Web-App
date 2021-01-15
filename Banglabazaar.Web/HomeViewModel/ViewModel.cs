using Bangalabazaar_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banglabazaar.Web.HomeViewModel
{
    public class ViewModel
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
      
    }
}