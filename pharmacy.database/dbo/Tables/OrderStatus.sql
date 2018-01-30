CREATE TABLE [dbo].[OrderStatus] (
    [OrderStatusID]   UNIQUEIDENTIFIER NOT NULL,
    [UserID]          UNIQUEIDENTIFIER NULL,
    [OrderID]         UNIQUEIDENTIFIER NOT NULL,
    [Status]          INT              NOT NULL,
    [StatusSetDate]   DATETIME         NOT NULL,
    [OrderLineStatus] INT              NULL,
    CONSTRAINT [PK_dbo.OrderStatus] PRIMARY KEY CLUSTERED ([OrderStatusID] ASC),
    CONSTRAINT [FK_dbo.OrderStatus_dbo.Order_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([OrderID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_OrderID]
    ON [dbo].[OrderStatus]([OrderID] ASC);

