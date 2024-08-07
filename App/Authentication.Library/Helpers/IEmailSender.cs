namespace Authentication.Library.Helpers
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string message);
	}

}
