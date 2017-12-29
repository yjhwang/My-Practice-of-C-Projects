using Backload.Contracts.Services.Database;
using System;

namespace Backload.Demo.Models
{
    /// <summary>
    /// A simple custom implementation of IBackloadStorageProviderFile used by the database plugin internally.
    /// Our implementation provides an additional "Description" property we receive from the client in order 
    /// to store a file description with the other file data to the database.
    /// </summary>
    public class CustomProviderFile : BackloadStorageProviderFile
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomProviderFile() : base()
        { }


        /// <summary>
        /// Constructor. Used to interact with the database plugin
        /// </summary>
        /// <param name="file">A IBackloadStorageProviderFile instance used by the database plugin</param>
        public CustomProviderFile(IBackloadStorageProviderFile file, string description) : base (file)
        {
            this.Description = description;
        }


        /// <summary>
        /// Constructor. Used to create a CustomProviderFile from an File entity object.
        /// </summary>
        /// <param name="file">A Upload object (current entity type)</param>
        /// <param name="loadData">True to load binary data into memory, else false</param>
        public CustomProviderFile(File file, bool loadData = true)
        {
            this.RowId = file.RowId;
            this.Id = file.FileId;
            this.Name = file.FileName;
            this.Original = file.FileName;
            this.Type = file.ContentType;
            this.Size = file.FileSize;
            this.UploadTime = file.UploadTime;
            if (loadData) this.Data = file.Data;
            this.Preview = file.Preview;
            this.Path = file.Path;
            this.Meta = file.Meta;

            this.Description = file.Description;
        }


        /// <summary>
        /// Additional custom description property
        /// </summary>
        public string Description { get; set; }
     
    }
}

