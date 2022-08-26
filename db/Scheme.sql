IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Bank')
BEGIN
    CREATE DATABASE [Bank]
END
GO

USE [Bank]
GO

/* Chain */
IF NOT EXISTS (SELECT * FROM sys.tables WHERE [object_id] = object_id('Chain'))
BEGIN
CREATE TABLE [dbo].[Chain] (
	[Id] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
    PRIMARY KEY ([Id]))
END
GO

ALTER TABLE [dbo].[Chain] ADD  CONSTRAINT [DF_Chain_Created]  DEFAULT (getdate()) FOR [Created]
GO

/* User */
IF NOT EXISTS (SELECT * FROM sys.tables WHERE [object_id] = object_id('User'))
BEGIN
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NULL,
	PRIMARY KEY ([Id]))
END
GO

/* Profile */
IF NOT EXISTS (SELECT * FROM sys.tables WHERE [object_id] = object_id('Profile'))
BEGIN
CREATE TABLE [dbo].[Profile](
	[Id] [uniqueidentifier] NOT NULL,
	[UserFk] [uniqueidentifier] NOT NULL,
	[PasswordHash] [varchar](32) NOT NULL,
	PRIMARY KEY ([Id])) 
END
GO

ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_User] FOREIGN KEY([UserFk]) REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_User]
GO

/* FinanceOperation */
IF NOT EXISTS (SELECT * FROM sys.tables WHERE [object_id] = object_id('FinanceOperation'))
BEGIN
CREATE TABLE [dbo].[FinanceOperation](
	[Id] [uniqueidentifier] NOT NULL,
	[UserFk] [uniqueidentifier] NOT NULL,
	[ChainFk] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[IsApproved] [bit] NOT NULL,
	PRIMARY KEY ([Id]))
END
GO

ALTER TABLE [dbo].[FinanceOperation] ADD  CONSTRAINT [DF_FinanceOperation_IsApproved]  DEFAULT ((0)) FOR [IsApproved]
GO

ALTER TABLE [dbo].[FinanceOperation]  WITH CHECK ADD  CONSTRAINT [FK_FinanceOperation_Chain] FOREIGN KEY([ChainFk]) REFERENCES [dbo].[Chain] ([Id])
GO

ALTER TABLE [dbo].[FinanceOperation] CHECK CONSTRAINT [FK_FinanceOperation_Chain]
GO

ALTER TABLE [dbo].[FinanceOperation]  WITH CHECK ADD  CONSTRAINT [FK_FinanceOperation_User] FOREIGN KEY([UserFk]) REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[FinanceOperation] CHECK CONSTRAINT [FK_FinanceOperation_User]
GO


