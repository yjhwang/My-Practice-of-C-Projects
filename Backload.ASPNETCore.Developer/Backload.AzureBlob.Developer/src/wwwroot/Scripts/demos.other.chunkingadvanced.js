
/* global $, window */

$(function () {
    'use strict';

    var msg_resume = ' bytes already uploaded. To resume upload click start.';
    var msg_exists = 'File already uploaded. You may need to refresh the page.';
    var msg_leave = 'You are currently uploading files. If you leave this page, your upload may not complete successfully.';

    var web_app = '/';                                                   // Name of a web application (usually in full IIS mode). Can be found in Properties/Web/Server/Project-Url. Example: http://localhost/Demo (Name of web application is "Demo")

    // In this example we set an objectContect (id) in the URL query (or as form parameter). The value of the opjectContext parameter   
    // will be added to the path as a sub-folder of the storage root folder (storageRoot\objectContext\file). You can assign a user id
    // to objectContext as user directory. Instead of setting the objectContext client side you can also be set server side, 
    // e.g. server side events (see also Custom Data Provider Demo, 2.2+).
    var url = web_app + 'Backload/FileHandler?objectContext=C5F260DD3787';


    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        url: url,
        maxChunkSize: 4000000,                                           // Size of file chunks (data packets): 4MB. Note: This is a Azure determined limit for a single chunk, but Backload can handle more).
        acceptFileTypes: /(jpg)|(jpeg)|(png)|(gif)|(pdf)$/i,             // Allowed file types

        // the add function is called immediately after a file was added to the client UI list, but not uploaded so far.
        add: function (e, data) {
            var that = this;

            // "getFileInfo" requests meta data for the file just added to the client UI list from the server.
            //   execute: getFileInfo({filename}) (get file or chunk meta data); 
            //   ts: time-stamp (prevents caching)
            $.getJSON(url, { execute: "getFileInfo(" + data.files[0].name + ")", t: e.timeStamp },
                // Server response
                function (result) {
                    // Response: If result.files.length == 0 or chunkInfo == null no pervious partial uploads
                    if (result.files.length > 0) {                                               // If result.files.length is 0 no previous partial uploads
                        var file = result.files[0];
                        var chunkInfo = file.extra.chunkInfo;
                        data.formData = { uuid: file.uuid };
                        if (chunkInfo != null) {                                      // To resume a partially uploaded file,
                            data.uploadedBytes = chunkInfo.uploadedSize;              // set the length of bytes already uploaded
                            alert(chunkInfo.uploadedSize + msg_resume);
                        } else if (data.files[0].size == file.size) {                            // If true, file already uploaded
                            alert(msg_exists);
                            return;                                                              // Do not add file to list and return silently
                        }
                    } else {
                        data.formData = { uuid: UUID.generate() };                               // File not uploaded so far. Generate a new uuid
                    }

                    $.blueimp.fileupload.prototype.options.add.call(that, e, data);              // Add file with updated data to list in the ui 
                })
        },
        send: function (e, data) {
            uploadingFiles += 1;
        },
        always: function (e, data) {
            uploadingFiles -= 1;
        }
    })


    // Warn user if file is currently uploaded. 
    var uploadingFiles = 0;
    $(window).bind('beforeunload', function () {
        if (uploadingFiles > 0) {
            return msg_leave;
        }
    });



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
