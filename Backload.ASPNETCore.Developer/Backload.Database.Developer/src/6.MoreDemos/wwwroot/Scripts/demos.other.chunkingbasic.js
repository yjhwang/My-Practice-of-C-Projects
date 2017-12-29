

/* global $, window */

$(function () {
    'use strict';

    var web_app = '/';                                                   // Name of a web application (usually in full IIS mode). Can be found in Properties/Web/Server/Project-Url. Example: http://localhost/Demo (Name of web application is "Demo")

    // In this example we set an objectContect (id) in the url query (or as form parameter). You can use a user id as 
    // objectContext give users only access to their own uploads. ObjectContext can also be set server side (See Custom Data Provider Demo).
    var url = web_app + 'CustomDatabase/DataHandler?objectContext=C5F260DD3787';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        url: url,
        maxChunkSize: 1000000,                                           // Size of file chunks (data packets): 1MB. Note: In a real world scenario chunk size depends on target infrastructure or use cases.
        acceptFileTypes: /(jpg)|(jpeg)|(png)|(gif)|(pdf)$/i,             // Allowed file types

        // Add method is called, when a file is added to the client side upload list.
        // The uuid is only needed on chunk uploads, where related chunks are identified by the uuid.
        add: function (e, data) {
            data.formData = { uuid: UUID.generate() };
            $.blueimp.fileupload.prototype.options.add.call(this, e, data);              // Add file with updated data to list in the ui 
        }
    })       




    // Optional: Load existing files:
    $('#fileupload').addClass('fileupload-processing');
    $.ajax({
        // Uncomment the following to send cross-domain cookies:
        // xhrFields: {withCredentials: true},
        url: url,
        dataType: 'json',
        context: $('#fileupload')[0]
    }).always(function () {
        $(this).removeClass('fileupload-processing');
    }).done(function (result) {
        $(this).fileupload('option', 'done')
            .call(this, $.Event('done'), { result: result });
    });
});
