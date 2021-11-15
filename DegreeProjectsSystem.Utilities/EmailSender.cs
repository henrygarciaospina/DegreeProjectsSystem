using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Threading.Tasks;
namespace DegreeProjectsSystem.Utilidades
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        public Task Execute(string email, string subject, string message)
        {
            MailMessage mm = new MailMessage();

            mm.To.Add(email);
            mm.Subject = subject;
            mm.Body = message;
            mm.From = new MailAddress("project.siglo@gmail.com");
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.sendgrid.net")
            {
                Port = 587,
                UseDefaultCredentials = true,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("apikey", "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")
            };

            return smtp.SendMailAsync(mm);
        }

    }
}
