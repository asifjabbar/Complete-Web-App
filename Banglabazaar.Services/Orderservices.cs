using Bangalabazaar_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banglabazaar.Database;
using System.Data.Entity;


namespace Banglabazaar.Services
{
   public class Orderservices
    {
        BBcontext context = new BBcontext();
        public List<Order> searchingOrders(string UserID,string status ,int pageNo, int pagesize)
        { 
            var Order = context.Orders.ToList();
           

            if (!string.IsNullOrEmpty(UserID))
            {
                
                Order = Order.Where(x => x.UserId.Contains(UserID)).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {

                Order = Order.Where(x => x.Status.Contains(status)).ToList();
            }

            return Order.Skip((pageNo - 1) * pagesize)
                    .Take(pagesize)
                    .ToList()
                     ;

        }
        public List<Order> searchingOrdersByUser(string UserID, string status, int pageNo, int pagesize)
        {
            var Order = context.Orders.Where(x=>x.UserId == UserID).ToList();


            if (!string.IsNullOrEmpty(UserID))
            {

                Order = Order.Where(x => x.UserId.Contains(UserID)).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {

                Order = Order.Where(x => x.Status.Contains(status)).ToList();
            }

            return Order.Skip((pageNo - 1) * pagesize)
                    .Take(pagesize)
                    .ToList()
                     ;

        }

        public int getOrderscount(string userID,string status)
        {
            var Order = context.Orders.ToList();


            if (!string.IsNullOrEmpty(userID))
            {

                Order = Order.Where(x => x.UserId.Contains(userID)).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {

                Order = Order.Where(x => x.Status.Contains(status)).ToList();
            }
            return Order.Count;
                     
        }
        public int getOrderscountByUser(string userID, string status)
        {
            var Order = context.Orders.Where(x=>x.UserId == userID).ToList();


            if (!string.IsNullOrEmpty(userID))
            {

                Order = Order.Where(x => x.UserId.Contains(userID)).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {

                Order = Order.Where(x => x.Status.Contains(status)).ToList();
            }
            return Order.Count;

        }
        public Order getOrderDetail(int ID)
        {
            return context.Orders.Where(x => x.ID == ID).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
        }
        public Order getOrderDetailByUser(int ID)
        {
            return context.Orders.Where(x => x.ID == ID).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
        }


        public bool UpdateOrderStatus(string status,int ID)
        {
            var order = context.Orders.Find(ID);
            order.Status = status;
            context.Entry(order).State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }
    }
}
