   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5   	       Food_Cost.All_Resturants   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Food_Cost     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        � Title$  
Resturants��5   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�62   $
��1000��6W   $	��600��6d   .��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��9* �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $
��1.4* �6#   62   �� 5      $��5* �6#   60   �� 5      $��4* �6#   60   �� 5      $��6* �6#   60   �� 5      $��4* �6#   60   �� 5      $��4* �6#   60   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Store=�6&   ���#566573$��35��6H   $��50��6T   �� 5      $��
Resturants��6   $	��0 5q�6*   $��1��68   $
��Bold"�6H   ���#566573$��15(�6o   5      &   System.Windows.Controls.DataGrid  5    
   -   5        $��ResturantsDGV��5       $��1��6)      AutoGenerateColumns .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6j   $��Stretch=�6z   ��. $
��Auto=�5!   F   $
��Auto=�6v      
IsReadOnly . $��Center=�5"      ��5    6   �� 5"      5$      5&      