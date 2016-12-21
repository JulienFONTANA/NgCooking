using ngCooking_Julien.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    static public class IngredientsModel
    {
        static public void FillDisplayIng(IngredientsDisplayViewModel ingDisplay)
        {
            List<Categories> allCat = new List<Categories>();
            allCat = ingDisplay.db.Categories.OrderBy(c => c.nameToDisplay).ToList();

            foreach (var cat in allCat)
            {
                var tmp = new CategoriesData();

                tmp.id = cat.id;
                tmp.nameToDisplay = cat.nameToDisplay;

                ingDisplay.catData.Add(tmp);
            }

            // Si aucun critère de recherche, quitter
            if (ingDisplay.NameFilter == "" && ingDisplay.CategoryFilter == "" && ingDisplay.CaloriesFilterMin == 0 && ingDisplay.CaloriesFilterMax == 0)
            {
                return;
            }

            List<Ingredients> allIng = new List<Ingredients>();
            allIng = ingDisplay.db.Ingredients.ToList();
            // Tri par nom
            if (ingDisplay.NameFilter != "")
            {
                allIng = allIng.OrderBy(i => i.name).Where(i => i.name.ToLower().Contains(ingDisplay.NameFilter.ToLower())).ToList();
            }
            // Tri par catégorie
            if (ingDisplay.CategoryFilter != "")
            {
                allIng = allIng.OrderBy(i => i.name).Where(i => i.category == ingDisplay.CategoryFilter).ToList();
            }
            // Tri par calories (Une valeur non nulle, Min < Max et Min positif)
            if (ingDisplay.CaloriesFilterMin != 0 || ingDisplay.CaloriesFilterMax != 0)
            {
                if (ingDisplay.CaloriesFilterMin <= ingDisplay.CaloriesFilterMax && ingDisplay.CaloriesFilterMin >= 0)
                {
                    allIng = allIng.OrderBy(i => i.name).Where(i => i.calories >= ingDisplay.CaloriesFilterMin && i.calories <= ingDisplay.CaloriesFilterMax).ToList();
                }
                else if (ingDisplay.NameFilter == "" && ingDisplay.CategoryFilter == "")
                {
                    allIng = null;
                }
            }
            

            if (allIng != null && allIng.Count != 0)
            {
                int maxCal = allIng.Max(i => i.calories);
                if (maxCal == 0)
                {
                    maxCal = 1;
                }

                foreach (var ing in allIng)
                {
                    var tmp = new IngredientData();
                    var tmpCat = new Categories();

                    tmp.id = ing.id;
                    tmp.name = ing.name;
                    tmp.picture = ing.picture;
                    tmp.category = ing.category;
                    tmp.calories = ing.calories;
                    tmp.isAvailable = ing.isAvailable;
                    tmpCat = allCat.Where(ac => ac.id == ing.category).First();
                    tmp.categoryName = tmpCat.nameToDisplay;
                    tmp.percentage = Convert.ToString(ing.calories * 100 / maxCal) + "%";

                    ingDisplay.ingVal.ingData.Add(tmp);
                }
            }

            // Pour chaque ingrédient à afficher
            // Pour chaque correspondance dans la bdd de catégories
            // On ajoute tout les ingrédients de la même catégorie sauf celui avec le même nom
            foreach (var simi in ingDisplay.ingVal.ingData)
            {
                var categoryToLookUp = simi.category;
                var similarInglist = new List<IngredientData>();
                foreach (var IngToLookUp in ingDisplay.db.Ingredients)
                {

                    if (IngToLookUp.category == categoryToLookUp && simi.name != IngToLookUp.name)
                    {
                        var tmp = new IngredientData();
                        tmp.id = IngToLookUp.id;
                        tmp.name = IngToLookUp.name;
                        tmp.picture = IngToLookUp.picture;
                        similarInglist.Add(tmp);
                    }

                }
                if (similarInglist.Count != 0)
                {
                    ingDisplay.ingVal.similIng.Add(simi.id, similarInglist);
                }
            }
        }
    }
}