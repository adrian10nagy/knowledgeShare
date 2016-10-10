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
    public class QuestionCrud
    {
        Question article = new Question();

        public List<QuestionBL> GetAll()
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Question> questions = DAL.SDK.Kit.Instance.Questions.GetAll();
            List<QuestionBL> mappedQuestions = poMapper.MapQuestionCollection(questions).ToList();
            return mappedQuestions;
        }

        public QuestionBL GetByAnswerId(int id)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            Question questions = DAL.SDK.Kit.Instance.Questions.GetQuestionByAnswerId(id);
            QuestionBL mappedQuestion = poMapper.MapQuestion(questions);
            return mappedQuestion;
        }

        public QuestionBL Get(int id)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            Question questions = DAL.SDK.Kit.Instance.Questions.GetQuestionById(id);
            QuestionBL mappedQuestion = poMapper.MapQuestion(questions);
            return mappedQuestion;
        }

        public List<QuestionBL> GetLatests(int nr)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Question> questions = DAL.SDK.Kit.Instance.Questions.GetLatestQuestions(nr);
            for (int i = 0; i < questions.Count(); i++)
            {
                questions[i].Answers = DAL.SDK.Kit.Instance.Answers.GetAnswers(questions[i].Id);
                questions[i].Title = questions[i].Title.GetLiteralAndNumericalFromString();
            }
            List<QuestionBL> mappedQuestion = poMapper.MapQuestionCollection(questions).ToList();
            return mappedQuestion;
        }

        public List<QuestionBL> ContributedBy(int userId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Question> questions = DAL.SDK.Kit.Instance.Questions.ContributedBy(userId);
            List<QuestionBL> mappedQuestion = poMapper.MapQuestionCollection(questions).ToList();
            return mappedQuestion;
        }

        public List<QuestionBL> GetByUserId(int userI)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Question> questions = DAL.SDK.Kit.Instance.Questions.GetByUserId(userI);
            List<QuestionBL> mappedQuestion = poMapper.MapQuestionCollection(questions).ToList();
            return mappedQuestion;
        }

        public List<QuestionBL> GetBySubcategoryId(int subcategoryId)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Question> questions = DAL.SDK.Kit.Instance.Questions.GetQuestionsBySubcategoryId(subcategoryId);
            List<QuestionBL> mappedQuestion = poMapper.MapQuestionCollection(questions).ToList();
            return mappedQuestion;
        }
        
        public void Insert(QuestionBL question)
        {
            BLToDALMapper poMapper = new BLToDALMapper();
            Question mappedQuestion = poMapper.MapQuestion(question);
            DAL.SDK.Kit.Instance.Questions.InsertQuestion(mappedQuestion);
        }

        public void Update(QuestionBL question)
        {
            BLToDALMapper poMapper = new BLToDALMapper();
            Question mappedQuestion = poMapper.MapQuestion(question);
            DAL.SDK.Kit.Instance.Questions.UpdateQuestion(mappedQuestion);
        }


        public void UpdateVote(int questionId, int vote)
        {
            DAL.SDK.Kit.Instance.Questions.UpdateVote(questionId, vote);
        }

        public void UpdateAnswerVote(int answerId, int vote)
        {
            DAL.SDK.Kit.Instance.Questions.UpdateAnswerVote(answerId, vote);
        }

        public void UpdateAnswerStatus(int answerId, int status, int questionId)
        {
            DAL.SDK.Kit.Instance.Questions.UpdateAnswerStatus(answerId, (DAL.Entities.AnswerFlags)status, questionId);
        }

        public void Remove(int questionId)
        {
            DAL.SDK.Kit.Instance.Questions.Remove(questionId);
        }
    }
}
