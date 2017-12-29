using Backload.Contracts.Context;
using Backload.Contracts.Context.Config;
using Backload.Contracts.FileHandler;
using Backload.Contracts.Status;
using Backload.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Backload.Demo.Controllers
{

    /// <summary>
    /// Custom controller with basic API method calls. Note: API method calls is a Pro feature
    /// </summary>
    public class CustomAPIController : Controller
    {
        private IHostingEnvironment _hosting;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting">IHostingEnvironment instance (injected)</param>
        public CustomAPIController(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }
            

        /// <summary>
        /// A Custom file handler. 
        /// To access it in an JavaScript AJAX request use: <code>var url = "/CustomAPI/FileHandler/";</code>.
        /// </summary>
        public async Task<ActionResult> FileHandler()
        {
            IBackloadResult result = null;
            IFileStatus status = null;
            try
            {
                // Create and initialize the handler
                IFileHandler handler = Backload.FileHandler.Create();
                handler.Init(this.HttpContext, _hosting);


                // This demo calls high level API methods. 
                // HTTP method related API methods are in handler.Services.[HttpMethod].
                // Low level API methods are in handler.Services.Core
                if (handler.Context.HttpMethod == "GET")
                    status = await handler.Services.GET.Execute();
                else if (handler.Context.HttpMethod == "POST")
                    status = await handler.Services.POST.Execute();
                else if (handler.Context.HttpMethod == "DELETE")
                    status = await handler.Services.DELETE.Execute();


                // Create a client plugin specific result. 
                // In this example we could simply call CreateResult(), because handler.FilesStatus also has the IFileStatus object
                result = handler.CreatePluginResult(status, PluginType.JQueryFileUpload);


                // Helper to create an ActionResult object from the IBackloadResult instance
                return ResultCreator.Create(result);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
