/*
   Wednesday, April 28, 20212:09:13 PM
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
CREATE TABLE dbo.Tmp_Process_BulkItems_Items
	(
	ProcessBulk_ID varchar(50) NOT NULL,
	ParentItem_ID varchar(50) NULL,
	ParentQty float(53) NULL,
	ParentCost float(53) NULL,
	ChiledItem_ID varchar(50) NULL,
	ChiledQty float(53) NULL,
	ChiledCost nchar(10) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Process_BulkItems_Items SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Process_BulkItems_Items)
	 EXEC('INSERT INTO dbo.Tmp_Process_BulkItems_Items (ProcessBulk_ID, ParentItem_ID, ParentQty, ParentCost, ChiledItem_ID, ChiledQty, ChiledCost)
		SELECT ProcessBulk_ID, ParentItem_ID, ParentQty, ParentCost, CONVERT(varchar(50), ChiledItem_ID), ChiledQty, ChiledCost FROM dbo.Process_BulkItems_Items WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Process_BulkItems_Items
GO
EXECUTE sp_rename N'dbo.Tmp_Process_BulkItems_Items', N'Process_BulkItems_Items', 'OBJECT' 
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Process_BulkItems_Items', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Process_BulkItems_Items', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Process_BulkItems_Items', 'Object', 'CONTROL') as Contr_Per 