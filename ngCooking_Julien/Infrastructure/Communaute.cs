using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ngCooking_Julien.Infrastructure
{
    public class Communaute
    {
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int level { get; set; }
        public string picture { get; set; }
        public string city { get; set; }
        public int birth { get; set; }
        public string bio { get; set; }
        public virtual ICollection<Recettes> recettes { get; set; }
        public virtual ICollection<Comments> comments { get; set; }
    }
}
