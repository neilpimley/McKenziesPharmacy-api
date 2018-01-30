CREATE TABLE [dbo].[Doctor] (
    [DoctorID]   UNIQUEIDENTIFIER NOT NULL,
    [PracticeID] UNIQUEIDENTIFIER NOT NULL,
    [TitleID]    UNIQUEIDENTIFIER NOT NULL,
    [Firstname]  NVARCHAR (100)   NULL,
    [Surname]    NVARCHAR (100)   NOT NULL,
    [CreatedOn]  DATETIME         NOT NULL,
    [ModifiedOn] DATETIME         NULL,
    CONSTRAINT [PK_dbo.Doctor] PRIMARY KEY CLUSTERED ([DoctorID] ASC),
    CONSTRAINT [FK_dbo.Doctor_dbo.Practice_PracticeID] FOREIGN KEY ([PracticeID]) REFERENCES [dbo].[Practice] ([PracticeID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Doctor_dbo.Title_TitleID] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);


GO
CREATE NONCLUSTERED INDEX [IX_PracticeID]
    ON [dbo].[Doctor]([PracticeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TitleID]
    ON [dbo].[Doctor]([TitleID] ASC);

