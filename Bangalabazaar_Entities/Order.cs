using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Bangalabazaar_Entities
{
   public class Order
    {
        public int ID { get; set; }
       
        public string UserId { get; set; }
        public DateTime OrderedAt { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage ="User Name Is Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 Character Required in User Name", MinimumLength = 3)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Address Is Required")]
        [StringLength(100, ErrorMessage = "Minimum 15 Character Required in Address", MinimumLength = 15)]
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        [Required(ErrorMessage ="Contact Number Is Required")]
     [RegularExpression(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)",ErrorMessage ="Enter Correct Phone Number")]
        public string Contact { get; set; }


    }
}
