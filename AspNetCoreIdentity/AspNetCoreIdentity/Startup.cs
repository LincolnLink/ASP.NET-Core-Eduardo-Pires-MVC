using AspNetCoreIdentity.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AspNetCoreIdentity
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile(path:"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            // Se for um ambiente de desenvolvimento, use os dados da minha maquina.
            if (hostEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }


            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Configuração do Identity
            services.AddIdentityConfig(Configuration);

            // Configuração de Autorização das CLAIM
            services.AddAuthorizationConfig();

            // As Injeções de Dependencia
            services.ResolveDependencies();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // retorna informações do erro, caso tenha algum.
                // Caso o ambiente seja de desenvolvimento.
                app.UseDeveloperExceptionPage();

            }
            else
            {
                /* Se não, encaminha para uma view de erro!*/
                /* app.UseExceptionHandler("/Home/Error");*/
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();

                
            }

            app.UseHttpsRedirection();
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
