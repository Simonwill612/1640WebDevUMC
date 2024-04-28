using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailService : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"]));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = htmlMessage;

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]), false);
            await client.AuthenticateAsync(_configuration["EmailSettings:UserName"], _configuration["EmailSettings:Password"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
