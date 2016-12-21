using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ngCooking_Julien.Infrastructure;
using ngCooking_Julien.Models;

namespace ngCooking_Julien.Controllers
{
    public class IngredientsController : Controller
    {
        ViewModel IngredientsView = new ViewModel();

        public IngredientsController()
        {
            ViewBag.Where = "Ingredients";
        }

        // GET: Ingredients
        public ActionResult Display(string NameFilter = "", string CategoryFilter = "", int CaloriesFilterMin = 0, int CaloriesFilterMax = 0)
        {
            var IngDisplay = new IngredientsDisplayViewModel();

            IngDisplay.NameFilter = NameFilter;
            IngDisplay.CategoryFilter = CategoryFilter;
            IngDisplay.CaloriesFilterMin = CaloriesFilterMin;
            IngDisplay.CaloriesFilterMax = CaloriesFilterMax;
            IngredientsModel.FillDisplayIng(IngDisplay);

            return View(IngDisplay);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IngredientsView.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
