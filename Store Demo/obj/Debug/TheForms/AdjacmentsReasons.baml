   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5      !    Food_Cost.AdjacmentsReasons   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Food_Cost     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � 
�������$��	80 65 0 0q�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��CategoryGBX��5   &   
���   �$��10q�69   ���#D0D3D4$��2q�6\   $��Adjacment Reasons��6p   $��15(�6�   .��5      �   5      �� 5      $��* �6'   �� 5      $��* �6'    5      �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5       $��* �6$   �� 5!      $��* �6$   .+�5$      �� $
��Bold"�6   $
��Code��6.   ���	#FFCD0E0E6:   �� 5%      -   5        $��Code_txt��5%      $��1��6*   $��2��67   {�.  6v   �� 5'      -   5        $��Active_chbx��5'      $��1��6.   $��Bottom=�6>   $��Right=�6Y   .��5(      �� �� 6"   
��� � �$
��Bold"�69   .�6K   Active6S   5)      5*      �� 5,      $
��Bold"�6   $��2��6.   ���	#FFCD0E0E$
��Name��6S   �� 5-      -   5        $��Name_txt��5-      $��3��6+   $��2��68   �� 5.      $��Name2��6   ���	#FFCD0E0E$��4��6@   �� 5/      -   5        $��	Name2_txt��5/      $��5��6+   $��2��68   � 51      $��6��6   $��2��6$     52      �� 53      $��* �6+   67   �� 54      $��* �6+   67   �� 55      $��* �6+   67   �� 56      $��* �6+   67   �� 57      $��* �6+   67   .+�5:      �� -   5        $��NewBtn��5:   -   $��0��6   
���   ����#cacfd2$��5q�6c   .��5;      ��  �� Orientation$ 
Horizontal=�6%   $
��Left=�6>   .+�5<      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Plus=�66   $
��Left=�6B   �� 5=      $��Right=�6(   New6J   5>      5?      �� 5@      -   5        $��SaveBtn��5@      $��1��6,   
��� � �$��5q�6O   .��5A      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5B        $ CheckboxMarkedCircle=�66   $
��Left=�6R   �� 5C      $��Right=�6(   Save6K   5D      5E      �� 5F      -   5        $��	UpdateBtn��5F      $��2��6.   
��� � �$��5q�6Q   .��5G      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5H        $ 	ArrowDown=�66   $
��Left=�6G   �� 5I      $��Right=�6(   Update6M   5J      5K      �� 5L      -	   5        $��UndoBtn��5L   -   $��3��6   $��5q�6<   .��5M      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5N        $
 Undo=�66   $
��Left=�6B   �� 5O      $��Right=�6)   Undo6L   5P      5Q      �� 5R      -
   5        $��	DeleteBtn��5R      $��4��6.   
����������#cb4335$��5q�6g   .��5S      �� $ 
Horizontal=�6%   $
��Left=�6>   .+�5T        $ Delete=�66   $
��Left=�6D   �� 5U      $��Right=�6(   Delete6M   5V      5W      5X      5Y      5Z      &   System.Windows.Controls.DataGrid  5\   
   -   5        $��
ReasonsDGV��5\      $��1��6%   $��2��65      CanUserAddRows .     AutoGenerateColumns . $��Stretch=�6�   ��. $
��Auto=�5]      $
��Auto=�6K      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�      
IsReadOnly . ��5\   ]   �� ��5^      ��  �� RelativeSource �� 	 �� AncestorType"	 �
 �� Path$
 ActualHeight�5_      5b      5c      