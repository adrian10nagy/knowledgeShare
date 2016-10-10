using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Answers
    {
        private static IAnswerRepository _repository;

        static Answers()
        {
            _repository = new Repository();
        }

		#region Get

		public Entities.Answer GetAnswerById(int id)
        {
            return _repository.GetAnswerById(id);
        }

        public List<Entities.Answer> GetAnswers(int id)
        {
            return _repository.GetAnswers(id);
        }

		#endregion

		#region Insert

		public int Insert(Entities.Answer answer)
        {
           return _repository.InsertAnswer(answer);
        }

		#endregion

        #region Delete

        public void RemoveAnswer(int id)
        {
            _repository.RemoveAnswer(id);

        }

        #endregion
    }
}
