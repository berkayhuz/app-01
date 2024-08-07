namespace Authentication.API.Helpers
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string confirmationLink, string username,
			string templatePath = null);
	}

}
