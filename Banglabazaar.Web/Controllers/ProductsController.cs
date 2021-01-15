using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bangalabazaar_Entities;
using Banglabazaar.Services;
using Banglabazaar.Database;
using Banglabazaar.Web.HomeViewModel;

namespace Banglabazaar.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        Productsservices product = new Productsservices();
        BBcontext context = new BBcontext();
        public ActionResult Index()
        {
            return View();
        }
        #region Product Creation
        public ActionResult Create()
        {
            Categoriesservices catergoryserice = new Categoriesservices();
            var model = catergoryserice.getallcategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(newcategoryviewmodel model)
        {

           
            Categoriesservices service = new Categoriesservices();

            var newproduct = new Product();
            newproduct.Name = model.Name;
            newproduct.Description = model.Description;
            newproduct.Price = model.Price;
            newproduct.ImageURL = model.ImageURL;
            newproduct.Category = service.getcategory(model.CategoryID);
            newproduct.IsActive = true;
            product.saveproducts(newproduct);

            return RedirectToAction("producttable");
           
        }

        #endregion
        #region Product Searching
        public ActionResult producttable(string search,int? pageNo)
        {
            Configservices configservice = new Configservices();
            var pageSize = configservice.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
        
            var totalrecords = product.getproductscount(search);

            model.Products = product.getproducts(search, pageNo.Value,10);

            model.Pager = new pager(totalrecords, pageNo, pageSize);
            return PartialView(model);


        }
        #endregion

        #region Product Updating
        [HttpGet]
        public ActionResult updateproduct(int ID)
        {
            bothviewmodel model = new bothviewmodel();
            model.Products = product.getproduct(ID);
            //var data = product.getproduct(ID);
            //var catagories = service.getcategories();
            model.Categories = context.Categories.ToList();
           

           

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult updatedproduct(newcategoryviewmodel model)
        {
            Categoriesservices service = new Categoriesservices();
            //var product = Productsservices.Instance.getproduct(model.ID);
            var existingproduct = product.getproduct(model.ID);

            existingproduct.Name = model.Name;
            existingproduct.Description = model.Description;
            existingproduct.Price = model.Price;
            existingproduct.Category = null;
            existingproduct.CategoryID = model.CategoryID;
            existingproduct.IsActive = true;
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingproduct.ImageURL = model.ImageURL;
            }
            product.updateproduct(existingproduct);

            //context.Categories.SingleOrDefault(x => x.ID == model.CategoryID);


            //context.SaveChanges();

           
            return RedirectToAction("producttable");
        }

        #endregion

        #region Product Deletion
        [HttpPost]
        public ActionResult deleteproduct(int ID)
        {
            var products = context.Products.Find(ID);
            products.IsActive = false;
            context.Entry(products).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("producttable");
        }
        #endregion
        [AllowAnonymous]
        public ActionResult Detail(int ID)
        {
            Productviewmodel model = new Productviewmodel();
            model.Product = product.getproduct(ID);
          

            return View(model);
        }
    }
}