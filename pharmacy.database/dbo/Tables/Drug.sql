CREATE TABLE [dbo].[Drug] (
    [DrugID]     UNIQUEIDENTIFIER NOT NULL,
    [DrugName]   NVARCHAR (256)   NOT NULL,
    [DrugDose]   NVARCHAR (256)   NULL,
    [PackSize]   INT              NOT NULL,
    [CreatedOn]  DATETIME         NOT NULL,
    [ModifiedOn] DATETIME         NULL,
    CONSTRAINT [PK_dbo.Drug] PRIMARY KEY CLUSTERED ([DrugID] ASC)
);

