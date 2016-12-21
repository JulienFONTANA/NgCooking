using Newtonsoft.Json;
using ngCooking_Julien.Infrastructure;
using ngCooking_Julien.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ngCooking_Julien.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.Where = "Home";
        }

        public ActionResult Index()
        {
            var model = new HomeRecetteViewModel();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Login(LogViewModel login) // email, password 
        //{
        //    return RedirectToAction("Index");
        //}
    }
}