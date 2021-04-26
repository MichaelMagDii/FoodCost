USE [FoodCost]
GO
DECLARE @sql VARCHAR(MAX) = ''
        , @crlf VARCHAR(2) = CHAR(13) + CHAR(10) ;

SELECT @sql = @sql + 'DROP VIEW ' + QUOTENAME(SCHEMA_NAME(schema_id)) + '.' + QUOTENAME(v.name) +';' + @crlf
FROM   sys.views v

PRINT @sql;
EXEC(@sql);
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'SPTransActions' ) 
	Drop PROCEDURE  SPTransActions
IF EXISTS(SELECT * FROM sysobjects WHERE name = 'BeginningEndingMonth' ) 
	Drop table BeginningEndingMonth
IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'BeginningEndingMonth' ) 
	CREATE TABLE[dbo].BeginningEndingMonth (Year varchar(50),Month varchar(50),FromDate datetime,ToDate datetime,Restaurant_ID int,Kitchen_ID int,Item_ID varchar(50),Qty bigint,Cost float);

IF EXISTS(SELECT * FROM sysobjects WHERE name = 'TransActions' ) 
	Drop table TransActions
IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'TransActions' ) 
	CREATE TABLE[dbo].TransActions (_DATE datetime,Restaurant_ID int,Kitchen_ID int,KitchenName varchar(50),Trantype varchar(50),ID varchar(50),Item_ID varchar(50),ItemName varchar(50),Qty bigint,Current_Qty bigint,Cost float,CurrentCost float);
