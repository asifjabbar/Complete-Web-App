using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bangalabazaar_Entities
{
   public class Product : BaseEntities
    {
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public virtual Category Category { get; set; }
        [Range(1,100000)]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
