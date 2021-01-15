using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banglabazaar.Database;
using Bangalabazaar_Entities;

namespace Banglabazaar.Services
{ 
   public class Configservices
    {
       
        BBcontext context = new BBcontext();

        public Config getconfig(string key)
        {

            var data = context.Configs.Find(key);
            return data;
        }
        public List<Config> getallconfig()
        {

            var data = context.Configs.ToList();
            return data;
        }
        public void UpdateConfig(string key,string value)
        {
          
            var data = context.Configs.Find(key);
            data.Value = value;
            context.Entry(data).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public int PageSize()
        {

            var PageSizeConfig = context.Configs.Find("PageSize");
            return PageSizeConfig != null ? int.Parse(PageSizeConfig.Value) : 10;
        }
        public string ShopPageSize()
        {

            var PageSizeConfig = context.Configs.Find("ShopPageSize");
            return PageSizeConfig != null ? PageSizeConfig.Value : "9";
        }

    }
}
