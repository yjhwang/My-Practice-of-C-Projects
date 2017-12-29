using Backload.Contracts.Services.Database;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backload.Demo.Models
{

    /// <summary>
    /// Represents a custom file entity to store an uploaded file
    /// </summary>
    [Table("Files3")]
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
            this.Update(file);
        }

        /// <summary>
        /// Constructor. Used to update initialize the current entity with Backload provided file date.
        /// </summary>
        /// <param name="file">File data</param>
        /// <param name="loggedOnUser">Used for a fictional user id we add to the column in this demo</param>
        public File(IBackloadStorageProviderFile file, Guid loggedOnUser)
        {
            this.Update(file);
            this.UserId = loggedOnUser;
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
        /// Original file name
        /// </summary>
        [Required]
        [StringLength(256)]
        public string OriginalName { get; set; }


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



        /// <summary>
        /// Additional custom column to store a user id
        /// </summary>
        [Required]
        public Guid UserId { get; set; }



        /// <summary>
        /// Updates the current instance with provided data
        /// </summary>
        /// <param name="file">An IBackloadStorageProviderFile object</param>
        /// <returns>Current object</returns>
        internal File Update(IBackloadStorageProviderFile file)
        {
            this.RowId = file.RowId;
            this.FileId = file.Id;
            this.FileName = file.Name;
            this.OriginalName = file.Original;
            this.ContentType = file.Type;
            this.FileSize = file.Size;
            this.UploadTime = file.UploadTime;
            this.Data = file.Data;
            this.Preview = file.Preview;
            this.Path = file.Path;
            this.Meta = file.Meta;

            return this;
        }

        /// <summary>
        /// Returns a BackloadStorageProviderFile from an File entity.
        /// </summary>
        /// <returns>A BackloadStorageProviderFile instance</returns>
        public BackloadStorageProviderFile ToProviderFile()
        {
            var file = new BackloadStorageProviderFile();
            file.RowId = this.RowId;
            file.Id = this.FileId;
            file.Name = this.FileName;
            file.Original = this.OriginalName;
            file.Type = this.ContentType;
            file.Size = this.FileSize;
            file.UploadTime = DateTime.UtcNow;
            file.Data = this.Data;
            file.Preview = this.Preview;
            file.Path = this.Path;
            file.Meta = this.Meta;

            return file;
        }


        #region Implicit/explicit cast operators

        /// <summary>
        /// Cast operator to cast a IBackloadStorageProviderFile instance to an File entity.
        /// </summary>
        /// <param name="file">IBackloadStorageProviderFile instance used by the Backload Database Plugin</param>
        /// <returns>A new File entity</returns>
        public static explicit operator File(BackloadStorageProviderFile file)
        {
            return new File(file);
        }
        
        /// <summary>
        /// Cast operator to cast an File entity to a IBackloadStorageProviderFile instance
        /// which is used by the Backload Database plugin.
        /// </summary>
        /// <param name="upload">An File entity</param>
        /// <returns>A new IBackloadStorageProviderFile instance used by the Backload Database Plugin</returns>
        public static implicit operator BackloadStorageProviderFile(File upload) 
        {
            return (BackloadStorageProviderFile)upload.ToProviderFile();
        }

        #endregion Implicit/explicit cast operators

    }
}
