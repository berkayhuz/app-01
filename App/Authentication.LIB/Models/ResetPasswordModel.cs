using System.ComponentModel.DataAnnotations;

namespace Authentication.LIB.Models
{
	public class ResetPasswordModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Token { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
