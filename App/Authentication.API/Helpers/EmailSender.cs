using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Authentication.API.Helpers
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

		private string LoadEmailTemplate(string templatePath)
		{
			try
			{
				using (var reader = new StreamReader(templatePath))
				{
					return reader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Failed to load email template from {TemplatePath}", templatePath);
				throw;
			}
		}

		public async Task SendEmailAsync(string email, string subject, string confirmationLink, string username, string templatePath = null)
		{
			try
			{
				string emailBody;

				if (templatePath != null)
				{
					emailBody = LoadEmailTemplate(templatePath);
					emailBody = emailBody.Replace("{{confirmationLink}}", confirmationLink);
					emailBody = emailBody.Replace("{{username}}", username);
				}
				else
				{
					emailBody = confirmationLink;
				}

				using (var client = new SmtpClient(_smtpServer, _smtpPort))
				{
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
					client.EnableSsl = true;

					var mailMessage = new MailMessage();
					mailMessage.From = new MailAddress(_smtpUsername);
					mailMessage.To.Add(email);
					mailMessage.Subject = subject;
					mailMessage.Body = emailBody;
					mailMessage.IsBodyHtml = true;

					await client.SendMailAsync(mailMessage);
				}
				Log.Information("Email sent to {Email}", email);
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Failed to send email to {Email}", email);
				throw;
			}
		}
	}
}
