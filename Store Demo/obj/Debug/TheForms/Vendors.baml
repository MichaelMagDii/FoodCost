   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Store_Demo
Store_Demo  5          Food_Cost.Vendors   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Store_Demo     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � 
�������$��	80 65 0 0q�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��StoreGBX��5   &   
���   �$��5q�66   ���#D0D3D4$��2q�6X   $��Vendor��6l   $��15(�6|   .��5      �  5      �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��2* �6$     5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5!      �� $
��Bold"�6   $
��Code��6.   ���	#FFCD0E0E6:   �� 5"      -   5        $��Code_txt��5"      $��1��6*   $��2��67   �� 5$      $��1��6   .+�5%      �� $�� ��6    $	��0 0q�6(   �� 5&      -   5        $��Active_chbx��5&      $��Bottom=�62   $��Right=�6M   .��5'      �� �� 6%   
��� � �$
��Bold"�6<   .�6N   Active6V   5(      5)      5*      �� 5,      $
��Bold"�6   ���	#FFCD0E0E$
��Name��6F   $��2��6R   �� 5-      -   5        $��Name_txt��5-      $��3��6*   $��2��67   � 5/      $��4��6   $��2��6$     50      �� 51      $��* �6+   67   �� 52      $��* �6+   67   �� 53      $��* �6+   67   �� 54      $��* �6+   67   �� 55      $��* �6+   67   .+�58      �� -   5        $��NewBtn��58   -   $��0��6   
���   ����#cacfd2$��5q�6}   .��59      ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   .+�5:      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�66   $
��Left=�6B   �� 5;      $��Right=�6(   New6J   5<      5=      �� 5?      -   5        $��SaveBtn��5?      $��1��6,   
��� � �$��5q�6O   .��5@      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5A        $ CheckboxMarkedCircle=�66   $
��Left=�6R   �� 5B      $��Right=�6(   Save6K   5C      5D      �� 5F      -   5        $��	UpdateBtn��5F      $��2��6.   
��� � �$��5q�6Q   .��5G      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5H        $ 	ArrowDown=�66   $
��Left=�6G   �� 5I      $��Right=�6(   Update6M   5J      5K      �� 5L      -   5        $��UndoBtn��5L   -   $��3��6   $��5q�6<   .��5M      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5N        $
 Undo=�66   $
��Left=�6B   �� 5O      $��Right=�6(   Undo6K   5P      5Q      �� 5R      -	   5        $��	DeleteBtn��5R      $��4��6.   
����������#cb4335$��5q�6g   .��5S      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5T        $ Delete=�66   $
��Left=�6D   �� 5U      $��Right=�6(   Delete6M   5V      5W      5X      5Z      5[      &   System.Windows.Controls.DataGrid  5]   
   -
   5        $��Vendors_DGV��5]      $��1��6&   $��2��66      AutoGenerateColumns .    CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   $��Stretch=�5^      ��. $
��Auto=�6T   $
��Auto=�5_         
IsReadOnly . ��5]   G   �� 5`      5b      5c      