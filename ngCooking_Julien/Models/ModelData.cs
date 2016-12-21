using ngCooking_Julien.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    public class ViewModel
    {
        public ngCookingDB db { get; set; }

        public ViewModel()
        {
            db = new ngCookingDB();
        }
    }

    public class CategoriesData
    {
        public string id { get; set; }
        public string nameToDisplay { get; set; }
    }

    public class IngredientData
    {
        public string id { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public bool isAvailable { get; set; }
        public string picture { get; set; }
        public int calories { get; set; }
        public string categoryName { get; set; }
        public string percentage { get; set; }
    }

    public class RecettesData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public int rating { get; set; }

        // Only in Receipe/Details
        public int calories { get; set; }
        public string preparation { get; set; }
        public double preciseRating { get; set; }
        
        //public int creatorId { get; set; }
        //public bool isAvailable { get; set; }
    }

    public class CommentsData
    {
        public int id { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int mark { get; set; }
        public string userName { get; set; }
        public int userId { get; set; }

        //public string recettesId { get; set; }
    }

    public class CommunityData
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string surname { get; set; }
        public int level { get; set; }
        public string picture { get; set; }
        public string city { get; set; }
        public int birth { get; set; }
        public string bio { get; set; }

        //public string email { get; set; }
        //public string password { get; set; }
    }
}