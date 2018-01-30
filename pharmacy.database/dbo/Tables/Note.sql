CREATE TABLE [dbo].[Note] (
    [NoteID]    UNIQUEIDENTIFIER NOT NULL,
    [Text]      NVARCHAR (MAX)   NULL,
    [CreatedOn] DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.Note] PRIMARY KEY CLUSTERED ([NoteID] ASC)
);

