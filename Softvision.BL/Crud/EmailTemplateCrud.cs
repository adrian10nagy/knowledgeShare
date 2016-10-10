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
    public class EmailTemplateCrud
    {

        public EmailTemplateBL GetByName(string emailTemplateName)
        {
            DALToBLMapper poMapper = new DALToBLMapper();
            EmailTemplate emailTemplate = DAL.SDK.Kit.Instance.EmailTemplates.GetEmailTemplateByName(emailTemplateName);
            EmailTemplateBL mappedEmailTemplate = poMapper.MapEmailTemplate(emailTemplate);
            return mappedEmailTemplate;
        }
    }
}
