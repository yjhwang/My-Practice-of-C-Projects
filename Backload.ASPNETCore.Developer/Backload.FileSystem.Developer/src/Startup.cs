using Backload.MiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


namespace Backload.Demo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            try { 

                // The following code shows how to add an internal handler as middleware. 
                // If you use a custom controller (e.g. custom events demo) you can remove this code

                // Method 1: Simple call to add the internal handler without logging or routing
                app.UseBackload();

                //// Method 2: Add logging (console logger)
                // loggerFactory.AddConsole();
                // var logger = loggerFactory.CreateLogger("Backload");
                // app.UseBackload(
                //     log =>
                //     {
                //         log.SetLogging(logger, LogLevel.Verbose);
                //     });

                // Method 3: Call internal handler only if path contains /Backload/FileHandler
                // app.MapWhen(
                //    context => context.Request.Path.Value.Contains("/Backload/FileHandler"),
                //    appBranch =>
                //    {
                //        appBranch.UseBackload();
                //    });


                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                });

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

        }
    }
}
