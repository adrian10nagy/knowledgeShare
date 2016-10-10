using Softvision.BL.Crud;
using Softvision.BL.Entities;
using Softvision.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Softvision.All.Controllers
{
    public class CommonController : Controller
    {
        [HttpGet]
        public ActionResult AsyncUpdateSubCategory(int categoryId)
        {
            var subCategories = KitBL.Instance.SubCategories.GetAllByCategoryId(categoryId);
            return PartialView("SubCategoryPartialView", subCategories);
        }

        [HttpGet]
        public ActionResult AsyncUpdateQuestionVotePV(QuestionBL question)
        {
            return PartialView("QuestionVotePV", question);
        }

        [HttpGet]
        public ActionResult AsyncUpdateArticleVotePV(ArticleBL article)
        {
            return PartialView("ArticleVotesPV", article);
        }

        [HttpGet]
        public ActionResult AsyncUpdateTrueEditorView(string parseText)
        {
            string textToView = parseText;
            if (parseText.Length > 5)
            {
                textToView = Softvision.BL.TrueEditor.TrueEditor.GenerateHTML(parseText);
            }

            ViewData["textToView"] = textToView;
            return PartialView("_TrueEditorViewPV");
        }
    }
}