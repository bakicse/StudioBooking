using Microsoft.AspNetCore.DataProtection;
using Presentation.API.ActionFilters;
using Repositories.Concretes.Base;
using Repositories.Contracts.Base;
using Services.Concretes.Base;
using Services.Contracts.Base;
using Shared.Cryptography;
using IMailService = Services.Contracts.Base.IMailService;

namespace Application.ConfigureServices;

internal static class DependencyExtension
{
    internal static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IMailService, MailService>();
    }

    internal static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    internal static void RegisterOtherDependencies(this IServiceCollection services)
    {
        services.AddScoped<EncryptionHelper>();
        services.AddSingleton<DataProtectionPurposeStrings>();
        services.AddSingleton(provider =>
        {
            var dataProtectionProvider = provider.GetRequiredService<IDataProtectionProvider>();
            var dataProtectionPurposeStrings = provider.GetRequiredService<DataProtectionPurposeStrings>();
            return new EncryptionHelper(dataProtectionProvider, dataProtectionPurposeStrings);
        });
        services.AddScoped<ValidationFilterAttribute>();
        services.AddScoped<ModelStateValidationFilter>();
    }
}
