using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentity.Extensions;


namespace AspNetCoreIdentity.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Regra vale para todos, 
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();


            return services;
        }

      
    }
}
