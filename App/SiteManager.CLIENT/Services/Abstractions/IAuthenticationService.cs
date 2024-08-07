using System.Threading.Tasks;
using SiteManager.CLIENT.Models;

namespace SiteManager.CLIENT.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync(LoginModel loginModel);
        Task<bool> ChangeEmailAsync(string newEmail, string token);
        Task<bool> ConfirmChangeEmailAsync(string token, string newEmail);
    }
}