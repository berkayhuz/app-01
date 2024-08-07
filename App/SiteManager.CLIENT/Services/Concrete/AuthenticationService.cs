
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using SiteManager.CLIENT.Models;
using System.Security.Claims;
using System.Text;

namespace SiteManager.CLIENT.Services.Concrete
{
    public class AuthenticationService : SiteManager.CLIENT.Services.Abstractions.IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, ILogger<AuthenticationService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticationAPI");
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<string> LoginAsync(LoginModel model)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/account/login", content);

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("API Response: {ResponseContent}", responseContent);

                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                    if (tokenResponse?.Token == null)
                    {
                        _logger.LogWarning("Token is null in the API response.");
                        throw new Exception("Token is null");
                    }

                    // Token'ı claim olarak ekle
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                        new Claim("Token", tokenResponse.Token)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    
                    return tokenResponse.Token;
                }
                else
                {
                    _logger.LogWarning("Login failed: {ResponseContent}", responseContent);
                    throw new Exception("Login failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in");
                throw;
            }
        }




        public async Task<bool> ChangeEmailAsync(string newEmail, string token)
        {
            var changeEmailModel = new { NewEmail = newEmail };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(changeEmailModel), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "authentication/changeemail")
            {
                Content = content
            };
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            _logger.LogDebug("Sending change email request for {NewEmail}", newEmail);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogDebug("Change email request succeeded for {NewEmail}", newEmail);
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Change email request failed for {NewEmail}: {ErrorContent}", newEmail, errorContent);
                return false;
            }
        }

        public async Task<bool> ConfirmChangeEmailAsync(string token, string newEmail)
        {
            var url = $"authentication/confirmchangeemail?token={token}&newEmail={newEmail}";

            _logger.LogDebug("Sending confirm change email request for {NewEmail}", newEmail);

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogDebug("Confirm change email request succeeded for {NewEmail}", newEmail);
                return true;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Confirm change email request failed for {NewEmail}: {ErrorContent}", newEmail, errorContent);
                return false;
            }
        }
    }
}
