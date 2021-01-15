using Bangalabazaar_Entities;
using Banglabazaar.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Banglabazaar.Services
{
   public class Categoriesservices

    {
        //#region Singleton
        //public static Categoriesservices Instance {

        //    get {
        //        if (instance == null)
        //            instance = new Categoriesservices();

        //        return instance;

        //    }
        //}
        //private static Categoriesservices instance { get; set; }
        //private Categoriesservices()
        //{

        //}

        //#endregion
        BBcontext context = new BBcontext();
        public void savecategories(Category c)
        {
           
               context.Categories.Add(c);
               context.SaveChanges();

           
        }

        public List<Category> getallcategories()
        {
            


                return context.Categories.Where(x=>x.IsActive==true).ToList();







        }
        public List<Category> getcategories(string search,int pageNo)
        {
            int pagesize =5;


            if (search != null)
            {


                return context.Categories
                    .Where(x => x.Name.Contains(search) && x.IsActive == true)
                    .OrderBy(o => o.ID)
                    .Skip((pageNo - 1) * pagesize)
                    .Take(pagesize)
                    .Include(p => p.Products)
                    .ToList();



            }
            else
            {
              var result= context.Categories.Where(x=>x.IsActive==true )
                    .Include(p => p.Products)
                    .OrderBy(o => o.ID)
                    .Skip((pageNo - 1) * pagesize)
                    .Take(pagesize)
                    .ToList();

                return result;
            }

          

           
        }

        public int getcategoriescount(string search)
        {

            if (search != null)
            {
                return context.Categories
                    .Where(x => x.Name.Contains(search) && x.IsActive==true).Count();
                   



            }
            else
            {
                return context.Categories.Where(x=>x.IsActive==true).Count();
                   
            }





          

           
        }

        public List<Category> getfeaturedcategories()
        {
            var category = context.Categories.Where(x=>x.IsFeatured==true && x.ImageURL!=null && x.IsActive==true).ToList();

            return category;
        }
        public Category getcategory(int ID)
        {
            var category = context.Categories.Single(x => x.ID==ID && x.IsActive==true);
            return category;

        }
        public Category update(int ID)
        {

            var update = context.Categories.Find(ID);
            return update;
        }
        public void updatecategory(Category c)
        {
          
            context.Entry(c).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public Category getdelete(int ID)
        {
            var category = context.Categories.Find(ID);
            return category;

        }
        public void delete(Category c)
        {
            //  Category obj = new Category();
            //var cat = context.Categories.Include(x=>x.Products).Where(x => x.ID == c.ID).SingleOrDefault();
            //  obj.ID = cat.ID;
            //  obj.Name = cat.Name;
            //  obj.ImageURL = cat.ImageURL;
            //  obj.IsFeatured = cat.IsFeatured;
            //  obj.Description = cat.Description;
            //  obj.Products = cat.Products;
            //var data = context.Categories.Where(x => x.ID == c.ID).SingleOrDefault();

            //context.Categories.Remove(data);
            context.Entry(c).State = System.Data.Entity.EntityState.Deleted;

            context.SaveChanges();
           

        }



    }
}
