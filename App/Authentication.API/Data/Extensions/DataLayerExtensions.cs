using Authentication.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Authentication.API.Data;

public static class DataLayerExtensions
{
    public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(config.GetConnectionString("AuthenticationConnection"),
                new MySqlServerVersion(new Version(8, 0, 22))));
        return services;
    }
}