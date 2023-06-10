using CyprusAirportTransfer.App.DTOs.Email;
using CyprusAirportTransfer.App.Interfaces;
using System.Net.Mail;

namespace CyprusAirportTransfer.Infrastructure.Services
{
    public class EmailService : IEmailService
    { 
        public async Task SendAsync(EmailRequestDto request)
        {
            var emailClient = new SmtpClient("localhost");
            var message = new MailMessage
            {
                From = new MailAddress(request.From),
                Subject = request.Subject,
                Body = request.Body
            };
            message.To.Add(new MailAddress(request.To));
            await emailClient.SendMailAsync(message);
        }
    }
}
