using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LookatBackend.Services
{
    public class EmailService
    {
        private readonly string _smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
        private readonly int _smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
        private readonly string _smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
        private readonly string _smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                // Configure the SMTP client
                using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    smtpClient.EnableSsl = true;

                    // Create the email message
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUsername), // Use the SMTP username as the "from" address
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false // Set to true if the email body contains HTML
                    };

                    mailMessage.To.Add(toEmail);

                    // Send the email asynchronously
                    await smtpClient.SendMailAsync(mailMessage);

                    // Log success
                    Console.WriteLine("Email sent successfully to " + toEmail);
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP error: {smtpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                throw;
            }
        }
    }
}