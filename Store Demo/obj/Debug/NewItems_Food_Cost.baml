   M S B A M L   `   `   `  ����  	Food_Cost"    Food_Cost.NewItems_Food_Cost   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5	      � -   5        $��
parentdrid��5	      $��	80 65 0 0q�6     � RowDefinitions  5
   
   �� 5      $��1* �6   �� 5      $	��12* �6   .+�5   
   � $��10q�6    � ColumnDefinitions 5      �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   .+�5      �� -   5        $��Backbtn��5   %   $��4��6   
�������
���iii�$��12(�6\   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.6.0.325, Culture=neutral, PublicKeyToken=null]]   Kind$ KeyboardReturn=�6.   �� 5      $��Right=�6    Back6B   5      5      5      � 5   
   $��1��6   .+�5       � -   5        $��MainGrid��5       
������� 5"      �� 5#      $��2* �6'   �� 5$      $��5* �6'   .+�5&      � $��0 20 0 0q�6     5'      �� 5(      $��1* �6(   �� 5)      $��6* �6(   �� 5*      $��1* �6(   �� 5+      $��1* �6(   .+�5-      � $��0 0 0 15q�6    5.      �� 5/      �� 50      �� 51      .+�55      �� -   5         � ToolTip$	 Add��55   !   
������ $��45��6[   .��56        $
 Plus=�66   $��20��6B   $��20��6M   
��� � �$��Center=�6l   57      �� 58      -   5        $��Reload��58   !   $��1��6/   $ Reload��6?   
������ $��45��6   .��59        $ Reload=�66   
�������$��20��6V   $��20��6a   $��Center=�6m   5:      �� 5;      -   5        $��2��5;   !   $ Remove��61   
������ $��45��6q   .��5<        $ Delete=�66   
���  ��$��20��6U   $��20��6`   $��Center=�6l   5=      5>      R� 5@      -   5        $��treeViewItems��5@   ,   $��1��6   ���	#FF2196F3$��2q�6�   $��10q�6�   6�   �� 5B      -   5        $��savebtn��5B   �   $��2��6   
��� � �$��12(�6=   $��2q�6K   $��10q�6W   $
 save��6c   .��5C      �� $ 
Horizontal=�6%   .+�5D        $ ThumbsUp=�66   $��0 0 5 0q�6F   �� 5E      $��Right=�6)   Save6L   5F      5G      �� 5I      $��3��6   $��12(�6*   $��2q�68   $��Center=�6D   $��10q�6_   .��5J      �� $ 
Horizontal=�6%   .+�5K        $ Close=�66   $��0 0 5 0q�6C   �� 5L      $��Right=�6)   Cancel6N   5M      5N      5P      � 5R      -	   5        $��	MainGrid2��5R      
�������$��2��6;   $
��0 20q�6K     5S      �� 5T      $��1* �6(   �� 5U      $��1* �6(   .+�5X      � -
   5        $��ItemsGroupBox��5X   ,   $��2��6   $y�Hidden=�6A   .+�5Y      �� {�. ���#D0D3D4$
��Item��6K   $
��Bold"�6Y   .��5Z      � -   5        $��detailsGrid��5Z   _   $��Normal"�6$   
���   � 5[   "   �� 5\   &   $��* �67   �� 5]   &   $��* �67   �� 5^   &   $��* �67   �� 5_   &   $��* �67   �� 5`   &   $��* �67   �� 5a   &   $��* �67   �� 5b   &   $��* �67   �� 5c   &   $��* �67     5e   "   �� 5f   &   $��* �64   �� 5g   &   $��* �64   �� 5h   &   $��* �64   �� 5i   &   $��* �64   �� 5j   &   $��* �64   �� 5k   &   $��* �64   �� 5l   &   $��* �64   �� 5m   &   $��* �64   .+�5o   "   �� -   5        $��	CodeLabel��5o   �   $��Code: *��6,   $��Center=�6;   $��Center=�6V   
���  ��6�   �� 5p   "   -   5        $��Codetxt��5p   U   $��1��6*   $��Center=�6:    �� 
IsReadOnly . $��Center=�6v   �� 5r   "   $��Manual Code: *��6,   $��Center=�6B   $��Center=�6]   
���  ��$��2��6�   �� 5s   "   -   5        $��Manual_Code_txt��5s   F   $��Center=�6+   $��3��6]    �� TextAlignment$ Center=�6n   6�   �� 5u   "   $��Bar Code��6,   $��4��6<   $��Center=�6L   $��Center=�6g   
���   �6�   �� 5v   "   -   5        $��
BarCodetxt��5v   U   $��5��6*   $��Center=�6:   6U   �� 5w   "   $��6��6)   $��GB��69   $��Center=�6F   $��Center=�6a   ���	#FF838F996~   � 5y   "   $��7��6'   $��Right=�67   $	��Top=�6S    5z   &   �� 5{   *   $	��74* �6;   �� 5|   *   $	��77* �6;   .+�5~   &   �� -   5        $��	ItemImage��5~   ,   $	��150��6>   $��gray.jpg��6J   $
��Fill=�6\   $��0,-14,0.4,-36.2q�6k   $��2��6�   6�   �� 5   &   -   5        $��Imagetxt��5   0   $��Click to Add Image��6@   
�������$��Center=�6m   $��Center=�6�   $��12,35,12.4,10.2q�6�   $��2��6�   $��22��6�   5�   #   �� 5�   "   $��1��6,   $��Name1: *��69   $��Center=�6I   $��Center=�6d   
���  ��6�   �� 5�   "   -   5        $��Name1txt��5�   b   $��1��6*   $��1��67   $��Center=�6G   $��Center=�6r   �� 5�   "   $��1��6,   $��2��69   $��Prefared Vendor: ��6I   $��Center=�6b   $��Center=�6}   �� 5�   "   -   5        $��PrefVendortxt��5�   b   $��1��6*   $��3��67   $��Center=�6G   $��Center=�6w    . 6�   �� 5�   "   -   5        $��1��5�   )   $��4��66   $	��...��6F   $��Center=�6T   $��Center=�6o   ���	#FF838F996�   �� 5�   "   $��2��6,   $��Name2: *��69   $��Center=�6J   $��Center=�6e   
���  ��6�   �� 5�   "   -   5        $��Name2txt��5�   b   $��2��6*   $��1��67   $��Center=�6G   $��Center=�6r   �� 5�   "   $��2��6,   $��2��69   $��Center=�6I   $��Specs��6d   $��Center=�6q   
���   �6�   �� 5�   "   -   5        $��Specstxt��5�   [   $��2��6*   $��3��67   $��2��6G   
���   �
���   �$��1q�6�    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn . $��Center=�6�   � 5�   "   $��3��6'   $��8��64    5�   &   �� 5�   *   $��1* �6;   �� 5�   *   $��2* �6;   �� 5�   *   $
��1.3* �6;   �� 5�   *   $��2* �6;   �� 5�   *   $��1* �6;   �� 5�   *   $��2* �6;   �� 5�   *   $��1* �6;   �� 5�   *   $��2* �6;   .+�5�   &   �� $��
Category: ��60   $��13(�6B   $��Center=�6P   $��Center=�6k   �� 5�   &   -   5        $��Categorytxt��5�   >   $��1��6.   $��3q�6Q   $��Center=�6]   $��0 0 0 20q�6x   {�.  $��Center=�6�   �� 5�   &   $��2��60   $��Department: ��6@   $��13(�6T   $��Center=�6b   $��Center=�6}   �� 5�   &   -   5        $��Departmenttxt��5�   >   $��3��6.   $��3q�6S   $��Center=�6_   $��0 0 0 20q�6z   {�.  $��Center=�6�   �� 5�   &   -   5        $��TheClass��5�   ?   $��Class: ��60   $��13(�6O   $��Center=�6]   $��4��6x   $��Center=�6�   �� 5�   &   -   5        $��Classtxt��5�   ?   $��5��6/   $��3q�6O   $��Center=�6[   $��0 0 0 20q�6v   {�.  $��Center=�6�   �� 5�   &   -   5        $��TheSubClass��5�   C   $��Sub Class: ��60   $��13(�6V   $��Center=�6d   $��6��6   $��Center=�6�   �� 5�   &   -   5        $��SubClasstxt��5�   >   $��7��6.   $��3q�6Q   $��Center=�6]   $��0 0 0 20q�6x   {�.  $��Center=�6�   5�   #   �� 5�   "   -   5        $��	Activecbx��5�   J   $��4��6+   $��Active��69   $��Center=�6[   �� 5�   "   -   5        $��
ExpDate_cb��5�   x   $��4��6+   $��1��68   $��Exp Date��6H   $��Center=�6[   6x   �� 5�   "   -   5        $��Inventory_Item��5�   `   $��4��6+   $��2��68   $��Non Inventory��6H   $��Center=�6v   �� 5�   "   -    5        $��CW_cb��5�   _   $��4��6+   $��3��68   $��Catch Weight��6H   6_   �� 5�   "   -!   5        $��BI_cb��5�   \   $��4��6+   $��4��68   $��	Bulk item��6H   6i   �� 5�   "   -"   5        $��PI_cb��5�   ^   $��4��6+   $��5��68   $��Parent item��6H   6k   �� 5�   "   -#   5        $��HI_cb��5�   \   $��4��6+   $��6��68   $��	Hot items��6H   6\   � 5�   "   $��5��6'   $��8��64    5�   &   �� 5�   *   $��* �6;   �� 5�   *   $��* �6;   �� 5�   *   $��* �6;   �� 5�   *   $��* �6;   .+�5�   &   �� -$   5        $��MUT_cb��5�   �   $��Multi Unit Track��6/   $��Center=�6J   $��Center=�6e   6�   �� 5�   &   $��1��61   $ 
Horizontal=�6A   $��Center=�6Z   $��Center=�6u   .+�5�   *   �� -%   5        $��TI_cb��5�   Z   $��Taxable item��63   $��1��6J   6g   �� 5�   *   -&   5        $��TI_Value��5�   4   $��30��6D   $y�Hidden=�6P   6�   �� 5�   *   -'   5        $��TI_Prec_icon��5�   P   $��%��60   $y�Hidden=�6<   6P   5�   '   �� 5�   &   $��2��61   $ 
Horizontal=�6A   $��Center=�6Z   $��Center=�6u   .+�5�   *   �� $��2��64   $��Yield: *��6D   $��Center=�6T   $��Center=�6o   
���  ��6�   �� 5�   *   -(   5        $��Yieldtxt��5�   �   $��3��62   $��50��6B   $��Center=�6M   $��Center=�6h   $��Center=�6�   �� 5�   *   $��3��64   $��%��6D   $��Center=�6M   $��Center=�6h   
���   �6�   5�   '   � 5�   &   $��3��6+    5�   *   �� 5�   .   $��* �6?   �� 5�   .   $��* �6?   �� 5�   .   $��* �6?   �� 5�   .   $��* �6?   .+�5�   *   �� $��	Weight: *��64   $��Center=�6E   
���  ��6`   �� 5�   *   -)   5        $��Weight��5�   �   $��1��62   $��Center=�6B   $
��Left=�6]   $��60��6x   $��10 0 0 0q�6�   $��Center=�6�   �� 5�   *   -*   5        $
��unit��5�   �   $��2��62   $��Center=�6B   $
��Left=�6]   
���   �$��50��6�   $��2q�6�   $��Center=�6�    . 6�   �� 5�   *   -+   5        $��3��5�   1   $��1��6A   $	��...��6O   $��Center=�6]   $��Center=�6x   ���	#FF838F996�   5�   '   5�   #   � 5�   "   $��6��6'   $��2��64   $��8��6E    5�   &   �� 5�   *   $
��1.8* �6;   �� 5�   *   .+�5�   &   �� $��Unit Conversion��6/   $	��120��6H   $��0 0 15 0q�6U   .��5�   *   �   5�   .   �� 5�   2   $
��0.8* �6@   �� 5�   2   �� 5�   2   $
��0.5* �6@    5�   .   �� 5�   2   �� 5�   2   �� 5�   2   �� 5�   2   �� 5�   2   �� 5�   2   .+�5�   .   �� $
��Unit��68   $��Center=�6D   �� 5�   .   -,   5        $��unit2��5�   C   $��1��66   $��Center=�6P   $	��5 0q�6t    . 6�   �� 5�   .   --   5        $��1��5�   5   $��1��6B   $	��...��6R   $��Center=�6`   $��Center=�6{   ���	#FF838F996�   �� 5�   .   $��2��68   $	�� = ��6H   $��Center=�6T   $��2��6q   $��Center=�6�   �� 5�   .   $	��Qty��68   $��Center=�6C   $��3��6`   �� 5�   .   -.   5        $��	ConvUnit2��5�   C   $��1��66   $��3��6T   $ Center=�6d   $	��5 0q�6{   $��Center=�6�   �� 5�   .   $
��Unit��68   $��Center=�6D   $��5��6a   $y�Hidden=�6q   �� 5�   .   -/   5        $��	unit_txt1��5�   `   $��4��66   $��1��6F   $	��5 0q�6S    . $��Center=�6�   �� 5�   .   -0   5        $��1��5�   5   $��5��6B   $	��...��6R   $��Center=�6`   $��Center=�6{   ���	#FF838F996�   5�   +   5   '   �� 5  &   -1   5        $��Unit_Conversion2��5  /   $y�Hidden=�6G   $��Unit Conversion 2��6[   $��1��6v   $	��110��6�   $��5 0 0 0q�6�   .��5  *   �   5  .   �� 5  2   $
��0.8* �6@   �� 5  2   �� 5  2   $
��0.5* �6@   .+�5  .   �� $
��Unit��68   $��Center=�6D   �� 5  .   -2   5        $��unit3��5  S   $��1��66   $
��10 0q�6E   $ Center=�6`   5  +   5  '   5  #   5     5      5!     � 5#     -3   5        $��CategoryGroupBox��5#     $y�Hidden=�63   .+�5$     �� ���#D0D3D4$��Category��6:   $��20(�6L   $
��Bold"�6d   .��5%     � $��Normal"�6$   
���   �$��13(�6_    5'  "   �� 5(  &   $��1* �67   �� 5)  &   $��2* �67   �� 5*  &   $��1* �67   �� 5+  &   $��2* �67     5.  "   �� 5/  &   $��2* �64   �� 50  &   $��2* �64   .+�53  "   �� $��Code: ��6,   $��13(�6:   $��Center=�6H   $��Center=�6c   
���  ��6�   �� 54  "   -4   5        $��CategoryCodetxt��54  *   $��1��6A   $��	0 0 10 10q�6Q   $��Center=�6d   {�.  6   �� 56  "   $
��ID: ��6,   $��2��68   $��13(�6H   $��Center=�6V   $��Center=�6q   
���  ��6�   �� 57  "   -5   5        $��CategoryIDtxt��57  *   $��3��6?   $��	0 0 10 10q�6O   $��Center=�6b   �� 59  "   $��1��6,   $��Name1: ��69   $��13(�6H   $��Center=�6V   $��Center=�6q   
���  ��6�   �� 5:  "   -6   5        $��CategoryName��5:  G   $��1��6*   $��1��67   $��	0 0 10 10q�6[   $��Center=�6n   �� 5<  "   $��1��6,   $��2��69   $��Name2: ��6I   $��13(�6Y   $��Center=�6g   $��Center=�6�   
���   �6�   �� 5=  "   -7   5        $��CategoryName2��5=  *   $��1��6?   $��3��6L   $��	0 0 10 10q�6\   $��Center=�6o   5?     5@     5A     � 5C     -8   5        $��DepartmentGroupBox��5C     $y�Hidden=�65   .+�5D     �� {�. ���#D0D3D4$��
Department��6K   $��20(�6_   $
��Bold"�6w   .��5E     � $��Normal"�6$   
���   �$��13(�6_    5G  "   �� 5H  &   $��1* �67   �� 5I  &   $��2* �67   �� 5J  &   $��1* �67   �� 5K  &   $��2* �67     5N  "   �� 5O  &   $��2* �64   �� 5P  &   $��2* �64   �� 5Q  &   $��2* �64   �� 5R  &   $��2* �64   .+�5V  "   �� $��Code: ��6,   $��13(�6:   $��Center=�6H   $��Center=�6c   
���  ��6�   �� 5W  "   -9   5        $��
depCodetxt��5W  *   $��1��6<   $��	0 0 10 10q�6L   $��Center=�6_   {�.  6z   �� 5Y  "   $��2��6,   $
��ID: ��6<   $��13(�6H   $��Center=�6V   $��Center=�6q   
���  ��6�   �� 5Z  "   -:   5        $��depIDtxt��5Z  *   $��3��6:   $��	0 0 10 10q�6J   $��Center=�6]   �� 5\  "   $��Name1: ��6-   $��1��6<   $��13(�6I   $��Center=�6W   $��Center=�6r   
���  ��6�   �� 5]  "   -;   5        $��
depNametxt��5]  *   $��1��6<   $��1��6I   $��	0 0 10 10q�6Y   $��Center=�6l   �� 5_  "   $��1��6,   $��2��69   $��Name2: ��6I   $��13(�6Y   $��Center=�6g   $��Center=�6�   
���   �6�   �� 5`  "   -<   5        $��depName2txt��5`  *   $��1��6=   $��3��6J   $��	0 0 10 10q�6Z   $��Center=�6m   �� 5b  "   $��2��6,   $��Center=�6:   $��Description��6U   $��13(�6h   $��Center=�6v   
���   �6�   �� 5c  "   -=   5        $��depDescriptiontxt��5c  *   $��2��6C   $��1��6P   $��3��6`   $��10q�6t   $��13(�6�   
���   �
���   �$��1q�6�   $
 Wrap=�6�    . $	��Top=�6�   5e     5f     5g     � 5i     ->   5        $��ClassGroupBox��5i     $y�Hidden=�60   .+�5j     �� {�. ���#D0D3D4$��Class��6K   $��20(�6Z   $
��Bold"�6r   .��5k     � $��Normal"�6$   
���   �$��13(�6_    5m  "   �� 5n  &   $��1* �67   �� 5o  &   $��2* �67   �� 5p  &   $��1* �67   �� 5q  &   $��2* �67     5t  "   �� 5u  &   $��2* �64   �� 5v  &   $��2* �64   �� 5w  &   $��2* �64   �� 5x  &   $��2* �64   .+�5|  "   �� $��Code: ��6,   $��13(�6:   $��Center=�6H   $��Center=�6c   
���  ��6�   �� 5}  "   -?   5        $��classCodetxt��5}  *   $��1��6>   $��	0 0 10 10q�6N   $��Center=�6a   {�.  6|   �� 5  "   $��2��6,   $
��ID: ��6<   $��13(�6H   $��Center=�6V   $��Center=�6q   
���  ��6�   �� 5�  "   -@   5        $��
classIDtxt��5�  *   $��3��6<   $��	0 0 10 10q�6L   $��Center=�6_   �� 5�  "   $��1��6,   $��Name: ��69   $��13(�6G   $��Center=�6U   $��Center=�6p   
���  ��6�   �� 5�  "   -A   5        $��classNametxt��5�  *   $��1��6>   $��1��6K   $��	0 0 10 10q�6[   $��Center=�6n   �� 5�  "   $��1��6,   $��2��69   $��Name2: ��6I   $��13(�6Y   $��Center=�6g   $��Center=�6�   
���   �6�   �� 5�  "   -B   5        $��className2txt��5�  *   $��1��6?   $��3��6L   $��	0 0 10 10q�6\   $��Center=�6o   �� 5�  "   $��2��6,   $��Center=�6:   $��Description��6U   $��13(�6h   $��Center=�6v   
���   �6�   �� 5�  "   -C   5        $��classDesctxt��5�  *   $��2��6>   $��1��6K   $��3��6[   $��10q�6o   $��13(�6{   
���   �
���   �$��1q�6�   $
 Wrap=�6�    . $	��Top=�6�   5�     5�     5�     � 5�     -D   5        $��SubClassGroupBox��5�     $y�Hidden=�63   .+�5�     �� {�. ���#D0D3D4$��	Sub Class��6J   $��20(�6]   $
��Bold"�6u   .��5�     � $��Normal"�6$   
���   �$��13(�6_    5�  "   �� 5�  &   $��1* �67   �� 5�  &   $��2* �67   �� 5�  &   $��1* �67   �� 5�  &   $��2* �67     5�  "   �� 5�  &   $��2* �64   �� 5�  &   $��2* �64   �� 5�  &   $��2* �64   �� 5�  &   $��2* �64   .+�5�  "   �� $��Code: ��6,   $��13(�6:   $��Center=�6H   $��Center=�6c   
���  ��6�   �� 5�  "   -E   5        $��subclassCodetxt��5�  *   $��1��6A   $��	0 0 10 10q�6Q   $��Center=�6d   {�.  6   �� 5�  "   $��2��6,   $
��ID: ��6<   $��13(�6H   $��Center=�6V   $��Center=�6q   
���  ��6�   �� 5�  "   -F   5        $��subclassIDtxt��5�  *   $��3��6?   $��	0 0 10 10q�6O   $��Center=�6b   �� 5�  "   $��1��6,   $��Name: ��69   $��13(�6G   $��Center=�6U   $��Center=�6p   
���  ��6�   �� 5�  "   -G   5        $��subclassNametxt��5�  7   $��1��6*   $��1��6N   $��	0 0 10 10q�6^   $��Center=�6q   �� 5�  "   $��1��6,   $��2��69   $��Name2: ��6I   $��13(�6Y   $��Center=�6g   $��Center=�6�   
���   �6�   �� 5�  "   -H   5        $��subclassName2txt��5�  *   $��1��6B   $��3��6O   $��	0 0 10 10q�6_   $��Center=�6r   �� 5�  "   $��2��6,   $��Center=�6:   $��Description��6U   $��13(�6h   $��Center=�6v   
���   �6�   �� 5�  "   -I   5        $��subclassDesctxt��5�  *   $��2��6A   $��1��6N   $��3��6^   $��10q�6r   $��13(�6~   
���   �
���   �$��1q�6�   $
 Wrap=�6�    . $	��Top=�6�   5�     5�     5�     5�     5�     5�     5�     5�     