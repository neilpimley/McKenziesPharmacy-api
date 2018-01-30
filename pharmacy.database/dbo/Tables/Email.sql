CREATE TABLE [dbo].[Email] (
    [EmailID]     UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]  UNIQUEIDENTIFIER NOT NULL,
    [ToAddress]   NVARCHAR (128)   NULL,
    [FromAddress] NVARCHAR (128)   NULL,
    [CcAddress]   NVARCHAR (128)   NULL,
    [Subject]     NVARCHAR (256)   NULL,
    [Body]        NVARCHAR (MAX)   NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    [Sent]        BIT              NOT NULL,
    CONSTRAINT [PK_dbo.Email] PRIMARY KEY CLUSTERED ([EmailID] ASC),
    CONSTRAINT [FK_dbo.Email_dbo.Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID])
);


GO
CREATE NONCLUSTERED INDEX [IX_CustomerID]
    ON [dbo].[Email]([CustomerID] ASC);

