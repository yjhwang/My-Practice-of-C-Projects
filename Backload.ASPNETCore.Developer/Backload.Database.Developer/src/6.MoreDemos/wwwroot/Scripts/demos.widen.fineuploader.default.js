/*jslint unparam: true */
/*global window, $ */
$(function () {
    'use strict';
    // Name of a web application (usually in full IIS mode). Can be found in Properties/Web/Server/Project-Url. Example: http://localhost/Demo (Name of web application is "Demo")
    var web_app = '/';

    // Filehandler endpoint. Note: We send a form parameter below ("objectContext": "030357B624D9") as a virtual storage path
    var url = web_app + 'CustomDatabase/DataHandler';


    var uploader = new qq.FineUploader({
        element: document.getElementById("fine-uploader"),
        template: 'qq-template',
        request: {
            endpoint: url,
            params: {                                                                       // Send a plugin param or set Fine Uploader in 
                plugin: "FineUploader",                                                     // Web.Backload.config as the default client plugin                                                    
                objectContext: "030357B624D9"
            }
        },

        session: {                                                                          // Initial GET request to load existing files
            endpoint: url,
            params: {                                                                       // Send a plugin param or set Fine Uploader in 
                plugin: "FineUploader",                                                     // Web.Backload.config as the default client plugin                                                      
                objectContext: "030357B624D9"
            }
        },

        deleteFile: {
            enabled: true                                                                   // Enable file delete
        },

        chunking: {
            enabled: false,                                                                  // true to enable file chunking
            partSize: 2000000                                                               // 2MB chunks Note: In a real world scenario chunk size depends on target infrastructure or use cases.
        },

        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'gif', 'png', 'pdf']
        },

        callbacks: {
            onComplete: function (id, name, response, xhr) {
                if ((response) && (response.success)) {
                    this.setDeleteFileEndpoint(response.deleteFileEndpoint, id);            // Set the deleteFileEndpoint adress
                }
            }
        }
    });
});
