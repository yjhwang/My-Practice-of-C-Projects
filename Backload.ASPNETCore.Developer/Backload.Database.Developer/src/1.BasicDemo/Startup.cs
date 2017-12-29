using Backload.Context.Tracing;
using Backload.Demo.Models;
using Backload.MiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            try
            {
                // Gets the connection string from appsettings.json and
                // creates a DBContext object to be used in the controller
                var conn = Configuration.GetConnectionString("FilesContext");
                services.AddDbContext<FilesContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("FilesContext")));
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            //TraceManager.SetTraceLevel(SourceLevels.Verbose);
            //var logger = loggerFactory.CreateLogger("Backload");
            //Backload.Context.Tracing.TraceManager.SetLogger(LogLevel.Debug, logger);

            try
            {

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
