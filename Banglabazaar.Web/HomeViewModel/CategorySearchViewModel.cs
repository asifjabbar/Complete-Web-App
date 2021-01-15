using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangalabazaar_Entities;

namespace Banglabazaar.Web.HomeViewModel
{
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }
        public pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }
}