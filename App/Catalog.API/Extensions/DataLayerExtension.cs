using Catalog.API.Data;
using Catalog.API.Data.Repositories.Abstractions;
using Catalog.API.Data.Repositories.Concretes;
using Catalog.API.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Extensions
{
    public static class DataLayerExtension
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseMySql(config.GetConnectionString("CatalogConnection"),
                    new MySqlServerVersion(new Version(8, 0, 22))));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}