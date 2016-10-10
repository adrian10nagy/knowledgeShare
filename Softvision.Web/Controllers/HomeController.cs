using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Softvision.BL.Crud;
using Softvision.BL.Entities;

namespace Softvision.All.Controllers
{
    public class HomeController : Controller
    {
        // [Route("Home/About/{orderId}/{customerId?}")]
        public ActionResult About(int orderId, int customerId = 1)
        {
            var x = System.Configuration.ConfigurationManager.AppSettings["Adrian"];
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(FormCollection collection)
        {
            var email = (collection["email"] != null) ? collection["email"] : string.Empty;
            var subject = (collection["subject"] != null) ? collection["subject"] : string.Empty;
            var message = (collection["message"] != null) ? collection["message"] : string.Empty;

            Softvision.BL.Services.SMTP.sendEmail(email, "info@knowledgeshare.ro", subject, message);

            return RedirectToAction("Contact", "Home");
        }

        public ActionResult Index()
        {
            //var ck = Softvision.Helpers.Web.Instance.Coookie.GetCookie("something");

            List<QuestionBL> questions = KitBL.Instance.Questions.GetLatests(10);
            List<ArticleBL> articles = KitBL.Instance.Articles.GetLatests(10);

            var articlesPerMonth = KitBL.Instance.Articles.GetAllByMonth();

            ViewData["articlesPerMonth"] = articlesPerMonth;
            ViewData["articles"] = articles;
            ViewData["questions"] = questions;

            return View();
        }
    }
}