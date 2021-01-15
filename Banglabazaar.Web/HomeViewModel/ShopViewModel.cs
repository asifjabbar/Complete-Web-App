using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangalabazaar_Entities;
using Banglabazaar.Web.Models;

namespace Banglabazaar.Web.HomeViewModel
{
    public class ShopViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductsIDs { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class Shopviewmodel
    {
        public FilterProductviewmodel filterproductsviewmodel = new FilterProductviewmodel();
        public int? sortby { get; set; }
        public string SearchTerm { get; set; }
        public int MaximumPrice { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public int? CategoryID { get; set; }
        public pager Pager { get; set; }
    }

    public class FilterProductviewmodel
    {
        public int? sortby { get; set; }
        public string SearchTerm { get; set; }

        public List<Product> Products { get; set; }
        public pager Pager { get; set; }
        public int? CategoryID { get; set; }
    }
}