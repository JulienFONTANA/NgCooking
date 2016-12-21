namespace ngCooking_Julien.Migrations
{
    using Infrastructure;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web.Helpers;
    internal sealed class Configuration : DbMigrationsConfiguration<ngCookingDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ngCookingDB context)
        {
            #region JSON
            #region Categories
            using (StreamReader r =
                new StreamReader("C:/Users/C17 Developer/Documents/Visual Studio 2015/Projects/ngCooking_Julien/ngCooking_Julien/DBFillerJson/categories.json"))
            {
                string json = r.ReadToEnd();
                List<Categories> categories = JsonConvert.DeserializeObject<List<Categories>>(json);

                foreach (var item in categories)
                {
                    context.Categories.AddOrUpdate(
                        new Categories()
                        {
                            id = item.id,
                            nameToDisplay = item.nameToDisplay
                        });
                }
                context.SaveChanges();
            }
            #endregion

            #region Ingredients
            using (StreamReader r =
                new StreamReader("C:/Users/C17 Developer/Documents/Visual Studio 2015/Projects/ngCooking_Julien/ngCooking_Julien/DBFillerJson/ingredients.json"))
            {
                string json = r.ReadToEnd();
                List<Ingredients> ingredients = JsonConvert.DeserializeObject<List<Ingredients>>(json);

                foreach (var item in ingredients)
                {
                    context.Ingredients.AddOrUpdate(
                        new Ingredients()
                        {
                            id = item.id,
                            name = item.name,
                            isAvailable = item.isAvailable,
                            picture = item.picture,
                            category = item.category,
                            calories = item.calories,
                            //recette = item.recette
                        });
                }
                context.SaveChanges();
            }
            #endregion

            #region Communaute
            using (StreamReader r =
                new StreamReader("C:/Users/C17 Developer/Documents/Visual Studio 2015/Projects/ngCooking_Julien/ngCooking_Julien/DBFillerJson/communaute.json"))
            {
                string json = r.ReadToEnd();
                List<Communaute> communaute = JsonConvert.DeserializeObject<List<Communaute>>(json);

                foreach (var item in communaute)
                {
                    context.Communaute.AddOrUpdate(
                        new Communaute()
                        {
                            id = item.id,
                            firstname = item.firstname,
                            surname = item.surname.ToUpper(),
                            email = item.email,
                            password = item.password,
                            level = item.level,
                            picture = item.picture,
                            city = item.city,
                            birth = item.birth,
                            bio = item.bio
                        });
                }
                context.SaveChanges();
            }
            #endregion

            #region Recettes
            using (StreamReader r =
                new StreamReader("C:/Users/C17 Developer/Documents/Visual Studio 2015/Projects/ngCooking_Julien/ngCooking_Julien/DBFillerJson/recettes.json"))
            {
                string json = r.ReadToEnd();
                List<JsonRecettesData> recettesData = JsonConvert.DeserializeObject<List<JsonRecettesData>>(json);

                foreach (var item in recettesData)
                {
                    int realCalories = 0;

                    List<Ingredients> ingredientsList = new List<Ingredients>();
                    foreach (var ing in item.ingredients)
                    {
                        ingredientsList.Add(context.Ingredients.SingleOrDefault(i => i.id == ing));
                        realCalories += context.Ingredients.SingleOrDefault(i => i.id == ing).calories;
                    }

                    List<Comments> commentsList = new List<Comments>();
                    if (item.comments != null)
                    {
                        foreach (var com in item.comments)
                        {
                            commentsList.Add(new Comments()
                            {
                                id = com.id,
                                comment = com.comment,
                                mark = com.mark,
                                recettesId = item.id,
                                title = com.title,
                                userId = com.userId
                            });
                        }
                    }

                    context.Recettes.AddOrUpdate(
                        new Recettes()
                        {
                            id = item.id,
                            name = item.name,
                            creatorId = item.creatorId,
                            isAvailable = item.isAvailable,
                            picture = item.picture,
                            calories = realCalories, //item.calories,
                            ingredients = ingredientsList,
                            preparation = item.preparation,
                            comments = commentsList
                        });
                }
                context.SaveChanges();
            }
            #endregion
            #endregion
        }
    }
}