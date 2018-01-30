CREATE TABLE [dbo].[Reminder] (
    [ReminderID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerID] UNIQUEIDENTIFIER NOT NULL,
    [SendTime]   DATETIME         NOT NULL,
    [Sent]       BIT              DEFAULT ((0)) NOT NULL,
    [CreatedOn]  DATETIME         NOT NULL,
    CONSTRAINT [PK_reminder] PRIMARY KEY CLUSTERED ([ReminderID] ASC)
);

