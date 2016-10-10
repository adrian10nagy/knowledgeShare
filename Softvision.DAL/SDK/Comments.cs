using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Comments
    {
        private static ICommentRepository _repository;

        static Comments()
        {
            _repository = new Repository();
        }

		#region Get

        public Entities.Comment GetCommentById(int id)
        {
            return _repository.GetCommentById(id);
        }

        public List<Entities.Comment> GetCommentsByArticleById(int id)
        {
            return _repository.GetCommentsByArticleById(id);
        }

		#endregion

		#region Insert

		public void InsertComment(Entities.Comment comment)
        {
            _repository.InsertComment(comment);
		}

		#endregion

        #region Delete

        public void RemoveComment(int commentId)
        {
            _repository.RemoveComment(commentId);
        }

        #endregion
    }
}
