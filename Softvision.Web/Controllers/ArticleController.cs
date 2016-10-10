using Softvision.BL.Helpers;
using Softvision.BL.Entities;
using Softvision.BL.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Softvision.All.Helpers.Base;
using Softvision.BL.TrueEditor;

namespace Softvision.All.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Index()
        {
            List<ArticleBL> articles = KitBL.Instance.Articles.GetAll();
            ViewData["articles"] = articles;

            return View();
        }

        public ActionResult Details(int id = 0, string title = null)
        {
            ArticleBL article;

            if (id != 0)
            {
                article = KitBL.Instance.Articles.Get(id);
            }
            else
            {
                article = KitBL.Instance.Articles.GetRandom();
            }

            if (article == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (title == null)
            {
                var aTitle = article.Title.GetLiteralAndNumericalFromString().Replace(' ', '-');

                return RedirectToAction("Details", "Article", new { id = article.Id, title = aTitle });
            }
            article.Comments = KitBL.Instance.Comments.GetAll(article.Id);
            article.User = KitBL.Instance.Users.GetById(article.IdUser);

            return View(article);
        }

        public ActionResult Subcategory(int id, string title = null)
        {
            List<ArticleBL> articles = KitBL.Instance.Articles.GetBySubcategoryId(id);

            if (articles.Count == 0)
            {
                return RedirectToAction("Index", "Article");
            }
            ViewData["articles"] = articles;

            return View();
        }

        public ActionResult History(int year = 0, int month = 0)
        {
            List<ArticleBL> articles = new List<ArticleBL>();
            articles = KitBL.Instance.Articles.GetByMonthAndYear(month, year);

            ViewData["year"] = year;
            ViewData["month"] = (month != 0 || month > 12) ? month : 0;
            ViewData["articles"] = articles;

            return View();
        }

        public ActionResult NewArticle()
        {
            var categories = KitBL.Instance.Categories.GetAll();
            ViewData["categories"] = categories;

            var languages = from LanguageBL s in Enum.GetValues(typeof(LanguageBL))
                            select new { ID = (int)s, Name = s.ToString() };
            ViewData["languages"] = new SelectList(languages, "ID", "Name", 1);

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewArticle(Softvision.BL.Entities.ArticleBL pArticle)
        {
            try
            {
                var editorTexareaText = Request.Form["editorTexarea"];

                if (editorTexareaText != null || editorTexareaText != string.Empty)
                {
                    pArticle.InternalRep = editorTexareaText.ToString();
                    pArticle.HTMLRep = TrueEditor.GenerateHTML(pArticle.InternalRep);
                    pArticle.CreatedDate = DateTime.Now;
                    pArticle.IdSubCategory = Request.Form["drpSubCategory"].ToInt();
                    pArticle.Language = (LanguageBL)Request.Form["drpLanguages"].ToInt();
                    if (BaseMVC.getUserId() != 0)
                    {
                        pArticle.IdUser = BaseMVC.getUserId();
                    }
                    KitBL.Instance.Articles.Insert(pArticle);
                }

                return RedirectToAction("Index", "Article");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit(int id)
        {
            if (!BaseMVC.IsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            // TODO: If has access to article

            var categories = KitBL.Instance.Categories.GetAll();
            ViewData["categories"] = categories;
            var article = KitBL.Instance.Articles.Get(id);
            var languages = from LanguageBL s in Enum.GetValues(typeof(LanguageBL))
                            select new { ID = (int)s, Name = s.ToString() };
            ViewData["languages"] = new SelectList(languages, "ID", "Name", 1);

            return View(article);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Softvision.BL.Entities.ArticleBL pArticle)
        {
            try
            {
                // TODO: If user has access + XSS
                var editorTexareaText = Request.Form["editorTexarea"];

                if (editorTexareaText != null || editorTexareaText != string.Empty)
                {
                    pArticle.InternalRep = editorTexareaText.ToString();
                    pArticle.HTMLRep = TrueEditor.GenerateHTML(pArticle.InternalRep);
                    pArticle.CreatedDate = DateTime.Now;
                    pArticle.IdSubCategory = Request.Form["drpSubCategory"].ToInt();
                    if (BaseMVC.getUserId() != 0)
                    {
                        pArticle.IdUser = BaseMVC.getUserId();
                    }

                    KitBL.Instance.Articles.Update(pArticle);
                }

                return RedirectToAction("MyArticles", "User");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult InsertComment(CommentBL comment)
        {
            try
            {
                if (BaseMVC.getUserId() != 0)
                {
                    comment.IdUser = BaseMVC.getUserId();
                }
                else if (comment.AnonymousEmail == null || comment.AnonymousName == null)
                {
                    return Json(new { success = false, annonymousCredentials = false });
                }

                var newUserId = KitBL.Instance.Comments.Insert(comment);
                if (newUserId > 0)
                {
                    UserBL user = new UserBL();
                    user.Id = newUserId;
                    user.UserType = UserTypeBL.Prospect;
                    user.FirstName = user.LastName = UserTypeBL.Member.ToString();
                    BaseMVC.setUser(user);

                    return Json(new { success = true, newUser = true });
                }
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });

            }
        }

        [HttpPost]
        public ActionResult AsyncUpdateArticleVote(int articleId, int typeId)
        {
            try
            {
                if (Session["articleVoted" + articleId] == null ||
                    (Session["articleVoted" + articleId].ToString() != null &&
                    int.Parse(Session["articleVoted" + articleId].ToString()) != typeId))
                {
                    KitBL.Instance.Articles.UpdateVote(articleId, typeId);
                    Session["articleVoted" + articleId] = typeId;
                    return Json(new { success = true, inserted = true });
                }
                return Json(new { success = true, inserted = false });
            }
            catch
            {
                return Json(new { success = false });

            }
        }


        [HttpPost]
        public ActionResult AsyncUpdateCommentVote(int commentId, int typeId)
        {
            try
            {
                if (Session["commentVoted" + commentId] == null ||
                    (Session["commentVoted" + commentId].ToString() != null &&
                    int.Parse(Session["commentVoted" + commentId].ToString()) != typeId))
                {
                    KitBL.Instance.Articles.UpdateCommentVote(commentId, typeId);
                    Session["commentVoted" + commentId] = typeId;
                    return Json(new { success = true, inserted = true });
                }
                return Json(new { success = true, inserted = false });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public ActionResult AsyncUpdateComments(int articleId)
        {
            var comments = KitBL.Instance.Comments.GetWithUsers(articleId);
            return PartialView("CommentsPartialView", comments);
        }

        [HttpPost]
        public ActionResult AsyncUpdateArticleRemove(int ArticleId)
        {
            try
            {
                if (BaseMVC.IsAdmin())
                {
                    KitBL.Instance.Articles.Remove(ArticleId);
                    return Json(new { success = true });
                }
            }
            catch
            {
                //log error
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public ActionResult AsyncUpdateArticles()
        {
            var articles = KitBL.Instance.Articles.GetAll();
            return PartialView("ArticlesPV", articles);
        }

        public ActionResult Category(int id, string title = null)
        {
            List<QuestionBL> questions = KitBL.Instance.Questions.GetAll();

            return View(questions);
        }
    }
}

