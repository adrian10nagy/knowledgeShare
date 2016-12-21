using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace DAL.Entities
{
    public class Answer
    {
        public int Id;
        public DateTime CreatedDate;
        public int Vote;
        public AnswerFlags Flags;
        public string AnonymousName;
        public string AnonymousEmail;
        public int IdUser;
        public int IdQuestion;
        public string InternalRep;
        public string HTMLRep;

        public int IdUserQuestion;
        public string UserFirstName;
        public string UserLastName;
    }

    [Flags]
    public enum AnswerFlags
    {
        Correct = 1,
        Irreleavnt = 2,
        Blocked = 4,
        NotConfirmed = 8
    }
}