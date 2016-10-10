using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DAL.Settings;

namespace DAL.Repositories
{
    public interface IArticleRepository
    {
        List<Article> GetAllArticles();
        Article GetArticleById(int id);
        Article GetArticleRandom();
        Article GetArticleByCommentId(int id);
        int InsertArticle(Article article);
        void UpdateArticleVote(int articleId, int vote);
        List<Article> GetLatestsArticles(int nr);
		void RemoveArticle(int articleId);
        void UpdateCommentVote(int commentId, int vote);
        void UpdateArticle(Article article);
        List<Article> GetArticlesBySubcategoryId(int subcategoryId);
        List<Article> GetArticlesByUserId(int userId);
    }

    public partial class Repository : IArticleRepository
    {
        #region Get

        public Article GetArticleById(int id)
        {
            Article article = null;
            _dbRead.Execute(
                "ArticleGetById",
            new[] { new SqlParameter("@Id", id) },
                r => article = new Article()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<ArticleFlags>(r, "Flags"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    SubCategory = Read<string>(r, "Name"),
                    IdCategory = Read<int>(r, "Id_ctg"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    Language = (Language)Read<int>(r, "LanguageId"),
                });
            return article;
        }


        public Article GetArticleRandom()
        {
            Article article = null;

            _dbRead.Execute(
                "ArticleGetRandom",
                null,
                r => article = new Article()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<ArticleFlags>(r, "Flags"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    SubCategory = Read<string>(r, "Name"),
                    IdCategory = Read<int>(r, "Id_ctg"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    Language = (Language)Read<int>(r, "LanguageId"),
                });

            return article;
        }


        public Article GetArticleByCommentId(int id)
        {
            Article article = null;

            _dbRead.Execute(
                "ArticleGetByCommentId",
            new[] { new SqlParameter("@CommentId", id) },
                r => article = new Article()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<ArticleFlags>(r, "Flags"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    Language = (Language)Read<int>(r, "LanguageId"),
                });

            return article;
        }

        public List<Article> GetAllArticles()
        {
            List<Article> articles = new List<Article>();
            _dbRead.Execute(
                "ArticlesGetAll",
            null,
                r => articles.Add(new Article()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<ArticleFlags>(r, "Flags"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    Language = (Language)Read<int>(r, "LanguageId"),
                }));

            return articles;
        }

        public List<Article> GetLatestsArticles(int nr)
        {
            List<Article> articles = new List<Article>();

            _dbRead.Execute(
                "ArticlesGetLatests",
            new[] { new SqlParameter("@Nr", nr) },
                r => articles.Add(new Article()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<ArticleFlags>(r, "Flags"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    Language = (Language)Read<int>(r, "LanguageId"),
                }));

            return articles;
        }

        public List<Article> GetArticlesBySubcategoryId(int subcategoryId)
        {
            List<Article> articles = new List<Article>();

            _dbRead.Execute(
                "ArticlesGetABySubcategory",
            new[] { new SqlParameter("@subCategoryId", subcategoryId) },
                r => articles.Add(new Article()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<ArticleFlags>(r, "Flags"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    Language = (Language)Read<int>(r, "LanguageId"),
                }));

            return articles;
        }

        public List<Article> GetArticlesByUserId(int userId)
        {
            List<Article> articles = new List<Article>();
            _dbRead.Execute(
                "ArticlesGetByUserId",
            new[] { new SqlParameter("@userId", userId) },
                r => articles.Add(new Article()
                {
                    Id = Read<int>(r, "Id"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<ArticleFlags>(r, "Flags"),
                    IdSubCategory = Read<int>(r, "Id_sctg"),
                    IdUser = Read<int>(r, "Id_usr"),
                    Title = Read<string>(r, "Title"),
                    HTMLRep = Read<string>(r, "HTMLRep"),
                    InternalRep = Read<string>(r, "InternalRep"),
                    Language = (Language)Read<int>(r, "LanguageId"),
                }));

            return articles;
        }
        
        #endregion

        #region Insert

        public int InsertArticle(Article article)
        {
            int articleId = 0;

            _dbRead.Execute(
                "ArticleInsert",
            new[] { 
                new SqlParameter("@CreatedDate", article.CreatedDate), 
                new SqlParameter("@Vote", article.Vote), 
                new SqlParameter("@Id_sctg", article.IdSubCategory), 
                new SqlParameter("@Flags", article.Flags), 
                new SqlParameter("@IdUser", article.IdUser), 
                new SqlParameter("@Title", article.Title), 
                new SqlParameter("@ArticleInternalRep", article.InternalRep), 
                new SqlParameter("@ArticleHtmlRep", article.HTMLRep), 
                new SqlParameter("@languageId", (int)article.Language), 
            },
                r => articleId = Read<int>(r, "Id")
            );
            return articleId;
        }

        #endregion

        #region Update

        public void UpdateArticleVote(int articleId, int vote)
        {
            _dbRead.Execute(
                "ArticleUpdateVote",
            new[] { 
                new SqlParameter("@Id", articleId),  
                new SqlParameter("@Vote", vote), 
            }, null);
        }

        public void UpdateCommentVote(int commentId, int vote)
        {
            _dbRead.Execute(
                "ArticleUpdateCommentVote",
            new[] { 
                new SqlParameter("@Id", commentId),  
                new SqlParameter("@Vote", vote), 
            }, null);
        }

        public void UpdateArticle(Article article)
        {
            _dbRead.Execute(
                "ArticleUpdate",
            new[] { 
                new SqlParameter("@Id", article.Id),  
                new SqlParameter("@title", article.Title),  
                new SqlParameter("@Id_sctg", article.IdSubCategory),  
                new SqlParameter("@InternalRep", article.InternalRep), 
                new SqlParameter("@HTMLRep", article.HTMLRep), 
            }, null);
        }

        #endregion

        #region Delete

        public void RemoveArticle(int articleId)
        {
            _dbRead.Execute(
                "ArticleRemove",
            new[] { 
                new SqlParameter("@Id", articleId), 
            }, null);
        }

        #endregion
    }

}