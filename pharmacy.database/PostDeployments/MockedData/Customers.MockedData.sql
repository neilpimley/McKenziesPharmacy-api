INSERT [dbo].[Address] ([AddressID], [AddressLine1], [AddressLine2], [AddressLine3], [Town], [County], [Postcode], [CreatedOn]) VALUES (N'3f270a18-e1c8-46a8-b846-f8fdd984c371', N'106 Greystown Avenue', NULL, NULL, NULL, NULL, N'BT9 6UL', CAST(N'2017-01-24 15:25:30.717' AS DateTime))
GO

INSERT INTO [dbo].[Customer] ([CustomerID],[UserID],[Email],[TitleID],[Firstname],[Lastname],[Sex],[Mobile],[Home],[Dob],[AddressID],[DoctorID],[ShopID],[CreatedOn],[ModifiedOn],[Active])
VALUES ('E4ACF66E-C638-44DC-8536-4918B82E452D','dummy-user-1','dummy-user-1@neilpimley.com','089a91a4-3fe0-4be3-97e1-de828df5db35','John','Dummy','Male','07909864966',null,'2004-09-19 00:00','3f270a18-e1c8-46a8-b846-f8fdd984c371','db8b6b6a-011e-48a3-93e6-04045fba48ac','3ac0c2f6-0ef2-4f0a-a120-02f15adb808c','2017-09-19 00:00',null,1)
GO
