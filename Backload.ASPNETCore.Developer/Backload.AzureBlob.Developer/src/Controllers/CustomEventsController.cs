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
    /// Custom controller for the events demo  Note: events must be enabled in the config.
    /// </summary>
    public class CustomEventsController : Controller
    {
        // User id. In a real project this user id should be a real logged in user id
        private string _currentLoggedInUserId = string.Empty;
        private IHostingEnvironment _hosting;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hosting">IHostingEnvironment instance (injected)</param>
        public CustomEventsController(IHostingEnvironment hosting)
        {
            _hosting = hosting;
        }

        
        /// <summary>
        /// A custom file handler. 
        /// To access it in an JavaScript AJAX request use: <code>var url = "/CustomEvents/FileHandler/";</code>.
        /// </summary>
        public async Task<ActionResult> FileHandler()
        {
            // Fake user id for demo purposes
            _currentLoggedInUserId = "97966ABE-0691-4874-958C-98AD07BB461C";

            try
            {
                // Create and initialize the handler
                IFileHandler handler = Backload.FileHandler.Create();


                // Attach event handlers to events
                handler.Events.ConfigurationLoaded += Events_ConfigurationLoaded;
                handler.Events.PreInitialization += Events_PreInitialization;
                handler.Events.GetFilesRequestStarted += Events_GetFilesRequestStarted;
                handler.Events.GetFilesRequestFinished += Events_GetFilesRequestFinished;
                handler.Events.StoreFileRequestStarted += Events_StoreFileRequestStarted;


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



        void Events_ConfigurationLoaded(IFileHandler sender, Contracts.Eventing.IConfigurationLoadedEventArgs e)
        {
            var config = e.Param.Configuration;

            // Use the ConfigurationLoaded event to change config settings. Note: In Backload Standard Edition,
            // configuration changes effect all subsequent requests. Professional editions can be set to request based configuration.
            config.GetInclSubFolders = true;
        }


        void Events_PreInitialization(IFileHandler sender, Contracts.Eventing.IPreInitializationEventArgs e)
        {
            // You can use ObjectContext to provide a private user storage space. In most examples we set 
            // ObjectContext in client side JavaScript, but you can also set it server side. 
            e.Param.BackloadValues.ObjectContext = _currentLoggedInUserId;
        }


        void Events_GetFilesRequestStarted(IFileHandler sender, Backload.Contracts.Eventing.IGetFilesRequestEventArgs e)
        {
            // Backload component has started the internal GET handler method. 
            // You can retrieve or change the search path.
            string searchPath = e.Param.SearchPath;
        }


        void Events_GetFilesRequestFinished(IFileHandler sender, Backload.Contracts.Eventing.IGetFilesRequestEventArgs e)
        {
            // Backload component has finished the internal GET handler method. 
            // Results can be found in e.Param.FileStatus or sender.FileStatus

            IFileStatus status = e.Param.FileStatus;
        }



        void Events_StoreFileRequestStarted(IFileHandler sender, Contracts.Eventing.IStoreFileRequestEventArgs e)
        {
            // Retrieve or change properties of the file to be stored.
            var file = e.Param.FileStatusItem;
        }

    }
}
