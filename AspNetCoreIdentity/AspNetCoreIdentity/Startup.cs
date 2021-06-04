using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AspNetCoreIdentity.Areas.Identity.Data;
using AspNetCoreIdentity.Extensions;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentity.Config;

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

            // Configuração do Identity
            services.AddIdentityConfig(Configuration);

            // Configuração de Autorização das CLAIM
            services.AddAuthorizationConfig();

            // As Injeções de Dependencia
            services.ResolveDependencies();


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
