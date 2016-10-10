using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IEmailTemplateRepository
    {
        EmailTemplate GetEmailTemplateById(int questionId);
        EmailTemplate GetEmailTemplateByName(string name);
    }

    public partial class Repository : IEmailTemplateRepository
    {
        public EmailTemplate GetEmailTemplateById(int id)
        {
            EmailTemplate emailTemplate = null;

            _dbRead.Execute(
                "EmailTemplateGetById",
            new[] { 
                new SqlParameter("@Id", id) 
            },
                r => emailTemplate = new EmailTemplate()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                    NrOfSubstitutes = Read<int>(r, "NrOfSubstitutes"),
                    Structure = Read<string>(r, "Structure")
                });

            return emailTemplate;
        }

        public EmailTemplate GetEmailTemplateByName(string name)
        {
            EmailTemplate emailTemplate = null;

            _dbRead.Execute(
                "EmailTemplateGetByName",
            new[] { 
                new SqlParameter("@name", name) 
            },
                r => emailTemplate = new EmailTemplate()
                {
                    Id = Read<int>(r, "Id"),
                    Name = Read<string>(r, "Name"),
                    NrOfSubstitutes = Read<int>(r, "NrOfSubstitutes"),
                    Structure = Read<string>(r, "Structure"),
                    Subject = Read<string>(r, "Subject")
                });

            return emailTemplate;
        }
    }
}
