using System.Net.Mail;
using System.Net;
using User_Microservices.IService;

namespace User_Microservices.Service
{
    public class EmailService :IEmailService
    {
        private readonly SmtpClient smtpClient;


        public EmailService()
        {
            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vikasghodke468@gmail.com", "Password"),

                EnableSsl = true

            };
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mailMessage = new MailMessage("vikasghodke468@gmail.com", to, subject, body)
            {
                IsBodyHtml = false
            };
            await smtpClient.SendMailAsync(mailMessage);
        }

    }
}

