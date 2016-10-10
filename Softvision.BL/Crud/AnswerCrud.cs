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

    public class AnswerCrud
    {
        Answer answer = new Answer();

        public List<AnswerBL> GetAll(int id)
        {
            var answersOrdered = new List<AnswerBL>();
            DALToBLMapper poMapper = new DALToBLMapper();
            List<Answer> answers = DAL.SDK.Kit.Instance.Answers.GetAnswers(id);
            List<AnswerBL> mappedAnswers = poMapper.MapAnswerCollection(answers).ToList();
            if (answers.Count > 0)
            {
                var corrects = answers.Where(c => c.Flags == DAL.Entities.AnswerFlags.Correct).ToList();
                if (corrects.Count() > 0)
                {
                    int correctId = 0;
                    correctId = corrects.First().Id;

                    if (correctId != 0)
                    {
                        //this is not good but is needed
                        var correctAnswer = mappedAnswers.Where(c => c.Id == correctId).First();
                        correctAnswer.Flag = Entities.AnswerFlags.Correct;
                        answersOrdered.Add(correctAnswer);
                        answersOrdered.AddRange(mappedAnswers.Where(c => c.Id != correctId));

                        return answersOrdered;
                    }
                }
            }

            return mappedAnswers;
        }

        public ArticleBL Get(int id)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            Article article = DAL.SDK.Kit.Instance.Articles.GetArticleById(id);
            ArticleBL mappedArticle = poMapper.MapArticle(article);
            return mappedArticle;
        }

        public void Remove(int answerId)
        {
            DAL.SDK.Kit.Instance.Answers.RemoveAnswer(answerId);
        }

        public int Insert(AnswerBL answer)
        {
            var insertNewUser = 0;
            BLToDALMapper poMapper = new BLToDALMapper();
            Answer mappedAnswer = poMapper.MapAnswer(answer);

            if (mappedAnswer.IdUser == 0 && (mappedAnswer.AnonymousEmail != "" || mappedAnswer.AnonymousName != ""))
            {
                mappedAnswer.IdUser = DAL.SDK.Kit.Instance.Users.Insert(new User
                {
                    Email = mappedAnswer.AnonymousEmail,
                    FirstName = mappedAnswer.AnonymousName,
                    LastName = "Guest",
                    Flags = UserFlags.NotConfirmed,
                    JoinDate = DateTime.Now,
                    UserType = UserType.Prospect,
                    PasswordHashed = "password"
                });
                insertNewUser = mappedAnswer.IdUser;

                if (mappedAnswer.IdUser == 0)
                {
                    mappedAnswer.IdUser = 6;
                }
                else
                {
                    mappedAnswer.Flags = DAL.Entities.AnswerFlags.NotConfirmed;
                }
            }
            mappedAnswer.CreatedDate = DateTime.Now;

            //insert answer
            var answerId = DAL.SDK.Kit.Instance.Answers.Insert(mappedAnswer);

            //send email
            var question = DAL.SDK.Kit.Instance.Questions.GetQuestionById(answer.IdQuestion);
            var user = DAL.SDK.Kit.Instance.Users.GetUserById(question.IdUser);
            Softvision.BL.Services.SMTP.sendEmail("info@knowledgeshare.ro", user.Email, "New answer to your question! ", user.FirstName + ", you have a new answer on on your question");

            return insertNewUser;
        }
    }
}
