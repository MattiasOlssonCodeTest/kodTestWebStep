CREATE DATABASE ListsAndTasksDb;
GO

USE [ListsAndTasksDb]
GO

/****** Object: Table [dbo].[Tasks] Script Date: 2023-09-18 10:21:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Lists] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Tasks] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [ListId]      INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tasks_ToLists] FOREIGN KEY ([ListId]) REFERENCES [dbo].[Lists] ([Id]) ON DELETE CASCADE
);
