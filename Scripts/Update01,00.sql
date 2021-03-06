/*
   Wednesday, October 21, 20202:17:50 PM
   User: sa
   Server: .
   Database: NilofarFoodCost
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
CREATE TABLE dbo.Tmp_PO
	(
	PO_Serial varchar(50) NOT NULL,
	PO_No int NULL,
	RestaurantID int NULL,
	KitchenID int NULL,
	Vendor_ID int NULL,
	PaymentTerm_ID int NULL,
	Create_Date datetime NULL,
	Delivery_Date datetime NULL,
	Post_Date datetime NULL,
	Last_Modified_Date datetime NULL,
	Approval_Date datetime NULL,
	Restaurant_ID int NULL,
	Kitchen_ID int NULL,
	WS int NULL,
	Comment nvarchar(100) NULL,
	Total_Price float(53) NULL,
	UserID int NULL,
	Status nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_PO SET (LOCK_ESCALATION = TABLE)
GO
IF EXISTS(SELECT * FROM dbo.PO)
	 EXEC('INSERT INTO dbo.Tmp_PO (PO_Serial, PO_No, RestaurantID, Vendor_ID, PaymentTerm_ID, Create_Date, Delivery_Date, Post_Date, Last_Modified_Date, Approval_Date, Restaurant_ID, Kitchen_ID, WS, Comment, Total_Price, UserID, Status)
		SELECT PO_Serial, PO_No, Ship_To, Vendor_ID, PaymentTerm_ID, Create_Date, Delivery_Date, Post_Date, Last_Modified_Date, Approval_Date, Restaurant_ID, Kitchen_ID, WS, Comment, Total_Price, UserID, Status FROM dbo.PO WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.PO
GO
EXECUTE sp_rename N'dbo.Tmp_PO', N'PO', 'OBJECT' 
GO
ALTER TABLE dbo.PO ADD CONSTRAINT
	PK_PO PRIMARY KEY CLUSTERED 
	(
	PO_Serial
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
