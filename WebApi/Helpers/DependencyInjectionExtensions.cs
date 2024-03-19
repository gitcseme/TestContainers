using Microsoft.EntityFrameworkCore;
using WebApi.EFConfig;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Helpers;

public static class DependencyInjectionExtensions
{
    private const string ConnectionStringName = "AppDbConnection";

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
        });

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        // services
        services.AddScoped<ICompanyService, CompanyService>();

        return services;
    }
}
