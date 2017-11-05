using _19._10._2017evening.Models;
using Autofac.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace _19._10._2017evening.Controllers
{
    public class HomeController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ApplicationUserManager userManager;

        
        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
           
            if (!User.IsInRole("Admin")) {

                return RedirectToAction("Index", "Student");
            }
            logger.Trace("Адмін заходить на сайт");
            return View();
            
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GetData()
        {
            logger.Debug("Вибираємо дані з бази данних"); 
           var list = UserManager.Users.ToList();
            foreach (ApplicationUser item in list) {

                logger.Debug("Створюємо поля для коректного відображення даних(тимчасово)");
                item.StudyDateString = item.StudyDate?.ToString("yyyy.MM.dd");
                item.RegisteredDateString = item.RegisteredDate?.ToString("yyyy.MM.dd HH:mm");
            }
            logger.Debug("Пересилаємо дані у View");
                    return Json(new { data = list.OrderBy(x=>x.StudyDate) }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Confirm()
        {
            logger.Debug("Повідомляємо юзера, про те, що йому вислано емейл");
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminButton()
        {

            if (User.IsInRole("Admin"))
            {
                logger.Trace("Адмін заходить у hangfire ");
                return Redirect("https://" + Request.Url.Authority + "/hangfire");               
            }
            logger.Error("Студент намагався зайти у hangfire");
            return RedirectToAction("Index", "Student");
        }
    }
}