CREATE TABLE [dbo].[Title] (
    [TitleID]   UNIQUEIDENTIFIER NOT NULL,
    [TitleName] NVARCHAR (MAX)   NULL,
    [CreatedOn] DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.Title] PRIMARY KEY CLUSTERED ([TitleID] ASC)
);

