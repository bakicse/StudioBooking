using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.MainDTOs.Mail;

namespace Application.ConfigureServices;

internal static class ServiceExtension
{
    internal static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(option => option
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("DbConnection")));
    }

    internal static void ConfigureMailSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettingsDto>(configuration.GetSection("MailSettings"));
    }

    internal static void ConfigureCors(this IServiceCollection services) 
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }
}