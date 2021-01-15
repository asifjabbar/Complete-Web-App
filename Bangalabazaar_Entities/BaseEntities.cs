using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bangalabazaar_Entities
{
   public class BaseEntities
    {
        public int ID { get; set; }
        [Required]
        [MinLength(5),MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
