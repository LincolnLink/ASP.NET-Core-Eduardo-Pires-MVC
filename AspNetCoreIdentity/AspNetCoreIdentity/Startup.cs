using AspNetCoreIdentity.Config;
using KissLog.AspNetCore;
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

            // Configura��o do Identity
            services.AddIdentityConfig(Configuration);

            // Configura��o de Autoriza��o das CLAIM
            services.AddAuthorizationConfig();

            // As Inje��es de Dependencia
            services.ResolveDependencies();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // retorna informa��es do erro, caso tenha algum.
                // Caso o ambiente seja de desenvolvimento.
                app.UseDeveloperExceptionPage();

            }
            else
            {
                /* Se n�o, encaminha para uma view de erro!*/
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

            //KissLog
            app.UseKissLogMiddleware(options => {
                LogConfig.ConfigureKissLog(options, Configuration);
            });

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
