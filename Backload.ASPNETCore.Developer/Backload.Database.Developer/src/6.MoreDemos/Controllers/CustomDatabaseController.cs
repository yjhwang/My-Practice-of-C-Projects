using Backload.Contracts.Context;
using Backload.Contracts.FileHandler;
using Backload.Demo.Models;
using Backload.Helper;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Backload.Demo.Controllers
{

    /// <summary>
    /// A custom database controller used in most demos
    /// </summary>
    public class CustomDatabaseController : Controller
    {
        private IHostingEnvironment _hosting;
        private FilesContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting">IHostingEnvironment instance (injected)</param>
        /// <param name="context">An Entity Framework Core DBContext instance (injected)</param>
        public CustomDatabaseController(IHostingEnvironment hosting, FilesContext context, ILoggerFactory logFactory)
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

            try
            {
				// Create and initialize the handler
				IFileHandler handler = Backload.FileHandler.Create();
				handler.Init(this.HttpContext, _hosting, _context);


				// Call the execution pipeline and get the result
				IBackloadResult result = await handler.Execute();


				// Save changes to database
				await _context.SaveChangesAsync();

				
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
