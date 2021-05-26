using System;
using Dev.UI.Site.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;


namespace Dev.UI.Site
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MeuDbContext>(optionsAction:options => 
                options.UseSqlServer(Configuration.GetConnectionString(name:"MeuDbContext")));

            // Antigo
            // services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.AddControllersWithViews();
            // Antigo
            // services.AddRazorPages();

            services.AddTransient<IPedidoRepository, PedidoRepository>();

            // Teste de Injeção de dependencia
            /*
            services.AddTransient<IOperacaoTransient, Operacao>();
            services.AddScoped<IOperacaoScoped, Operacao>();
            services.AddSingleton<IOperacaoSingleton, Operacao>();
            services.AddSingleton<IOperacaoSingletonInstance>(new Operacao(id:Guid.Empty));
            services.AddTransient<OperacaoService>();*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Usa os arquivo estaticos
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /*endpoints.MapControllers();*/

                
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");

                /*endpoints.MapControllerRoute("areas", 
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");*/

                // Pode configurar rotas por aqui ou não.
               /* endpoints.MapAreaControllerRoute(
                   name: "AreaProdutos",
                   areaName: "Produtos",
                   pattern: "Produtos/{controller=Cadastro}/{action=Index}/{id?}");*/
                /*
                endpoints.MapAreaControllerRoute(
                   name: "AreaVendas",
                   areaName: "Vendas",
                   pattern: "Vendas/{controller=Pedidos}/{action=Index}/{id?}");*/



                // Antigo
                // endpoints.MapGet("/", async context =>
                // {
                //     await context.Response.WriteAsync("Hello World!");
                // });
                // endpoints.MapControllers();      


            });

            // Antigo
            // app.UseMvc(routes =>
            // {
            //     // routes.MapRoute(
            //     //     name: "default",
            //     //     template: "{controller=Home}/{action=Index}/{id?}");

            //     routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");

            // });

            // app.UseMvc();

        }
    }
}
