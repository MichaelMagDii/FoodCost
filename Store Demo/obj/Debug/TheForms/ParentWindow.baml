   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5   	       Food_Cost.ParentWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Food_Cost     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  ParentWindow��5   	   $	��600��6   $	��650��6+   .��5
      � -   5        $��MainGrid��5
      .+�5   
   � -   5        $��
ParentGrid��5      $y�Visible=�6!    � RowDefinitions 5      �� 5      6"   �� 5      $��8* �6    6.   �� 5      6"    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   �� 5      6%   �� 5      6%   .+�5      �� $��2��6   $
��Bold"�6,   $��Center=�6>   $��Center=�6[   Item Name :6�   �� 5      -   5        $��ItemNametxt��5   B   $��2��6   $��Center=�6'   $��2��6U   $��0 0 50 0q�6i   &   System.Windows.Controls.DataGrid  5      -   5          Name$ ParentItemsDGV��5   O   $��1��6   $��4��6$      CanUserAddRows .    AutoGenerateColumns .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5   -   $��Stretch=�6=   ��. $
��Auto=�5      $
��Auto=�6A      
IsReadOnly . $��Center=�5   ~   ��5   t   �� ��5      ��  �� RelativeSource �� 	 �� AncestorType"	 �
 �� Path$
 ActualHeight�6~   �� 5!      -   5        
���**��$��5q�5!   )    � ToolTip$	 Add��64   $��2��6C   $��0��6P   .��5"      ��  �� Orientation$ 
Horizontal=�6   .+�5#      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ DatabaseCheck=�6.   �� 5$      $��Right=�6!   	Add Items6H   5%      5&      �� 5(      -   5        
��� � �$��5q�5(   )   $
 Save��64   $��2��6D   $��1��6Q   .��5)      �� $ 
Horizontal=�6   .+�5*        $ Store=�6.   �� 5+      $��Right=�6    Save6C   5,      5-      �� 5/      -   5        
����  �$��5q�5/   (   $ Cancel��63   $��2��6E   $��3��6R   .��50      �� $ 
Horizontal=�6   .+�51        $ Cancel=�6.   �� 52      $��Right=�6!   Cancel6G   53      54      �� 56      -   5        
���  ��$��Center=�56   &   $��5q�6A   $��2��6L   $��2��6Y   .��57      �� $ 
Horizontal=�6   .+�58        $ Delete=�6.   �� 59      $��Right=�6!   Delete6F   5:      5;      5<      � 5>   
   -	   5        $��	ShowItems��5>      $y�Hidden=�6    .+�5?      �� ._�5@      �� .+�5A      � .+�5B      �� $��1��6&   .+�5C      �  5D   "   �� 5E   &   $��* �67   6C   �� 5F   &   $��3* �67   6D   �� 5G   &   $	��12* �67   6E   �� 5H   &   $��4* �67   6D   �� 5I   &   $��4* �67   6D   �� 5J   &   $��4* �67   6D   .+�5M   "     $��0��6:   $ Database=�6J   ���#566573$��28��6o   $��30��6{   $��3q�6�   �� 5N   "   $��Items��6,   $	��0 5q�69   $��1��6G   $
��Bold"�6W   ���#566573$��15(�6~   �� 5O   "   $��Search��6,   $	��0 5q�6:   $��2��6H   $
��Bold"�6X   $��Center=�6j   $��Center=�6�   ���#566573$��15(�6�   �� 5P   "   -
   5        $��	SearchTxt��5P   *   $��3��6;   $��Center=�6K   $��Stretch=�6f   $��12(�6�   6�   	� 5Q   "   -   5        $��RadioByCode��5Q   v   $��4��6.   $��Center=�6>   $��Center=�6[   .��6�   	By Code6�   	� 5R   "   -   5        $��RadioByName��5R   v   $��5��6.   $��Center=�6>   $��Center=�6[   .��6�   	By Name6�   5T        5V      -   5        $ ItemsDGV��5V   &    . $ * 6n   $��Stretch=�6~   ��. $
��Auto=�5W      $
��Auto=�6K    . $��Center=�5Y   -   ��5V   8   �� ��5X      ��  �� "	 �$
 ActualHeight�5Y   -   5[      5\      5]      5^      5_      5`      5b      