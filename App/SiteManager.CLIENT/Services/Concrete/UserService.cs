using SiteManager.CLIENT.Areas.Authentication.Models.User;
using SiteManager.CLIENT.Services.Abstractions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SiteManager.CLIENT.Models;
using System.Security.Claims;

namespace SiteManager.CLIENT.Services.Concrete
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ILogger<UserService> _logger;

		public UserService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
		{
			_httpClient = httpClientFactory.CreateClient("AuthenticationAPI");
			_httpContextAccessor = httpContextAccessor;
			_logger = logger;
		}

		public async Task<UserModel> GetUserAsync()
		{
			var token = _httpContextAccessor.HttpContext.User.FindFirst("Token")?.Value;

			if (string.IsNullOrEmpty(token))
			{
				_logger.LogWarning("Token not found in user claims.");
				return null;
			}

			_logger.LogInformation("Token found: {Token}", token);

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.GetAsync("/api/Account/getuser");
			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				_logger.LogInformation("User data received: {Data}", responseContent);

				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				var userModel = JsonSerializer.Deserialize<UserModel>(responseContent, options);
				_logger.LogInformation("User data deserialized: {@User}", userModel);
				return userModel;
			}

			_logger.LogWarning("Failed to fetch user information. Status Code: {StatusCode}", response.StatusCode);
			return null;
		}

		public async Task<List<AddressModel>> GetAddressesAsync()
		{
			var token = _httpContextAccessor.HttpContext.User.FindFirst("Token")?.Value;

			if (string.IsNullOrEmpty(token))
			{
				_logger.LogWarning("Token not found in user claims.");
				return null;
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.GetAsync("/api/Address/addresses");
			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				_logger.LogInformation("Addresses data received: {Data}", responseContent);

				if (string.IsNullOrWhiteSpace(responseContent))
				{
					_logger.LogWarning("Empty response received from API.");
					throw new Exception("Adres bulunamadı");
				}

				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};

				try
				{
					if (responseContent.Contains("No addresses found"))
					{
						_logger.LogWarning("No addresses found in the response.");
						throw new Exception("Adres bulunamadı");
					}

					var addresses = JsonSerializer.Deserialize<List<AddressModel>>(responseContent, options);
					if (addresses == null || addresses.Count == 0)
					{
						_logger.LogWarning("No addresses found in the response.");
						throw new Exception("Adres bulunamadı");
					}

					_logger.LogInformation("Addresses data deserialized: {@Addresses}", addresses);
					return addresses;
				}
				catch (JsonException ex)
				{
					_logger.LogError(ex, "JSON deserialization error: {ResponseContent}", responseContent);
					throw new Exception("Geçersiz adres verisi alındı.");
				}
			}

			_logger.LogWarning("Failed to fetch addresses. Status Code: {StatusCode}", response.StatusCode);
			throw new Exception("Adresler getirilemedi. Lütfen tekrar deneyin.");
		}

		public async Task<AddressModel> GetAddressAsync(Guid id)
		{
			var token = _httpContextAccessor.HttpContext.User.FindFirst("Token")?.Value;

			if (string.IsNullOrEmpty(token))
			{
				_logger.LogWarning("Token not found in user claims.");
				return null;
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.GetAsync($"/api/Address/addresses/{id}");
			if (response.IsSuccessStatusCode)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				_logger.LogInformation("Address data received: {Data}", responseContent);

				var options = new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				};
				var address = JsonSerializer.Deserialize<AddressModel>(responseContent, options);
				_logger.LogInformation("Address data deserialized: {@Address}", address);
				return address;
			}

			_logger.LogWarning("Failed to fetch address. Status Code: {StatusCode}", response.StatusCode);
			return null;
		}

		public async Task<bool> DeleteAddressAsync(Guid id)
		{
			var token = _httpContextAccessor.HttpContext.User.FindFirst("Token")?.Value;

			if (string.IsNullOrEmpty(token))
			{
				_logger.LogWarning("Token not found in user claims.");
				return false;
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.DeleteAsync($"/api/Address/addresses/{id}");
			if (response.IsSuccessStatusCode)
			{
				_logger.LogInformation("Address deleted successfully. Address ID: {AddressId}", id);
				return true;
			}

			_logger.LogWarning("Failed to delete address. Status Code: {StatusCode}", response.StatusCode);
			return false;
		}

		public async Task<bool> AddAddressAsync(AddressModel model)
		{
			var token = _httpContextAccessor.HttpContext.User.FindFirst("Token")?.Value;

			if (string.IsNullOrEmpty(token))
			{
				_logger.LogWarning("Token not found in user claims.");
				return false;
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

			// Mevcut adres sayısını kontrol etmek için kullanıcı bilgilerini al
			var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
			if (string.IsNullOrEmpty(userEmail))
			{
				_logger.LogWarning("User email not found in user claims.");
				return false;
			}

			// Kullanıcının mevcut adreslerini getir
			var response = await _httpClient.GetAsync($"/api/Address/user/{userEmail}/addresses");
			if (!response.IsSuccessStatusCode)
			{
				_logger.LogWarning("Failed to retrieve user addresses. Status Code: {StatusCode}", response.StatusCode);
				return false;
			}

			var addressList = await response.Content.ReadAsStringAsync();
			var addresses = JsonSerializer.Deserialize<List<AddressModel>>(addressList);

			if (addresses != null && addresses.Count >= 5)
			{
				_logger.LogWarning("Address limit reached. User cannot add more addresses.");
				return false;
			}

			var jsonContent = JsonSerializer.Serialize(model);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

			response = await _httpClient.PostAsync("/api/Address/addresses", content);
			if (response.IsSuccessStatusCode)
			{
				_logger.LogInformation("Address added successfully. Address: {@Address}", model);
				return true;
			}

			_logger.LogWarning("Failed to add address. Status Code: {StatusCode}", response.StatusCode);
			return false;
		}
        public async Task<bool> UpdateAddressAsync(Guid id, AddressModel model)
        {
            var token = _httpContextAccessor.HttpContext.User.FindFirst("Token")?.Value;

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Token not found in user claims.");
                return false;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Address/addresses/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Address updated successfully. Address ID: {AddressId}", id);
                return true;
            }

            _logger.LogWarning("Failed to update address. Status Code: {StatusCode}", response.StatusCode);
            return false;
        }
    }
}
