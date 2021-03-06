/*
   Sunday, March 28, 20214:48:20 PM
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
CREATE TABLE dbo.Tmp_Setup_Recipes
	(
	Code varchar(50) NOT NULL,
	CrossCode int NULL,
	Name nvarchar(50) NULL,
	Name2 nvarchar(50) NULL,
	Category_ID int NULL,
	SubCategory_ID int NULL,
	IsActive bit NULL,
	YiledQty float(53) NULL,
	Unit varchar(50) NULL,
	UnitQty float(53) NULL,
	Cost float(53) NULL,
	Total_Cost float(53) NULL,
	Qty float(53) NULL,
	CreateDate datetime NULL,
	LastModifiedDate datetime NULL,
	UserID varchar(10) NULL,
	WS varchar(10) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Setup_Recipes SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Setup_Recipes)
	 EXEC('INSERT INTO dbo.Tmp_Setup_Recipes (Code, CrossCode, Name, Name2, Category_ID, SubCategory_ID, IsActive, YiledQty, Unit, UnitQty, Cost, Total_Cost, Qty, CreateDate, LastModifiedDate, UserID, WS)
		SELECT CONVERT(varchar(50), Code), CrossCode, Name, Name2, Category_ID, SubCategory_ID, IsActive, YiledQty, Unit, UnitQty, Cost, Total_Cost, Qty, CreateDate, LastModifiedDate, UserID, WS FROM dbo.Setup_Recipes WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.Setup_RecipeItems
	DROP CONSTRAINT FK_Setup_RecipeItems_Setup_Recipes
GO
DROP TABLE dbo.Setup_Recipes
GO
EXECUTE sp_rename N'dbo.Tmp_Setup_Recipes', N'Setup_Recipes', 'OBJECT' 
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Setup_Recipes ON dbo.Setup_Recipes
	(
	Code
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Setup_RecipeItems
	(
	Item_Code varchar(50) NULL,
	Recipe_ID varchar(50) NULL,
	Qty float(53) NULL,
	Recipe_Unit varchar(50) NULL,
	Recipe_Code varchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Setup_RecipeItems SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.Setup_RecipeItems)
	 EXEC('INSERT INTO dbo.Tmp_Setup_RecipeItems (Item_Code, Recipe_ID, Qty, Recipe_Unit, Recipe_Code)
		SELECT Item_Code, Recipe_ID, Qty, Recipe_Unit, CONVERT(varchar(50), Recipe_Code) FROM dbo.Setup_RecipeItems WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Setup_RecipeItems
GO
EXECUTE sp_rename N'dbo.Tmp_Setup_RecipeItems', N'Setup_RecipeItems', 'OBJECT' 
GO
ALTER TABLE dbo.Setup_RecipeItems ADD CONSTRAINT
	FK_Setup_RecipeItems_Setup_Recipes FOREIGN KEY
	(
	Recipe_Code
	) REFERENCES dbo.Setup_Recipes
	(
	Code
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
