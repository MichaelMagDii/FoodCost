/*
   Sunday, March 28, 20215:58:58 PM
   User: sa
   Server: .
   Database: Catering
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Transfer_Kitchens_Items ADD
	Recipes bit NULL
GO
ALTER TABLE dbo.Transfer_Kitchens_Items SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
