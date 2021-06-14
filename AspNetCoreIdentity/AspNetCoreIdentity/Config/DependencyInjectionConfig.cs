using AspNetCoreIdentity.Extensions;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentity.Config
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Regra vale para todos, 
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            // KissLog
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });
            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

            return services;
        }      
    }
}
