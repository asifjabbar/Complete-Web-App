using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bangalabazaar_Entities
{
     public class Config
    {
      [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
