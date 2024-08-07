using Catalog.API.Services.Abstractions;
using Catalog.API.Services.Concrete;
using Catalog.LIB.AutoMapper;
using SharedLibrary.Helpers.Images;
using System.Reflection;

namespace Catalog.API.Extensions
{
    public static class ServiceLayerExtension
    {
        public static void LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IImageHelper, ImageHelper>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(assembly);
            });

        }
    }
}