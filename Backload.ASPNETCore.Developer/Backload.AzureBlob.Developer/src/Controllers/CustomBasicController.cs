using Backload.Contracts.Context;
using Backload.Contracts.FileHandler;
using Backload.Contracts.Status;
using Backload.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Backload.Demo.Controllers
{

    /// <summary>
    /// Custom controller with minimum code.
    /// </summary>
    public class CustomBasicController : Controller
    {
        // User id. In a real project this user id should be a real logged in user id
        private string _currentLoggedInUserId = string.Empty;
        private IHostingEnvironment _hosting;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting">IHostingEnvironment instance (injected)</param>
        public CustomBasicController(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }

        
        /// <summary>
        /// A custom file handler. 
        /// To access it in an JavaScript AJAX request use: <code>var url = "/CustomEvents/FileHandler/";</code>.
        /// </summary>
        public async Task<ActionResult> FileHandler()
        {
            try
            {
                // Create the handler
                IFileHandler handler = Backload.FileHandler.Create();


                // Init Backload's execution environment and execute the request
                handler.Init(this.HttpContext, _hosting);
                IBackloadResult result = await handler.Execute();


                // Helper to create an ActionResult object from the IBackloadResult instance
                return ResultCreator.Create(result);
            }
            catch (Exception e)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
