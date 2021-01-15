using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bangalabazaar_Entities;
using System.ComponentModel.DataAnnotations;

namespace Banglabazaar.Web.HomeViewModel
{
    public class newcategoryviewmodel
    {
        public int ID { get; set; }
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(1,100000)]
        public decimal Price { get; set; }
        public string ImageURL { get; set; }

        public int CategoryID { get; set; }

    
    }
}