using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public interface IQuestionRepository
	{
		Question GetQuestionById(int id);
        Question GetQuestionByAnswerId(int id);
		List<Question> GetAllQuestions();
		List<Question> GetLatestQuestions(int nr);
        List<Question> ContributedBy(int userId);
        List<Question> GetByUserId(int userId);
		int InsertQuestion(Question question);
		void UpdateQuestionVote(int questionId, int vote);
		void RemoveQuestion(int questionId);
		void UpdateAnswerVote(int answerId, int vote);
        void UpdateAnswerStatus(int answerId, AnswerFlags status, int questionId);
        void UpdateQuestion(Question question);
        List<Question> GetQuestionsBySubcategoryId(int subcategoryId);
	};

	public partial class Repository : IQuestionRepository
	{
		#region Retrive

		public Question GetQuestionById(int id)
		{
            Question question = null;
			_dbRead.Execute(
				"QuestionGetById",
			new[] { new SqlParameter("@Id", id) },
				r => question = new Question()
				{
					Id = Read<int>(r, "Id"),
					CreatedDate = Read<DateTime>(r, "CreatedDate"),
					Vote = Read<int>(r, "Vote"),
					Flags = Read<QuestionFlags>(r, "Flags"),
					IdUser = Read<int>(r, "Id_usr"),
					Title = Read<string>(r, "Title"),
					IdSubCategory = Read<int>(r, "Id_sctg"),
                    SubCategory = Read<string>(r, "Name"),
                    IdCategory = Read<int>(r, "Id_ctg"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
				});
			return question;
		}

        public Question GetQuestionByAnswerId(int id)
        {
            Question question = null;
            
            _dbRead.Execute(
                "QuestionGetByAnswerId",
            new[] { 
                new SqlParameter("@AnswerId", id) 
            },
                r => question = new Question()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<QuestionFlags>(r, "Flags"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                });

            return question;
        }

		public List<Question> GetAllQuestions()
		{
            List<Question> questions = new List<Question>();
			_dbRead.Execute(
				"QuestionsGetAll",
			null,
				r => questions.Add(new Question()
				{
					Id = Read<int>(r, "Id"),
					CreatedDate = Read<DateTime>(r, "CreatedDate"),
					Vote = Read<int>(r, "Vote"),
					Flags = Read<QuestionFlags>(r, "Flags"),
					IdSubCategory = Read<int>(r, "Id_sctg"),
					IdUser = Read<int>(r, "Id_usr"),
					Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
				}));

			return questions;
		}

		public List<Question> GetLatestQuestions(int nr)
		{
            List<Question> questions = new List<Question>();
			_dbRead.Execute(
				"QuestionsGetLatests",
			new[] { new SqlParameter("@Nr", nr) },
				r => questions.Add(new Question()
				{
					Id = Read<int>(r, "Id"),
					CreatedDate = Read<DateTime>(r, "CreatedDate"),
					Vote = Read<int>(r, "Vote"),
					Flags = Read<QuestionFlags>(r, "Flags"),
					IdSubCategory = Read<int>(r, "Id_sctg"),
					IdUser = Read<int>(r, "Id_usr"),
					Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
				}));

			return questions;
		}

        public List<Question> GetQuestionsBySubcategoryId(int subcategoryId)
        {
            List<Question> questions = new List<Question>();

            _dbRead.Execute(
                "QuestionsGetBySubcategoryId",
            new[] { new SqlParameter("@subcategoryId", subcategoryId) },
                r => questions.Add(new Question()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<QuestionFlags>(r, "Flags"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                }));

            return questions;
        }

        public List<Question> ContributedBy(int userId)
		{
            List<Question> questions = new List<Question>();
			_dbRead.Execute(
				"QuestionsGetContributedBy",
            new[] { new SqlParameter("@UserId", userId) },
				r => questions.Add(new Question()
				{
					Id = Read<int>(r, "Id"),
					CreatedDate = Read<DateTime>(r, "CreatedDate"),
					Vote = Read<int>(r, "Vote"),
					Flags = Read<QuestionFlags>(r, "Flags"),
					IdSubCategory = Read<int>(r, "Id_sctg"),
                    Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
				}));

			return questions;
		}

        public List<Question> GetByUserId(int userId)
		{
            List<Question> questions = new List<Question>();
			_dbRead.Execute(
				"QuestionsGetByUserId",
            new[] { new SqlParameter("@UserId", userId) },
				r => questions.Add(new Question()
				{
					Id = Read<int>(r, "Id"),
					CreatedDate = Read<DateTime>(r, "CreatedDate"),
					Vote = Read<int>(r, "Vote"),
					Flags = Read<QuestionFlags>(r, "Flags"),
					IdSubCategory = Read<int>(r, "Id_sctg"),
					Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
				}));

			return questions;
		}

		#endregion

		#region Insert
		public int InsertQuestion(Question question)
		{
			int questionId = 0;
			_dbRead.Execute(
				"QuestionInsert",
			new[] { 
                new SqlParameter("@CreatedDate", question.CreatedDate), 
                new SqlParameter("@Vote", question.Vote), 
                new SqlParameter("@Id_sctg", question.IdSubCategory), 
                new SqlParameter("@Flags", question.Flags), 
                new SqlParameter("@IdUser", question.IdUser), 
                new SqlParameter("@Title", question.Title), 
                new SqlParameter("@questionHtmlRep", question.HTMLRep), 
                new SqlParameter("@questionInternalRep", question.InternalRep), 
            },
				r => questionId = Read<int>(r, "Id")
			);
			return questionId;
		}


		#endregion

		#region Update

		public void UpdateQuestionVote(int questionId, int vote)
		{
			_dbRead.Execute(
				"QuestionUpdateVote",
			new[] { 
                new SqlParameter("@Id", questionId), 
                new SqlParameter("@Vote", vote), 
            }, null);
		}

		public void UpdateAnswerVote(int answerId, int vote)
		{
			_dbRead.Execute(
				"QuestionUpdateAnswerVote",
			new[] { 
                new SqlParameter("@Id", answerId), 
                new SqlParameter("@Vote", vote), 
            }, null);
		}

        public void UpdateAnswerStatus(int answerId, AnswerFlags status, int questionId)
		{
			_dbRead.Execute(
				"QuestionUpdateAnswerStatus",
				new[] { 
					new SqlParameter("@AnswerId", answerId), 
					 new SqlParameter("@Status", (int)status), 
					 new SqlParameter("@QuestionId", questionId), 
				}, null);
		}

        public void UpdateQuestion(Question question)
        {
            _dbRead.Execute(
                "QuestionUpdate",
            new[] { 
                new SqlParameter("@Id", question.Id),  
                new SqlParameter("@title", question.Title),  
                new SqlParameter("@Id_sctg", question.IdSubCategory),  
                new SqlParameter("@InternalRep", question.InternalRep), 
                new SqlParameter("@HTMLRep", question.HTMLRep), 
            }, null);
        }
		#endregion

		#region Delete
		public void RemoveQuestion(int questionId)
		{
			_dbRead.Execute(
				"QuestionRemove",
			new[] { 
                new SqlParameter("@Id", questionId), 
            }, null);
		}

		#endregion
	}
}
