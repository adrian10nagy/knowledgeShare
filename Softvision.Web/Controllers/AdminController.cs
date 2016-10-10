using Softvision.All.Helpers.Base;
using Softvision.BL.Crud;
using Softvision.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Softvision.All.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult NewSubcategory()
        {
            var categories = KitBL.Instance.Categories.GetAll();
            ViewData["categories"] = categories;

            return View();
        }

        [HttpPost]
        public ActionResult NewSubCategory()
        {
            try
            {
                var newSubCategory = Request.Form["newSubCategory"];
                var drpCategory = Request.Form["drpCategory"];

                if (newSubCategory != "" && drpCategory != "")
                {
                    int drpCategoryValue;
                    int.TryParse(drpCategory.ToString(), out drpCategoryValue);

                    KitBL.Instance.SubCategories.Insert(drpCategoryValue, newSubCategory.ToString());
                }

            }
            catch
            {
                RedirectToAction("Index", "Home");
                return View();
            }
            return View("Index");
        }

        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(FormCollection collection)
        {
            try
            {
                var newCategory = Request.Form["newCategory"];

                if (newCategory != string.Empty)
                {
                    KitBL.Instance.Categories.Insert(newCategory);
                }

            }
            catch
            {
                RedirectToAction("Index", "Home");
                return View();
            }
            return View();
        }

        #region User

        public ActionResult UsersAccess()
        {
            if (!BaseMVC.IsAdmin())
            {
                RedirectToAction("Index", "Home");
            }

            var users = KitBL.Instance.Users.GetAll();
            ViewData["users"] = users;

            return View();
        }

        [HttpPost]
        public ActionResult UsersAccess(FormCollection collection)
        {
            try
            {
                var accessType = int.Parse(Request.Form["drpAccessType"].ToString());
                var userId = int.Parse(Request.Form["drpUser"].ToString());
                KitBL.Instance.Users.RemoveFromAuthors(userId, accessType);
            }
            catch
            {
                RedirectToAction("Index", "Home");
                return View();
            }

            RedirectToAction("Index", "Admin");
            return View();
        }

        #endregion

        #region Question

        public ActionResult Question(int id)
        {
            QuestionBL question = KitBL.Instance.Questions.Get(id);

            return View(question);
        }

        [HttpGet]
        public ActionResult AsyncUpdateQuestions()
        {
            var questions = KitBL.Instance.Questions.GetAll();
            return PartialView("Admin/QuestionsManagePV", questions);
        }

        [HttpGet]
        public ActionResult AsyncUpdateAnswersAdmin(int answerId)
        {
            var answers = KitBL.Instance.Answers.GetAll(answerId);
            return PartialView("Admin/AnswersManagePV", answers);
        }

        #endregion

        #region Article

        public ActionResult Article(int id)
        {
            ArticleBL article = KitBL.Instance.Articles.Get(id);

            return View(article);
        }

        [HttpGet]
        public ActionResult AsyncUpdateArticles()
        {
            var articles = KitBL.Instance.Articles.GetAll();
            return PartialView("Admin/ArticlesManagePV", articles);
        }

        [HttpGet]
        public ActionResult AsyncUpdateCommentsAdmin(int articleId, string title = null)
        {
            var comments = KitBL.Instance.Comments.GetWithUsers(articleId);
            return PartialView("Admin/CommentsManagePV", comments);
        }

        [HttpPost]
        public ActionResult AsyncArticleCommentRemove(int commentId)
        {
            try
            {
                if (BaseMVC.IsAdmin())
                {
                    var article = KitBL.Instance.Articles.GetByCommentId(commentId);
                    KitBL.Instance.Comments.Remove(commentId);
                    return Json(new { removed = true, articleId = article.Id });
                }
            }
            catch
            {
                //log error
            }
            return Json(new { removed = false });
        }

        [HttpPost]
        public ActionResult AsyncAnswerRemove(int answerId)
        {
            try
            {
                if (BaseMVC.IsAdmin())
                {
                    var question = KitBL.Instance.Questions.GetByAnswerId(answerId);
                    KitBL.Instance.Answers.Remove(answerId);
                    return Json(new { removed = true, questionId = question.Id });
                }
            }
            catch
            {
                //log error
            }
            return Json(new { removed = false });
        }
        #endregion
    }
}
