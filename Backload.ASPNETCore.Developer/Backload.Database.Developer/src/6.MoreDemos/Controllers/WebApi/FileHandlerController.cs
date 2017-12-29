using Backload.Contracts.Context;
using Backload.Contracts.FileHandler;
using Backload.Demo.Models;
using Backload.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Backload.Demo.WebApi.Controllers.Api
{

    [Route("api/[controller]")]
    public class FileHandlerController : Controller
    {
        private IHostingEnvironment _hosting;
        private IFileHandler _handler;
        private FilesContext _context;


        /// <param name="context">An Entity Framework Core DBContext instance (injected)</param>
        public FileHandlerController(IHostingEnvironment hosting, FilesContext context)
        {
            this._hosting = hosting;
            this._context = context;

            // Create the file handler
            _handler = Backload.FileHandler.Create();
        }



        // Requests meta data of files already stored 
        // GET api/FileHandler
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Initialize the file handler
                _handler.Init(this.HttpContext, _hosting, _context);

                // Call the execution pipeline and get the result
                IBackloadResult result = await _handler.Execute();

                // Helper to create an ActionResult object from the IBackloadResult instance
                return ResultCreator.Create(result);
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }




        // Deletes a specific file from storage 
        // DELETE api/FileHandler?fileName=[name]  
        [HttpDelete]
        public async Task<IActionResult> Delete(string fileName)
        {
            try
            {
                // Initialize the file handler
                _handler.Init(this.HttpContext, _hosting, _context);

                // Call the execution pipeline and get the result
                IBackloadResult result = await _handler.Execute();

                // Save changes to database
                await _context.SaveChangesAsync();

                // Helper to create an ActionResult object from the IBackloadResult instance
                return ResultCreator.Create(result);
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }




        // Stores a file
        // POST api/FileHandler 
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            try
            {
                // Initialize the file handler
                _handler.Init(this.HttpContext, _hosting, _context);

                // Call the execution pipeline and get the result
                IBackloadResult result = await _handler.Execute();

                // Save changes to database
                await _context.SaveChangesAsync();

                // Helper to create an ActionResult object from the IBackloadResult instance
                return ResultCreator.Create(result);
            }
            catch
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}