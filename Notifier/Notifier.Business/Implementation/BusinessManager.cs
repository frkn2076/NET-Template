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
        private readonly string _fromMailAddress;
        public BusinessManager()
        {
            _fromMailAddress = Environment.GetEnvironmentVariable("FromMailAddress");
            var fromMailPassword = Environment.GetEnvironmentVariable("FromMailPassword");
            var fromMailCredentials = new NetworkCredential(_fromMailAddress, fromMailPassword);

            _smtpClient = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = false
            };
        }
        private async Task SendMail(string header, string subject, string body, params string[] toList)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(_fromMailAddress, header);

            toList.ToList().ForEach(to => mail.To.Add(to));

            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            await _smtpClient.SendMailAsync(mail);
        }

        async Task IBusinessManager.SendMail(string header, string subject, string body, params string[] toList) => await SendMail(header, subject, body, toList);
    }
}
