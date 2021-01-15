using Banglabazaar.Web.HomeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banglabazaar.Services;

namespace Banglabazaar.Web.Controllers
{
    public class WidgetsController : Controller
    {
        Productsservices productservice = new Productsservices();
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts,int? CategoryID=0)
        {
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.IsLatestProducts = isLatestProducts;
            if (isLatestProducts)
            {
                model.Products = productservice.getlatestproducts(4);

            }
            else if(CategoryID.HasValue && CategoryID>0)
            {
                model.Products = productservice.getproductsbycategory(CategoryID.Value,4);

            }
            else
            {
                model.Products = productservice.getproducts(1,8);
            }
            return PartialView(model);
        }
   
    }
}