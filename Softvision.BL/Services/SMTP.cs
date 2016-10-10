using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Services
{
    public static class SMTP
    {
        public static bool sendEmail(string from, string destination, string subject, string body)
        {
            try
            {
                MailMessage objeto_mail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.Host = "smtp.knowledgeshare.ro";
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("info@knowledgeshare.ro", "share1");
                objeto_mail.From = new MailAddress(from);
                objeto_mail.To.Add(new MailAddress(destination));
                objeto_mail.To.Add(new MailAddress("info@knowledgeshare.ro"));
                objeto_mail.Subject = subject;
                objeto_mail.Body = body;
                client.Send(objeto_mail);

                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
