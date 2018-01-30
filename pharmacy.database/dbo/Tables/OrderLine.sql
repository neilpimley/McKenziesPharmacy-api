CREATE TABLE [dbo].[OrderLine] (
    [OrderLineID]     UNIQUEIDENTIFIER NOT NULL,
    [OrderID]         UNIQUEIDENTIFIER NOT NULL,
    [DrugID]          UNIQUEIDENTIFIER NOT NULL,
    [Qty]             INT              NOT NULL,
    [CreatedOn]       DATETIME         NOT NULL,
    [OrderLineStatus] INT              NULL,
    CONSTRAINT [PK_dbo.OrderLine] PRIMARY KEY CLUSTERED ([OrderLineID] ASC),
    CONSTRAINT [FK_dbo.OrderLine_dbo.Drug_DrugID] FOREIGN KEY ([DrugID]) REFERENCES [dbo].[Drug] ([DrugID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.OrderLine_dbo.Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([OrderID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DrugID]
    ON [dbo].[OrderLine]([DrugID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OrderID]
    ON [dbo].[OrderLine]([OrderID] ASC);

