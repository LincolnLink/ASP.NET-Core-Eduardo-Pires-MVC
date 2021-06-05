using AspNetCoreIdentity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using AspNetCoreIdentity.Areas.Identity.Data;
using Microsoft.Extensions.Configuration;


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


        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {

            // Configurando contexto do banco de dados, e a connectionString!
            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("AspNetCoreIdentityContextConnection"))
            );

            // Configurando o Identity, IdentityUser é uma classe que representa um usuario.
            // IdendityRole: Ativa a configuração das "Roles", São as autorizações a mais alem da autenticação.
            // AddfaultUI: configuracao das pages, o Padrão é BootStrap4.
            // AddEntityFrameworkStores: está configurando o tipo de acesso a dados que o Identity vai usar.
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

            // Para as pages funcionar.
            services.AddRazorPages();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            // Configurando os Cookies
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });


            return services;
        }

    }

    
}
