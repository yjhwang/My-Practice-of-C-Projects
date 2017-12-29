using Backload.Contracts.Services.Database;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace Backload.Demo.Models
{

    /// <summary>
    /// Database context, implements IBackloadStorageProvider
    /// </summary>
    public partial class FilesContext : DbContext, IBackloadStorageProvider
    {

        /// <summary>
        /// For demo purposes only:
        /// Creates an expression for the where clause to pre-filter the result based on some args parameters
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
        /// Converts the entity type (File) to a BackloadStorageProviderFile type. Backload usually provides and 
        /// expects IBackloadStorageProviderFile or an IEnumerable of this type as parameter or return values.
        /// </summary>
        /// <param name="args">ICommandArgument instance</param>
        /// <returns>An Expression for the select clause of the Get() method</returns>
        private Expression<Func<File, BackloadStorageProviderFile>> GetSelectExpression(ICommandArgument args) 
        {
            Expression<Func<File, BackloadStorageProviderFile>> express = 
                e => new BackloadStorageProviderFile() { 
                RowId = e.RowId,
                Id = e.FileId,
                Name = e.FileName,
                Original = e.OriginalName,
                Type = e.ContentType,
                Size = e.FileSize,
                UploadTime = e.UploadTime,
                Data = ((args.LoadData) ? e.Data : null), // Load file data into memory only in requested
                Preview = e.Preview,
                Path = e.Path,
                Meta = e.Meta
            };

            return express;
        }

    }
}
