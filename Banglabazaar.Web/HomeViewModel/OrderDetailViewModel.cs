using Bangalabazaar_Entities;
using Banglabazaar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banglabazaar.Web.HomeViewModel
{
    public class OrderDetailViewModel
    {
       
        public List<string> availablestatus { get; set; }

        public Order Order { get; set; }
        public ApplicationUser OrderBy { get; set; }
      
    }
}