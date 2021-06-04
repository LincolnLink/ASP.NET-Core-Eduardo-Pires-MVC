using AspNetCoreIdentity.Extensions;
using Microsoft.Extensions.DependencyInjection;


namespace AspNetCoreIdentity.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    name: "PodeExcluir",
                    configurePolicy: policy => policy.RequireClaim("PodeExcluir")
                );

                options.AddPolicy(
                    name: "PodeLer",
                    configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer"))
                );

                options.AddPolicy(
                    name: "PodeEscrever",
                    configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever"))
                );
            });


            return services;

        }
    }
}
