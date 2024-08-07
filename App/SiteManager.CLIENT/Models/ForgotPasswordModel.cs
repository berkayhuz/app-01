using System.ComponentModel.DataAnnotations;

namespace SiteManager.CLIENT.Models
{
	public class ForgotPasswordModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}

}
