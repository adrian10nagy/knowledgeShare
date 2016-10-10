using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Entities
{
    public class QuestionBL
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Vote { get; set; }
        public QuestionFlags Flags { get; set; }
        public string AnonymousName { get; set; }
        public string AnonymousEmail { get; set; }
        public string HTMLRep { get; set; }
        public string InternalRep { get; set; }

        public int IdUser { get; set; }
        public int IdSubCategory { get; set; }
        public string SubCategory { get; set; }
        public int IdCategory { get; set; }

        public List<AnswerBL> Answers{ get; set; }

        public UserBL User { get; set; }
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
