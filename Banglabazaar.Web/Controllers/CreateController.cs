using Bangalabazaar_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banglabazaar.Services;
using Banglabazaar.Database;
using System.Data.Entity;
using Banglabazaar.Web.HomeViewModel;

namespace Banglabazaar.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CreateController : Controller
    {
        Categoriesservices service = new Categoriesservices();
        BBcontext context = new BBcontext();
        public ActionResult Index()
        {
            
            return View();

        }
        public ActionResult Create()
        {


                    return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Category Category)
        {
            if (ModelState.IsValid)
            {
                Category.IsActive = true;
                service.savecategories(Category);
                return RedirectToAction("categorytable");

            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
           
        }
        public ActionResult categorytable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.SearchTerm = search;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            //int pagesize = 5;
            var totalrecords = service.getcategoriescount(search);

            model.Categories = service.getcategories(search,pageNo.Value);
           
            if (model.Categories != null)
            {

                model.Pager = new pager(totalrecords, pageNo,5);
                //model.Categories=model.Categories.OrderBy(x => x.ID).Skip((page - 1) * pagesize).Take(pagesize).ToList();

              

                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult categorytable(int ID)
        {
            var list = service.getcategory(ID);

            return View(list);
        }
        public ActionResult edit(int ID)
        {
            var edit = service.update(ID);
            return PartialView(edit);
        }
        [HttpPost]
        public ActionResult edit(categoryeditviewmodel model)
        {
            //context.Entry(c).State = System.Data.Entity.EntityState.Modified;
            //context.SaveChanges();
            var existingCategory = service.getcategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.IsFeatured = model.isFeatured;
            service.updatecategory(existingCategory);
            return RedirectToAction("categorytable");
            
        }
        public ActionResult delete(int ID)
        {
           
            var delete = service.getdelete(ID);
            return View(delete);
        }
        [HttpPost]
        public ActionResult delete(Category c)
        {
            var category = context.Categories.Find(c.ID);
            category.IsActive = false;

            var model = context.Products.Where(x => x.Category.ID == c.ID && x.IsActive==true);
            foreach (var product in model)
            {
                product.IsActive = false;
                context.Entry(product).State = EntityState.Modified;
              

            }
           
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
            //service.delete(c);
            return RedirectToAction("categorytable");  
        }
        public ActionResult Signout()
        {

          
            return RedirectToAction("Index", "Home");
        }
    }
}