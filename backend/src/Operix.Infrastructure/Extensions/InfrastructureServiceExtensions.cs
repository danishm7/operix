using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Operix.Infrastructure.Data;
using Operix.Application.Interfaces.Persistence;
using Operix.Infrastructure.Repositories;

namespace Operix.Infrastructure.Extensions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var useInMemoryDatabase = bool.TryParse(configuration["UseInMemoryDatabase"], out var useInMemory) && useInMemory;

        // Register DbContext
        if (useInMemoryDatabase)
        {
            services.AddDbContext<OperixDbContext>(options =>
            {
                options.UseInMemoryDatabase("OperixInMemoryDb")
                    .UseSnakeCaseNamingConvention();
            });
        }
        else
        {
            services.AddDbContext<OperixDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .UseSnakeCaseNamingConvention();
            });
        }

        // Register Repositories
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        // Register Infrastructure Services

        return services;
    }
}