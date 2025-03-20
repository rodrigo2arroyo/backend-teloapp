using System.Net;
using System.Net.Mail;

namespace TeloApi.Features.User.Services;

public class EmailService(IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = false)
    {
        try
        {
            var smtpClient = new SmtpClient(configuration["Email:SmtpServer"])
            {
                Port = int.Parse(configuration["Email:SmtpPort"]),
                Credentials = new NetworkCredential(
                    configuration["Email:SmtpUser"], 
                    configuration["Email:SmtpPass"]
                ),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration["Email:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error enviando email: {ex.Message}");
            throw;
        }
    }
}