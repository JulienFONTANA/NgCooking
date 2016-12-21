using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ngCooking_Julien.Infrastructure
{
    public class Recettes
    {
        [Key]
        public string id { get; set; }
        public int creatorId { get; set; }
        public string name { get; set; }
        public bool isAvailable { get; set; }
        public string picture { get; set; }
        public int calories { get; set; }
        public string preparation { get; set; }
        public virtual ICollection<Ingredients> ingredients { get; set; }
        public virtual ICollection<Comments> comments { get; set; }

        [ForeignKey("creatorId")]
        public virtual Communaute communaute { get; set; }
    }
}
