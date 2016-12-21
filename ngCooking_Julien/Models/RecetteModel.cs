using ngCooking_Julien.Infrastructure;
using ngCooking_Julien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    static public class RecetteModel
    {
        static public void FillRecetteVM(HomeRecetteViewModel rvm)
        {
            List<Recettes> bestRecettes = rvm.db.Recettes.OrderByDescending(r => r.comments.Average(m => m.mark)).Take(4).ToList();
            List<Recettes> newRecettes = rvm.db.Recettes.OrderBy(r => r.id).Take(4).ToList();

            foreach (var brd in bestRecettes)
            {
                var tmp = new RecettesData();

                tmp.id = brd.id;
                tmp.name = brd.name;
                tmp.picture = brd.picture;
                if (brd.comments.Count != 0)
                    tmp.rating = Convert.ToInt32(brd.comments.Average(c => c.mark));
                else
                    tmp.rating = 2;

                rvm.bestRecettesData.Add(tmp);
            }

            foreach (var nrd in newRecettes)
            {
                var tmp = new RecettesData();

                tmp.id = nrd.id;
                tmp.name = nrd.name;
                tmp.picture = nrd.picture;
                if (nrd.comments.Count != 0)
                    tmp.rating = Convert.ToInt32(nrd.comments.Average(c => c.mark));
                else
                    tmp.rating = 2;

                rvm.newRecettesData.Add(tmp);
            }
        }

        static public void FillRecetteSearch(RecetteViewModel recetteView)
        {
            if (recetteView.NameFilter != "" || recetteView.IngFilter != "" || recetteView.CaloriesFilterMin != 0 || recetteView.CaloriesFilterMax != 0)
            {
                var allRec = recetteView.db.Recettes.ToList();

                // Tri par nom
                if (recetteView.NameFilter != "")
                {
                    allRec = allRec.OrderBy(r => r.name).Where(i => i.name.ToLower().Contains(recetteView.NameFilter.ToLower())).ToList();
                }
                //Tri par ingredients
                if (recetteView.IngFilter != "")
                {
                    var ingToLookUp = recetteView.IngFilter.Trim().Split(',').ToList();
                    allRec = allRec.OrderBy(r => r.name).Where(i => ingToLookUp.Any(str => i.name.Contains(str))).ToList();
                }
                // Tri par calories
                if (recetteView.CaloriesFilterMin != 0 || recetteView.CaloriesFilterMax != 0)
                {
                    if (recetteView.CaloriesFilterMin >= 0 && recetteView.CaloriesFilterMin < recetteView.CaloriesFilterMax)
                    {
                        allRec = allRec.OrderBy(r => r.name).Where(r => r.calories >= recetteView.CaloriesFilterMin && r.calories <= recetteView.CaloriesFilterMax).ToList();
                    }
                    else if (recetteView.NameFilter == "" || recetteView.IngFilter == "")
                    {
                        allRec = null;
                    }
                }

                // Fill recetteView.search
                if (allRec != null && allRec.Count != 0)
                {
                    foreach (var rec in allRec)
                    {
                        var tmp = new RecettesData();

                        tmp.id = rec.id;
                        tmp.name = rec.name;
                        tmp.picture = rec.picture;
                        tmp.calories = rec.calories;

                        if (rec.comments.Count != 0)
                            tmp.rating = Convert.ToInt32(rec.comments.Average(c => c.mark));
                        else
                            tmp.rating = 2;

                        recetteView.search.Add(tmp);
                    }
                }
            }
        }

        static public void FillRecetteDetails(ReceipeDetailsViewModel rdvm, string id)
        {
            // Receipe info
            Recettes recette = rdvm.db.Recettes.Find(id);

            var tmp = new RecettesData();
            tmp.id = recette.id;
            tmp.name = recette.name;
            tmp.picture = recette.picture;
            tmp.calories = recette.calories;
            tmp.preparation = recette.preparation;

            if (recette.comments.Count != 0)
            {
                tmp.preciseRating = (Convert.ToDouble(recette.comments.Sum(c => c.mark)) / recette.comments.Count);
                tmp.rating = Convert.ToInt32(recette.comments.Average(c => c.mark));
            }
            else
            {
                tmp.preciseRating = 2.0;
                tmp.rating = 2;
            }
            rdvm.recette = tmp;


            // Ingredients info
            foreach (var ingredient in recette.ingredients)
            {
                var ing = new IngredientData();

                ing.id = ingredient.id;
                ing.name = ingredient.name;
                ing.picture = ingredient.picture;

                rdvm.ingredients.Add(ing);
            }


            // Comment info
            if (recette.comments.Count != 0)
            {
                foreach (var comment in recette.comments)
                {
                    var comm = new CommentsData();

                    comm.id = comment.id;
                    comm.mark = comment.mark;
                    comm.title = comment.title;
                    comm.userId = comment.userId;
                    comm.comment = comment.comment;
                    comm.userName = rdvm.db.Communaute.Where(c => c.id == comment.userId).SingleOrDefault().firstname
                            + " " + rdvm.db.Communaute.Where(c => c.id == comment.userId).SingleOrDefault().surname;

                    rdvm.comments.Add(comm);
                }
            }
        }

        static public void FillCategories(CreateReceipe cr)
        {
            var catList = cr.db.Categories.ToList();
            foreach (var cat in catList)
            {
                var cData = new CategoriesData();

                cData.id = cat.id;
                cData.nameToDisplay = cat.nameToDisplay;

                cr.categories.Add(cData);
            }
        }

        static public Dictionary<string, List<IngredientData>> FillIngInCat(ngCookingDB context)
        {
            var dico = new Dictionary<string, List<IngredientData>>();
            var ingList = context.Ingredients.ToList();
            var catList = context.Categories.ToList();

            // Category "all"
            var ingr = new List<IngredientData>();
            foreach (var ing in ingList)
            {
                var tmp = new IngredientData();

                tmp.id = ing.id;
                tmp.name = ing.name;
                tmp.category = ing.category;
                tmp.picture = ing.picture;
                tmp.calories = ing.calories;
                tmp.isAvailable = ing.isAvailable;

                ingr.Add(tmp);
            }
            dico.Add("all", ingr);

            // Other categories
            foreach (var cat in catList)
            {
                var ingr2 = new List<IngredientData>();
                foreach (var ing in ingList)
                {
                    if (cat.id == ing.category)
                    {
                        var tmp = new IngredientData();

                        tmp.id = ing.id;
                        tmp.name = ing.name;
                        tmp.category = ing.category;
                        tmp.picture = ing.picture;
                        tmp.calories = ing.calories;
                        tmp.isAvailable = ing.isAvailable;

                        ingr2.Add(tmp);
                    }
                }
                dico.Add(cat.id, ingr2);
            }
            return dico;
        }
    }
}
