using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Softvision.BL.Entities
{
    public class ArticleBL
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Vote { get; set; }
        public ArticleFlags Flags { get; set; }
        public string AnonymousName { get; set; }
        public string AnonymousEmail { get; set; }
        public string InternalRep { get; set; }
        public string HTMLRep { get; set; }
        public LanguageBL Language { get; set; }
        //for ajax passing purpose only
        public int OrderNr { get; set; }

        public int IdUser { get; set; }
        public int IdSubCategory { get; set; }
        public string SubCategory { get; set; }

        public int IdCategory { get; set; }
        public List<CommentBL> Comments { get; set; }

        public UserBL User { get; set; }
    }

    [Flags]
    public enum ArticleFlags
    {
        TopArticle = 1,
        Irreleavnt = 2,
        Blocked = 4,
        NotConfirmed = 8
    }
}
