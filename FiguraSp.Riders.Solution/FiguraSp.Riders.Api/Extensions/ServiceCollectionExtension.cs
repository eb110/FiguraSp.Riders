using Figurasp.Riders.Service.Services;
using FiguraSp.Riders.Model.Data;
using FiguraSp.SharedLibrary.DependencyInjection;

namespace FiguraSp.Riders.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSharedDbConnection(this IServiceCollection services, IConfiguration config)
        {
            //add db
            SharedService.AddSharedSqlService<RidersDbContext>(services, config);

            return services;
        }

        public static IServiceCollection AddSharedJwtScheme(this IServiceCollection services, IConfiguration config)
        {
            //add db
            SharedService.AddJwtSharedService(services, config);

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IRiderService, RiderService>();

            return services;
        }
    }
}
