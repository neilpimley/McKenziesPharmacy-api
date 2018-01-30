CREATE TABLE [dbo].[Customer] (
    [CustomerID] UNIQUEIDENTIFIER NOT NULL,
    [UserID]     NVARCHAR (50)    NOT NULL,
    [Email]      NVARCHAR (MAX)   NOT NULL,
    [TitleID]    UNIQUEIDENTIFIER NOT NULL,
    [Firstname]  NVARCHAR (MAX)   NOT NULL,
    [Lastname]   NVARCHAR (MAX)   NOT NULL,
    [Sex]        NVARCHAR (MAX)   NOT NULL,
    [Mobile]     NVARCHAR (MAX)   NULL,
    [Home]       NVARCHAR (MAX)   NULL,
    [Dob]        DATETIME         NOT NULL,
    [AddressID]  UNIQUEIDENTIFIER NOT NULL,
    [DoctorID]   UNIQUEIDENTIFIER NOT NULL,
    [ShopID]     UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]  DATETIME         NOT NULL,
    [ModifiedOn] DATETIME         NULL,
    [Active]     BIT              NOT NULL,
    CONSTRAINT [PK_dbo.Customer] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_dbo.Customer_dbo.Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Customer_dbo.Doctor_DoctorID] FOREIGN KEY ([DoctorID]) REFERENCES [dbo].[Doctor] ([DoctorID]),
    CONSTRAINT [FK_dbo.Customer_dbo.Shop_ShopID] FOREIGN KEY ([ShopID]) REFERENCES [dbo].[Shop] ([ShopID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Customer_dbo.Title_TitleID] FOREIGN KEY ([TitleID]) REFERENCES [dbo].[Title] ([TitleID])
);

