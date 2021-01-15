using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banglabazaar.Services;
using Banglabazaar.Web.HomeViewModel;
using Banglabazaar.Database;

namespace Banglabazaar.Web.Controllers
{
    public class HomeController : Controller
    {
       
        Categoriesservices service = new Categoriesservices();
       
        public ActionResult Index()
        {
            
            ViewModel model = new ViewModel();
            model.FeaturedCategories = service.getfeaturedcategories();
           
           
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    
    }
}