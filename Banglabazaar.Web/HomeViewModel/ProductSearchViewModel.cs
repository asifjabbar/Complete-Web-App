using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangalabazaar_Entities;
namespace Banglabazaar.Web.HomeViewModel
{
    public class ProductSearchViewModel
    {
  
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
        public pager Pager { get; set; }

    }
}