   M S B A M L   `   `   `  ����D  @Food_Cost, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Food_Cost	Food_Cost  5   	       Food_Cost.Close_Month   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Food_Cost     5   	     � Title$  Close Month��5   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�63   $	��300��6X   $	��200��6d   .��5	      �� .+�5
   
   �� $��previous Month:��6   � 5   
   .+�5      �� -   5        $��	PrevMonth��5   ;   $��Sep,2019��6   $��
30 10 0 10q�6'   6;   �� 5      -   5         ��Name$ OpenBtn��5   i   $��Right=�6   $��0,6,15,8q�61   $
��Open��6C   $��67��6R   $��30��6]   $y�Hidden=�6�   5      �� 5   
   $��Current Month:��6   � 5   
   .+�5      �� -   5        $��	CurrMonth��5   ;   $��Oct,2019��6   $��
30 10 0 10q�6'   6;   �� 5      -   5        $��CloseBtn��5   j   $��Right=�6   $��0,6,15,8q�61   $��Close��6C   $��67��6S   $��30��6^   6z   5      � 5   
   -   5        $��	ProgressB��5   0   $��13��6   $
��10,0q�6"    � Minimum$ 0Q�6C    � Maximum$	 100Q�6O    � Value 6]   �� *WorkerState+5      5      