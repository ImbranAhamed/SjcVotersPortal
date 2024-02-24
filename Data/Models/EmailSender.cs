using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SjcVotersPortal.Data.Models;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using var mailMessage = new MailMessage("admin@sjc.com", email);
        mailMessage.Subject = subject;
        mailMessage.Body = htmlMessage;
        mailMessage.IsBodyHtml = true;
            
        var smtp = new SmtpClient();
        smtp.Host = "localhost";
        smtp.Port = 25;
            
        smtp.Send(mailMessage);
        
        return Task.CompletedTask;
    }
}