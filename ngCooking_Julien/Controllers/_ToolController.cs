using ngCooking_Julien.Infrastructure;
using ngCooking_Julien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngCooking_Julien.Controllers
{
    public class _ToolController : Controller
    {
        private readonly ngCookingDB db = new ngCookingDB();

        [ChildActionOnly]
        public ActionResult ReceipeCount() // MAJ du nombre de recettes
        {
            var receipeCount = db.Recettes.Count();

            return PartialView("_WelcomeHeader", receipeCount);
        }
    }
}