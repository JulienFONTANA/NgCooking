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
using System.Web.Security;

namespace ngCooking_Julien.Controllers
{
    public class CommunityController : Controller
    {
        ViewModel CommunityView = new ViewModel();

        public CommunityController()
        {
            ViewBag.Where = "Community";
        }

        public ActionResult Index()
        {
            CommunityIndexViewModel CommunityIndexView = new CommunityIndexViewModel();
            string selectorValue = "az";

            CommunityIndexView.fromIndex = CommunityModel.fromIndex(CommunityIndexView.fromIndex, CommunityIndexView.display, CommunityIndexView.dbSize);
            CommunityModel.FillIndexCom(CommunityIndexView, selectorValue);

            return View(CommunityIndexView);
        }

        public ActionResult ComDisplay(int from, string selectorValue)
        {
            CommunityIndexViewModel CommunityIndexView = new CommunityIndexViewModel();
            CommunityIndexView.fromIndex = CommunityModel.fromIndex(from, CommunityIndexView.display, CommunityIndexView.dbSize);
            CommunityModel.FillIndexCom(CommunityIndexView, selectorValue);

            return PartialView("_CommunityDisplay", CommunityIndexView);
        }

        // GET: Community/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CommunityDetailsViewModel CommunityDetailsView = new CommunityDetailsViewModel();
            CommunityModel.FillDetailsCom(CommunityDetailsView, id);

            if (CommunityDetailsView.cData == null)
            {
                return HttpNotFound();
            }
            return View(CommunityDetailsView);
        }

        // GET: Community/Create
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // jonathan@mail.com - c17
        [HttpPost]
        public ActionResult Login(LogViewModel log, string lastUrl)
        {
            var loginBool = CommunityView.db.Communaute.Any(c => c.email == log.mail && c.password == log.password);

            // Correct Login
            if (loginBool)
            {
                var user = CommunityView.db.Communaute.Where(c => c.email == log.mail).SingleOrDefault();
                FormsAuthentication.SetAuthCookie(user.id.ToString(), true);
                if (lastUrl == null)
                {
                    return Redirect("/home");
                }
                return Redirect(lastUrl);
            }
            // Incorrect Login
            else
            {
                return View(log);
            }
        }

        public ActionResult RedirectToComDetails(LogViewModel log)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return Redirect("/Community/");

            var userId = Convert.ToInt32(FormsAuthentication.Decrypt(authCookie.Value).Name);
            return Redirect("/Community/Details/" + userId);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //FormsAuthentication.RedirectToLoginPage();

            return Redirect("/home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CommunityView.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
