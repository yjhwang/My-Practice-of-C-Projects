using Backload.Contracts.Services.Database;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backload.Demo.Models
{

    /// <summary>
    /// Represents a file entity within the database context.
    /// The database layout can be completely different (see Examples 4 and 5), but Backload uses an
    /// IBackloadStorageProviderFile instance to communicate with your application.
    /// </summary>
    [Table("Files")]
    public class File : IBackloadStorageProviderFile
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public File()
        { 
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="file">File data</param>
        public File(IBackloadStorageProviderFile file)
        {
            this.Update(file);
        }



        /// <summary>
        /// Auto generated roe number
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RowId { get; set; }


        /// <summary>
        /// GUID used to identify the file
        /// </summary>
        [Key]
        public Guid Id { get; set; }


        /// <summary>
        /// File name
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Name { get; set; }


        /// <summary>
        /// Original file name
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Original { get; set; }


        /// <summary>
        /// Mime type of the file (e.g. image/jpg)
        /// </summary>
        [Required]
        [StringLength(25)]
        public string Type { get; set; }


        /// <summary>
        /// File size in bytes
        /// </summary>
        [Required]
        public long Size { get; set; }


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
        /// Preview file as byte array. This column is null if generation of 
        /// previews (thumbnails) is disabled in Web.Backload.config
        /// </summary>
        public byte[] Preview { get; set; }


        /// <summary>
        /// A virtual path used to identify a file by path and name (default: id)
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
        /// Updates the current instance.
        /// </summary>
        /// <param name="file">An IBackloadStorageProviderFile instance provided by Backload</param>
        /// <returns>Current entity</returns>
        public IBackloadStorageProviderFile Update(IBackloadStorageProviderFile file)
        {
            this.RowId = file.RowId;
            this.Id = file.Id;
            this.Name = file.Name;
            this.Original = file.Original;
            this.Type = file.Type;
            this.Size = file.Size;
            this.UploadTime = file.UploadTime;
            this.Data = file.Data;
            this.Preview = file.Preview;
            this.Path = file.Path;
            this.Meta = file.Meta;

            return this;
        }

    }
}
