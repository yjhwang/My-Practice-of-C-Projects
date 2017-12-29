using Backload.Contracts.Context;
using Backload.Contracts.Context.Config;
using Backload.Contracts.FileHandler;
using Backload.Contracts.Status;
using Backload.Demo.Models;
using Backload.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Backload.Demo.Controllers
{

    /// <summary>
    /// A custom database controller used in most demos
    /// </summary>
    public class CustomAPIController : Controller
    {
        private IHostingEnvironment _hosting;
        private FilesContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting">IHostingEnvironment instance (injected)</param>
        /// <param name="context">An Entity Framework Core DBContext instance (injected)</param>
        public CustomAPIController(IHostingEnvironment hosting, FilesContext context, ILoggerFactory logFactory)
        {
            _hosting = hosting;
            _context = context;
        }

        /// <summary>
        /// Custom database handler. 
        /// To access it in an JavaScript AJAX request use: <code>var url = "/CustomDatabase/DataHandler";</code>.
        /// </summary>
        public async Task<ActionResult> DataHandler()
        {
            IBackloadResult result = null;
            IFileStatus status = null;

            try
            {
                // Create and initialize the handler
                IFileHandler handler = Backload.FileHandler.Create();
                handler.Init(this.HttpContext, _hosting, _context);


                // This demo calls high level API methods. 
                // Http methhod related API methods are in handler.Services.[HttpMethod].
                // Low level API methods are in handler.Services.Core
                if (handler.Context.HttpMethod == "GET")
                    status = await handler.Services.GET.Execute();
                else if (handler.Context.HttpMethod == "POST")
                    status = await handler.Services.POST.Execute();
                else if (handler.Context.HttpMethod == "DELETE")
                    status = await handler.Services.DELETE.Execute();


                // Create a client plugin specific result. 
                // In this example we could simply call CreateResult(), because handler.FilesStatus also has the IFileStatus object
                if (handler.Context.RequestType == RequestType.Default)
                { 
                    result = handler.CreatePluginResult(status, PluginType.JQueryFileUpload);

                    // Save changes to database
                    await _context.SaveChangesAsync();
                }
                else if ((handler.Context.RequestType == RequestType.File) || (handler.Context.RequestType == RequestType.Thumbnail))
                {
                    if (status.Files.Count == 0)
                        return new StatusCodeResult((int)HttpStatusCode.NotFound);
                    
                    var file = status.Files[0];
                    result = handler.CreateFileResult(file, file.ContentType);
                }
                else
                {
                    result = handler.CreateResult();
                }

				
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
