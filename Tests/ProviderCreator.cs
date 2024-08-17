using Microsoft.Extensions.DependencyInjection;
using WebApi.Extensions;

namespace Tests;

public static class ProviderCreator
{
    private static IServiceProvider Provider()
    {
        var services = new ServiceCollection();

        services.AddValidation();

        return services.BuildServiceProvider();
    }
    
    public static T Get<T>()
    {
        var provider = Provider();
        return provider.GetRequiredService<T>();
    }
}