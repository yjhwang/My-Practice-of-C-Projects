using Backload.Contracts.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Backload.Demo.Models
{

    /// <summary>
    /// Database context, implements IBackloadMappedStorageProvider which enhances IBackloadStorageProvider with a ColumnMapping attribute
    /// </summary>
    public partial class FilesContext : DbContext, IBackloadMappedStorageProvider
    {
        // Mapping table used for raw SQL commands. Set only names that differ from the defaults.
        private static IMappingTable _mapTable = new MappingTable() { Id = "FileId", Name = "FileName", Type = "ContentType", Size = "FileSize" };


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
            // Maps the custom column names. Column mapping is only needed for raw SQL commands. Providing a IColumnMapping instance 
            // enables Backload to format SQL commands with custom column names. This can also be done in configuration (Example 6).
            _mapping = new ColumnMapping(_mapTable);
        }



        #region IBackloadMappedStorageProvider implementation

        private IColumnMapping _mapping;

        /// <summary>
        /// ColumnMapping provides properties and methods to map columns for SQL commands.
        /// </summary>
        public IColumnMapping ColumnMapping { get { return _mapping; } }



        /// <summary>
        /// Returns a list of files stored in the database matching the criteria in args
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>A list of files </returns>
        public IEnumerable<IBackloadStorageProviderFile> Get(ICommandArgument args)
        {
            // Optional: Let's make use of the properties in args to pre-filter and convert the result
            var where = GetWhereExpression(args);
            var select = GetSelectExpression(args);

            // Pre-filter the results and cast the result into BackloadStorageProviderFile enumeration
            return this.Files.Where(where).Select(select);
        }



        /// <summary>
        /// Adds or updates an  entity (file)
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>A CustomProviderFile instance (your IBackloadStorageProviderFile implementation</returns>
        public IBackloadStorageProviderFile Add(ICommandArgument args)
        {
            var f = this.Files.Add(new File(args.FileContext));
            if (args.SaveChanges) this.SaveChanges();

            // Cast to BackloadStorageProviderFile. You can also use: f.ToProviderFile(); (see entity definition in File.cs)
            return (CustomProviderFile)f.Entity;
        }



        /// <summary>
        /// Updates a file entity or adds a new one if no file with the file id exists
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>An IBackloadStorageProviderFile instance</returns>
        public IBackloadStorageProviderFile Update(ICommandArgument args)
        {
            File f = this.Files.Where(e => e.FileId == args.FileId).FirstOrDefault();

            // Update or add file to context
            if (f != null) f.Update(args.FileContext);
            else f = this.Files.Add(new File(args.FileContext)).Entity;
            if (args.SaveChanges) this.SaveChanges();

            return (CustomProviderFile)f;
        }



        /// <summary>
        /// Removes an entity from the database context
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>IBackloadStorageProviderFile instance</returns>
        public IBackloadStorageProviderFile Remove(ICommandArgument args)
        {
            var file = this.Files.Where(e => e.FileId == args.FileId).FirstOrDefault();
            if (file != null)
            {
                this.Files.Remove(file);
                if (args.SaveChanges) this.SaveChanges();
            }

            // Cast to CustomProviderFile. You can also use: f.ToProviderFile(); (see entity definition in File.cs)
            return (CustomProviderFile)file;
        }


        /// <summary>
        /// Executes an SQL SELECT command with parameters.
        /// </summary>
        /// <param name="args">Provides command parameters</param>
        /// <returns>A list of type T (Usually IBackloadStorageProviderFile)</returns>
        public IQueryable<IBackloadStorageProviderFile> SqlQueryCore(ICommandArgument args)
        {
            IQueryable<IBackloadStorageProviderFile> files = null;
            files = this.Files.FromSql<File>(args.SqlCommand, args.SqlParameter).Select(e => new CustomProviderFile(e, args.LoadData));

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
            var file = this.Files.Where(e => e.FileId == args.FileId).FirstOrDefault();
            this.Entry(file).State = EntityState.Detached;

            return 1;
        }

        #endregion IBackloadStorageProvider implementation

    }
}
