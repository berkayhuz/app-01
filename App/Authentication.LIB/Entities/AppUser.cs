using Authentication.LIB.Enums;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Authentication.LIB.Entities
{
	public class AppUser : IdentityUser<Guid>
	{
		public string FirstName { get; set; } = "İsim";
		public string LastName { get; set; } = "Soyisim";
		public string Slug { get; set; } = string.Empty;
		public string? EmailVerificationCode { get; set; }
		public DateTime? BirthDate { get; set; }
		public Gender Gender { get; set; }
		public bool Newsteller { get; set; } = true;
		public bool KvkkPolicy { get; set; } = true;
		public bool EMessage { get; set; } = true;
		public bool ExpressConsentText { get; set; } = true;
		[JsonIgnore]
		public ICollection<Address> Addresses { get; set; }

		public AppUser(string email, bool expressConsentText, bool kvkkPolicy)
		{
			UserName = email;
			Email = email;
			ExpressConsentText = expressConsentText;
			KvkkPolicy = kvkkPolicy;
			if (string.IsNullOrEmpty(Slug))
			{
				Slug = "user-" + Guid.NewGuid().ToString("N").ToLower();
			}
			Addresses = new HashSet<Address>();
		}
	}
}