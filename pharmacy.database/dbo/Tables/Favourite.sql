CREATE TABLE [dbo].[Favourite] (
    [FavouriteID] UNIQUEIDENTIFIER NOT NULL,
    [CustomerID]  UNIQUEIDENTIFIER NOT NULL,
    [DrugID]      UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         NOT NULL,
    CONSTRAINT [PK_dbo.Favourite] PRIMARY KEY CLUSTERED ([FavouriteID] ASC),
    CONSTRAINT [FK_dbo.Favourite_dbo.Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customer] ([CustomerID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Favourite_dbo.Drug_DrugID] FOREIGN KEY ([DrugID]) REFERENCES [dbo].[Drug] ([DrugID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CustomerID]
    ON [dbo].[Favourite]([CustomerID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DrugID]
    ON [dbo].[Favourite]([DrugID] ASC);

