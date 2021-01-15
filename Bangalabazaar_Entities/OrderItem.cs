using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangalabazaar_Entities
{
   public class OrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public decimal Price { get; set; }
        public int Qauntity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public int ProductID { get; set; }
    }
}
