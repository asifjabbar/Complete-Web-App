using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Banglabazaar.Web.HomeViewModel
{
    public class categoryeditviewmodel
    {

        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string ImageURL { get; set; }

        public bool isFeatured { get; set; }
    }
}