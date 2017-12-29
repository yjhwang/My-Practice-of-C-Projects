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
    /// A custom database controller used in the database storage demo
    /// </summary>
    /// <remarks>
    /// IMPORTANT NOTE:
    /// Because we use the same configuration file for all demos, this database demo implements a workaround for the missing
    /// filesUrlPattern="{Backload}" setting in the configuration file. In your project configure filesUrlPattern="{Backload}"
    /// </remarks>
    public class CustomDatabaseController : Controller
    {
        private IHostingEnvironment _hosting;
        private FilesContext _context;

        // Demo: We add this user id to an additional column in our database table
        private Guid _loggedOnUser = Guid.Parse("50682932-ED69-4C73-9C85-C8F666928A23");

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting">IHostingEnvironment instance (injected)</param>
        public CustomDatabaseController(IHostingEnvironment hosting, FilesContext context, ILoggerFactory logFactory)
        {
            _hosting = hosting;
            _context = context;

            // Demo: We add a user id to a custom column in our database table
            _context.LoggedOnUser = _loggedOnUser;
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
