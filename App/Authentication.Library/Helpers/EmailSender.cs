using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Authentication.Library.Helpers
{
	public class EmailSender : IEmailSender
	{
		private readonly string _smtpServer;
		private readonly int _smtpPort;
		private readonly string _smtpUsername;
		private readonly string _smtpPassword;

		public EmailSender(IConfiguration configuration)
		{
			_smtpServer = configuration["EmailSettings:SmtpServer"];
			_smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
			_smtpUsername = configuration["EmailSettings:SmtpUsername"];
			_smtpPassword = configuration["EmailSettings:SmtpPassword"];
		}

		public async Task SendEmailAsync(string email, string subject, string message)
		{
			using (var client = new SmtpClient(_smtpServer, _smtpPort))
			{
				client.UseDefaultCredentials = false;
				client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
				client.EnableSsl = true;

				var mailMessage = new MailMessage();
				mailMessage.From = new MailAddress(_smtpUsername);
				mailMessage.To.Add(email);
				mailMessage.Subject = subject;
				mailMessage.Body = message;
				mailMessage.IsBodyHtml = true;

				await client.SendMailAsync(mailMessage);
			}
		}
	}
}
