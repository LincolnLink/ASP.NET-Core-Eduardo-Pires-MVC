using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreIdentity.Areas.Identity.Data;
using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            // Configurando contexto do banco de dados, e a connectionString!
            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("AspNetCoreIdentityContextConnection"))
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

            // Configuração de Autorização das CLAIM
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    name:"PodeExcluir",
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

            // Regra vale para todos, 
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // Para o Identity funcionar!
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Para as rotas com pages funcionar
                endpoints.MapRazorPages();
            });
        }
    }
}
