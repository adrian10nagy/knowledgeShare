using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SDK
{
    public class EmailTemplates
    {
         private static IEmailTemplateRepository _repository;

         static EmailTemplates()
        {
            _repository = new Repository();
        }

		public EmailTemplate GetEmailTemplateById(int id)
        {
            return _repository.GetEmailTemplateById(id);
        }

        public EmailTemplate GetEmailTemplateByName(string name)
        {
            return _repository.GetEmailTemplateByName(name);
        }
    }
}
