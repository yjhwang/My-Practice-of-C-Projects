IMPORTANT NOTES

This demo uses a Ms SQL Server LocalDB database which is created 
upon the first request in "~/App_Data/". The LocalDB runtime must 
be installed and the instance name must be "MSSQLLocalDB" (see ~/Web.config).
You can change this name, if you have an existing LocalDB instance with different 
name (Sql Server 2012 has "v11.0" as instance name by default).

LocalDB for SqlServer 2014 (Express) is included in the SQL Server Advanced package. 
LocalDB for SqlServer 2012 (Express) can be downloaded as single package.

LocalDB can be managed (start/stop/delete/create instace) at the command promnt.
> SqlLocalDB i

You can also manage an existing instance with Sql Server Management Studio:
Connect to your instance in the connection dialog, for example: 
(localdb)\MSSQLLocalDB
 

VERY IMPORTANT: 
DO NOT DELETE THE DATABASE FILES BY HAND, BECAUSE THE LOCALDB ENGINE DOES
NOT RECONGNIZE THIS AND YOU ARE NOT ABLE TO CREATE THE FILES AGAIN. LOCAL 
DB TREATS THE FILES AS EXISTING DATABASE, AND YOU RECEIVE STRANGE ERRORS
LIKE "INVALID OBJECT NAME" OR "DATABASE ALREADY EXISTS".
IF THIS HAPPENS DELETE THE INSTANCE AT THE COMMAND PROMT, OR DELETE THE
CONNECTION WITHIN SQL SERVER MANAGEMENT STUDIO (SEE ABOVE).
MORE: http://odetocode.com/blogs/scott/archive/2012/08/15/a-troubleshooting-guide-for-entity-framework-connections-amp-migrations.aspx


*************************************************************************
SQL SCRIPT:

USE [FILES]

GO
CREATE TABLE [dbo].[Files](
	[RowId] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Original] [nvarchar](256) NOT NULL,
	[Type] [varchar](25) NOT NULL,
	[Size] [bigint] NOT NULL,
	[UploadTime] [datetime] NOT NULL,
	[Data] [varbinary](max) NULL,
	[Preview] [varbinary](max) NULL,
	[Path] [varchar](512) NOT NULL,
	[Meta] [varbinary](512) NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE TABLE [dbo].[Files3](
	[RowId] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[OriginalName] [nvarchar](256) NOT NULL,
	[ContentType] [varchar](25) NOT NULL,
	[FileSize] [bigint] NOT NULL,
	[UploadTime] [datetime] NOT NULL,
	[Data] [varbinary](max) NULL,
	[Preview] [varbinary](max) NULL,
	[Path] [varchar](512) NOT NULL,
	[Meta] [varbinary](512) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Files3] PRIMARY KEY CLUSTERED ([FileId] ASC)
)
GO

CREATE TABLE [dbo].[Files4](
	[RowId] [bigint] IDENTITY(1,1) NOT NULL,
	[FileId] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[ContentType] [varchar](25) NOT NULL,
	[FileSize] [bigint] NOT NULL,
	[UploadTime] [datetime] NOT NULL,
	[Data] [varbinary](max) NULL,
	[Preview] [varbinary](max) NULL,
	[Path] [varchar](512) NOT NULL,
	[Meta] [varbinary](512) NULL,
	[Description] [varchar](512) NULL,
 CONSTRAINT [PK_Files4] PRIMARY KEY CLUSTERED ([FileId] ASC)
)
GO