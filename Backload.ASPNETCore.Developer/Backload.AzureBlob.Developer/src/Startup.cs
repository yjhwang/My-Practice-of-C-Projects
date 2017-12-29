using Backload.Contracts.FileHandler;
using Backload.MiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System;
using Backload.Contracts.Context;
using Backload.Context;

namespace Backload.Demo
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            // IMPORTANT NOTE: YOU NEED TO SET A VALID AZURE STORAGE CONNECTION STRING OR TO START THE STORAGE EMULATOR FOR THIS DEMO:
            // C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator>AzureStorageEmulator.exe start

            // Optional read Azure Blob Storage connection string from the appsettings.json file 
            // based on the current environment: Development, Staging, Production.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Use one of the methods below to add the storage connection info via dependency injection
            // The Connection string can be stored in the application.json file (default connection name: "StorageConnectionString")
            // This service can also be injected into a controller
            string conn = this.Configuration.GetConnectionString("StorageConnectionString");
            services.AddBackloadConnectionService(new BackloadConnectionInfo(conn));

            //services.AddBackloadConnectionService(this.Configuration);  // Uses default connection string name: "StorageConnectionString"
            //services.AddBackloadConnectionService(this.Configuration, "StorageConnectionString");

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
