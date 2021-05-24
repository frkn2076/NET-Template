using Infra.Constants;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Notifier.Business.Implementation
{
    public class BusinessManager : IBusinessManager
    {
        private readonly SmtpClient _smtpClient;
        public BusinessManager()
        {
            _smtpClient = new SmtpClient()
            {
                Port = Convert.ToInt32(PrebuiltVariables.SMTPPort),
                Host = PrebuiltVariables.SMTPHost,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(PrebuiltVariables.SMTPFromMailAddress, PrebuiltVariables.SMTPFromMailPassword)
            };

        }
        private async Task SendMailAsync(string header, string subject, string body, params string[] toList)
        {
            await Task.Run(() => {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(PrebuiltVariables.SMTPFromMailAddress, header);

                toList.ToList().ForEach(to => mail.To.Add(to));

                mail.Subject = subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "<b>Test Mail</b><br>using <b>HTML</b>.";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;

                _smtpClient.SendMailAsync(mail);
            });
        }

        async Task IBusinessManager.SendMailAsync(string header, string subject, string body, params string[] toList) 
            => await SendMailAsync(header, subject, body, toList);
    }
}
