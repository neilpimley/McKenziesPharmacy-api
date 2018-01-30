CREATE TABLE [dbo].[ReminderOrder] (
    [ReminderOrderID] UNIQUEIDENTIFIER NOT NULL,
    [ReminderID]      UNIQUEIDENTIFIER NOT NULL,
    [OrderID]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_reminder_order] PRIMARY KEY CLUSTERED ([ReminderOrderID] ASC),
    FOREIGN KEY ([ReminderID]) REFERENCES [dbo].[Reminder] ([ReminderID]),
    CONSTRAINT [FK__dbo.ReminderOrder__dbo.Reminder_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [dbo].[Order] ([OrderID])
);

