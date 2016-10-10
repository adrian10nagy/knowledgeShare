using Softvision.All.Helpers.Base;
using Softvision.BL.Crud;
using Softvision.BL.Entities;
using Softvision.BL.Helpers;
using Softvision.BL.TrueEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Softvision.All.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            List<QuestionBL> questions = KitBL.Instance.Questions.GetAll();
            ViewData["questions"] = questions;

            return View();
        }

        public ActionResult Details(int id, string title = null)
        {
            QuestionBL question = KitBL.Instance.Questions.Get(id);
            if (question == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (title == null)
            {
                string url = Request.Url.AbsolutePath;
                var qTitle = question.Title.Replace(" ", "-");
                //calls 2 times the view, try change it to RedirectToAction?
                Response.Redirect(url + "/" + qTitle);
            }
            question.Answers = KitBL.Instance.Answers.GetAll(id);
            question.User = KitBL.Instance.Users.GetById(question.IdUser);

            return View(question);
        }

        public ActionResult Subcategory(int id, string title = null)
        {
            List<QuestionBL> questions = KitBL.Instance.Questions.GetBySubcategoryId(id);

            if (questions.Count == 0)
            {
                return RedirectToAction("Index", "Question");
            }
            ViewData["questions"] = questions;

            return View();
        }

        public ActionResult Create()
        {
            var categories = KitBL.Instance.Categories.GetAll();
            ViewData["categories"] = categories;

            var subCategories = KitBL.Instance.SubCategories.GetAll();
            ViewData["subcategories"] = subCategories;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Softvision.BL.Entities.QuestionBL pQuestion)
        {
            try
            {
                var idSubCategory = Request.Form["drpSubCategory"];
                var questionBody = Request.Form["editorTexarea"];

                if (idSubCategory != null || questionBody != null)
                {
                    pQuestion.CreatedDate = DateTime.Now;
                    pQuestion.IdSubCategory = idSubCategory.ToInt();
                    pQuestion.InternalRep = questionBody;
                    pQuestion.HTMLRep= TrueEditor.GenerateHTML(questionBody);
                    
                    if (BaseMVC.getUserId() != 0)
                    {
                        pQuestion.IdUser = BaseMVC.getUserId();
                    }
                    KitBL.Instance.Questions.Insert(pQuestion);
                }

                return RedirectToAction("Index", "Question");
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
            var question = KitBL.Instance.Questions.Get(id);

            return View(question);
        }

        [HttpPost]
        public ActionResult Edit(int id, Softvision.BL.Entities.QuestionBL pQuestion)
        {
            try
            {
                // TODO: If user has access + XSS
                var editorTexareaText = Request.Form["editorTexarea"];

                if (editorTexareaText != null || editorTexareaText != string.Empty)
                {
                    var editorText = editorTexareaText.ToString();
                    pQuestion.InternalRep = editorText;
                    pQuestion.HTMLRep = TrueEditor.GenerateHTML(editorText);
                    pQuestion.CreatedDate = DateTime.Now;
                    pQuestion.IdSubCategory = Request.Form["drpSubCategory"].ToInt();
                    if (BaseMVC.getUserId() != 0)
                    {
                        pQuestion.IdUser = BaseMVC.getUserId();
                    }

                    KitBL.Instance.Questions.Update(pQuestion);
                }
                return RedirectToAction("MyQuestions", "User");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public ActionResult InsertAnswer(AnswerBL answer)
        {
            try
            {
                answer.HTMLRep = TrueEditor.GenerateHTML(answer.Body);
                if (BaseMVC.getUserId() != 0)
                {
                    answer.IdUser = BaseMVC.getUserId();
                }
                else if (answer.AnonymousEmail == null || answer.AnonymousName == null)
                {
                    return Json(new { success = false, annonymousCredentials = false });
                }

                var newUserId = KitBL.Instance.Answers.Insert(answer);
                if(newUserId > 0 )
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

        [HttpGet]
        public ActionResult AsyncUpdateAnswers(int questionId)
        {
            var comments = KitBL.Instance.Answers.GetAll(questionId);
            return PartialView("AnswersPartialView", comments);
        }

        [HttpPost]
        public ActionResult AsyncUpdateQuestionVote(int questionId, int typeId)
        {
            try
            {
                if (Session["questionVoted" + questionId] == null ||
                    (Session["questionVoted" + questionId].ToString() != null &&
                    int.Parse(Session["questionVoted" + questionId].ToString()) != typeId))
                {
                    KitBL.Instance.Questions.UpdateVote(questionId, typeId);
                    Session["questionVoted" + questionId] = typeId;
                    return Json(new { success = true, inserted = true });
                }
                return Json(new { success = true, inserted = false });
            }
            catch
            {
                //log error
            }
            return Json(new { success = false });
        }
        
            
        [HttpPost]
        public ActionResult AsyncUpdateAnswerVote(int answerId, int typeId)
        {
            try
            {
                if (Session["answerVoted" + answerId] == null || 
                    (Session["answerVoted" + answerId].ToString() != null && 
                    int.Parse(Session["answerVoted" + answerId].ToString()) != typeId))
                {
                    KitBL.Instance.Questions.UpdateAnswerVote(answerId, typeId);
                    Session["answerVoted" + answerId] = typeId;
                    return Json(new { success = true, inserted = true });
                }
                return Json(new { success = true, inserted = false });
            }
            catch
            {
                //log error
            }
            return Json(new { success = false });
        }

		[HttpPost]
		public ActionResult AsyncUpdateAnswerStatus(int answerId, AnswerFlags statusId, int questionId)
		{
			try
			{
                KitBL.Instance.Questions.UpdateAnswerStatus(answerId, (int)statusId, questionId);
				return Json(new { success = true });
			}
			catch
			{
				//log error
			}
			return Json(new { success = false });
		}


        [HttpPost]
        public ActionResult AsyncUpdateQuestionRemove(int questionId)
        {
            try
            {
                if (BaseMVC.IsAdmin())
                {
                    KitBL.Instance.Questions.Remove(questionId);
                    return Json(new { success = true});
                }
            }
            catch
            {
                //log error
            }
            return Json(new { success = false });
        }


        [HttpGet]
        public ActionResult AsyncUpdateQuestions()
        {
            var questions = KitBL.Instance.Questions.GetAll();
            return PartialView("QuestionsPV", questions);
        }

        public ActionResult Category(int id, string title = null)
        {
            List<ArticleBL> articles = KitBL.Instance.Articles.GetAll();
            
            return View(articles);
        }
    }
}
