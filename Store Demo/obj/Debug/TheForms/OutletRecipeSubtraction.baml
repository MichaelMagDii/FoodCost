   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5      '    !Food_Cost.OutletRecipeSubtraction   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Food_Cost     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5      � .+�5   
   �� $��	80 65 0 0q�6   .+�5      �� $��Stretch=�6   .+�5      � 
�������.+�5      �� .+�5      � .+�5      �� $��Outlet Recipe Subtraction��6'   $��
10 10 0 10q�6J   $��20(�6^   $
��Bold"�6l   
���   �.��5   "   � $��13(�6'   $��Normal"�6?   $	��150��6]     � ColumnDefinitions  5   &   �� 5   *   $��3* �6;   �� 5   *   $��4* �6;   �� 5   *   $��4* �6;    � RowDefinitions 5   &   �� 5   *   �� 5   *   �� 5   *   .+�5!   &   �� $��	Resturant��61   
���  ��$��Center=�6S   $��Center=�6n   $��0,0,0,0q�6�   �� 5"   &   -   5        $��
StoreIDcbx��5"   n   $��1��6/   $��Center=�6?   
���   � �� TextAlignment$ Center=�6�   $��Center=�6�   6�   �� 5&   &   $��Kitchen��61   $��1��6@   
���  ��$��Center=�6^   $��Center=�6y   $��0,0,0,0q�6�   �� 5'   &   -   5        $��
Kitchencbx��5'   {   $��1��6/   $��1��6?   $��Center=�6L   
���   �$ Center=�6�   $��Center=�6�   � 5*   &   $��2��6+   $��3��68   .+�5+   *   ��  �� Orientation$ 
Horizontal=�65   $��Center=�6N   $��	10,0,10,0q�6k   $	��561��6~   $��15(�6�   .+�5,   .   �� $
��From��64   $��
30 10 20 0q�6C   
���  ��6W   (  "System.Windows.Controls.DatePicker  5-   .   -   5        $
��From��5-   E   $	��150��69   6E   �� 5.   .   $��To��64   $��
70 10 20 0q�6A   
���  ��6U     5/   .   -   5        $��To��5/   E   $	��150��69   6E   50   +   51   '   52   #   53      54      57      58      � 5:        5<      �� 5=      $��* �6+   �� 5>      $��* �6+   �� 5?      $��* �6+   �� 5@      $��* �6+   .+�5F      �� -   5        $��SubtractionBtn��5F   -   $��1��6   
���   ����#cacfd2$��12(�6l   $��2q�6z   $��10,10,9.8,10q�6�   $
��Auto��6�   .��5G      �� $ 
Horizontal=�6%   .+�5H      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�66   $��20��6B   �� 5I      $��Right=�6(   Subtraction6S   5J      5K      �� 5M      -   5        $��	DeleteBtn��5M   -   $��3��6   
���  ��$��12(�6P   $��2q�6^   $��10q�6j   $y�Hidden=�6w   .��5N      �� $ 
Horizontal=�6%   .+�5O        $ Delete=�66   $��20��6D   $��20��6O   �� 5P      $��Right=�6(   Delete6M   5Q      5R      5T      5U      5V      5W      5X      