CREATE TABLE [dbo].[Practice] (
    [PracticeID]   UNIQUEIDENTIFIER NOT NULL,
    [PracticeName] NVARCHAR (256)   NULL,
    [AddressID]    UNIQUEIDENTIFIER NOT NULL,
    [Phone]        NVARCHAR (50)    NULL,
    [EmailAddress] NVARCHAR (128)   NULL,
    [CreatedOn]    DATETIME         NOT NULL,
    [ModifiedOn]   DATETIME         NULL,
    CONSTRAINT [PK_dbo.Practice] PRIMARY KEY CLUSTERED ([PracticeID] ASC),
    CONSTRAINT [FK_dbo.Practice_dbo.Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID])
);


GO
CREATE NONCLUSTERED INDEX [IX_AddressID]
    ON [dbo].[Practice]([AddressID] ASC);

