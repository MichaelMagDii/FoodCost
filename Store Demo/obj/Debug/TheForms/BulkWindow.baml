   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5   	       Food_Cost.BulkWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Food_Cost     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  
BulkWindow��5	   	   $	��600��6   $	��650��6)   .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      $��8* �6   6*   �� 5      $��2* �6   6*    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   .+�5   
   �� $��2��6   $
��Bold"�6(   $��Center=�6:   $��Center=�6W   Item Name :6�   �� 5   
   -   5        $��ItemNametxt��5   >   $��2��6   $��Center=�6#   $��2��6Q   $��0 0 50 0q�6e    �� 
IsReadOnly . 6w   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ BulkItemsDGV��5         CanUserAddRows .     AutoGenerateColumns .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   $��Stretch=�6�   ��. $
��Auto=�5      $
��Auto=�6L   $��0 0 0 0q�5      $��4��6-   $��1��6A   ��5   *   �� 5   N     5    
   -   5        $ ItemsDGV��5        .  $ * 6V   $��Stretch=�6f   ��. $
��Auto=�5!      $
��Auto=�6L      
IsReadOnly . $��0 0 0 0q�5"      $��4��6-   $��1��6A   $y�Hidden=�6{   ��5    &   �� 5"   {   � 5$   
   $��2��6   $��2��6#    5%      �� 5&      6"   �� 5'      6"    5)      �� 5*      6%   �� 5+      6%   .+�5/      �� $��0��6   $
��Bold"�6%   $��Center=�67   $��Center=�6T   
Weight :6z   �� 50      -   5        $��WaisWeightttxt��50   @   $��0��6   $��Center=�6%   $��1��6V   $��0 0 60 0q�6f    . $
��Bold"�6�   �� 51      $��0��6   $��1��6%   $
��Bold"�65   $��Center=�6G   $��Center=�6d   %6�   �� 52      $��2��6   $
��Bold"�6%   $��Center=�67   $��Center=�6T   Cost :6x   �� 53      -   5        $��WaistCosttxt��53   @   $��2��6   $��Center=�6%   $��1��6T   $��0 0 60 0q�6d    . $
��Bold"�6�   �� 54      $��1��6   $��1��6%   $
��Bold"�65   $��Center=�6G   $��Center=�6d   %6�   55      � 58   
   $��2��6   $��2��6   $��2��6,    59      �� 5:      �� 5;      �� 5<      .+�5?      �� -   5        
��� � �$��5q�5?   )   	 � ToolTip$		 Add��64   $��1��6B   .��5@      �� 
 �� Orientation$
 
Horizontal=�6   .+�5A      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Store=�6.   �� 5B      $��Right=�6!   Save6D   5C      5D      �� 5F      -   5        
���**��$��5q�5F   (   .��5G      �� $
 
Horizontal=�6   .+�5H        $	 Add=�6.   �� 5I      $��Right=�6!   
Add Item6H   5J      5K      �� 5M      -   5        
���  ��$��2��5M   &   $��5q�66   .��5N      �� $
 
Horizontal=�6   .+�5O        $ Delete=�6.   �� 5P      $��Right=�6!   Delete6G   5Q      5R      5S      5U      5W      