using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dev.UI.Site
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {                     

            // services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.AddControllersWithViews();
            // services.AddRazorPages();
             
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
