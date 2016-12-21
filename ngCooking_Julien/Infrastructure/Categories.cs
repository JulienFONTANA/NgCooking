using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ngCooking_Julien.Infrastructure
{
    public class Categories
    {
        [Key]
        public string id { get; set; }
        public string nameToDisplay { get; set; }
    }
}
