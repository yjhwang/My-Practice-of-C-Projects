using Backload.Contracts.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
//#if NET461
//using System.Data.Entity;
//#endif

namespace Backload.Demo.Models
{

    /// <summary>
    /// Database context, implements IBackloadStorageProvider
    /// </summary>
    public class FilesContext : DbContext, IBackloadStorageProvider
    {

        /// <summary>
        /// The main DBSet
        /// </summary>
        public DbSet<File> Files { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Context init options</param>
        public FilesContext(DbContextOptions<FilesContext> options) : base(options)
        {
        }



        #region IBackloadStorageProvider implementation


        /// <summary>
        /// Returns a list of files stored in the database matching the criteria in args
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>A list of files </returns>
        public IEnumerable<IBackloadStorageProviderFile> Get(ICommandArgument args)
        {
            return this.Files;
        }



        /// <summary>
        /// Adds a new entity (file)
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>An IBackloadStorageProviderFile instance</returns>
        public IBackloadStorageProviderFile Add(ICommandArgument args)
        {
            var f = this.Files.Add(new File(args.FileContext));
            if (args.SaveChanges) this.SaveChanges();

            return f.Entity;
        }



        /// <summary>
        /// Updates a file entity or adds a new one if no file with the file id exists
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>An IBackloadStorageProviderFile instance</returns>
        public IBackloadStorageProviderFile Update(ICommandArgument args)
        {
            File f = this.Files.Where(e => e.Id == args.FileId).FirstOrDefault();
            
            // Update or add file to context
            if (f != null) f.Update(args.FileContext);
            else f = this.Files.Add(new File(args.FileContext)).Entity;
            if (args.SaveChanges) this.SaveChanges();

            return f;
        }



        /// <summary>
        /// Removes an entity from the database context
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>IBackloadStorageProviderFile instance</returns>
        public IBackloadStorageProviderFile Remove(ICommandArgument args)
        {
            var file = this.Files.Where(e => e.Id == args.FileId).FirstOrDefault();
            if (file != null)
            {
                this.Files.Remove(file);
                if (args.SaveChanges) this.SaveChanges();
            }

            return file;
        }


        /// <summary>
        /// Executes an SQL SELECT command with parameters.
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>A list of type T (Usually IBackloadStorageProviderFile)</returns>
        public IQueryable<IBackloadStorageProviderFile> SqlQueryCore(ICommandArgument args)
        {
            IQueryable<IBackloadStorageProviderFile> files = null;
            files = this.Files.FromSql<File>(args.SqlCommand, args.SqlParameter).Select(e => new BackloadStorageProviderFile(e.Size, e.Meta, e.Data));

            return files;
        }



        /// <summary>
        /// Executes an SQL SELECT command with parameters.
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>A list of type T</returns>
        public IQueryable<T> SqlQuery<T>(ICommandArgument args)
        {
            return this.Files.FromSql<File>(args.SqlCommand, args.SqlParameter).Cast<T>();
        }



        /// <summary>
        /// Executes an SQL action command (Update, Insert, Delete) with parameters
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>Number of rows affected</returns>
        public int SqlExecute(ICommandArgument args)
        {
            //string sql = args.ColumnMapping.GetSqlCommand(args, _mapTable);
            if (this.Database.ExecuteSqlCommand(args.SqlCommand, args.SqlParameter) == 0)
                return 0;

            // Set entity state to be modified by SQL, 
            var file = this.Files.Where(e => e.Id == args.FileId).FirstOrDefault();
            this.Entry(file).State = EntityState.Detached;

            return 1;
        }

#endregion IBackloadStorageProvider implementation

    }
}
