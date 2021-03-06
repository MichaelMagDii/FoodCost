USE [FoodCost]
GO
/****** Object:  View [dbo].[BeginningEndingMonthView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BeginningEndingMonthView]
AS
SELECT        dbo.Kitchens_Setup.Name AS KitchenName, dbo.BeginningEndingMonth.Year, dbo.BeginningEndingMonth.Month, dbo.BeginningEndingMonth.FromDate, dbo.BeginningEndingMonth.ToDate, 
                         dbo.BeginningEndingMonth.Restaurant_ID, dbo.BeginningEndingMonth.Kitchen_ID, dbo.BeginningEndingMonth.Item_ID, dbo.BeginningEndingMonth.Qty, dbo.BeginningEndingMonth.Cost
FROM            dbo.BeginningEndingMonth INNER JOIN
                         dbo.Kitchens_Setup ON dbo.BeginningEndingMonth.Restaurant_ID = dbo.Kitchens_Setup.RestaurantID AND dbo.BeginningEndingMonth.Kitchen_ID = dbo.Kitchens_Setup.Code



GO
/****** Object:  View [dbo].[BinAdjView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BinAdjView]
AS
SELECT        dbo.Adjacment_Items.AdjacmentableQty, dbo.Adjacment_Items.Variance, dbo.Adjacment_tbl.Adjacment_Date, dbo.Adjacment_Items.Item_ID, dbo.Setup_Items.Name AS ItemName, dbo.Adjacment_tbl.KitchenID AS Kitchen_ID, 
                         dbo.Adjacment_tbl.Resturant_ID AS Restaurant_ID, dbo.Adjacment_Items.Qty, dbo.Store_Setup.Name AS RestaurantName, dbo.Kitchens_Setup.Name AS KitchenName, dbo.Adjacment_Items.Cost, dbo.Setup_Items.Unit, 
                         dbo.Adjacment_Items.Adjacment_ID, dbo.Setup_Items.Category
FROM            dbo.Adjacment_Items INNER JOIN
                         dbo.Adjacment_tbl ON dbo.Adjacment_Items.Adjacment_ID = dbo.Adjacment_tbl.Adjacment_ID INNER JOIN
                         dbo.Store_Setup ON dbo.Adjacment_tbl.Resturant_ID = dbo.Store_Setup.Code INNER JOIN
                         dbo.Setup_Items ON dbo.Adjacment_Items.Item_ID = dbo.Setup_Items.Code INNER JOIN
                         dbo.Kitchens_Setup ON dbo.Adjacment_tbl.KitchenID = dbo.Kitchens_Setup.Code AND dbo.Adjacment_tbl.Resturant_ID = dbo.Kitchens_Setup.RestaurantID


GO
/****** Object:  View [dbo].[BinCard]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BinCard]
AS
SELECT        dbo.RO_Items.Item_ID, dbo.RO_Items.Qty, dbo.RO_Items.Unit, dbo.RO_Items.Serial, dbo.RO_Items.Net_Price, dbo.RO.Receiving_Date AS Date, dbo.RO.Type, dbo.RO_Items.RO_No, dbo.RO.Kitchen_ID, 
                         dbo.Kitchens_Setup.Name AS KitchenName, dbo.Kitchens_Setup.Code, '' AS TransDetails, '' AS ItemName, dbo.RO_Items.Price_Without_Tax, dbo.RO_Items.Tax, dbo.RO_Items.Price_With_Tax, dbo.RO.Resturant_ID, 
                         '' AS RestaurantName, 0.0 AS Cost, '' AS [Net Cost], 0.0 AS OnHand, 0.0 AS Min, 0.0 AS Max, '' AS BCost, '' AS ECost, '' AS BQty, '' AS EQty
FROM            dbo.RO INNER JOIN
                         dbo.RO_Items ON dbo.RO.RO_Serial = dbo.RO_Items.RO_No INNER JOIN
                         dbo.Kitchens_Setup ON dbo.RO.Kitchen_ID = dbo.Kitchens_Setup.Code AND dbo.RO.RO_Serial = dbo.Kitchens_Setup.Name2 INNER JOIN
                         dbo.Setup_Items ON dbo.RO_Items.Item_ID = dbo.Setup_Items.Code

GO
/****** Object:  View [dbo].[GeneratedRecipesView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GeneratedRecipesView]
AS
SELECT        dbo.GenerateRecipe_Items.Generate_ID, dbo.GenerateRecipe_tbl.Generate_Date, dbo.GenerateRecipe_tbl.Resturant_ID AS Restaurant_ID, dbo.GenerateRecipe_tbl.Kitchen_ID, dbo.GenerateRecipe_Items.Item_ID, 
                         dbo.GenerateRecipe_Items.ItemQty, dbo.Kitchens_Setup.Name AS KitchenName, dbo.Store_Setup.Name AS RestaurantName, dbo.GenerateRecipe_tbl.Qty, dbo.Setup_Items.Name AS ItemName, 
                         dbo.GenerateRecipe_tbl.Recipe_ID, dbo.Setup_Items.Category, dbo.GenerateRecipe_Items.Cost, dbo.GenerateRecipe_Items.Net_Cost, dbo.Setup_Recipes.Name AS RecipeName, dbo.Setup_Recipes.Unit AS RecipesUnit, 
                         dbo.Setup_Items.Unit AS ItemUnit, '' AS PrevQty, '' AS CurrQty
FROM            dbo.GenerateRecipe_Items INNER JOIN
                         dbo.GenerateRecipe_tbl ON dbo.GenerateRecipe_Items.Generate_ID = dbo.GenerateRecipe_tbl.Generate_ID INNER JOIN
                         dbo.Kitchens_Setup ON dbo.GenerateRecipe_tbl.Resturant_ID = dbo.Kitchens_Setup.RestaurantID AND dbo.GenerateRecipe_tbl.Kitchen_ID = dbo.Kitchens_Setup.Code INNER JOIN
                         dbo.Store_Setup ON dbo.Kitchens_Setup.RestaurantID = dbo.Store_Setup.Code INNER JOIN
                         dbo.Setup_Items ON dbo.GenerateRecipe_Items.Item_ID = dbo.Setup_Items.Code INNER JOIN
                         dbo.Setup_Recipes ON dbo.GenerateRecipe_tbl.Recipe_ID = dbo.Setup_Recipes.Code
WHERE        (dbo.GenerateRecipe_Items.Item_ID IS NOT NULL)

GO
/****** Object:  View [dbo].[InventoryStats]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[InventoryStats]
AS
SELECT        dbo.Items.ItemID, dbo.Items.Qty, dbo.Items.Units, dbo.Items.Last_Cost, dbo.Items.Current_Cost, dbo.Items.ShufledID, dbo.Items.MinNumber, dbo.Items.MaxNumber, dbo.Items.Net_Cost, 
                         dbo.Store_Setup.Name AS ResturantName, dbo.Kitchens_Setup.Name AS KitchenName, dbo.Setup_Items.Name AS ItemName, dbo.Items.RestaurantID AS Restaurant_ID, dbo.Items.KitchenID AS Kitchen_ID
FROM            dbo.Items INNER JOIN
                         dbo.Store_Setup ON dbo.Items.RestaurantID = dbo.Store_Setup.Code INNER JOIN
                         dbo.Kitchens_Setup ON dbo.Items.KitchenID = dbo.Kitchens_Setup.Code AND dbo.Items.RestaurantID = dbo.Kitchens_Setup.RestaurantID INNER JOIN
                         dbo.Setup_Items ON dbo.Items.ItemID = dbo.Setup_Items.Code







GO
/****** Object:  View [dbo].[POItems]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[POItems]
AS
SELECT        dbo.PO.PO_No, dbo.PO_Items.Item_ID, dbo.PO.Vendor_ID, dbo.PO.PaymentTerm_ID, dbo.PO.Create_Date, dbo.PO.Delivery_Date, dbo.PO.Post_Date, dbo.PO.Last_Modified_Date, dbo.PO.Approval_Date, dbo.PO.WS, 
                         dbo.PO.Comment, dbo.PO_Items.Qty, dbo.PO_Items.Unit, dbo.PO_Items.Serial, dbo.PO_Items.Net_Price, dbo.Setup_Items.Name AS ItemName, dbo.Store_Setup.Name AS RestaurantName, 
                         dbo.Kitchens_Setup.Name AS KitchenName, dbo.Vendors.Name AS VendorName, dbo.PO.Kitchen_ID, dbo.PO.Restaurant_ID, dbo.PO_Items.PO_Serial, dbo.PO_Items.Price_Without_Tax, dbo.PO_Items.Tax, 
                         dbo.PO_Items.Price_With_Tax, dbo.PO_Items.Tax_Included
FROM            dbo.PO INNER JOIN
                         dbo.PO_Items ON dbo.PO.PO_Serial = dbo.PO_Items.PO_Serial INNER JOIN
                         dbo.Setup_Items ON dbo.PO_Items.Item_ID = dbo.Setup_Items.Code INNER JOIN
                         dbo.Kitchens_Setup ON dbo.PO.Kitchen_ID = dbo.Kitchens_Setup.Code AND dbo.PO.Restaurant_ID = dbo.Kitchens_Setup.RestaurantID INNER JOIN
                         dbo.Vendors ON dbo.PO.Vendor_ID = dbo.Vendors.VendorID INNER JOIN
                         dbo.Store_Setup ON dbo.Kitchens_Setup.RestaurantID = dbo.Store_Setup.Code






GO
/****** Object:  View [dbo].[POView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[POView]
AS
SELECT        dbo.PO.PO_No, dbo.PO.PaymentTerm_ID, dbo.PO.Create_Date, dbo.PO.Delivery_Date, dbo.PO.Post_Date, dbo.PO.Last_Modified_Date, dbo.PO.Approval_Date, dbo.PO.WS, dbo.PO.Comment, 
                         dbo.Store_Setup.Code AS StoreID, dbo.Kitchens_Setup.Name AS KitchenName, dbo.Vendors.Name AS VendorName, dbo.Store_Setup.Name AS ResturantName, dbo.PO.Restaurant_ID, dbo.PO.Kitchen_ID, dbo.PO.Ship_To, 
                         dbo.PO.Vendor_ID, dbo.PO.Total_Price, dbo.PO.PO_Serial
FROM            dbo.PO INNER JOIN
                         dbo.Vendors ON dbo.PO.Vendor_ID = dbo.Vendors.VendorID INNER JOIN
                         dbo.Kitchens_Setup ON dbo.PO.Kitchen_ID = dbo.Kitchens_Setup.Code AND dbo.PO.Restaurant_ID = dbo.Kitchens_Setup.RestaurantID INNER JOIN
                         dbo.Store_Setup ON dbo.Kitchens_Setup.RestaurantID = dbo.Store_Setup.Code






GO
/****** Object:  View [dbo].[ReceiveItemsView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReceiveItemsView]
AS
SELECT        dbo.RO.RO_Serial, dbo.RO.Transactions_No, dbo.RO.Status, dbo.RO.Receiving_Date, dbo.RO.Resturant_ID AS Restaurant_ID, dbo.RO.Kitchen_ID, dbo.RO.WS, dbo.RO.Type, dbo.RO.Comment, dbo.RO_Items.Item_ID, 
                         dbo.RO_Items.Qty, dbo.RO_Items.Unit, dbo.RO_Items.Serial, dbo.RO_Items.Net_Price, dbo.Setup_Items.Name AS ItemName, dbo.Store_Setup.Name AS RestaurantName, dbo.Kitchens_Setup.Name AS KitchenName, 
                         dbo.RO.RO_No, dbo.RO_Items.Price_Without_Tax, dbo.RO_Items.Tax, dbo.RO_Items.Price_With_Tax, dbo.RO_Items.QtyOnHand_To, dbo.RO_Items.Cost_To, dbo.RO_Items.QtyOnHand_From, dbo.RO_Items.Cost_From, 
                         dbo.RO.Create_Date, dbo.RO.Post_Date, dbo.RO.UserID, dbo.RO.Last_Modified_Date, dbo.RO.Approval_Date, dbo.Setup_Items.Category
FROM            dbo.RO INNER JOIN
                         dbo.RO_Items ON dbo.RO.RO_Serial = dbo.RO_Items.RO_No INNER JOIN
                         dbo.Setup_Items ON dbo.RO_Items.Item_ID = dbo.Setup_Items.Code INNER JOIN
                         dbo.Kitchens_Setup ON dbo.RO.Kitchen_ID = dbo.Kitchens_Setup.Code AND dbo.RO.Resturant_ID = dbo.Kitchens_Setup.RestaurantID INNER JOIN
                         dbo.Store_Setup ON dbo.Kitchens_Setup.RestaurantID = dbo.Store_Setup.Code

GO
/****** Object:  View [dbo].[ReceiveOrderView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReceiveOrderView]
AS
SELECT        dbo.RO.RO_Serial, dbo.RO.Transactions_No, dbo.RO.Status, dbo.RO.Receiving_Date, dbo.RO.Resturant_ID AS Restaurant_ID, dbo.RO.Kitchen_ID, dbo.RO.WS, dbo.RO.Type, dbo.RO.Comment, dbo.Store_Setup.Code AS StoreID, 
                         dbo.Kitchens_Setup.Name AS KitchenName, dbo.Store_Setup.Name AS ResturantName
FROM            dbo.RO INNER JOIN
                         dbo.Kitchens_Setup ON dbo.RO.Kitchen_ID = dbo.Kitchens_Setup.Code AND dbo.RO.Resturant_ID = dbo.Kitchens_Setup.RestaurantID INNER JOIN
                         dbo.Store_Setup ON dbo.Kitchens_Setup.RestaurantID = dbo.Store_Setup.Code







GO
/****** Object:  View [dbo].[RecipesItemsView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RecipesItemsView]
AS
SELECT        dbo.Setup_RecipeItems.Name AS ItemName, dbo.Setup_Recipes.Code, dbo.Setup_Recipes.Name AS RecipeName, dbo.Setup_RecipeCategory.Name AS CatName, dbo.Setup_RecipeSubCategories.Name AS SubCatName, 
                         dbo.Setup_RecipeItems.Qty, dbo.Setup_RecipeItems.Recipe_Unit, dbo.Setup_RecipeItems.Cost, dbo.Setup_RecipeItems.Total_Cost, dbo.Setup_RecipeItems.Cost_Precentage, dbo.Setup_RecipeItems.Recipe_Code, 
                         dbo.Setup_Recipes.Category_ID, dbo.Setup_Recipes.SubCategory_ID
FROM            dbo.Setup_RecipeItems INNER JOIN
                         dbo.Setup_Recipes ON dbo.Setup_RecipeItems.Recipe_Code = dbo.Setup_Recipes.Code INNER JOIN
                         dbo.Setup_RecipeSubCategories ON dbo.Setup_Recipes.SubCategory_ID = dbo.Setup_RecipeSubCategories.Code INNER JOIN
                         dbo.Setup_RecipeCategory ON dbo.Setup_Recipes.Category_ID = dbo.Setup_RecipeCategory.Code







GO
/****** Object:  View [dbo].[TransActionsView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TransActionsView]
AS
SELECT        dbo.TransActions._DATE, dbo.TransActions.Restaurant_ID, dbo.TransActions.Kitchen_ID, dbo.TransActions.KitchenName, dbo.TransActions.Trantype, dbo.TransActions.ID, dbo.TransActions.Item_ID, dbo.TransActions.Qty, 
                         dbo.TransActions.Current_Qty, dbo.TransActions.Cost, dbo.TransActions.CurrentCost, dbo.Setup_Items.Unit, dbo.Store_Setup.Name AS RestaurantName, '' AS BCost, '' AS ECost, '' AS BQty, '' AS EQty, 
                         dbo.Setup_Items.Name AS ItemName
FROM            dbo.TransActions INNER JOIN
                         dbo.Setup_Items ON dbo.TransActions.Item_ID = dbo.Setup_Items.Code INNER JOIN
                         dbo.Store_Setup ON dbo.TransActions.Restaurant_ID = dbo.Store_Setup.Code

GO
/****** Object:  View [dbo].[TransferItemsIn]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TransferItemsIn]
AS
SELECT        dbo.RO.Resturant_ID AS Restaurant_ID, dbo.RO.Kitchen_ID, Store_Setup_1.Name AS RestaurantName, Kitchens_Setup_1.Name AS KitchenName, dbo.RO_Items.Item_ID, dbo.Setup_Items.Name AS ItemName, 
                         dbo.RO.RO_Serial, dbo.RO.RO_No, dbo.RO.Transactions_No, dbo.RO.Status, dbo.RO.Receiving_Date, dbo.RO.Create_Date, dbo.RO.Type, dbo.RO.Comment, dbo.RO.Post_Date, dbo.RO.UserID, dbo.RO.Last_Modified_Date, 
                         dbo.RO.Approval_Date, dbo.RO.WS, dbo.RO_Items.Qty, dbo.RO_Items.Unit, dbo.RO_Items.Serial, dbo.RO_Items.Price_Without_Tax, dbo.RO_Items.Tax, dbo.RO_Items.Price_With_Tax AS Cost, dbo.RO_Items.Net_Price, 
                         dbo.Setup_Items.Category
FROM            dbo.Store_Setup AS Store_Setup_1 INNER JOIN
                         dbo.Kitchens_Setup AS Kitchens_Setup_1 ON Store_Setup_1.Code = Kitchens_Setup_1.RestaurantID INNER JOIN
                         dbo.RO ON Kitchens_Setup_1.RestaurantID = dbo.RO.Resturant_ID AND Kitchens_Setup_1.Code = dbo.RO.Kitchen_ID INNER JOIN
                         dbo.Setup_Items INNER JOIN
                         dbo.RO_Items ON dbo.Setup_Items.Code = dbo.RO_Items.Item_ID ON dbo.RO.RO_Serial = dbo.RO_Items.RO_No
WHERE        (dbo.RO.Type = 'Transfer_Resturant') OR
                         (dbo.RO.Type = 'Transfer_Kitchen')

GO
/****** Object:  View [dbo].[TransferItemsOut]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TransferItemsOut]
AS
SELECT        dbo.Requests_tbl.From_Resturant_ID AS Restaurant_ID, dbo.Requests_tbl.From_Kitchen_ID AS Kitchen_ID, dbo.Store_Setup.Name AS RestaurantName, dbo.Kitchens_Setup.Name AS KitchenName, 
                         dbo.Requests_Items.Item_ID, dbo.Setup_Items.Name AS ItemName, dbo.Requests_tbl.Request_Serial, dbo.Requests_tbl.Manual_Request_No, dbo.Requests_tbl.Request_Date, dbo.Requests_tbl.Comment, 
                         dbo.Requests_tbl.To_Resturant_ID, dbo.Requests_tbl.To_Kitchen_ID, dbo.Requests_tbl.WS, dbo.Requests_tbl.Post, dbo.Requests_tbl.Hold, dbo.Requests_tbl.Post_Date, dbo.Requests_tbl.Hold_Date, dbo.Requests_tbl.Type, 
                         dbo.Requests_tbl.Modifiled_Date, dbo.Requests_tbl.UserID, dbo.Requests_Items.Qty, dbo.Requests_Items.Unit, dbo.Requests_Items.serial, dbo.Requests_Items.Cost, dbo.Requests_Items.Net_Cost, 
                         dbo.Setup_Items.Category
FROM            dbo.Setup_Items INNER JOIN
                         dbo.Store_Setup INNER JOIN
                         dbo.Kitchens_Setup ON dbo.Store_Setup.Code = dbo.Kitchens_Setup.RestaurantID INNER JOIN
                         dbo.Requests_tbl INNER JOIN
                         dbo.Requests_Items ON dbo.Requests_tbl.Request_Serial = dbo.Requests_Items.Request_ID ON dbo.Kitchens_Setup.RestaurantID = dbo.Requests_tbl.From_Resturant_ID AND 
                         dbo.Kitchens_Setup.Code = dbo.Requests_tbl.From_Kitchen_ID ON dbo.Setup_Items.Code = dbo.Requests_Items.Item_ID


GO
/****** Object:  View [dbo].[VendorsView]    Script Date: 2/10/2020 1:53:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VendorsView]
AS
SELECT        dbo.PO.PO_No, dbo.PO.Vendor_ID, dbo.PO.PaymentTerm_ID, dbo.PO.Create_Date, dbo.PO.Post_Date, dbo.PO.Last_Modified_Date, dbo.PO.Approval_Date, dbo.PO.WS, dbo.PO.Comment, 
                         dbo.Store_Setup.Name AS RestaurantName, dbo.Kitchens_Setup.Name AS KitchenName, dbo.Vendors.Name AS VendorName, dbo.PO.Kitchen_ID, dbo.PO.Restaurant_ID, dbo.RO.Status, dbo.RO.Receiving_Date, 
                         dbo.PO.Total_Price, dbo.Vendors.Code AS V_ID
FROM            dbo.PO INNER JOIN
                         dbo.Kitchens_Setup ON dbo.PO.Kitchen_ID = dbo.Kitchens_Setup.Code AND dbo.PO.Restaurant_ID = dbo.Kitchens_Setup.RestaurantID INNER JOIN
                         dbo.Vendors ON dbo.PO.Vendor_ID = dbo.Vendors.VendorID INNER JOIN
                         dbo.Store_Setup ON dbo.Kitchens_Setup.RestaurantID = dbo.Store_Setup.Code INNER JOIN
                         dbo.RO ON dbo.PO.PO_No = dbo.RO.Transactions_No






GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RO"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RO_Items"
            Begin Extent = 
               Top = 6
               Left = 268
               Bottom = 136
               Right = 454
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Kitchens_Setup"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Setup_Items"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 426
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BinCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BinCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[65] 4[3] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "GenerateRecipe_Items"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 177
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "GenerateRecipe_tbl"
            Begin Extent = 
               Top = 0
               Left = 345
               Bottom = 208
               Right = 538
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Kitchens_Setup"
            Begin Extent = 
               Top = 0
               Left = 800
               Bottom = 218
               Right = 975
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Store_Setup"
            Begin Extent = 
               Top = 5
               Left = 1050
               Bottom = 157
               Right = 1220
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Setup_Items"
            Begin Extent = 
               Top = 220
               Left = 119
               Bottom = 408
               Right = 317
            End
            DisplayFlags = 280
            TopColumn = 10
         End
         Begin Table = "Setup_Recipes"
            Begin Extent = 
               Top = 191
               Left = 577
               Bottom = 458
               Right = 770
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
         Width = 284
      ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GeneratedRecipesView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'   Width = 1500
         Width = 3585
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GeneratedRecipesView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GeneratedRecipesView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[53] 4[9] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RO"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "RO_Items"
            Begin Extent = 
               Top = 6
               Left = 268
               Bottom = 136
               Right = 454
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Setup_Items"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 218
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Kitchens_Setup"
            Begin Extent = 
               Top = 138
               Left = 256
               Bottom = 268
               Right = 426
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Store_Setup"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1995
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReceiveItemsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReceiveItemsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReceiveItemsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[15] 2[27] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TransActions"
            Begin Extent = 
               Top = 1
               Left = 315
               Bottom = 286
               Right = 485
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Setup_Items"
            Begin Extent = 
               Top = 10
               Left = 53
               Bottom = 296
               Right = 250
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Store_Setup"
            Begin Extent = 
               Top = 6
               Left = 578
               Bottom = 210
               Right = 765
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransActionsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransActionsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[24] 2[5] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Store_Setup_1"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Kitchens_Setup_1"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RO"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Setup_Items"
            Begin Extent = 
               Top = 138
               Left = 268
               Bottom = 268
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RO_Items"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 28
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransferItemsIn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'        Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 2175
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransferItemsIn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TransferItemsIn'
GO
