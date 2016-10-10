using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class Articles
    {
        private static IArticleRepository _repository;

        static Articles()
        {
            _repository = new Repository();
        }

		#region Get

        public List<Article> GetAll()
        {
            return _repository.GetAllArticles();
        }

        public List<Article> GetLatestsArticles(int nr)
        {
            return _repository.GetLatestsArticles(nr);
        }

        public Article GetArticleById(int id)
        {
            return _repository.GetArticleById(id);
        }

        public Article GetArticleRandom()
        {
            return _repository.GetArticleRandom();
        }

        public Article GetArticleByCommentId(int id)
        {
            return _repository.GetArticleByCommentId(id);
        }

        public List<Article> GetArticlesBySubcategoryId(int subcategoryId)
        {
            return _repository.GetArticlesBySubcategoryId(subcategoryId);
        }

        public List<Article> GetArticlesByUserId(int userId)
        {
            return _repository.GetArticlesByUserId(userId);
        }
        
        #endregion

		#region Insert

		public void InsertArticle(Article article)
        {
            if (article.AnonymousEmail == null || article.AnonymousName == null)
            {
                article.AnonymousEmail = string.Empty;
                article.AnonymousName = string.Empty;
            }

            if (article.IdUser == 0)
            {
                if (article.AnonymousEmail != string.Empty || article.AnonymousName != string.Empty)
                {

                    article.IdUser = SDK.Kit.Instance.Users.Insert(new User
                    {
                        Email = article.AnonymousEmail,
                        FirstName = article.AnonymousName,
                        LastName = article.AnonymousName,
                        Flags = UserFlags.NotConfirmed,
                        JoinDate = DateTime.Now,
                        UserType = UserType.Prospect,
                        PasswordHashed = "password"
                    });
                    if (article.IdUser == 0)
                    {
                        article.IdUser = 6;
                    }
                    else
                    {
                        article.Flags = ArticleFlags.NotConfirmed;
                    }
                }
                else
                {
                    return;
                }
            }
            var insertedAnswerId = _repository.InsertArticle(article);
        }

		#endregion

		#region Update

        public void UpdateVote(int articleId, int vote)
        {
            _repository.UpdateArticleVote(articleId, vote);
        }

        public void UpdateCommentVote(int commentId, int vote)
        {
            _repository.UpdateCommentVote(commentId, vote);
        }

        public void UpdateArticle(Article article)
        {
            _repository.UpdateArticle(article);
        }

		#endregion

		#region delete
		
		public void Remove(int articleId)
        {
            _repository.RemoveArticle(articleId);
		}

		#endregion
	}
}
