/*
Post-Deployment Script Template							
------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
------------------------------------------------------------------------------------
*/


PRINT N'Starting Post-Deployment [SEED] Script...';


--seed data
:r .\PostDeployments\SeedData\Titles.SeedData.sql
:r .\PostDeployments\SeedData\Address.SeedData.sql
:r .\PostDeployments\SeedData\Drugs.SeedData.sql
:r .\PostDeployments\SeedData\Practices.SeedData.sql
:r .\PostDeployments\SeedData\Doctors.SeedData.sql
:r .\PostDeployments\SeedData\Shops.SeedData.sql


--mocked data
:r .\PostDeployments\MockedData\Customers.MockedData.sql