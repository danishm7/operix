using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Operix.Infrastructure.Data;
using Operix.Application.Interfaces.Persistence;
using Operix.Infrastructure.Repositories;
using Operix.Infrastructure.Data.Interceptors;
using Operix.Application.Interfaces;
using Operix.Infrastructure.Services;

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
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(services.BuildServiceProvider().GetRequiredService<AuditSaveChangesInterceptor>());
            });
        }
        else
        {
            services.AddScoped<AuditSaveChangesInterceptor>();
            services.AddDbContext<OperixDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .UseSnakeCaseNamingConvention()
                    .AddInterceptors(services.BuildServiceProvider().GetRequiredService<AuditSaveChangesInterceptor>());
            });
        }
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<OperixDbContext>());

        // Register Repositories
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();

        // Register Infrastructure Services
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();

        return services;
    }
}