using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ngCooking_Julien.Infrastructure
{
    public class ngCookingDB : DbContext
    {
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Recettes> Recettes { get; set; }
        public DbSet<Communaute> Communaute { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}