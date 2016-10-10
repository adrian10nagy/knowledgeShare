using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Entities
{

    public class AnswerBL
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Vote { get; set; }
        public AnswerFlags Flag { get; set; }
        public string AnonymousName { get; set; }
        public string AnonymousEmail { get; set; }
        public int IdUser { get; set; }
        public int IdQuestion { get; set; }
        public string InternalRep { get; set; }
        public string HTMLRep { get; set; }
        public string Body{ get; set; }
        public string UserFirstName;
        public string UserLastName;
        //id of user who put the question
        public int IdUserQuestion { get; set; }
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
