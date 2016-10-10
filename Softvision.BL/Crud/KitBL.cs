using Softvision.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Crud
{
    public interface IKit : IDisposable
    {
        ArticleCrud Articles { get; }
        QuestionCrud Questions { get; }
        CategoryCrud Categories { get; }
    }

    public class KitBL : IKit
    {
        private static KitBL _instance = new KitBL();
        public static KitBL Instance { get { return _instance ?? getInstance(); } }

        static KitBL getInstance()
        {
            return new KitBL();
        }

        public ArticleCrud Articles { get { return new ArticleCrud(); } }
        public AnswerCrud Answers { get { return new AnswerCrud(); } }
        public CategoryCrud Categories { get { return new CategoryCrud(); } }
        public CommentCrud Comments { get { return new CommentCrud(); } }
        public QuestionCrud Questions { get { return new QuestionCrud(); } }
        public SubCategoryCrud SubCategories { get { return new SubCategoryCrud(); } }
        public UserCrud Users { get { return new UserCrud(); } }
        public EmailTemplateCrud EmailTemplate { get { return new EmailTemplateCrud(); } }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
