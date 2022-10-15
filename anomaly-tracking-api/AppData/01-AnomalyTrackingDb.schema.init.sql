
Use [master]
GO

DROP DATABASE IF EXISTS [AnomalyTrackingDb]
GO

CREATE DATABASE [AnomalyTrackingDb]
GO

USE [AnomalyTrackingDb]
GO

CREATE TABLE [dbo].[LvfUserRoleDb] (
	[Id]								INT NOT NULL,
	[Label]								NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_LvfUserRoleDb] PRIMARY KEY CLUSTERED ([Id] ASC),
);



CREATE TABLE [dbo].[UserDb](
    [Id]								    INT	IDENTITY(1,1) NOT NULL,
	[FirstName]								NVARCHAR(50) NOT NULL,
	[LastName]								NVARCHAR(50) NOT NULL,
	[Code]								    NVARCHAR(50) NOT NULL,
	[Password]								NVARCHAR(10) NULL,
    [LvfUserRole]							INT NOT NULL,
	[LastModificationDate]				    DATETIME NOT NULL,

    CONSTRAINT [FK_UserDb_LvfUserRole_Id] FOREIGN KEY (LvfUserRole) REFERENCES [dbo].[LvfUserRoleDb] ([Id]),
	CONSTRAINT [PK_UserDb] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ClientAddressDb](
	[Id]									INT	IDENTITY(1,1) NOT NULL,
	[StreetNumber]							NVARCHAR(10) NULL,
	[StreetName]							NVARCHAR(50) NULL,
	[ZipCode]								NVARCHAR(10) NULL,
	[City]									NVARCHAR(50) NULL,
	[CountryCode]							NVARCHAR(3) NULL DEFAULT 'MAR',
	[FurtherInformation]					NVARCHAR(100) NULL,

	CONSTRAINT [PK_ClientAddressDb] PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[ClientDb] (
	[Id]								INT	IDENTITY(1,1) NOT NULL,
	[Label]								NVARCHAR(50) NOT NULL,
	[Email]								NVARCHAR(50) NOT NULL,
	[PhoneNumber]						NVARCHAR(25) NULL,
	[AddressId]								INT NULL,
	[LastModificationDate]				DATETIME NOT NULL,

	CONSTRAINT [PK_ClientDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_ClientDb_ClientAddressDb_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[ClientAddressDb] ([Id]),
	CONSTRAINT [UK_ClientDb_Name] UNIQUE ([Label]),

);

CREATE TABLE [dbo].[MoldDb] (
	[Id]								INT	IDENTITY(1,1) NOT NULL,
	[Label]								NVARCHAR(50) NOT NULL,
	[LastModificationDate]				    DATETIME NOT NULL,
	[ClientId]								INT	 NOT NULL,

	CONSTRAINT [PK_MoldDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	 CONSTRAINT [FK_MoldDb_ClientdB_Id] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[ClientDb] ([Id])
);
CREATE TABLE [dbo].[CavityDb] (
	[Id]								INT	IDENTITY(1,1) NOT NULL,
	[Label]								NVARCHAR(50) NOT NULL,
	[LastModificationDate]				    DATETIME NOT NULL,
	[MoldId]								INT	 NOT NULL,

	CONSTRAINT [PK_CavityDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	 CONSTRAINT [FK_CavityDb_MolddB_Id] FOREIGN KEY ([MoldId]) REFERENCES [dbo].[MoldDb] ([Id])
);

CREATE TABLE [dbo].[ProductDb] (
	[Id]								INT	IDENTITY(1,1) NOT NULL,
	[Ref]								NVARCHAR(50) NOT NULL,
	[LastModificationDate]				    DATETIME NOT NULL,
	CONSTRAINT [PK_ProductDb] PRIMARY KEY CLUSTERED ([Id] ASC),
);
CREATE TABLE [dbo].[ProcessDb](
	[Id]									INT	IDENTITY(1,1) NOT NULL,
	[Label]							NVARCHAR(50)NULL,
	[LastModificationDate]				    DATETIME NOT NULL,
	CONSTRAINT [PK_ProcessDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	 
	
);

CREATE TABLE [dbo].[AnomalyTypeDb](
	[Id]									INT	IDENTITY(1,1) NOT NULL,
	[Label]							NVARCHAR(250)NULL,
	[LastModificationDate]				    DATETIME NOT NULL,
	[ProcessId]								INT	 NOT NULL,
	CONSTRAINT [PK_AnomalyTypeDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_AnomalyTypeDb_ProcessDb_ProcessId] FOREIGN KEY ([ProcessId]) REFERENCES [dbo].[ProcessDb] ([Id]),
	
);


CREATE TABLE [dbo].[AnomalyDb](

	[Id]									       INT	IDENTITY(1,1) NOT NULL,
	[AnomalyTypeId]						  	       INT	 NOT NULL,
	[FaceId]						  	           INT	 NOT NULL,

	CONSTRAINT [PK_AnomalyDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_AnomalyDb_AnomalyType_Id] FOREIGN KEY ([AnomalyTypeId]) REFERENCES [dbo].[AnomalyTypeDb] ([Id]),
);


CREATE TABLE [dbo].[FaceDb](
	[Id]									INT	IDENTITY(1,1) NOT NULL,
	[Label]						  	        NVARCHAR(50)NULL,
	[LastModificationDate]				    DATETIME NOT NULL,
	[Position]						  	    NVARCHAR(50)NULL,
	[AnomalyId]						  	     INT	 NOT NULL,



	CONSTRAINT [PK_FaceDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	 CONSTRAINT [FK_FaceDb_AnomalydB_Id] FOREIGN KEY ([AnomalyId]) REFERENCES [dbo].[AnomalyDb] ([Id])

);

CREATE TABLE [dbo].[AnomalyDeclarationDb](
	[Id]									INT	IDENTITY(1,1) NOT NULL,
	[LastModificationDate]				    DATETIME NOT NULL,
	[UserId]								INT	  NULL,
	[ProcessId]								INT	 NOT NULL,
	[CavityId]								INT	 NOT NULL,
	[AnomalyId]								INT	  NULL,
	[ProductId]								INT	  NULL,

	CONSTRAINT [PK_AnomalyDeclarationDb] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_AnomalyDeclarationDb_User_Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserDb] ([Id]),
	CONSTRAINT [FK_AnomalyDeclarationDb_Process_Id] FOREIGN KEY ([ProcessId]) REFERENCES [dbo].[ProcessDb] ([Id]),
	CONSTRAINT [FK_AnomalyDeclarationDb_Cavity_Id] FOREIGN KEY ([CavityId]) REFERENCES [dbo].[CavityDb] ([Id]),
	CONSTRAINT [FK_AnomalyDeclarationDb_Anomaly_Id] FOREIGN KEY ([AnomalyId]) REFERENCES [dbo].[AnomalyDb] ([Id]),

);


