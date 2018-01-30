CREATE TABLE [dbo].[Order] (
    [OrderID]     UNIQUEIDENTIFIER NOT NULL,
    [OrderDate]   DATETIME         NOT NULL,
    [NoteID]      UNIQUEIDENTIFIER NULL,
    [CustomerID]  UNIQUEIDENTIFIER NULL,
    [OrderStatus] INT              NOT NULL,
    CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [FK_dbo.Order_dbo.Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]),
    CONSTRAINT [FK_dbo.Order_dbo.Note_NoteID] FOREIGN KEY ([NoteID]) REFERENCES [dbo].[Note] ([NoteID])
);


GO
CREATE NONCLUSTERED INDEX [IX_CustomerID]
    ON [dbo].[Order]([CustomerID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_NoteID]
    ON [dbo].[Order]([NoteID] ASC);

