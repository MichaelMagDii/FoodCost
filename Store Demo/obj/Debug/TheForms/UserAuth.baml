   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Store_Demo
Store_Demo  5   	       Food_Cost.UserAuth   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Store_Demo     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � Title$  User Authentication��5   	   $	��700��6%   $	��700��62    � WindowStartupLocation$ CenterScreen=�6?   .��5
      �� .��5   
   � ���	#FFEEEEEE � ColumnDefinitions 5   
   �� 5      $��15 �6   6,   �� 5      $��* �6   6+   �� 5      $��* �6   6+   .+�5   
   �� $��1��6   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ AccountMultiple=�6&   ���#566573$��24��6R   $��24��6^   $��3q�6i   �� 5      -   5        $��FloorGBX��5   e   
���   �$��Stretch=�6*   $��Stretch=�6F   $��0 0 20 0q�6u   ���#D0D3D4$��2q�6�   $��10q�6�   $��New User Class��6�   $��18(�6�   .��5      �� .+�5      � $��0 20 0 0q�6    5      �� 5      �� 5      .+�5      �� $��0 0 0 0q�6%   $��0��66   .+�5      �� $
��Bold"�6(   $��UserClassID��6:   $��0q�6M   $��14(�6X   ���	#FFCD0E0E6f   �� 5      -   5        $��UserClassIDtxt��5   &   $��1q�6<   $��13(�6H    �� 
IsReadOnly . 6V   5      �� 5!      $��1��6&   $��	0 0 10 18q�66   .+�5"      �� -   5        $��Active_chbx��5"   '   $��Bottom=�6:   $��Right=�6U   $��
0,5,-0.8,0q�6q   .��5#   "   �� $��14(�6,   �� 6=   
��� � �$
��Bold"�6T   .�6f   Active6n   5$   #   5%      5&      5(      � 5)      $��0 40 0 0q�6    5*      �� 5+      $��* �6/   6;   .+�5.      �� $��0��6&   $��0 0 0 0q�66   .+�5/      �� $
��Bold"�6(   ���	#FFCD0E0E$��User Class (ex: Admin)��6R   $	��0 0q�6p   $��14(�6}   �� 50      -   5        $��Nametxt��50   &   $��1q�65   $��13(�6A   $
��0 10q�6O   51      52      � 53      $��0 35 0 0q�6    54      �� 55      $��* �6/   6;   �� 56      $��* �6/   6;   �� 57      $��* �6/   6;   �� 58      $��* �6/   6;   �� 59      $��* �6/   6;   �� 5;      $��0 �6/   6;   .+�5?      �� -   5        $��NewBtn��5?   �   $��0��6!   
���   ����#cacfd2$��12(�6Y   $��0 0 0 0q�6g   $��-5,0,10,0.2q�6y   $
��Auto��6�   .��5@      ��  �� Orientation$ 
Horizontal=�6)   $
��Left=�6B   $��0q�6]   .+�5A   "     $
 Plus=�6:   $
��Left=�6F   $��20��6a   �� 5B   "    �� Padding$ 0q�6,   $��0q�68   $��Right=�6C   New6e   5C      5D      �� 5E      -   5        $��saveBtn��5E   !   
��� � �$��12(�6C   $��2q�6Q   $��52,0,4.6,0.2q�6]   $��2��6s   .��5F      �� $ 
Horizontal=�6)   $
��Left=�6B   $��0q�6_   .+�5G   "     $ CheckboxMarkedCircle=�6:   $
��Left=�6V   $��20��6q   $��20��6|   �� 5H   "   $ 0q�6,   $��0q�68   $��Right=�6C   Save6f   5I      5J      �� 5K      -   5        $��	UpdateBtn��5K   !   $��2��62   
��� � �$��12(�6W   $��2q�6e   $��
0,0,50.2,0q�6q   $��2��6�   $	��Top=�6�   .��5L      �� $ 
Horizontal=�6)   $
��Left=�6B   $��0q�6_   .+�5M   "     $ 	ArrowDown=�6:   $
��Left=�6K   �� 5N   "   $ 0q�6,   $��0q�68   $��Right=�6C   Update6h   5O      5P      �� 5Q      -   5        $��UndoBtn��5Q   x   $��3��6!   $��12(�61   $��2q�6?   $��10.6,0,51.2,0.2q�6K   $��2��6d   .��5R      �� $ 
Horizontal=�6)   $
��Left=�6B   $��0q�6_   .+�5S   "     $
 Undo=�6:   $
��Left=�6F   �� 5T   "   $ 0q�6,   $��0q�68   $��Right=�6C   Undo6f   5U      5V      �� 5W      -	   5        $��	DeleteBtn��5W   !   $��4��62   
����������#cb4335$��12(�6k   $��0q�6y   $��9.6,0,-9.8,0.2q�6�   $��2��6�   .��5X      �� $ 
Horizontal=�6)   $
��Left=�6B   $��0q�6_   .+�5Y   "     $ Delete=�6:   $
��Left=�6H   �� 5Z   "   $ 0q�6,   $��0q�68   $��Right=�6C   Delete6h   5[      5\      5]      5^      5_      5`      �� 5b   
   -
   5        $��PrivillageStackPanel��5b   &   $��2��6   $	��650��6B   $	��Top=�6O   .+�5c      �� -   5        $��Justtxt��5c   a   $��User Classes��6   $
��Bold"�6,   ���#566573$��15(�6S   6a   �� 5e      -   5        $��Backbtn��5e      
�������
���iii�$��10(�6P   $��Right=�6^   $y�Hidden=�6�   .��5f      �� $ 
Horizontal=�6!   .+�5g        $ KeyboardReturn=�62   �� 5h      -   5         ��Name$
 Back��5h   <   $��Right=�6    $
��Back��6J   5i      5j      � 5l      
�������$	��700��6*   .+�5n      �� -   5        $��ListBox_UserPrivileges��5n   S   
�������$	��Top=�6.   $	��600��6F   $y�Hidden=�6q   .@�5o      �� -   5        $��	SelectAll��5o   #   $	��Top=�6p   .��6�   
Select All6�   Q� 5q      -   5        $��Purchase��5q   i   $��Purchase Order��6#   $
��Left=�6;    Q� 
IsSelected .  .@�5r      �� -   5        $��
CheckAllPO��5r   #   
	 � Tag$	 1��65   .��6=   	Check All6H   �� 5s      -   5        $��SearchPO��5s   #   $��Search��63   $	 1��6D   �� 5t      -   5        $��CopyPO��5t   #   $
��Copy��61   $	 1��6@   �� 5u      -   5        $��NewPO��5u   #   $	��New��60   $	 1��6>   �� 5v      -   5        $��OrderPO��5v   #   $��Order��62   $	 1��6B   �� 5w      -   5        $
��Undo��5w   #   $
��Undo��6/   $	 1��6>   �� 5x      -   5        $��	AddItemPO��5x   #   $��Add Item��64   $	 1��6G   �� 5y      -   5        $��RemoveItemPO��5y   #   $��Remove Item��67   $	 1��6M   5z      Q� 5|      -   5        $��Recieve��5|   h   $��Recieve Order��6#   $
��Left=�6:    .  .@�5}      �� -   5        $��RecieveOrder_List��5}   6   
�������.@�5~      Q� -   5        $��	RecievePO��5~   s   $��Recieve Purchase��6+   $
��Left=�6E    .  $	 2��6�   .@�5   "   �� -   5        $��CheckAllROPO��5   +   $	 2��6?   .��6G   	Check All6R   �� 5�   "   -   5        $��RecieveROPO��5�   +   $��Recieve��6>   $	 2��6P   5�      Q� 5�      -   5        $��RecieveReturantTrnsfer��5�   {   $��Recieve Kitchen Transfer��6+   $
��Left=�6M    .  .@�5�   "   �� -   5        $��CheckAllROResturant��5�   +   $	 1��6F   .��6N   	Check All6Y   �� 5�   "   -    5        $��SearchROResturant��5�   +   $��Search��6D   $	 1��6U   �� 5�   "   -!   5        $��RecieveROResturant��5�   +   $
��Copy��6E   $	 1��6T   �� 5�   "   -"   5        $��EditROResturant��5�   +   $
��Edit��6B   $	 1��6Q   �� 5�   "   -#   5        $��UndoROResturant��5�   +   $
��Undo��6B   $	 1��6Q   �� 5�   "   -$   5        $��CancelROResturant��5�   +   $��Cancel��6D   $	 1��6U   5�      Q� 5�      -%   5        $��RecieveKitchen��5�   {   $��Recieve Kitchen Transfer��6+   $
��Left=�6M    .  .@�5�   "   �� -&   5        $��CheckAllROKitchen��5�   +   $	 1��6D   .��6L   	Check All6W   �� 5�   "   -'   5        $��SearchROKitchen��5�   +   $��Search��6B   $	 1��6S   �� 5�   "   -(   5        $��RecieveROKitchen��5�   +   $
��Copy��6C   $	 1��6R   �� 5�   "   -)   5        $��EditROKitchen��5�   +   $
��Edit��6@   $	 1��6O   �� 5�   "   -*   5        $��UndoROKitchen��5�   +   $
��Undo��6@   $	 1��6O   �� 5�   "   -+   5        $��CancelROKitchen��5�   +   $��Cancel��6B   $	 1��6S   5�      Q� 5�      -,   5        $��RecieveWithoutPurchse��5�   {   $��Recieve Without Purchase��6+   $
��Left=�6M    .  .@�5�   "   �� --   5        $��CheckAllROWithout��5�   +   $	 1��6D   .��6L   	Check All6W   �� 5�   "   -.   5        $��SearchROWithout��5�   +   $��Search��6B   $	 1��6S   �� 5�   "   -/   5        $��RecieveROWithout��5�   +   $��Recieve��6C   $	 1��6U   �� 5�   "   -0   5        $��EditROWithout��5�   +   $
��Edit��6@   $	 1��6O   �� 5�   "   -1   5        $��UndoROWithout��5�   +   $
��Undo��6@   $	 1��6O   �� 5�   "   -2   5        $��CancelROWithout��5�   +   $��Cancel��6B   $	 1��6S   5�      Q� 5�   "   -3   5        $��Request��5�   o   $��Requests��6/   $
��Left=�6A    .  .@�5�   &   �� -4   5        $��CheckAllRequests��5�   /   $	 1��6G   .��6O   	Check All6Z   �� 5�   &   -5   5        $��Requests��5�   /   $��Recieve��6?   $	 1��6Q   5�   #   5�      5�      Q� 5�      -6   5        $��TransferResturant��5�   m   $��Transfer Resturant��6#   $
��Left=�6?    .  .@�5�      �� -7   5        $��CheckAllTransferResturant��5�   #   $	 1��6D   .��6L   	Check All6W   �� 5�      -8   5        $��SearchTransferResturant��5�   #   $��Search��6B   $	 1��6S   �� 5�      -9   5        $��TransferTrnsferResturant��5�   #   $��Transfer��6C   $	 1��6V   �� 5�      -:   5        $��unodTransferResturant��5�   #   $
��Undo��6@   $	 1��6O   �� 5�      -;   5        $��AddItemTransferReasturant��5�   #   $��Add Item��6D   $	 1��6W   �� 5�      -<   5        $!��RemoveItemTransferResturant��5�   #   $��Remove Item��6F   $	 1��6\   5�      Q� 5�      -=   5        $��TransferKitchen��5�   k   $��Transfer Kitchen��6#   $
��Left=�6=    .  .@�5�      �� ->   5        $��CheckAllTransferKitchen��5�   #   $	 1��6B   .��6J   	Check All6U   �� 5�      -?   5        $��SearchTransferKitchen��5�   #   $��Search��6@   $	 1��6Q   �� 5�      -@   5        $��TransferTrnsferKitchen��5�   #   $��Transfer��6A   $	 1��6T   �� 5�      -A   5        $��unodTransferKitchen��5�   #   $
��Undo��6>   $	 1��6M   �� 5�      -B   5        $��AddItemTransferKitchen��5�   #   $��Add Item��6A   $	 1��6T   �� 5�      -C   5        $��RemoveItemTransferKitchen��5�   #   $��Remove Item��6D   $	 1��6Z   5�      Q� 5�      -D   5        $��
Resturants��5�   e   $��
Resturants��6#   $
��Left=�67    .  .@�5�      �� -E   5        $��CheckAllResturants��5�   #   $	 1��6=   .��6E   	Check All6P   �� 5�      -F   5        $��NewResturants��5�   #   $	��New��68   $	 1��6F   �� 5�      -G   5        $��SaveResturants��5�   #   $
��Save��69   $	 1��6H   �� 5�      -H   5        $��UpdateResturants��5�   #   $��Update��6;   $	 1��6L   �� 5�      -I   5        $��UndoResturants��5�   #   $
��Undo��69   $	 1��6H   �� 5�      -J   5        $��DeleteResturants��5�   #   $��Delete Resturants��6;   $	 1��6W   5�      Q� 5�      -K   5        $��Kitchens��5�   c   $��Kitchens��6#   $
��Left=�65    .  .@�5�      �� -L   5        $��CheckAllKitchens��5�   #   $	 1��6;   .��6C   	Check All6N   �� 5�      -M   5        $��NewKitchens��5�   #   $	��New��66   $	 1��6D   �� 5�      -N   5        $��SaveKitchens��5�   #   $
��Save��67   $	 1��6F   �� 5�      -O   5        $��UpdateKitchens��5�   #   $��Update��69   $	 1��6J   �� 5�      -P   5        $��UndoKitchens��5�   #   $
��Undo��67   $	 1��6F   �� 5�      -Q   5        $��DeleteKitchens��5�   #   $��Delete Kitchens��69   $	 1��6S   5�      Q� 5�      -R   5        $��Items��5�   `   $��Items��6#   $
��Left=�62    .  .@�5�      �� -S   5        $��CheckAllItems��5�   #   $	 1��68   .��6@   	Check All6K   �� 5�      -T   5        $��AddItems��5�   #   $��	Add Items��63   $	 1��6G   �� 5�      -U   5        $��DeleteItems��5�   #   $��Delete��66   $	 1��6G   �� 5�      -V   5        $��	SaveItems��5�   #   $��
Save Items��64   $	 1��6I   5�      Q� 5�      -W   5        $��Recipes��5�   b   $��Recipes��6#   $
��Left=�64    .  .@�5�      �� -X   5        $��CheckAllRecipes��5�   #   $	 1��6:   .��6B   	Check All6M   �� 5�      -Y   5        $��SearchRecipes��5�   #   $��Search��68   $	 1��6I   �� 5�      -Z   5        $��ReNewRecipes��5�   #   $��ReNew��67   $	 1��6G   �� 5�      -[   5        $��
NewRecipes��5�   #   $��New Recipes��65   $	 1��6K   �� 5�      -\   5        $��SaveRecipes��5�   #   $
��Save��66   $	 1��6E   �� 5�      -]   5        $
��Edit��5�   #   $��Edit Recipes��6/   $	 1��6F   �� 5�      -^   5        $��undoRecipes��5�   #   $
��Undo��66   $	 1��6E   �� 5�      -_   5        $��DeleteRecipes��5�   #   $��Delete��68   $	 1��6I   �� 5�      -`   5        $��RemoveItemRecipes��5�   #   $��RemoveItemRecipes��6<   $	 1��6X   �� 5�      -a   5        $��
AddRecipes��5�   #   $��Add Sub Recipes��65   $	 1��6O   �� 5�      -b   5        $��AddItemRecipes��5�   #   $��Add Recipe Item��69   $	 1��6S   5�      Q� 5�      -c   5        $��GenerateBatch��5�   k   $��Generate Recipes��6#   $
��Left=�6=    .  .@�5�      �� -d   5        $��CheckAllGenerateBatch��5�   #   $	 1��6@   .��6H   	Check All6S   �� 5�      -e   5        $��GenertaeGenerate��5�   #   $��Genertae Recipe��6;   $	 1��6U   �� 5�      -f   5        $��OrderRequesationGenerate��5�   #   $��Order Requesation��6C   $	 1��6_   5�      Q� 5�      -g   5        $��KitchenItems��5�   i   $��Kitchen Items��6#   $
��Left=�6:    .  .@�5�      �� -h   5        $��CheckAllKitchenItems��5�   #   $	 1��6?   .��6G   	Check All6R   �� 5�      -i   5        $��AddKitchenItems��5�   #   $��	Add Items��6:   $	 1��6N   �� 5�      -j   5        $��SaveKitchenItems��5�   #   $
��Save��6;   $	 1��6J   �� 5�      -k   5        $��DeleteKitchenItems��5�   #   $��Delete��6=   $	 1��6N   5�      Q� 5�      -l   5        $��RecipeCategoryAndSub��5�   {   $&�� Recipe Category and Sub Category��6#   $
��Left=�6M    .  .@�5�      �� -m   5        $��CheckAllRecipeCategory��5�   #   $	 1��6A   .��6I   	Check All6T   �� 5�      -n   5        $��NeRecipeCategory��5�   #   $	��New��6;   $	 1��6I   �� 5�      -o   5        $��SaveRecipeCategory��5�   #   $
��Save��6=   $	 1��6L   �� 5�      -p   5        $��UpdateRecipeCategory��5�   #   $��Update��6?   $	 1��6P   �� 5�      -q   5        $��UndoRecipeCategory��5�   #   $
��Undo��6=   $	 1��6L   �� 5�      -r   5        $��DeleteRecipeCatgoey��5�   #   $��Delete Category��6>   $	 1��6X   �� 5�      -s   5        $��CheckAllRecipeSubCategory��5�   #   $	 1��6D   .��6L   	Check All6W   �� 5�      -t   5        $��NeRecipeSubCategory��5�   #   $	��New��6>   $	 1��6L   �� 5�      -u   5        $��SaveRecipeSubCategory��5�   #   $
��Save��6@   $	 1��6O   �� 5�      -v   5        $��UpdateRecipeSubCategory��5�   #   $��Update��6B   $	 1��6S   �� 5�      -w   5        $��UndoRecipeSubCategory��5�   #   $
��Undo��6@   $	 1��6O   �� 5�      -x   5        $��DeleteRecipeSubCatgoey��5�   #   $��Delete Category��6A   $	 1��6[   5�      Q� 5�      -y   5        $��ProcessBulk��5�   o   $��Processs Bulk Items��6#   $
��Left=�6@    .  .@�5�      �� -z   5        $��CheckAllBulk��5�   #   $	 1��67   .��6?   	Check All6J   �� 5�      -{   5        $��DoProcessBulk��5�   #   $��
Do Process��68   $	 1��6M   5�      Q� 5      -|   5        $��AddjacmentItems��5   l   $��Addjacment Items��6#   $
��Left=�6=    .  .@�5     �� -}   5        $��CheckAllAddjacment��5  #   $	 1��6=   .��6E   	Check All6P   �� 5     -~   5        $��AddItemAddjacment��5  #   $��	Add Items��6<   $	 1��6P   �� 5     -   5        $��DeleteAddjacment��5  #   $��Delete��6;   $	 1��6L   �� 5     -�   5        $��AdjacmentAdjacment��5  #   $
��Save��6=   $	 1��6L   5     Q� 5     -�   5        $��AdjacmentReasons��5  n   $��Addjacment Reasons��6#   $
��Left=�6?    .  .@�5     �� -�   5        $��CheckAllAddjacmentReasons��5  #   $	 1��6D   .��6L   	Check All6W   �� 5	     -�   5        $��NewAddjacmentReasons��5	  #   $	��New��6?   $	 1��6M   �� 5
     -�   5        $��SaveAddjacmentReasons��5
  #   $
��Save��6@   $	 1��6O   �� 5     -�   5        $��UpdateAddjacmentReasonss��5  #   $��Update��6C   $	 1��6T   �� 5     -�   5        $��UndoAddjacmentReasons��5  #   $
��Undo��6@   $	 1��6O   �� 5     -�   5        $��DeleteAddjacmentReasons��5  #   $��Delete Category��6B   $	 1��6\   5     Q� 5     -�   5        $��PhysicalInventory��5  n   $��Physical Inventory��6#   $
��Left=�6?    .  .@�5     �� -�   5        $��CheckAllPhysicalInventory��5  #   $	 1��6D   .��6L   	Check All6W   �� 5     -�   5        $��StartTheInventory��5  #   $��Start Items��6<   $	 1��6R   �� 5     -�   5        $��SaveTheInventory��5  #   $��
Save Items��6;   $	 1��6P   �� 5     -�   5        $ ��InventoryPhysicalInventory��5  #   $��Make Inventory��6E   $	 1��6^   �� 5     -�   5        $��PrintPhysicalInventory��5  #   $��Print Inventory sheet��6A   $	 1��6a   5     Q� 5$     -�   5        $��Vendors��5$  b   $��Vendors��6#   $
��Left=�64    .  .@�5%     �� -�   5        $��CheckAllVendors��5%  #   $	 1��6:   .��6B   	Check All6M   �� 5&     -�   5        $��
NewVendors��5&  #   $	��New��65   $	 1��6C   �� 5'     -�   5        $��SaveVendors��5'  #   $
��Save��66   $	 1��6E   �� 5(     -�   5        $��UpdateVendors��5(  #   $��Update��68   $	 1��6I   �� 5)     -�   5        $��UndoVendors��5)  #   $
��Undo��66   $	 1��6E   �� 5*     -�   5        $��DeleteVendors��5*  #   $��Delete Vendors��68   $	 1��6Q   5+     Q� 5-     -�   5        $��Users��5-  `   $��Users��6#   $
��Left=�62    .  .@�5.     �� -�   5        $��CheckAllUsers��5.  #   $	 1��68   .��6@   	Check All6K   �� 5/     -�   5        $��NewUsers��5/  #   $	��New��63   $	 1��6A   �� 50     -�   5        $��	SaveUsers��50  #   $
��Save��64   $	 1��6C   �� 51     -�   5        $��UpdateUsers��51  #   $��Update��66   $	 1��6G   �� 52     -�   5        $��	UndoUsers��52  #   $
��Undo��64   $	 1��6C   �� 53     -�   5        $��DeleteUsers��53  #   $��Delete Users��66   $	 1��6M   �� 54     -�   5        $��	UsersAuth��54  #   $��Users Authentication��64   $	 1��6S   55     Q� 57     -�   5        $��UsersAuthentication��57  n   $��UsersAuthentication��6#   $
��Left=�6@    .  .@�58     �� -�   5        $!��CheckAllUsersAuthentication��58  #   $	 1��6F   .��6N   	Check All6Y   �� 59     -�   5        $��NewUsersAuthentication��59  #   $	��New��6A   $	 1��6O   �� 5:     -�   5        $��SaveUsersAuthentication��5:  #   $
��Save��6B   $	 1��6Q   �� 5;     -�   5        $��UpdateUsersAuthentication��5;  #   $��Update��6D   $	 1��6U   �� 5<     -�   5        $��UndoUsersAuthentication��5<  #   $
��Undo��6B   $	 1��6Q   �� 5=     -�   5        $��DeleteUsersAuthentication��5=  #   $��Delete Authentication��6D   $	 1��6d   5>     Q� 5@     -�   5        $��Units��5@  `   $��Units��6#   $
��Left=�62    .  .@�5A     �� -�   5        $��CheckAllUnits��5A  #   $	 1��68   .��6@   	Check All6K   �� 5B     -�   5        $��NewUnits��5B  #   $	��New��63   $	 1��6A   �� 5C     -�   5        $��	SaveUnits��5C  #   $
��Save��64   $	 1��6C   �� 5D     -�   5        $��UpdateUnits��5D  #   $��Update��66   $	 1��6G   �� 5E     -�   5        $��	UndoUnits��5E  #   $
��Undo��64   $	 1��6C   �� 5F     -�   5        $��DeleteUnits��5F  #   $��Delete Unit��66   $	 1��6L   �� 5G     -�   5        $��NewConvUnits��5G  #   $	��New��67   $	 1��6E   �� 5H     -�   5        $��SaveConvUnits��5H  #   $
��Save��68   $	 1��6G   �� 5I     -�   5        $��UpdateConvUnits��5I  #   $��Update��6:   $	 1��6K   �� 5J     -�   5        $��UndoConvUnits��5J  #   $
��Undo��68   $	 1��6G   �� 5K     -�   5        $��DeleteConvUnits��5K  #   $��Delete Unit��6:   $	 1��6P   5L     5M     �� 5O     -�   5        $��ListBox_UserClasses��5O  f   
�������
���   �$	��350��6A   $	��Top=�6N   6�   5P     5Q     5S     5T     5V     