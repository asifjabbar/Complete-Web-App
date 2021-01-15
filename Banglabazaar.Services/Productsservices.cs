using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banglabazaar.Database;
using Bangalabazaar_Entities;
using System.Data.Entity;

namespace Banglabazaar.Services
{
   public class Productsservices
    {
        //#region Singleton
        //public static Productsservices Instance {
        //    get {
        //        if (instance == null)
        //           instance = new Productsservices();

        //        return instance;

        //    }
        //}
        //private static Productsservices instance { get; set; }
        //private Productsservices()
        //{

        //}
        //#endregion

        BBcontext context = new BBcontext();
        public void saveproducts(Product c)
        {
            context.Entry(c.Category).State = System.Data.Entity.EntityState.Modified;
            context.Products.Add(c);
            context.SaveChanges();


        }

        public List<Product> searchingproducts(string searchterm, int? minimumprice, int? maximumprice, int? categoryID, int? sortBy,int pageNo,int pagesize)
        {
            var product = context.Products.Where(x=>x.IsActive==true).ToList();
            if (categoryID.HasValue)
            {
                product = product.Where(x => x.Category.ID == categoryID.Value).ToList();

            }

            else
                       
            if (!string.IsNullOrEmpty(searchterm))
            {

                product = product.Where(x => x.Name.ToLower().Contains(searchterm.ToLower())).ToList();
            }
            if(minimumprice.HasValue)
            {
                product = product.Where(x => x.Price >= minimumprice.Value).ToList();
            }
            if (maximumprice.HasValue)
            {
                product = product.Where(x => x.Price <= maximumprice.Value).ToList();
            }
            if(sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 4:
                        product = product.OrderByDescending(x => x.Price).ToList();
                      
                        break;
                    case 3:
                      
                        product = product.OrderBy(x => x.Price).ToList();
                        break;
                  
                    default:
                        product = product.OrderBy(x => x.ID).ToList();
                        break;
                }
            }
            return product.Skip((pageNo - 1) * pagesize)
                    .Take(pagesize)
                    .ToList()
                     ;

        }

        public int searchingproductscount(string searchterm, int? minimumprice, int? maximumprice, int? categoryID, int? sortBy)
        {
            var product = context.Products.Where(x => x.IsActive == true).ToList();
            if (categoryID.HasValue)
            {
                product = product.Where(x => x.Category.ID == categoryID.Value).ToList();

            }

            else

            if (!string.IsNullOrEmpty(searchterm))
            {
                product = product.Where(x => x.Name.ToLower().Contains(searchterm.ToLower())).ToList();
                //product = product.Where(x => x.Name.Contains(searchterm)).ToList();
            }
            if (minimumprice.HasValue)
            {
                product = product.Where(x => x.Price >= minimumprice.Value).ToList();
            }
            if (maximumprice.HasValue)
            {
                product = product.Where(x => x.Price <= maximumprice.Value).ToList();
            }
            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 4:
                        product = product.OrderByDescending(x => x.Price).ToList();

                        break;
                    case 3:

                        product = product.OrderBy(x => x.Price).ToList();
                        break;

                    default:
                        product = product.OrderBy(x => x.ID).ToList();
                        break;
                }
            }
            return product.Count;

        }

        public List<Product> getproducts(string search, int pageNo, int pagesize)
        {
            


            if (search != null)
            {
                return context.Products
                    .Where(x => x.Name.Contains(search) && x.IsActive == true)
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pagesize)
                    .Take(pagesize)
                    .Include(x => x.Category)
                    .ToList();



            }
            else
            {
                return context.Products.Where(x=>x.IsActive==true)
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pagesize)
                    .Take(pagesize)
                    .Include(x => x.Category)
                    .ToList();
            }




        }

        public int getproductscount(string search)
        {



            if (search != null)
            {
                return context.Products
                    .Where(x => x.Name.Contains(search) && x.IsActive==true)
                    .Count();



            }
            else
            {
                return context.Products.Where(x=>x.IsActive==true)
                   .Count();
            }




        }

        public List<Product> getproducts(int pageNo)
        {
            int pagesize = 10;

            var product = context.Products.Where(x => x.IsActive == true).OrderBy(x => x.ID).Skip((pageNo - 1) * pagesize).Take(pagesize).Include(x => x.Category).ToList();
          

            return product;
        }

        public List<Product> getproducts(int pageNo,int pagesize)
        {
           

            return context.Products.Where(x => x.IsActive == true).OrderByDescending(x => x.ID).Skip((pageNo - 1) * pagesize).Take(pagesize).Include(x => x.Category).ToList();


           
        }
        public List<Product> getproductsbycategory(int CategoryID,int pagesize)
        {


            return context.Products.Where(x=>x.Category.ID==CategoryID && x.IsActive==true).OrderByDescending(x => x.ID).Take(pagesize).Include(x => x.Category).ToList();



        }

        public List<Product> getlatestproducts(int numofproducts)
        {
           

            return context.Products.Where(x => x.IsActive == true).OrderByDescending(x => x.ID).Take(numofproducts).Include(x => x.Category).ToList();
        

           
        }
        public Product getproduct(int ID)
        {
            var product = context.Products.Where(x=>x.ID==ID && x.IsActive==true).Single();
            return product;

        }
       

        public int Getmaximumprice()
        {
            return (int)(context.Products.Where(x=>x.IsActive==true).Max(x => x.Price));
           
        }
        public List<Product> getproducts(List<int> IDs)
        {
            var product = context.Products.Where(products => IDs.Contains(products.ID)).ToList();
            return product;

        }
        public List<Decimal> getproductsprice(List<int> IDs)
        {
            var productprice = context.Products.Where(products => IDs.Contains(products.ID)).Select(x=>x.Price).ToList();
            return productprice;

        }
        public Product update(int ID)
        {
         
            var update = context.Products.Find(ID);
            return update;
        }
        public void updateproduct(Product c)
        {
            
            context.Entry(c).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
     
        public void delete(int ID)
        {
            var c = context.Products.Find(ID);
            context.Entry(c).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();


        }
        public List<Product> searchproduct(string search)
        {

            var data = context.Products.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList();
            return data;
        }


    }
}
