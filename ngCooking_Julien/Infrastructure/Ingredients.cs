using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ngCooking_Julien.Infrastructure
{
    public class Ingredients
    {
        [Key]
        public string id { get; set; }
        public string category { get; set; }
		public string name { get; set; }
        public bool isAvailable { get; set; }
        public string picture { get; set; }
        public int calories { get; set; }
        public virtual ICollection<Recettes> recette { get; set; }

        [ForeignKey("category")]
        public virtual Categories categories { get; set; }

        //public string recette { get; set; }
        //[ForeignKey("recette")]
        //public virtual Recettes recettes { get; set; }
    }
}