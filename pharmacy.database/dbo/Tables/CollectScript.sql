CREATE TABLE [dbo].[CollectScript] (
    [OrderID]         UNIQUEIDENTIFIER NOT NULL,
    [OrderDate]       DATETIME         NOT NULL,
    [Customer]        NVARCHAR (MAX)   NOT NULL,
    [CustomerAddress] NVARCHAR (MAX)   NULL,
    [OrderStatus]     INT              NOT NULL,
    [NumItems]        INT              NOT NULL,
    [DoctorID]        UNIQUEIDENTIFIER NOT NULL,
    [ShopID]          UNIQUEIDENTIFIER NOT NULL,
    [Notes]           NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_dbo.CollectScript] PRIMARY KEY CLUSTERED ([OrderID] ASC),
    CONSTRAINT [FK_dbo.CollectScript_dbo.Doctor_DoctorID] FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctor] ([DoctorID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.CollectScript_dbo.Shop_ShopID] FOREIGN KEY ([ShopID]) REFERENCES [dbo].[Shop] ([ShopID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_DoctorID]
    ON [dbo].[CollectScript]([DoctorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ShopID]
    ON [dbo].[CollectScript]([ShopID] ASC);

