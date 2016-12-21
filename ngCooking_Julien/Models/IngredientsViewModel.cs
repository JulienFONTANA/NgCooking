using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    public class IngredientsDisplayViewModel : ViewModel
    {
        public string NameFilter { get; set; }
        public string CategoryFilter { get; set; }
        public int CaloriesFilterMin { get; set; }
        public int CaloriesFilterMax { get; set; }
        public List<CategoriesData> catData { get; set; }
        public IngredientsValuesToDisplay ingVal { get; set; }

        public IngredientsDisplayViewModel()
        {
            catData = new List<CategoriesData>();
            ingVal = new IngredientsValuesToDisplay();
        }
    }

    // HELPPPPP
    public class IngredientsValuesToDisplay
    {
        public List<IngredientData> ingData { get; set; }
        public Dictionary<string, List<IngredientData>> similIng { get; set; }

        public IngredientsValuesToDisplay()
        {
            ingData = new List<IngredientData>();
            similIng = new Dictionary<string, List<IngredientData>>();
        }
    }
}