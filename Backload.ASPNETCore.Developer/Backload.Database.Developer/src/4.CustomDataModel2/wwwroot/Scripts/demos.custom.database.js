$(function () {
    'use strict';

    var web_app = '/';                                                   // Name of a web application (usually in full IIS mode). Can be found in Properties/Web/Server/Project-Url. Example: http://localhost/Demo (Name of web application is "Demo")

    // We use custom file handler (~/Controllers/CustomDatabaseController.cd):
    // In this example we set an objectContect (id) in the URL query (or as form parameter). The value of the opjectContext parameter  
    // will be added to the path as a sub-folder of the storage root folder (storageRoot\objectContext\file). You can assign a user id
    // to objectContext as user directory. Instead of setting the objectContext client side you can also be set server side, 
    // e.g. server side events (see also Custom Data Provider Demo, 2.2+).
    var url = web_app + 'CustomDatabase/DataHandler?objectContext=C5F260DD3787';


    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        url: url,
        //maxChunkSize: 5000000,                                           // Optional: file chunking with 5MB chunks (Pro feature)
        acceptFileTypes: /(jpg)|(jpeg)|(png)|(gif)|(pdf)$/i,             // Allowed file types

        // Add method is called, when a file is added to the client side upload list.
        // The UUID is only needed on chunk uploads, where related chunks are identified by the UUID.
        add: function (e, data) {
            var uuid = UUID.generate();
            // We set the uuid to the file object, so we can identify the file in the row of the template (see send method and upload template)
            data.files[0].uuid = uuid;
            data.formData = { uuid: uuid };
            $.blueimp.fileupload.prototype.options.add.call(this, e, data);              // Add file with updated data to list in the ui 
        },
        send: function (e, data) {
            var desc = "No description available";
            // Select the description input field based on the row with the uuid we generated in the add method
			// and append the description to form data posted to the client
            var $row = $('tr[data-fileid="' + data.files[0].uuid + '"');
            if ($row.length > 0) {
                var $desc = $row.find('input[name=description]');
                if ($desc.length > 0) desc = $desc.val();
				
                // data.data is available only in standard mode, data.formData in chunking mode
                if (data.data) data.data.append("description", desc);
                else data.formData["description"] = desc;
            }
        }
    })



    // Load existing files:
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
