using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.SDK
{
    public interface IKit : IDisposable
    {
        Answers Answers { get; }
        Articles Articles { get; }
        Comments Comments { get; }
        Questions Questions { get; }
        EmailTemplates EmailTemplates { get; }
        Users Users { get; }
        TrafficLogs TrafficLogs { get; }
    }

    public class Kit : IKit
    {
        private static Kit _instance = new Kit();
        public static Kit Instance { get { return _instance ?? getInstance(); } }

        static Kit getInstance()
        {
            return new Kit();
        }

        public Answers Answers { get { return new Answers(); } }
        public Articles Articles { get { return new Articles(); } }
        public Categories Categories { get { return new Categories(); } }
        public Comments Comments { get { return new Comments(); } }
        public Questions Questions { get { return new Questions(); } }
        public Users Users { get { return new Users(); } }
		public SubCategories SubCategories { get { return new SubCategories(); } }
        public TrafficLogs TrafficLogs { get { return new TrafficLogs(); } }
        public EmailTemplates EmailTemplates { get { return new EmailTemplates(); } }
        public void Dispose()
        { 
            throw new NotImplementedException();
        }
    }
}