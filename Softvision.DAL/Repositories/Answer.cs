using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAnswerRepository
    {
        Answer GetAnswerById(int questionId);
        List<Answer> GetAnswers(int id);
        int InsertAnswer(Answer answer);
        void RemoveAnswer(int id);
    }

    public partial class Repository: IAnswerRepository
	{
		#region Get

		public Answer GetAnswerById(int id)
        {
            Answer answer = null;
            _dbRead.Execute(
                "AnswerGetById",
            new[] { new SqlParameter("@Id", id) },
                r => answer = new Answer()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<AnswerFlags>(r, "Flags"),
                    IdUser = Read<int>(r, "Id_usr"),
                    HTMLRep = Read<string>(r, "HtmlRep"),
                    InternalRep = Read<string>(r, "InternalRep")
                });
            return answer;
        }

        public List<Answer> GetAnswers(int questionId)
        {
            List<Answer> answer = new List<Answer>();
            _dbRead.Execute(
                "AnswersGetAll",
            new[] { new SqlParameter("@QuestionId", questionId) },
                r => answer.Add(new Answer()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<AnswerFlags>(r, "Flags"),
                    IdUser = Read<int>(r, "Id_usr"),
                    UserFirstName = Read<string>(r, "FirstName"),
                    UserLastName = Read<string>(r, "LastName"),
                    IdUserQuestion = Read<int>(r, "IdUserQuestion"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    IdQuestion = questionId
                }));

            return answer;
        }

		#endregion

		#region Insert

		public int InsertAnswer(Entities.Answer answer)
        {
            int answerId = 0;
            _dbRead.Execute(
				"AnswerInsert",
                new[] { 
					new SqlParameter("@CreatedDate", answer.CreatedDate), 
					new SqlParameter("@Vote", answer.Vote), 
					new SqlParameter("@Flags", answer.Flags),
					new SqlParameter("@IdUser", answer.IdUser), 
					new SqlParameter("@Id_qst", answer.IdQuestion),
					new SqlParameter("@HtmlRep", answer.HTMLRep),
					new SqlParameter("@InternalRep", answer.InternalRep)
				},
                r => answerId = Read<int>(r, "Id")
            );
            return answerId;
        }

        #endregion

        #region Delete

        public void RemoveAnswer(int answerId)
        {
            _dbRead.Execute(
                "AnswerRemove",
            new[] { 
                new SqlParameter("@answerId", answerId), 
            }, null);
        }

        #endregion


    }
}
