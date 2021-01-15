using Bangalabazaar_Entities;
using Banglabazaar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banglabazaar.Web.HomeViewModel
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public pager Pager { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }
       
        public DateTime OrderedAt { get; set; }
    }

}