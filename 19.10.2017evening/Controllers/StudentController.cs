using _19._10._2017evening.Models;
using Hangfire;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _19._10._2017evening.Controllers
{
    public class StudentController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private ApplicationUserManager userManager;     
        public StudentController()
        {
        }

        public StudentController(ApplicationUserManager userManager)
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
        // GET: Student
        [Authorize]
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                logger.Warn("Неавторизований користувач або адмін у системі.");
                return RedirectToAction("Login", "Account");
            }
            
           
            logger.Trace("Перевіряємо чи користувач є адмін");
            if (User.IsInRole("Admin"))
            {
                logger.Warn("Користувач є адміном, нічого йому тут робити");
                return RedirectToAction("Index", "Home");
              
            }
            else {

                logger.Trace("Отримаємо назву студента");
                ViewBag.Name = User.Identity.Name;
                ViewBag.Position = "Student";
                var item = UserManager.Users.Where(x => x.UserName == User.Identity.Name).ToList().FirstOrDefault();
               
                if (item.EmailConfirmed)
                {
                    logger.Trace("Акаунт підтверджено");
                    ViewBag.verify = "Your account is verified";
                }
                else
                {
                    logger.Trace("Акаунт не підтверджно");
                    ViewBag.verify = "Please verify your account";
                }
                
            }
            return View();
        }      
        [Authorize]
        [HttpPost]
        public ActionResult Index(DateTime studyDate)
        { // get the entity framework context.
            logger.Trace("Отримуємо дані про студента");
            string name = User.Identity.Name;
            ViewBag.Name = name;
            ViewBag.Position = "Student";
            ViewBag.Okay = false;
            logger.Trace("Звертаємося до бази даних");
            var dbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            // save the changes to objects tracked by this context


            logger.Trace("Шукаємо користувача із бази даних");
            var item = UserManager.Users.Where(x => x.UserName == name).ToList().FirstOrDefault();
            if (item.EmailConfirmed)
            {
                ViewBag.verify = "Your account is verified";
            }
            else
            {
                ViewBag.verify = "Please verify your account";
            }
            logger.Trace("Готуємось до встановлення часу навчання студента");
            if (item.StudyDate == null && studyDate.Ticks > DateTime.UtcNow.Ticks)
            {
                logger.Debug("Валідація дати успішна");
                item.StudyDate = studyDate;
                dbContext.SaveChanges();
                DateTime? studydate = item.StudyDate;

                TimeSpan totaltime = ((DateTime)studydate).Subtract(DateTime.UtcNow);
                TimeSpan timeformonth = TimeSpan.FromDays(30) - TimeSpan.FromHours(6);
                TimeSpan timeforweek = TimeSpan.FromDays(7) - TimeSpan.FromHours(6);
                int hours = totaltime.Hours + 6;
                int minutes = totaltime.Minutes;

                if (totaltime.TotalDays > 30)
                {
                    TimeSpan TimeBeforeMonth = totaltime - timeformonth;
                    BackgroundJob.Schedule(() => sendMail(item, "before month"), TimeBeforeMonth);
                }
                if (totaltime.TotalDays > 7)
                {
                    TimeSpan TimeBeforeWeek = totaltime - timeforweek;
                    BackgroundJob.Schedule(() => sendMail(item, "before 7 days"), TimeBeforeWeek);
                }
                if (totaltime.TotalDays > 1)
                {
                    
                    TimeSpan TimeBeforeDayAt8 = TimeSpan.FromDays(totaltime.Days) + TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes) - TimeSpan.FromDays(1);
                    BackgroundJob.Schedule(() => sendMail(item, "tomorow"), TimeBeforeDayAt8);
                }

                ViewBag.Okay = true;
                return View();
            }
            else
            {
                logger.Warn("Дата не валідна.");
                ViewBag.Message = "You are already assigned study date or your study plan is already started";
                return View();
            }        
        }
        public async Task<ActionResult> sendMail(ApplicationUser model,string subject)
        {
            logger.Debug("Відправляємо емейл");
            #region formatter
            string text = string.Format("Your study will start at {0}",subject);
       //    string html = "";
            #endregion

            var msg = new MailMessage();
            msg.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString());
            msg.To.Add(new MailAddress(model.Email));

            msg.Subject = "StudyDate";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
          
            msg.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
            smtpClient.Credentials = credentials;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(msg);
            return RedirectToAction("Index");
        }

    }
}