using DAL.Entities;
using Softvision.BL.Entities;
using Softvision.BL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Softvision.BL.Helpers;

namespace Softvision.BL.Crud
{
    public class ArticleCrud
    {
        Article article = new Article();

        public List<ArticleBL> GetAll()
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Article> articles = DAL.SDK.Kit.Instance.Articles.GetAll();
            List<ArticleBL> mappedArticles = poMapper.MapArticleCollection(articles).ToList();
            return mappedArticles;
        }

        public List<ArticleBL> GetByMonthAndYear(int month=9, int year=0)
        {

            List<ArticleBL> mappedArticles = GetAll();

            if (month != 0)
            {
                mappedArticles = mappedArticles.Where(ar => ar.CreatedDate.Month == month).ToList();
            }

            if (year != 0)
            {
                mappedArticles = mappedArticles.Where(ar => ar.CreatedDate.Year == year).ToList();
            }

            return mappedArticles;
        }

        public List<ArticlesPerMonth> GetAllByMonth()
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Article> articles = DAL.SDK.Kit.Instance.Articles.GetAll();
            List<ArticleBL> mappedArticles = poMapper.MapArticleCollection(articles).ToList();

            var orderedArticles = mappedArticles.OrderBy(date => date.CreatedDate).ToList();

            List<ArticlesPerMonth> articlesperMonthArray = new List<ArticlesPerMonth>();
            List<ArticleBL> listArticle = new List<ArticleBL>();
            var month = 0;
            int year = 0;

            foreach (var item in mappedArticles)
            {
                if (year < item.CreatedDate.Year || month < item.CreatedDate.Month)
                {
                    if (listArticle.Count > 0)
                    {
                        articlesperMonthArray.Add(new ArticlesPerMonth()
                        {
                            articles = listArticle,
                            Month = month,
                            Year = year
                        });
                    }
                    listArticle = new List<ArticleBL>();
                }

                listArticle.Add(item);

                year = item.CreatedDate.Year;
                month = item.CreatedDate.Month;
            }

            articlesperMonthArray.Add(new ArticlesPerMonth()
            {
                articles = listArticle,
                Month = month,
                Year = year
            });

            return articlesperMonthArray;
        }

        public ArticleBL GetByCommentId(int questionId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            Article article = DAL.SDK.Kit.Instance.Articles.GetArticleByCommentId(questionId);
            ArticleBL mappedArticle = poMapper.MapArticle(article);
            return mappedArticle;
        }

        public List<ArticleBL> GetLatests(int nr)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Article> articles = DAL.SDK.Kit.Instance.Articles.GetLatestsArticles(nr);
            for (int i = 0; i < articles.Count(); i++)
            {
                articles[i].Comments = DAL.SDK.Kit.Instance.Comments.GetCommentsByArticleById(articles[i].Id);
                articles[i].Title = articles[i].Title.GetLiteralAndNumericalFromString();
            }
            List<ArticleBL> mappedArticles = poMapper.MapArticleCollection(articles).ToList();
            return mappedArticles;
        }

        public List<ArticleBL> GetByUserId(int userId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Article> articles = DAL.SDK.Kit.Instance.Articles.GetArticlesByUserId(userId);
            List<ArticleBL> mappedArticles = poMapper.MapArticleCollection(articles).ToList();
            return mappedArticles;
        }        

        public List<ArticleBL> GetBySubcategoryId(int SubcategoryId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Article> articles = DAL.SDK.Kit.Instance.Articles.GetArticlesBySubcategoryId(SubcategoryId);
            for (int i = 0; i < articles.Count(); i++)
            {
                articles[i].Comments = DAL.SDK.Kit.Instance.Comments.GetCommentsByArticleById(articles[i].Id);
            }
            List<ArticleBL> mappedArticles = poMapper.MapArticleCollection(articles).ToList();
            return mappedArticles;
        }

        public ArticleBL Get(int id)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            Article article = DAL.SDK.Kit.Instance.Articles.GetArticleById(id);
            ArticleBL mappedArticle = poMapper.MapArticle(article);
            return mappedArticle;
        }

        public ArticleBL GetRandom()
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            Article article = DAL.SDK.Kit.Instance.Articles.GetArticleRandom();
            ArticleBL mappedArticle = poMapper.MapArticle(article);
            return mappedArticle;
        }

        public void Insert(ArticleBL article)
        {
            BLToDALMapper poMapper = new BLToDALMapper();
            Article mappedArticle = poMapper.MapArticle(article);
            DAL.SDK.Kit.Instance.Articles.InsertArticle(mappedArticle);
        }

        public void UpdateVote(int articleId, int vote)
        {
            DAL.SDK.Kit.Instance.Articles.UpdateVote(articleId, vote);
        }

        public void UpdateCommentVote(int commentId, int vote)
        {
            DAL.SDK.Kit.Instance.Articles.UpdateCommentVote(commentId, vote);
        }

        public void Update(ArticleBL article)
        {
            BLToDALMapper poMapper = new BLToDALMapper();
            Article mappedArticle = poMapper.MapArticle(article);
            DAL.SDK.Kit.Instance.Articles.UpdateArticle(mappedArticle);
        }

        public void Remove(int articleId)
        {
            DAL.SDK.Kit.Instance.Articles.Remove(articleId);
        }
    }
}
