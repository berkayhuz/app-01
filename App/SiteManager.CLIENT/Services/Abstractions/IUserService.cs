using SiteManager.CLIENT.Areas.Authentication.Models.User;
using System.Threading.Tasks;
using SiteManager.CLIENT.Models;

namespace SiteManager.CLIENT.Services.Abstractions
{
	public interface IUserService
    {
        Task<UserModel> GetUserAsync();
        Task<List<AddressModel>> GetAddressesAsync();
        Task<AddressModel> GetAddressAsync(Guid id);
        Task<bool> DeleteAddressAsync(Guid id);
        Task<bool> AddAddressAsync(AddressModel model);
        Task<bool> UpdateAddressAsync(Guid id, AddressModel model);
    }
}