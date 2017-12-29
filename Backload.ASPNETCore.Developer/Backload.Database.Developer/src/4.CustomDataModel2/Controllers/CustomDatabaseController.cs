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
    public class CustomDatabaseController : Controller
    {
        private IHostingEnvironment _hosting;
        private FilesContext _context;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting">IHostingEnvironment instance (injected)</param>
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


                // Events
                handler.Events.GetFilesRequestFinished += Events_GetFilesRequestFinished;
                handler.Events.StoreFileRequestStarted += Events_StoreFileRequestStarted;


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



        void Events_GetFilesRequestFinished(IFileHandler sender, Contracts.Eventing.IGetFilesRequestEventArgs e)
        {
            foreach (var file in e.Param.FileStatus.Files)
            {
                // CustomProviderFile is your implementation of IBackloadStorageProviderFile interface
                // with an additional "Description" property
                var entity = (CustomProviderFile)file.ProviderFile;

                // Optional: PublicMetaData can be used to return additional data to the client in a GET request
                // CustomResultData (demo) can be a simple class with a UploadTime and a Description property
                file.MetaData.PublicMetaData = new CustomResultData(entity.UploadTime, entity.Description);

                // If you only need the description you can set it directly
                // file.MetaData.PublicMetaData = entity.Description;
            }
        }


        void Events_StoreFileRequestStarted(IFileHandler sender, Contracts.Eventing.IStoreFileRequestEventArgs e)
        {
            // Get the description send with the form parameter "description"
            string description = "No description available";
            if (e.Param.CustomFormValues.ContainsKey("description")) description = e.Param.CustomFormValues["description"];

            // Get the IFileStatusItem file
            var file = e.Param.FileStatusItem;

            // CustomProviderFile is a simple implementation of IBackloadStorageProviderFile interface
            // with an additional "Description" property
            var entity = new CustomProviderFile(file.ProviderFile, description);

            // Assign your implementation to file.ProviderFile
            file.ProviderFile = entity;


            // Set the custom meta data to be posted back to the client
            file.MetaData.PublicMetaData = new CustomResultData(entity.UploadTime, description);
        }
    }



    #region CustomResultData demo helper class

    /// <summary>
    /// Returns some custom data used in this demo
    /// Using lowercase properties, because they are returned as JSON object to the client
    /// </summary>
    public class CustomResultData
    {
        public CustomResultData()
        { }


        public CustomResultData(DateTime uploadTime, string description)
        {
            this.uploadTime = uploadTime.ToString();
            this.description = description;
        }


        public string uploadTime { get; set; }
        public string description { get; set; }
    }

    #endregion CustomResultData demo helper class

}
