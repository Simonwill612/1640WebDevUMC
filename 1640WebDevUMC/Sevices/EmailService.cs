using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_configuration["EmailSettings:SenderName"], _configuration["EmailSettings:SenderEmail"])); // Sender's email address and name from configuration
        emailMessage.To.Add(new MailboxAddress("", email)); // Recipient's email address
        emailMessage.Subject = subject; // Email subject

        // Create the body part of the email
        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = message; // HTML message body

        emailMessage.Body = bodyBuilder.ToMessageBody(); // Set the message body

        // Connect to the SMTP server and send the email
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]), false); // SMTP server address and port from configuration
            await client.AuthenticateAsync(_configuration["EmailSettings:UserName"], _configuration["EmailSettings:Password"]); // Your email credentials from configuration
            await client.SendAsync(emailMessage); // Send the email
            await client.DisconnectAsync(true); // Disconnect from the SMTP server
        }
    }
}
