using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailSender : IEmailSender
    {
        MailMessage mailMessage;
        public Task Sender(string mail, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential("Mail","Password");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp@gmail.com";
            smtpClient.Host = "smtp@hotmail.com";
            smtpClient.EnableSsl = true;
            mailMessage.To.Add(mail);
            mailMessage.From = new MailAddress("Mail", mail);
            mailMessage.Subject = "Sistemimize Hoş geldiniz.";
            mailMessage.Body = "Rent A Car'ı tercih ettiğiniz için teşekkür ederiz. Hoş geldiniz.!!!";
            smtpClient.Send(mailMessage);
            return Task.CompletedTask;
        }
    }
}
