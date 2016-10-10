using Softvision.BL.Crud;
using Softvision.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Softvision.All.Helpers.Base;
using Softvision.BL.Services;

namespace Softvision.All.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("userId");
            Session.Remove("userFirstName");
            Session.Remove("userLastName");
            Session.Remove("userType");

            BaseMVC.unsetUser();
            Softvision.Helpers.Web.Instance.Session.unsetUser();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile()
        {
            if (!BaseMVC.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            var user = KitBL.Instance.Users.GetById(BaseMVC.getUserId());
            ViewData["user"] = user;

            return View();
        }

        [HttpPost]
        public ActionResult EmailPreference(FormCollection collection)
        {
            if (!BaseMVC.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            
            int emailPreference = 1;

            var emailPreferenceNewslettter = (collection["chkEmailPreferenceNewslettter"] != null) ? collection["chkEmailPreferenceNewslettter"].Contains("true") : false;
            var emailPreferenceArticle = (collection["chkEmailPreferenceArticle"] != null) ? collection["chkEmailPreferenceArticle"].Contains("true") : false;
            var emailPreferenceQuestion = (collection["chkEmailPreferenceQuestion"] != null) ? collection["chkEmailPreferenceQuestion"].Contains("true") : false;
            var emailPreferenceAnswers = (collection["chkEmailPreferenceAnswers"] != null) ? collection["chkEmailPreferenceAnswers"].Contains("true") : false;
            var emailPreferenceComments = (collection["chkEmailPreferenceComments"] != null) ? collection["chkEmailPreferenceComments"].Contains("true") : false;

            if(!emailPreferenceNewslettter)
            {
                emailPreference = emailPreference + (int)EmailPreferenceBL.NoNewsletter;
            }

            if(!emailPreferenceArticle)
            {
                emailPreference = emailPreference + (int)EmailPreferenceBL.NoArticles;
            }
            
            if(!emailPreferenceQuestion)
            {
                emailPreference = emailPreference + (int)EmailPreferenceBL.NoQuestions;
            }
            
            if(!emailPreferenceAnswers)
            {
                emailPreference = emailPreference + (int)EmailPreferenceBL.NoAnswers;
            }
            
            if(!emailPreferenceComments)
            {
                emailPreference = emailPreference + (int)EmailPreferenceBL.NoComments;
            }
            
            KitBL.Instance.Users.SetEmailPeference(BaseMVC.getUserId() , emailPreference);

            return RedirectToAction("Profile", "User");
        }

        public ActionResult MyArticles()
        {
            var userId = BaseMVC.getUserId();

            if (userId == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            List<ArticleBL> articles = KitBL.Instance.Articles.GetByUserId(userId);

            return View(articles);
        }

        public ActionResult MyQuestions()
        {
            if (BaseMVC.getUserId() == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var userId = BaseMVC.getUserId();

            //to implement

            List<QuestionBL> questions = KitBL.Instance.Questions.GetByUserId(userId);
            //  return PartialView("ArticlesPV", articles);

            return View(questions);
        }

        public ActionResult MyContributions()
        {
            var userId = BaseMVC.getUserId();
            
            if (userId != 0)
            {
                return RedirectToAction("Index", "Home");
            }

            List<QuestionBL> questions = KitBL.Instance.Questions.ContributedBy(userId);

            return View(questions);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            UserBL user = new UserBL
            {
                Email = collection["registerUserEmail"],
                PasswordHashed = collection["registerUserPassword"],
                FirstName = collection["registerUserName"],
                LastName = "Guest",
                Flags = UserFlagsBL.Default,
                JoinDate = DateTime.Now,
                UserType = UserTypeBL.Member
            };

            var userId = KitBL.Instance.Users.Insert(user);

            var success = AsyncUserLogin(user.Email, user.PasswordHashed);
            var emailTemplate = KitBL.Instance.EmailTemplate.GetByName("GeneralRegister");
            SMTP.sendEmail("info@knowledgeshare.ro", user.Email, emailTemplate.Subject, emailTemplate.Structure);

            return RedirectToAction("Index", "Home");
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LostPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LostPassword(FormCollection collection)
        {
            if (collection["recoverUserEmail"] == null)
            {
                return RedirectToAction("LostPassword", "User");
            }

            var userEmail = collection["recoverUserEmail"].ToString();
            var user = KitBL.Instance.Users.GetByEmail(userEmail);

            if (user != null && user.Email != "" && user.Email != null)
            {
                var newPassword = user.PasswordHashed + "NewPassword";
                SMTP.sendEmail("info@knowledgeshare.ro", user.Email, "Password change request!", "Please change your password, visit www.Knowledgeshare.ro/User/ChangePassword . Your temporary password is:" + newPassword);
            }
            return RedirectToAction("Profile", "User");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            if (collection["UserEmail"] == null || collection["UserOldPassword"] == null || collection["UserNewPassword"] == null || collection["UserNewPasswordConfirm"] == null)
            {
                return RedirectToAction("ChangePassword", "User");
            }

            var userEmail = (collection["UserEmail"] != null)?collection["UserEmail"].ToString(): null;
            var UserOldPassword = (collection["UserOldPassword"] != null)? collection["UserOldPassword"].ToString(): null;
            var UserNewPassword = (collection["UserNewPassword"] != null)? collection["UserNewPassword"].ToString(): null;
            var UserNewPasswordConfirm = (collection["UserNewPasswordConfirm"] != null)? collection["UserNewPasswordConfirm"].ToString(): null;

            var user = KitBL.Instance.Users.GetByEmailPass(userEmail, UserOldPassword);

            if (user == null)
            {
                user = KitBL.Instance.Users.GetByEmailPass(userEmail, UserOldPassword.Replace("NewPassword", string.Empty));
            }

            if (user != null && UserNewPassword == UserNewPasswordConfirm)
            {
                user.PasswordHashed = UserNewPassword;
                KitBL.Instance.Users.Update(user);
                SMTP.sendEmail("info@knowledgeshare.ro", user.Email, "Your password was changed!", "Thank you for using knowledgeshare.ro . Your password was changed at your request, if you lose it you can generate a new one anytime.");
                return RedirectToAction("Profile", "User");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult AsyncUserLogin(string userEmail, string userPass)
        {
            var user = KitBL.Instance.Users.GetByEmailPass(userEmail, userPass);

            var userResetPassword = KitBL.Instance.Users.GetByEmailPass(userEmail.Replace("NewPassword", ""), userPass);
            if (user != null && user.isDeleted == 1)
            {
                return Json(new { success = false });
            }
            if (userResetPassword != null && userResetPassword.isDeleted == 1)
            {
                return Json(new { success = false });
            }

            if (user != null && user.Id > 0)
            {
                Session["userId"] = user.Id;
                Session["userType"] = (int)user.UserType;
                Session["userFirstName"] = user.FirstName;
                Session["userLastName"] = user.LastName;

                //Set login credentials
                BaseMVC.setUser(user);

                return Json(new { success = true });
            }
            else if (userResetPassword != null && userResetPassword.Id > 0)
            {
                Session["userId"] = userResetPassword.Id;
                Session["userType"] = (int)userResetPassword.UserType;
                Session["userFirstName"] = userResetPassword.FirstName;
                Session["userLastName"] = userResetPassword.LastName;

                //Set login credentials
                BaseMVC.setUser(userResetPassword);

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

    }
}
