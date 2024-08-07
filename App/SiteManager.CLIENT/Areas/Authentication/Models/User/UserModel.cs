using System.Text.Json.Serialization;

namespace SiteManager.CLIENT.Areas.Authentication.Models.User
{
    public class UserModel
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

    }

}
