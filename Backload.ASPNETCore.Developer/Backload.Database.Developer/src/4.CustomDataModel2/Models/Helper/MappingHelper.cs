using Backload.Contracts.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;


namespace Backload.Demo.Models
{

    /// <summary>
    /// Database context, implements IBackloadStorageProvider
    /// </summary>
    public partial class FilesContext : DbContext, IBackloadStorageProvider
    {

        /// <summary>
        /// For demo purposes only:
        /// Creates an expression for the where clause to pre-filter the result based on properties in the ICommandArgs parameter
        /// </summary>
        /// <param name="args">ICommandArgument instance</param>
        /// <returns>An Expression for the where clause of the Get() method</returns>
        private Expression<Func<File, bool>> GetWhereExpression(ICommandArgument args) 
        {
            Expression<Func<File, bool>> express = (e => true);
            if (args.CommandType == CommandArgumentType.GetFilesWithPattern) express = (e => e.FileName == args.Pattern);
            else if (args.CommandType == CommandArgumentType.GetFileById) express = (e => e.FileId == args.FileId);

            return express;
        }


        /// <summary>
        /// Needed with custom data model.
        /// Converts the entity type (File) to a CustomProviderFile type (your IBackloadStorageProviderFile implementation).
        /// The database plugin uses IBackloadStorageProviderFile internally.
        /// </summary>
        /// <param name="args">ICommandArgument instance</param>
        /// <returns>An Expression for the select clause of the Get() method</returns>
        private Expression<Func<File, CustomProviderFile>> GetSelectExpression(ICommandArgument args) 
        {
            Expression<Func<File, CustomProviderFile>> express =
                e => new CustomProviderFile() { 
                RowId = e.RowId,
                Id = e.FileId,
                Name = e.FileName,
                Original = e.FileName,
                Type = e.ContentType,
                Size = e.FileSize,
                UploadTime = e.UploadTime,
                Data = ((args.LoadData) ? e.Data : null), // Load file data into memory only in requested
                Preview = e.Preview,
                Path = e.Path,
                Meta = e.Meta,
                Description = e.Description
            };

            return express;
        }

    }
}
