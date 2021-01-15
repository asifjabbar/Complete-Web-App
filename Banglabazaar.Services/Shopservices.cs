using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banglabazaar.Database;
using Bangalabazaar_Entities;

namespace Banglabazaar.Services
{
   public class Shopservices
    {
        BBcontext context = new BBcontext();

        public int saveOrder(Order order)
        {
            context.Orders.Add(order);
           return context.SaveChanges();


        }
    }
}
