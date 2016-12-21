using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ngCooking_Julien.Infrastructure
{
    public class Comments
    {
        [Key]
        public int id { get; set; }
        public string recettesId { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int mark { get; set; }

        [ForeignKey("userId")]
        public virtual Communaute communaute { get; set; }

        [ForeignKey("recettesId")]
        public virtual Recettes recettes { get; set; }
    }
}
