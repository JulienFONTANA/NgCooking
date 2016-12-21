using Newtonsoft.Json;
using ngCooking_Julien.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    public class HomeRecetteViewModel : ViewModel
    {
        public List<RecettesData> bestRecettesData { get; set; }
        public List<RecettesData> newRecettesData { get; set; }

        public HomeRecetteViewModel()
        {
            bestRecettesData = new List<RecettesData>();
            newRecettesData = new List<RecettesData>();
            RecetteModel.FillRecetteVM(this);
        }
    }

    public class RecetteViewModel : HomeRecetteViewModel
    {
        public string NameFilter { get; set; }
        public string IngFilter { get; set; }
        public int CaloriesFilterMin { get; set; }
        public int CaloriesFilterMax { get; set; }

        public List<RecettesData> search { get; set; }

        public RecetteViewModel()
        {
            search = new List<RecettesData>();
        }
    }

    public class ReceipeDetailsViewModel : HomeRecetteViewModel
    {
        public string title { get; set; }
        public string comment { get; set; }
        public int mark { get; set; }
        public string recetteId { get; set; }
        public int userId { get; set; }

        public RecettesData recette { get; set; }
        public List<CommentsData> comments { get; set; }
        public List<IngredientData> ingredients { get; set; }

        public ReceipeDetailsViewModel()
        {
            recette = new RecettesData();
            comments = new List<CommentsData>();
            ingredients = new List<IngredientData>();
        }
    }

    public class CreateReceipe : ViewModel
    {
        [Required]
        public string recName { get; set; }
        [Required]
        public string recPrep { get; set; }
        [Required]
        public HttpPostedFileBase pictureFile { get; set; }
        public string picturePath { get; set; }
        public int userId { get; set; }
        public int recCalories { get; set; }
        public string ingInCat { get; set; }
        public List<CategoriesData> categories { get; set; }
        [Required]
        public string recIdIngredients { get; set; } // Ingredients ids used in receipe
        public List<IngredientData> recIngredients { get; set; } // Ingredients used in receipe

        public CreateReceipe()
        {
            recIngredients = new List<IngredientData>();
            categories = new List<CategoriesData>();
            RecetteModel.FillCategories(this);

            ingInCat = JsonConvert.SerializeObject(RecetteModel.FillIngInCat(this.db));

            //DEBUG
            recIdIngredients = null;
            recName = null;
            recPrep = null;
            pictureFile = null;
            recCalories = 0;
            userId = 1;
        }
    }
}
