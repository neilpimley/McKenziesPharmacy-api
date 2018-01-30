CREATE TABLE [dbo].[Shop] (
    [ShopID]     UNIQUEIDENTIFIER NOT NULL,
    [ShopName]   NVARCHAR (MAX)   NULL,
    [Phone]      NVARCHAR (MAX)   NULL,
    [Fax]        NVARCHAR (MAX)   NULL,
    [Email]      NVARCHAR (MAX)   NULL,
    [AddressID]  UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]  DATETIME         NOT NULL,
    [ModifiedOn] DATETIME         NULL,
    CONSTRAINT [PK_dbo.Shop] PRIMARY KEY CLUSTERED ([ShopID] ASC),
    CONSTRAINT [FK_dbo.Shop_dbo.Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID])
);


GO
CREATE NONCLUSTERED INDEX [IX_AddressID]
    ON [dbo].[Shop]([AddressID] ASC);

