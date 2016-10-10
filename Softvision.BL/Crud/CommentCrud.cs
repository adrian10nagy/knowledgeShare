using DAL.Entities;
using Softvision.BL.Entities;
using Softvision.BL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Crud
{
    public class CommentCrud
    {
        public List<CommentBL> GetAll(int articleId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Comment> comment = DAL.SDK.Kit.Instance.Comments.GetCommentsByArticleById(articleId);
            List<CommentBL> mappedComments = poMapper.MapCommentCollection(comment).ToList();
            return mappedComments;
        }

        public List<CommentBL> GetWithUsers(int articleId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Comment> comments = DAL.SDK.Kit.Instance.Comments.GetCommentsByArticleById(articleId);
            for (int i = 0; i < comments.Count(); i++)
            {
                comments[i].User = DAL.SDK.Kit.Instance.Users.GetUserById(comments[i].IdUser);
            } 
            List<CommentBL> mappedComments = poMapper.MapCommentCollection(comments).ToList();

            return mappedComments;
        }

        public int Insert(CommentBL comment)
        {
            var insertNewUser = 0;
            BLToDALMapper poMapper = new BLToDALMapper();
            Comment mappedComment = poMapper.MapComment(comment);

            if (mappedComment.IdUser == 0 )
            {
                mappedComment.IdUser = DAL.SDK.Kit.Instance.Users.Insert(new User
                {
                    Email = comment.AnonymousEmail,
                    FirstName = comment.AnonymousName,
                    LastName = "Guest",
                    Flags = UserFlags.NotConfirmed,
                    JoinDate = DateTime.Now,
                    UserType = UserType.Prospect,
                    PasswordHashed = "password"
                });
                insertNewUser = mappedComment.IdUser;
                Softvision.BL.Services.SMTP.sendEmail("info@knowledgeshare.ro", comment.AnonymousEmail, "Welcome " + comment.AnonymousName + " to Knowledgeshare.ro!", "Welcome! Please log in and continue contributing to our community! Your login email is: " + comment.AnonymousEmail);

                if (mappedComment.IdUser == 0)
                {
                    mappedComment.IdUser = 6;
                }
                else
                {
                    mappedComment.Flags = CommentsFlags.NotConfirmed;
                }
            }
            mappedComment.CreatedDate = DateTime.Now;
            DAL.SDK.Kit.Instance.Comments.InsertComment(mappedComment);

            //send email
            var article = DAL.SDK.Kit.Instance.Articles.GetArticleById(comment.IdArticle);
            var user = DAL.SDK.Kit.Instance.Users.GetUserById(article.IdUser);
            Softvision.BL.Services.SMTP.sendEmail("info@knowledgeshare.ro", user.Email, "New comment to your article!", user.FirstName + ", you have a new comment on on your aricle " + article.Title + ". You can check it out: " + comment.TextField);

            return insertNewUser;
        }

        public void Remove(int commentId)
        {
            DAL.SDK.Kit.Instance.Comments.RemoveComment(commentId);
        }
    }
}
