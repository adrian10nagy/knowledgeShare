using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Entities
{
    public class CommentBL
    {
        public int Id { get; set; }
        public string TextField { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Vote { get; set; }
        CommentsFlagBL Flag { get; set; }
        public string AnonymousName { get; set; }
        public string AnonymousEmail { get; set; }
        public int IdArticle { get; set; }
        public int IdUser { get; set; }

        public ArticleBL Article { get; set; }
        public UserBL User { get; set; }

    }

    [Flags]
    public enum CommentsFlagBL
    {
        Irreleavnt = 1,
        Blocked = 2,
        TopCommentCorrect = 4,
        NotConfirmed = 8
    }
}
