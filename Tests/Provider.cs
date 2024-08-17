using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Extensions;

namespace Tests;

public static class Provider
{
    private static readonly IServiceProvider Prov;
    
    static Provider()
    {
        var services = new ServiceCollection();

        services.AddValidation();
        services.AddServices();
        services.AddMappers();
        services.AddAuthServices();
        services.Configure<JwtOptions>(options =>
        {
            options.SecretKey = "JqC8zn`3/[}tyu=m6*~Kdf)pQ#RY+xhN;4b79:VvPW.5kTcg(emA~;+E2*k&PdL%D{tNFshjpxb9,en^q5Tr!?6g.U@)c>Q#f3(K";
            options.Issuer = "repair-identity-service";
            options.Expiration = 24;
        });

        Prov = services.BuildServiceProvider();
    }
    
    public static T Get<T>()
    {
        return Prov.GetRequiredService<T>();
    }
}