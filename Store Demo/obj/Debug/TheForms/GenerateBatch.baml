   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5          Food_Cost.GenerateBatch   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Food_Cost     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � 
�������$��	80 65 0 0q�6   $��15(�61     � RowDefinitions  5   
   �� 5      $��3* �6   �� 5      $��1* �6   �� 5      $��6* �6   .+�5   
   �� $��Generate Batch��6   $��5q�6+   $
��Bold"�66   .��5      � $��Normal"�6    � ColumnDefinitions 5      �� 5      $��3* �6'   �� 5      $��3* �6'   �� 5      $��5* �6'   �� 5      $��2* �6'   �� 5      $��2* �6'     5      �� 5      �� 5      �� 5      .+�5"      �� $��1��6   $��	Resturant��6,   
���  ��$��Center=�6N   $��Center=�6i   � 5#      $��2��6    5$      �� 5%      $��2* �6+   �� 5&      $��1* �6+   .+�5(      �� -   5        $��
StoreIDcbx��5(      $��Stretch=�60   $��Stretch=�6L   
���   �$��Center=�6}   �� 5)      -   5        $��RestaurantBtn��5)   -   $��1��6   
�������
���   �$��5q�6i   .��5*      ��  �� Orientation$ 
Horizontal=�6%   .+�5+      �� $��Right=�6)   . . .6M   5,      5-      5.      �� 50      $��1��6   $��Kitchen��6-   $��1��6<   
���  ��$��Center=�6Z   $��Center=�6u   � 51      $��2��6   $��1��6'    52      �� 53      $��2* �6+   �� 54      $��1* �6+   .+�56      �� -   5        $��
Kitchencbx��56      $��Stretch=�60   $��Stretch=�6L   
���   �$��Center=�6}   �� 57      -   5        $��
KitchenBtn��57   -   $��1��6   
�������
���   �$��5q�6f   {�.  $��Center=�6�   .��58      �� $ 
Horizontal=�6%   .+�59      �� $��Right=�6)   . . .6M   5:      5;      5<      �� 5>      $��1��6   $��Recipe��6,   $��2��6:   
���  ��$��Center=�6X   $��Center=�6s   � 5?      $��2��6   $��2��6'    5@      �� 5A      $��2* �6+   �� 5B      $��1* �6+   .+�5D      �� -   5        $��	Recipecbx��5D      $��Stretch=�6/   $��Stretch=�6K   
���   �$��Center=�6|   �� 5E      -   5        $��	RecipeBtn��5E   -   $��1��6   
�������
���   �$��5q�6e   {�.  .��5F      �� $ 
Horizontal=�6%   .+�5G      �� $��Right=�6)   . . .6M   5H      5I      5J      � 5K      $��2��6   $��3��6$    5L      �� 5M      �� 5N      .+�5P      �� -   5        $��QtyofRecipetxt��5P      
���   �$��Stretch=�6H   $��Stretch=�6d   $��5q�6�   {�.  6�   �� 5Q      -   5        $��UnitofRecipelbl��5Q   ,   $��1��6   
���   �$��Stretch=�6W   $��Stretch=�6s   $��5q�6�   5R      �� 5S      $ 
Horizontal=�6   $��2��66   $��3��6C   5U      5V      5W      � 5Y   
   $��1��6    5Z      �� 5[      $��* �6#   �� 5\      $��* �6#   �� 5]      $��* �6#   �� 5^      $��* �6#   .+�5a      �� -	   5        $��GenerateBtn��5a   %   $��1��6   
���   ����#cacfd2{�.  .��5b      �� $ 
Horizontal=�6   .+�5c      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6.   $��20��6:   �� 5d      $��Right=�6    Generate Recipe6N   5e      5f      �� 5h      -
   5        $��ReqBtn��5h   &   $��2��6   
��� � �$��12(�6H   $��2q�6V   $��	10 0 70 0q�6b   {�.  $y�Hidden=�6�   .��5i      �� $ 
Horizontal=�6   .+�5j        $ Inbox=�6.   $��20��6;   $��20��6F   �� 5k      $��Right=�6    Order Requsation6O   5l      5m      �� 5n      -   5        $��	DeleteBtn��5n   %   $��3��6   
���  ��$��12(�6H   $��2q�6V   $��10q�6b   $y�Hidden=�6o   .��5o      �� $ 
Horizontal=�6   .+�5p        $ Delete=�6.   $��20��6<   $��20��6G   �� 5q      $��Right=�6    Delete6E   5r      5s      5u      � 5w   
   $��2��6   .+�5x      �� $ Vertical=�6   .+�5y      &   System.Windows.Controls.DataGrid  -   5        $��
RecipesDGV��5y      
���   �$��1q�6A   $
��Auto=�6U      AutoGenerateColumns . $
��Auto=�6�   ��.    CanUserAddRows .  $y�Hidden=�5z   �      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   ��5y   �   �� 5{      � 5|       5}      �� 5~      �� 5      .+�5�      �� -   5        $��NameoftotalCost��5�   I   $��Total Cost  :��6!   
���   �$��Center=�6`   $��Center=�6{   $
��Bold)�6�   $��20(�6�   $��0,10,0,0q�6�   $y�Hidden=�6�   �� 5�      -   5        $��TotalCosttxt��5�   �   $��1��6   $��Center=�6.   
���   �$��2q�6^   
���   �$
��Bold)�6�   $��20(�6�    �� TextAlignment$ Center=�6�   $��Center=�6�   $��
0 10 500 0q�6�   $y�Hidden=�6  5�      5�      5�      5�      5�      