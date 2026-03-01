using Microsoft.AspNetCore.DataProtection;

public static partial class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddDataProtectionInfrastructure(this IServiceCollection services)
    {
        //Only for development, in production use a secure key management solution like Azure Key Vault or AWS KMS
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/.aspnet/DataProtection-Keys"));

        return services;
    }
}
