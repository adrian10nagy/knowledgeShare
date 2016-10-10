using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Comment
    {
        public int Id;
        public string TextField;
        public DateTime CreatedDate;
        public int Vote;
        public CommentsFlags Flags;
        public string AnonymousName;
        public string AnonymousEmail;
        public int IdArticle;
        public int IdUser;

        public Article Article;
        public User User;

    }

    [Flags]
    public enum CommentsFlags
    {
        None = 0,
        Irreleavnt = 1,
        Blocked = 2,
        TopCommentCorrect = 4,
        NotConfirmed = 8,
    }
}