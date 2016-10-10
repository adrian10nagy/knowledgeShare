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
    class BLToDALMapper
    {
        public BLToDALMapper()
        {
            Mapper.CreateMap<ArticleBL, Article>();
            Mapper.CreateMap<AnswerBL, Answer>();
            Mapper.CreateMap<QuestionBL, Question>();
            Mapper.CreateMap<CategoryBL, Category>();
            Mapper.CreateMap<SubCategoryBL, SubCategory>();
            Mapper.CreateMap<CommentBL, Comment>();
            Mapper.CreateMap<UserBL, User>();
            Mapper.CreateMap<Softvision.BL.Entities.AnswerFlags, DAL.Entities.AnswerFlags>();
            Mapper.CreateMap<EmailTemplateBL, EmailTemplate>();

        }

        #region singleModelMappings

        public Article MapArticle(ArticleBL article)
        {
            return Mapper.Map<ArticleBL, Article>(article);
        }

        public Answer MapAnswer(AnswerBL answer)
        {
            return Mapper.Map<AnswerBL, Answer>(answer);
        }

        public Category MapCategory(CategoryBL category)
        {
            return Mapper.Map<CategoryBL, Category>(category);
        }

        public Comment MapComment(CommentBL comment)
        {
            return Mapper.Map<CommentBL, Comment>(comment);
        }

        public Question MapQuestion(QuestionBL question)
        {
            return Mapper.Map<QuestionBL, Question>(question);
        }

        public SubCategory MapSubCategory(SubCategoryBL subCategory)
        {
            return Mapper.Map<SubCategoryBL, SubCategory>(subCategory);
        }

        public User MapUser(UserBL user)
        {
            return Mapper.Map<UserBL, User>(user);
        }

        public EmailTemplate MapEmailTemplate(EmailTemplateBL emailTemplate)
        {
            return Mapper.Map<EmailTemplateBL, EmailTemplate>(emailTemplate);
        } 
        #endregion

    }
}
