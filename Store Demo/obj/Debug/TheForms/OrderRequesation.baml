   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5           Food_Cost.OrderRequesation   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:Food_Cost     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5
      � .+�5   
   � -   5        $��
parentdrid��5      .+�5      �� -   5        $��
stackpanel��5   R   $��	80 65 0 0q�6   $	��Top=�6,   $
��auto��6D   .+�5      �� $��Stretch=�6   .+�5      � -   5        $��MainGrid��5      
�������  � ColumnDefinitions  5      �� 5      $��1* �6/   .+�5      �� .+�5      �� $�� Request Items��6'   $��1��6?   $��
10 0 10 10q�6O   $��20(�6c   $
��Bold"�6q   $��Stretch=�6�   .��5   "   �� $��13(�6-   $��Normal"�6E   .+�5   &   &   System.Windows.Controls.DataGrid  -   5        $��ItemsDGV��5   /   $
��Auto=�6?   $	��350��6�   
���   �$��1q�6�   $
��Auto=�5   )      AutoGenerateColumns . ��.    CanUserAddRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5   /   $��Stretch=�6?   $��Center=�5   ~   ��5   o   �� ��5      ��  �� RelativeSource ��  �� AncestorType" � �� Path$ ActualHeight�5   '   5   #   5       5"      5#      � 5%      -   5        $��grid2��5%      $��1��6)     5'      �� 5(      $��* �6/   �� 5)      $��* �6/   �� 5*      $��* �6/   �� 5+      $��* �6/   �� 5,      $��* �6/   �� 5-      $��* �6/   .+�51      �� -   5        $��SaveBtn��51   1   $��3��6!   
��� � �$��12(�6T   $��2q�6b   $��10q�6n   .��52      ��  �� Orientation$ 
Horizontal=�6)   .+�53   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ CheckboxMarkedCircle=�6:   $��20��6V   $��20��6a   �� 54   "   $��Right=�6,   Order6P   55      56      �� 59      -   5        $��UndoBtn��59   W   $��4��6!   $��12(�61   $��2q�6?   $��10q�6K   {�. .��5:      �� $ 
Horizontal=�6)   .+�5;   "     $
 Undo=�6:   �� 5<   "   $��Right=�6,   Undo6O   5=      5>      �� 5@      -   5        $��	DeleteBtn��5@   1   $��5��6!   
����������#cb4335$��2q�6j   $��12(�6v   $��10q�6�   {�.  $y�Hidden=�6�   .��5A      �� $ 
Horizontal=�6)   .+�5B   "     $ Delete=�6:   �� 5C   "   $��Right=�6-   Remove6R   5D      5E      5G      5H      5I      5J      5K      5L      