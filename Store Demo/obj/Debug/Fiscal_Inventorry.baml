   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5      !    Food_Cost.Fiscal_Inventorry   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      #localclr-namespace:Food_Cost  5      .��5
      � .+�5   
   �� $��	80 65 0 0q�6   .+�5      �� .+�5      � 
�������.+�5      �� .+�5      � .+�5      �� $��Fiscal Inventory��6'   $��
10 10 0 10q�6A   $��20(�6U   $
��Bold"�6c     � ToolTip$3  -You should Chose Ressturant and kitchen Here!��6   .��5   "   � $��13(�6'   $��Normal"�6?   $��80��6]    � ColumnDefinitions 5   &   �� 5   *   $��3* �6;   �� 5   *   $��4* �6;   �� 5   *   $��4* �6;    � RowDefinitions 5   &   �� 5   *   �� 5   *   .+�5   &   �� $��	Resturant��61   
���  ��$��Center=�6S   $��Center=�6n   $��0,0,0,0q�6�   �� 5    &   -   5        $��	Outletcbx��5    n   $��1��6/   $��Center=�6?   
���   � �� TextAlignment$ Center=�6   $��Center=�6�   6�   �� 5"   &   $��Kitchen��60   $��1��6?   
���  ��$��Center=�6]   $��Center=�6x   �� 5#   &   -   5        $��
Kitchencbx��5#   {   $��1��6/   $��1��6?   $��Center=�6L   
���   �$ Center=�6�   $��Center=�6�   6�   5$   #   5%      5&      �� 5(      $��Items��6#   $��1��62   $��10 10 10 10q�6B   $��20(�6W   $
��Bold"�6e   $��Stretch=�6�   .��5)      �� $��13(�6)   $��Normal"�6A   .+�5+   "   &   System.Windows.Controls.DataGrid  -   5        $��ItemsDGV��5+   +   $
��Auto=�6;   $	��400��6�   $	��400��6�   
���   �$��1q�6�   $
��Auto=�5,   5      AutoGenerateColumns . ��.    CanUserAddRows .  ��5+   k   �� 5.   #   5/      50      52      53      � 54       56      �� 57      $��* �6+   �� 58      $��* �6+   �� 59      $��* �6+   �� 5:      $��* �6+   .+�5@      �� -   5        $��AddBtn��5@   A   $��1��6   $��2��6-   
���   ����#cacfd2$��12(�6x   $��2q�6�   $��10,10,9.8,10q�6�   $
��Auto��6�   {�.  .��5A      ��  �� Orientation$ 
Horizontal=�6%   .+�5B      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$
 Plus=�66   $��20��6B   �� 5C      $��Right=�6(   
Update QTy6Q   5D      5E      �� 5G      -   5        $��SaveBtn��5G   -   $��2��6   
��� � �$��12(�6P   $��2q�6^   $��10q�6j   $y�Hidden=�6v   .��5H      �� $ 
Horizontal=�6%   .+�5I        $ Inbox=�66   $��20��6C   $��20��6N   �� 5J      $��Right=�6(   Save6K   5K      5L      �� 5N      -   5        $��	DeleteBtn��5N   -   $��3��6   
���  ��$��12(�6P   $��2q�6^   $��10q�6j   $y�Hidden=�6v   .��5O      �� $ 
Horizontal=�6%   .+�5P        $ Delete=�66   $��20��6D   $��20��6O   �� 5Q      $��Right=�6(   Delete6M   5R      5S      5V      5W      5X      5Y      5Z      