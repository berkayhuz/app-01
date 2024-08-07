namespace Authentication.API.Helpers.TokenHelpers
{
    public interface ITokenStorageService
    {
        Task StoreTokenAsync(Guid userId, string token);
        Task<string> GetTokenAsync(Guid userId);
    }

}
