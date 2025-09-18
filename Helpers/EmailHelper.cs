using KOG.ECommerce.Features.Common.Emails.DTOs;
using MailKit.Net.Smtp;
using MimeKit;
namespace KOG.ECommerce.Helpers
{
    public static class EmailHelper
    {
        public static async Task<EmailDTO> SendEmailAsync(List<string> toEmails, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("KOG", "sys@kog-eg.com"));

            
            foreach (var email in toEmails)
            {
                message.To.Add(new MailboxAddress("", email));
            }

            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("mail.kog-eg.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync("sys@kog-eg.com", "Welcome@303090#");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            EmailDTO emailDTO = new EmailDTO
            {
                Subject = subject,
                Body = body,
                EmailAdresses= toEmails,

            };
            return emailDTO;
        }
    }
}
