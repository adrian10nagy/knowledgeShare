using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Questions
    {
         private static IQuestionRepository _repository;

        static Questions()
        {
            _repository = new Repository();
        }

		#region Get

		public List<Question> GetLatestQuestions(int nr)
        {
            return _repository.GetLatestQuestions(nr);
        }

        public List<Question> ContributedBy(int userId)
        {
            return _repository.ContributedBy(userId);
        }

        public List<Question> GetByUserId(int userId)
        {
            return _repository.GetByUserId(userId);
        }
        
        public Entities.Question GetQuestionById(int id)
        {
            return _repository.GetQuestionById(id);
        }

        public Entities.Question GetQuestionByAnswerId(int id)
        {
            return _repository.GetQuestionByAnswerId(id);
        }
        
        public List<Entities.Question> GetAll()
        {
            return _repository.GetAllQuestions();
        }

		#endregion

		#region Insert

		public void InsertQuestion(Question question)
        {
            if (question.AnonymousEmail == null || question.AnonymousName == null)
            {
                question.AnonymousEmail = string.Empty;
                question.AnonymousName = string.Empty;
            }

            if (question.IdUser == 0)
            {
                if (question.AnonymousEmail != string.Empty || question.AnonymousName != string.Empty)
                {
                    question.IdUser = SDK.Kit.Instance.Users.Insert(new User
                    {
                        Email = question.AnonymousEmail,
                        FirstName = question.AnonymousName,
                        LastName = question.AnonymousName,
                        Flags = UserFlags.NotConfirmed,
                        JoinDate = DateTime.Now,
                        UserType = UserType.Prospect,
                        PasswordHashed = "password"
                    });
                    if (question.IdUser == 0)
                    {
                        question.IdUser = 6;
                    }
                    else
                    {
                        question.Flags = QuestionFlags.NotConfirmed;
                    }
                }
                else
                {
                    return;
                }
            }
            
            var insertedAnswerId = _repository.InsertQuestion(question);
        }

		#endregion

		#region Update

		public void UpdateVote(int questionId, int vote)
        {
            _repository.UpdateQuestionVote(questionId, vote);
        }

        public void UpdateAnswerVote(int answerId, int vote)
        {
            _repository.UpdateAnswerVote(answerId, vote);
        }

        public void UpdateAnswerStatus(int answerId, AnswerFlags status, int questionId)
        {
            _repository.UpdateAnswerStatus(answerId, status, questionId);
        }

        public void UpdateQuestion(Question question)
        {
            _repository.UpdateQuestion(question);
        }

        public List<Question> GetQuestionsBySubcategoryId(int subcategoryId)
        {
            return _repository.GetQuestionsBySubcategoryId(subcategoryId);
        }
		#endregion

		#region Delete

		public void Remove(int questionId)
        {
            _repository.RemoveQuestion(questionId);
        }

		#endregion

    }
}
