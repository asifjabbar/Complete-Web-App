using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bangalabazaar_Entities
{
   public class Category : BaseEntities
    {
        public string ImageURL { get; set; }
        public List<Product> Products { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }

    }
}
