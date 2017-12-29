using Backload.Contracts.Services.Database;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backload.Demo.Models
{

    /// <summary>
    /// Represents a custom file entity to store an uploaded file
    /// </summary>
    [Table("Files4")]
    public class File
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public File()
        { 
        }

        /// <summary>
        /// Constructor. Used to update initialize the current entity with Backload provided file date.
        /// </summary>
        /// <param name="file">File data</param>
        public File(IBackloadStorageProviderFile file)
        {
            var custom = (CustomProviderFile)file;

            this.Update(file, custom.Description);
        }




        /// <summary>
        /// Auto generated row number
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RowId { get; set; }


        /// <summary>
        /// GUID used to identify the file
        /// </summary>
        [Key]
        public Guid FileId { get; set; }


        /// <summary>
        /// File name
        /// </summary>
        [Required]
        [StringLength(256)]
        public string FileName { get; set; }


        /// <summary>
        /// Mime type of the file (e.g. image/jpg)
        /// </summary>
        [Required]
        [StringLength(25)]
        public string ContentType { get; set; }


        /// <summary>
        /// File size in bytes
        /// </summary>
        [Required]
        public long FileSize { get; set; }


        /// <summary>
        /// Upload time of the file (or last chunk)
        /// </summary>
        [Required]
        public DateTime UploadTime { get; set; }


        /// <summary>
        /// File data (byte array). This column is null if the file is stored 
        /// with option "FileSystem" or "SqlFileTable" enabled in the configuration
        /// </summary>
        public byte[] Data { get; set; }


        /// <summary>
        /// Preview file as byte array. This column is null, generation of 
        /// previews (thumbnails) is disabled in Web.Backload.config
        /// </summary>
        public byte[] Preview { get; set; }


        /// <summary>
        /// A virtual path used to identify a file by path and name (usually id)
        /// </summary>
        [Required]
        [StringLength(512)]
        public string Path { get; set; }


        /// <summary>
        /// Internally used to store temp file data (usually on chunk uploads)
        /// </summary>
        [MaxLength(512)]
        public byte[] Meta { get; set; }



        private string _description = "No description available";

        /// <summary>
        /// Additional custom column to a file description
        /// </summary>
        [Required]
        public string Description { get { return _description; } set { _description = value; } }



        /// <summary>
        /// Updates the current instance with provided data
        /// </summary>
        /// <param name="file">An IBackloadStorageProviderFile object</param>
        /// <param name="description">Optional: A file description if available</param>
        /// <returns>Current object</returns>
        internal File Update(IBackloadStorageProviderFile file, string description = "No description available")
        {
            this.RowId = file.RowId;
            this.FileId = file.Id;
            this.FileName = file.Name;
            this.ContentType = file.Type;
            this.FileSize = file.Size;
            this.UploadTime = file.UploadTime;
            this.Data = file.Data;
            this.Preview = file.Preview;
            this.Path = file.Path;
            this.Meta = file.Meta;

            this.Description = description;

            return this;
        }



        #region Implicit/explicit cast operators

        /// <summary>
        /// Cast operator to cast a IBackloadStorageProviderFile instance to an Upload entity.
        /// </summary>
        /// <param name="file">IBackloadStorageProviderFile instance used by the Backload Database Plugin</param>
        /// <returns>A new Upload entity</returns>
        public static explicit operator File(CustomProviderFile file)
        {
            return new File(file);
        }
        
        /// <summary>
        /// Cast operator to cast an Upload entity to a IBackloadStorageProviderFile implementation
        /// which is used by the Backload Database plugin.
        /// </summary>
        /// <param name="upload">An Upload entity</param>
        /// <returns>A new IBackloadStorageProviderFile instance used by the Backload Database Plugin</returns>
        public static implicit operator CustomProviderFile(File upload) 
        {
            var file = new CustomProviderFile();
            file.RowId = upload.RowId;
            file.Id = upload.FileId;
            file.Name = upload.FileName;
            file.Original = upload.FileName;
            file.Type = upload.ContentType;
            file.Size = upload.FileSize;
            file.UploadTime = upload.UploadTime;
            file.Data = upload.Data;
            file.Preview = upload.Preview;
            file.Path = upload.Path;
            file.Meta = upload.Meta;
            file.Description = upload.Description;

            return file;
        }

        #endregion Implicit/explicit cast operators

    }
}
