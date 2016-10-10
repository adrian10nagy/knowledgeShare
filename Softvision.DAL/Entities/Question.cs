using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Question
    {
        public int Id;
        public string Title;
        public DateTime CreatedDate;
        public int Vote;
        public QuestionFlags Flags;
        public string AnonymousName;
        public string AnonymousEmail;
        public string HTMLRep;
        public string InternalRep;
        
        public int IdUser;
        public int IdSubCategory;
        public string SubCategory;
        public int IdCategory { get; set; }

        public List<Answer> Answers;
    }

    [Flags]
    public enum QuestionFlags
    {
        Answered = 1,
        Duplicate = 2,
        Blocked = 4,
        NotConfirmed = 8

    }
}
