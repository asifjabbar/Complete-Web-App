using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banglabazaar.Services;
using Banglabazaar.Web.HomeViewModel;
using Banglabazaar.Web.Code;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Bangalabazaar_Entities;
using Banglabazaar.Database;

namespace Banglabazaar.Web.Controllers
{
  
    public class ShopController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

      

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        BBcontext context = new BBcontext();

        Productsservices service = new Productsservices();
        Categoriesservices categoryservice = new Categoriesservices();
      
        public ActionResult Index(string Searchterm,int? minimumprice,int?maximumprice,int? categoryID,int? sortBy,int? pageNo)
        {
            Configservices configservice = new Configservices();
            var PageSize = configservice.ShopPageSize();
           var Pagesizee = int.Parse(PageSize);
            Shopviewmodel model = new Shopviewmodel();

            model.FeaturedCategories = context.Categories.ToList();
           
            model.MaximumPrice = service.Getmaximumprice();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
           
            model.sortby = sortBy;
            model.CategoryID = categoryID;
            model.SearchTerm = Searchterm;
            int totalcount =service.searchingproductscount(Searchterm, minimumprice, maximumprice, categoryID, sortBy);
            model.Products = service.searchingproducts(Searchterm, minimumprice, maximumprice, categoryID, sortBy, pageNo.Value, Pagesizee);
            model.Pager = new pager(totalcount,pageNo, Pagesizee);
        
            return View(model);
        }

        public ActionResult FilterProducts(string Searchterm, int? minimumprice, int? maximumprice, int? categoryID, int? sortBy, int? pageNo)
        {
            Configservices configservice = new Configservices();
            var PageSize = configservice.ShopPageSize();
            var Pagesizee = int.Parse(PageSize);
            FilterProductviewmodel model = new FilterProductviewmodel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.sortby = sortBy;
            model.CategoryID = categoryID;
            model.SearchTerm = Searchterm;
           
            int totalcount = service.searchingproductscount(Searchterm, minimumprice, maximumprice, categoryID, sortBy);
            model.Products = service.searchingproducts(Searchterm, minimumprice, maximumprice, categoryID, sortBy, pageNo.Value, Pagesizee);
           
            model.Pager = new pager(totalcount, pageNo, Pagesizee);

            return PartialView(model);
        }
        [Authorize]
        public ActionResult Checkout()
        {
            ShopViewModel model = new ShopViewModel();

            var cartproductcookie = Request.Cookies["cartproduct"];
            if(cartproductcookie !=null && !string.IsNullOrEmpty(cartproductcookie.Value))
            {
                model.CartProductsIDs = cartproductcookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts= service.getproducts(model.CartProductsIDs);
                model.User = UserManager.FindById(User.Identity.GetUserId());
                

            }
           

            return View(model);
        }
        [Authorize]
        public JsonResult PlaceOrder(string ProductIDs,string Contact,string UserName,string Address)
                  {
            Shopservices shopservice = new Shopservices();
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (Contact!=null && Contact!="")
            {
               
                if (!string.IsNullOrEmpty(ProductIDs))
                {
                    var productIds = ProductIDs.Split('-');

                    List<OrderItem> o = new List<OrderItem>();

                    foreach (var i in productIds)
                    {
                        int id = int.Parse(i);

                        if (o.Any(x => x.ProductID == id))
                        {
                            o.Single(x => x.ProductID == id).Qauntity += 1;
                        }
                        else
                        {
                            o.Add(new OrderItem() { ProductID = id, Qauntity = 1, Price = context.Products.Single(x => x.ID == id).Price });
                        }
                    }
                    var ids = ProductIDs.Split('-').Select(x => int.Parse(x)).ToList();
                    var ProductQty = ProductIDs.Split('-').Select(x => int.Parse(x)).ToList();
                    var Boughtproducts = service.getproducts(ids);
                    Order NewOrder = new Order();
                    NewOrder.UserId = User.Identity.GetUserId();
                    NewOrder.OrderedAt = DateTime.Now;
                    NewOrder.Status = "Pending";
                    NewOrder.TotalAmount = Boughtproducts.Sum(x => x.Price * ProductQty.Where(productID => productID == x.ID).Count());
                    NewOrder.OrderItems = o;
                    NewOrder.Contact = Contact;
                    NewOrder.UserName = UserName;
                    NewOrder.Address = Address;
                    var rowseffected = shopservice.saveOrder(NewOrder);

                    result.Data = new { Success = true, Rows = rowseffected };
                }
                else
                {
                    result.Data = new { Success = false };

                }
            }
            else
            {
                result.Data = new { Success = false };

            }
         

            return result;
        }
    }
}