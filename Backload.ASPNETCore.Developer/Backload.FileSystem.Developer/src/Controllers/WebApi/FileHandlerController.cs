using Backload.Contracts.Context;
using Backload.Contracts.FileHandler;
using Backload.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Backload.Demo.WebApi.Controllers.Api
{

    [Route("api/[controller]")]
    public class FileHandlerController : Controller
    {
        private IFileHandler _handler;
        private IHostingEnvironment _hosting;

        public FileHandlerController(IHostingEnvironment hosting)
        {
            // Create the handler
            _handler = Backload.FileHandler.Create();

            this._hosting = hosting;
        }



        // Requests meta data of files already stored 
        // GET api/FileHandler
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Initialize the handler
                _handler.Init(this.HttpContext, _hosting);

                // Call the execution pipeline and get the result
                IBackloadResult result = await _handler.Execute();

                // Helper to create an ActionResult object from the IBackloadResult instance
                return ResultCreator.Create(result);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
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
                // Initialize the handler
                _handler.Init(this.HttpContext, _hosting);

                // Call the execution pipeline and get the result
                IBackloadResult result = await _handler.Execute();

                // Helper to create an ActionResult object from the IBackloadResult instance
                return ResultCreator.Create(result);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

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
                // Initialize the handler
                _handler.Init(this.HttpContext, _hosting);

                // Call the execution pipeline and get the result
                IBackloadResult result = await _handler.Execute();

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