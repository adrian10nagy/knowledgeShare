using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ICommentRepository
    {
        Comment GetCommentById(int id);
        List<Comment> GetCommentsByArticleById(int id);
        void InsertComment(Comment comment);
        void RemoveComment(int commentId);
    }

    public partial class Repository : ICommentRepository
    {
        #region Get

        public Comment GetCommentById(int id)
        {
            throw new NotImplementedException();

            //return new Comment();

        }

        public List<Comment> GetCommentsByArticleById(int id)
        {
            List<Comment> comments = new List<Comment>();
            _dbRead.Execute(
                "CommentsGetByArticleId",
            new[] { new SqlParameter("@articleId", id) },
                r => comments.Add(new Comment()
                {
                    Id = Read<int>(r, "Id"),
                    TextField = Read<string>(r, "TextField"),
                    CreatedDate = Read<DateTime>(r, "CreatedDate"),
                    Vote = Read<int>(r, "Vote"),
                    Flags = Read<CommentsFlags>(r, "Flags"),
                    IdUser = Read<int>(r, "Id_usr"),
                    IdArticle = Read<int>(r, "Id_art"),

                }));

            return comments;
        }

        #endregion

        #region Insert

        public void InsertComment(Comment comment)
        {
            _dbRead.Execute(
                "CommentInsert",
                new[] { 
                    new SqlParameter("@IdArticle", comment.IdArticle), 
                    new SqlParameter("@IdUser", comment.IdUser), 
                    new SqlParameter("@TextField", comment.TextField), 
                    new SqlParameter("@CreatedDate", comment.CreatedDate)
            }, null);

        }

        #endregion


        #region Delete

        public void RemoveComment(int commentId)
        {
            _dbRead.Execute(
                "CommentRemove",
                new[] { 
                        new SqlParameter("@CommentId", commentId)
                    }, null
            );
        }

        #endregion

    }
}
