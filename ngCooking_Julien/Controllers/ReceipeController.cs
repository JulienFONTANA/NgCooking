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
using System.IO;
using System.Web.Security;

namespace ngCooking_Julien.Controllers
{
    public class ReceipeController : Controller
    {
        private CreateReceipe createView { get; }

        public ReceipeController()
        {
            ViewBag.Where = "Receipe";
            createView = new CreateReceipe();
        }

        // GET: Receipe
        public ActionResult Display(string NameFilter = "", string IngFilter = "", int CaloriesFilterMin = 0, int CaloriesFilterMax = 0)
        {
            var RecetteView = new RecetteViewModel();
            RecetteView.NameFilter = NameFilter;
            RecetteView.IngFilter = IngFilter;
            RecetteView.CaloriesFilterMin = CaloriesFilterMin;
            RecetteView.CaloriesFilterMax = CaloriesFilterMax;

            RecetteModel.FillRecetteSearch(RecetteView);

            return View(RecetteView);
        }

        // GET: Receipe/Details/poulet-citron
        [HttpGet]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var recette = new ReceipeDetailsViewModel();
            RecetteModel.FillRecetteDetails(recette, id);

            if (recette == null)
            {
                return HttpNotFound();
            }
            return View(recette);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Details(ReceipeDetailsViewModel rd)
        {
            if (rd != null && rd.title != null && rd.comment != null && rd.mark != 0 && rd.recetteId != null)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                var cookieId = Convert.ToInt32(FormsAuthentication.Decrypt(authCookie.Value).Name);

                if (rd.db.Comments.Any(c => c.userId == cookieId))
                {
                    return RedirectToAction("Details");
                }

                var Comm = new Comments();
                Comm.userId = cookieId;
                Comm.title = rd.title;
                Comm.comment = rd.comment;
                Comm.mark = rd.mark;

                Comm.recettesId = rd.recetteId;

                rd.db.Comments.Add(Comm);
                rd.db.SaveChanges();
                return RedirectToAction("Details");
            }
            return RedirectToAction("Details");
        }

        // GET: Receipe/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View(createView);
        }

        // POST: Receipe/Create
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CreateReceipe cr)
        {
            if (ModelState.IsValid)
            {
                var recettes = new Recettes();

                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                recettes.creatorId = Convert.ToInt32(FormsAuthentication.Decrypt(authCookie.Value).Name);

                recettes.id = cr.recName.ToLower().Replace(" ", "-");
                recettes.name = cr.recName;

                recettes.ingredients = new List<Ingredients>();
                foreach (var item in cr.recIdIngredients.Split(','))
                {
                    recettes.ingredients.Add(cr.db.Ingredients.SingleOrDefault(m => m.id == item));
                }

                recettes.calories = cr.recCalories;
                recettes.isAvailable = true;

                HttpPostedFileBase file = cr.pictureFile;
                if (file.ContentLength > 0)
                {
                    string uploadDir = "~/Content/img/recettes/";
                    string path = Path.Combine(Server.MapPath(uploadDir), file.FileName);
                    file.SaveAs(path);
                    recettes.picture = "img/recettes/" + Path.GetFileName(file.FileName);
                }

                recettes.preparation = cr.recPrep;

                cr.db.Recettes.Add(recettes);
                cr.db.SaveChanges();
                return Redirect("/Community/Details/" + recettes.creatorId);
            }
            return View(cr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                createView.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}