using AutoMapper;
using DAL.Entities;
using Softvision.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Mappings
{
    class DALToBLMapper
    {
        public DALToBLMapper(){
            Mapper.CreateMap<Article, ArticleBL>();
            Mapper.CreateMap<Answer, AnswerBL>();
            Mapper.CreateMap<Category, CategoryBL>();
            Mapper.CreateMap<Comment, CommentBL>();
            Mapper.CreateMap<Question, QuestionBL>();
            Mapper.CreateMap<SubCategory, SubCategoryBL>();
            Mapper.CreateMap<User, UserBL>();
            Mapper.CreateMap<DAL.Entities.AnswerFlags, Softvision.BL.Entities.AnswerFlags>();
            Mapper.CreateMap<DAL.Entities.ArticleFlags, Softvision.BL.Entities.ArticleFlags>();
            Mapper.CreateMap<DAL.Entities.CommentsFlags, Softvision.BL.Entities.CommentsFlagBL>();
            Mapper.CreateMap<DAL.Entities.QuestionFlags, Softvision.BL.Entities.QuestionFlags>();
            Mapper.CreateMap<DAL.Entities.EmailTemplate, Softvision.BL.Entities.EmailTemplateBL>();
            Mapper.CreateMap<DAL.Entities.UserFlags, Softvision.BL.Entities.UserFlagsBL>();
            Mapper.CreateMap<DAL.Entities.UserType, Softvision.BL.Entities.UserTypeBL>();
        }

        #region singleModelMappings
        public AnswerBL MapAnswer(Answer answer)
        {
            return Mapper.Map<Answer, AnswerBL>(answer);
        }
        
        public ArticleBL MapArticle(Article article)
        {
            return Mapper.Map<Article, ArticleBL>(article);
        }

        public Softvision.BL.Entities.ArticleFlags MapArticleFlags(DAL.Entities.ArticleFlags articleFlags)
        {
            return Mapper.Map<DAL.Entities.ArticleFlags, Softvision.BL.Entities.ArticleFlags>(articleFlags);
        }

        public CategoryBL MapCategory(Category category)
        {
            return Mapper.Map<Category, CategoryBL>(category);
        }

        public CommentBL MapComment(Comment comment)
        {
            return Mapper.Map<Comment, CommentBL>(comment);
        }
        
        public QuestionBL MapQuestion(Question question)
        {
            return Mapper.Map<Question, QuestionBL>(question);
        }

        public SubCategoryBL MapSubCategory(SubCategory subCategory)
        {
            return Mapper.Map<SubCategory, SubCategoryBL>(subCategory);
        }

        public UserBL MapUser(User user)
        {
            return Mapper.Map<User, UserBL>(user);
        }

        public EmailTemplateBL MapEmailTemplate(EmailTemplate emailTemplate)
        {
            return Mapper.Map<EmailTemplate, EmailTemplateBL>(emailTemplate);
        }
        #endregion

        #region CollectionMapping
        public ICollection<ArticleBL> MapArticleCollection(ICollection<Article> article)
        {
            return article.Select(a => Mapper.Map<Article, ArticleBL>(a)).ToList();
        }

        public ICollection<AnswerBL> MapAnswerCollection(ICollection<Answer> answer)
        {
            return answer.Select(a => Mapper.Map<Answer, AnswerBL>(a)).ToList();
        }

        public ICollection<CategoryBL> MapCategoryCollection(ICollection<Category> category)
        {
            return category.Select(a => Mapper.Map<Category, CategoryBL>(a)).ToList();
        }
        
        public ICollection<CommentBL> MapCommentCollection(ICollection<Comment> comment)
        {
            return comment.Select(a => Mapper.Map<Comment, CommentBL>(a)).ToList();
        }

        public ICollection<QuestionBL> MapQuestionCollection(ICollection<Question> question)
        {
            return question.Select(a => Mapper.Map<Question, QuestionBL>(a)).ToList();
        }
        
        public ICollection<SubCategoryBL> MapSubCategoryCollection(ICollection<SubCategory> subCategory)
        {
            return subCategory.Select(a => Mapper.Map<SubCategory, SubCategoryBL>(a)).ToList();
        }

        public ICollection<UserBL> MapUserCollection(ICollection<User> users)
        {
            return users.Select(a => Mapper.Map<User, UserBL>(a)).ToList();
        }

        public ICollection<EmailTemplateBL> MapEmailTemplateCollection(ICollection<EmailTemplate> users)
        {
            return users.Select(a => Mapper.Map<EmailTemplate, EmailTemplateBL>(a)).ToList();
        }

        #endregion
    }
}
