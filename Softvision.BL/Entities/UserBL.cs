using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Entities
{
    public class UserBL
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public int isDeleted { get; set; }
        public UserFlagsBL Flags { get; set; }
        public UserTypeBL UserType { get; set; }
        public EmailPreferenceBL EmailPreference { get; set; }
    }

    [Flags]
    public enum UserFlagsBL
    {
        Default = 0,
        Blocked = 1,
        NotConfirmed = 2
    }

    public enum UserTypeBL
    {
        Admin = 1,
        Author = 2,
        Member = 3,
        Prospect = 4
    }

    [Flags]
    public enum EmailPreferenceBL
    {
        None = 0,
        All = 1,
        NoArticles = 2,
        NoQuestions = 4,
        NoAnswers = 8,
        NoComments = 16,
        NoNewsletter = 32,
    }
}
