using Microsoft.AspNetCore.Mvc;

namespace Backload.Demo.Controllers
{

    public class HomeController : Controller
    {

        // Empty view
        public IActionResult Empty()
        {
            return View();
        }


        // Start view
        public IActionResult Index()
        {
            return View();
        }


        #region jQuery File Upload Plugin

        // Basic theme (Bootstrap) 
        public IActionResult Basic()
        {
            return View();
        }  
  
        //Basic Plus theme (Bootstrap) 
        public IActionResult BasicPlus()
        {
            return View();
        }

        // Basic Plus UI theme (Bootstrap) 
        public IActionResult BasicPlusUI()
        {
            return View();
        }

        // AngularJS theme
        public IActionResult AngularJS()
        {
            return View();
        }

        // jQuery UI theme
        public IActionResult JQueryUI()
        {
            return View();
        }

#endregion



        #region PlUpload

        // Moxiecode PlUpload plugin simple demo
        public IActionResult PlUploadSimple()
        {
            return View();
        }

        // Moxiecode PlUpload plugin ui demo
        public IActionResult PlUploadUI()
        {
            return View();
        }

        #endregion



        #region Fine Uploader

        // Fine Uploader default demo
        public IActionResult FineUploaderDefault()
        {
            return View();
        }

        // Fine Uploader gallery demo
        public IActionResult FineUploaderGallery()
        {
            return View();
        }

        // Fine Uploader simple thumbnails demo
        public IActionResult FineUploaderSimple()
        {
            return View();
        }

        #endregion



        #region Other demos with integrated controller

        // Integrated controller with basic file chunking demo
        public IActionResult OtherChunkingBasic()
        {
            return View();
        }

        // Integrated controller with resume chunked files demo
        public IActionResult OtherChunkingResume()
        {
            return View();
        }

        // Integrated controller with file overwrite protection demo
        public IActionResult OtherChunkingAdvanced()
        {
            return View();
        }

        // Integrated controller with pause chunk upload demo
        public IActionResult OtherChunkingPauseResume()
        {
            return View();
        }

        // Integrated controller with tracing demo
        public IActionResult OtherTracing()
        {
            return View();
        }

        // Integrated controller with tracing demo
        public IActionResult OtherCustomClient()
        {
            return View();
        }
        
        #endregion



        #region Custom controller

        // Custom controller with events demo
        public IActionResult CustomEvents()
        {
            return View();
        }

        // Custom controller with basic API method calls
        public IActionResult CustomAPI()
        {
            return View();
        }

        // Custom controller with post processing demo
        public IActionResult CustomPostProcessing()
        {
            return View();
        }

        // Custom controller with data provider demo
        public IActionResult CustomDataProvider()
        {
            return View();
        }

        // Custom controller for the WebApi controller demo
        public IActionResult CustomWebApi()
        {
            return View();
        }

        #endregion

    }
}
