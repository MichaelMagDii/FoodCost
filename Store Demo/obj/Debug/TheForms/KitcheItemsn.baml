   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5          Food_Cost.KitcheItemsn   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      #localclr-namespace:Food_Cost  5      .��5
      � 
�������$��	80 65 0 0q�6     � RowDefinitions  5   
   �� 5      $��2* �6   �� 5      $��7* �6   �� 5      $��1* �6   .+�5   
   �� $��Kitchens��6   $��10q�6%   $
��Bold"�61   .��5      � $��Normal"�6    � ColumnDefinitions 5      �� 5      $��3* �6'   �� 5      $��4* �6'   �� 5      $��4* �6'     5      �� 5      �� 5      .+�5      �� $��
Ressturant��6   
���  ��$��Center=�6@   $��Center=�6[   �� 5       -   5        $��	Outletcbx��5    Y   $��1��6   $��Center=�6+   
���   � �� TextAlignment$ Center=�6j   $��Center=�6�   6�   �� 5"      $��Kitchen��6   $��1��6+   
���  ��$��Center=�6I   $��Center=�6d   �� 5#      -   5        $��
Kitchencbx��5#   g   $��1��6   $��1��6+   $��Center=�68   
���   �$ Center=�6y   $��Center=�6�   6�   5&      5'      �� 5)   
   $��Items��6   $��1��6"   $��10q�6/   $
��Bold"�6;   $��Stretch=�6W   .��5+      &   System.Windows.Controls.DataGrid  -   5        $��ItemsDGV��5+      $
��Auto=�6'   
���   �$��1q�6�   $
��Auto=�6�      AutoGenerateColumns . ��.    CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5,   �   ��5+   W   �� 5.      5/      � 51   
   -   5        $��
ButtonGrid��51      $��2��6   $y�Hidden=�6.    53      �� 54      $��* �6#   �� 55      $��* �6#   �� 56      $��* �6#   �� 57      $��* �6#   .+�5=      �� -   5        $��AddBtn��5=   %   $��1��6   
���   ����#cacfd2$��10q�6[   .��5>      ��  �� Orientation$ 
Horizontal=�6   .+�5?      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�6.   $��20��6:   �� 5@      $��Right=�6    
Add Item6G   5A      5B      �� 5D      -   5        $��SaveBtn��5D   %   $��2��6   
��� � �$��10q�6H   .��5E      �� $ 
Horizontal=�6   .+�5F        $ Inbox=�6.   $��20��6;   $��20��6F   �� 5G      $��Right=�6    Save6C   5H      5I      �� 5K      -   5        $��	DeleteBtn��5K   %   $��3��6   
���  ��$��10q�6G   .��5L      �� $ 
Horizontal=�6   .+�5M        $ Delete=�6.   $��20��6<   $��20��6G   �� 5N      $��Right=�6    Delete6E   5O      5P      5S      5T      5V      