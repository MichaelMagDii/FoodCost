   M S B A M L   `   `   `  ����  	Food_Cost    Food_Cost.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � WindowState$  	Maximized=�5       � Title$ 
MainWindow��62   
������� � 	Resources 5	         OpenMenu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  	CloseMenu��%�  &	        &	 �     �� 5
   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $ 70Q�63     5      $ 
0:0:0.0002��6'   $	 230Q�6<   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $	 200Q�63     5      $ 
0:0:0.0002��6'   $ 70Q�6<   5      5      5       � Triggers 5      :� 5   
    :� RoutedEvent$ ButtonBase.Click��6   	 :� 
SourceName$	 ButtonOpenMenu��66   .[�5      �� #��[  6   5      :� 5   
   $ ButtonBase.Click��6   $	 ButtonCloseMenu��66   .[�5      �� #��[ 6   5      .��5!      � ���	#FFE2E1E7.+�5"   
   � ���	#FFDEDEDE.+�5#      � -   5        
 �Name$
 GridMain��5#      �� 5$      -   5        $��BackGroundImage��5$      $��Logo.jpg��6+   6?   � 5%      $��60��6   $	��Top=�6   ���	#FF5C99D6 J� Effect 5&      3  -System.Windows.Media.Effects.DropShadowEffect  5'         
BlurRadius$ 1Q�6'      ShadowDepth$ 1Q�66      Color$ 	#FFACACAC��6F   .+�5)      ��  �� Orientation$ 
Horizontal=�6   $��Right=�66   .+�5*      �� $�� ��6    $��Center=�6(   $��16(�6C   '  !MaterialDesignThemes.Wpf.PopupBox  5,         PlacementMode$ BottomAndAlignRightEdges=�6.      	StaysOpen .  $��10q�6i      PopupContent. 5-      �� $	��150��6%   .+�5.      �� -   5        $��Login��5.   %   65   �� 5/      $��Settings��6%   �� 50      $��Account��6%   �� 51      $
��Help��6%   �� 52      �� 53      $��Logout��6%   54      55      57      �� 58      -   5        $��
NameofForm��58   T   $��Center=�6   $��Center=�69   $��22(�6f   59      5:      � 5<      -   5        $
 GridMenu��5<      $��80��6%   $
��Left=�60   ���	#FF31577E 5=        5>      $ 2Q�6'   $ 1Q�66   $ 	#FFAEAEAE��6F    � RowDefinitions 5@      �� 5A      $
��0.8* �6$   �� 5B      $	��10* �6$   .+�5E      � .+�5F      � 
������� J� RenderTransformOrigin$ 0.5,0.2&�6.   z�5G      V� 5H      .~�5I   "   �� �� 5J   "   �� 5K   "   T� 5L   "   5M      .+�5O      �� -   5         ��Name$ ButtonOpenMenu��5O   !   $��60��69   $��60��6E   $��5q�6P   $��Right=�6[   $	��Top=�6w    0 ,MaterialDesignFloatingActionMiniAccentButton#��[ 6�   ���	#FF31577E��6�   Z� ��6�   Z� .��5P      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Menu=�66   $��25��6B   $��25��6M   5Q      �� 5R      -   5        $ ButtonCloseMenu��5R   !   $y�	Collapsed=�6:   $��60��6Q   $��60��6]   $��5q�6h   $��Right=�6s   $	��Top=�6�   #��[ 6�   ���	#FF31577E��6�   Z� ��6  Z� .��5S        $ 	ArrowLeft=�66   $��25��6G   $��25��6R   5T      �� 5U      $	��100��6    $��Bottom=�6-   5V      5W      � 5Y      $��1��6   .+�5Z      �� -   5         ��Name$ ListViewMenu��5Z      ���	#FF5C99D6$��Disabled=�6L   .@�5[      �� -	   5         ��Name$ PurchaseOrder��5[   '   $��55��6>   .��5\      �� $ 
Horizontal=�6)   .+�5]   "     $ CartOutline=�6:   $��30��6M   $��30��6Y   ���#85c1e9$��5q�6y   �� 5^   "   $
��Bold"�6,   $��16(�6>   $��
Purchasing��6L   ���#85c1e9$��Center=�6s   $
��20 0q�6�   5_      5`      �� 5b      -
   5        $ RecieveOrder��5b   '   $��55��6=   .��5c      �� $ 
Horizontal=�6)   .+�5d   "     $ Import=�6:   $��30��6H   $��30��6T   ���#85c1e9$��5q�6t   �� 5e   "   $
��Bold"�6,   $��16(�6>   $��	Receiving��6L   ���#85c1e9$��Center=�6r   $
��20 0q�6�   5f      5g      �� 5i      -   5        $ Transfer_Kitchen��5i   '   $��50��6A   .��5j      �� $ 
Horizontal=�6)   .+�5k   "     $ CubeSend=�6:   $��30��6J   $��30��6V   ���#85c1e9$��5q�6v   �� 5l   "   $
��Bold"�6,   $��16(�6>   $��Transfer Kitchen��6L   ���#85c1e9$��Center=�6y   $
��20 0q�6�   5m      5n      �� 5p      -   5        $ Transfer_Resturant��5p   '   $��50��6C   .��5q      �� $ 
Horizontal=�6)   .+�5r   "     $ TransferWithinAStation=�6:   $��30��6X   $��30��6d   ���#85c1e9$��5q�6�   �� 5s   "   $
��Bold"�6,   $��16(�6>   $��Transfer Returant��6L   ���#85c1e9$��Center=�6z   $
��20 0q�6�   5t      5u      �� 5w      6&   �� 5y      -   5        $ 	Inventory��5y   '   $��50��6:   .��5z      �� $ 
Horizontal=�6)   .+�5{   "     $ Store=�6:   $��30��6G   $��25��6S   $	��5 0q�6^   �� 5|   "   $��13(�6,   $��restaurants��6:   $��Center=�6M   $
��30 0q�6h   5}      5~      �� 5�      -   5        $ Items��5�   '   $��50��66   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ BoxVariantClosed=�6:   $��30��6S   $��25��6_   $	��5 0q�6j   �� 5�   "   $��13(�6,   $��Center=�6:   $
��30 0q�6U   $��
Items View��6c   5�      5�      �� 5�      -   5        $ Recipes��5�   '   $��50��68   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ ChefHat=�6:   $��30��6J   $��25��6V   $	��5 0q�6a   �� 5�   "   $��12(�6,   $��Recipes��6:   $��Center=�6I   $
��30 0q�6d   5�      5�      �� 5�      -   5        $ GenerateBatch��5�   '   $��50��6>   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ 
FilterList=�6:   $��30��6M   $��25��6Y   $	��5 0q�6d   �� 5�   "   $��12(�6,   $��Generate Batch��6:   $��Center=�6P   $
��30 0q�6k   5�      5�      �� 5�      -   5        $ KitcheItemsn��5�   '   $��50��6=   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Kitchen=�6:   $��30��6J   $��25��6V   $	��5 0q�6a   �� 5�   "   $��12(�6,   $��Kitchen Items��6:   $��Center=�6O   $
��30 0q�6j   5�      5�      �� 5�      -   5        $ CategoriesAndSub��5�   '   $��50��6A   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Category=�6:   $��30��6K   $��25��6W   $	��5 0q�6b   �� 5�   "   $��12(�6,   $��Categories and SUB��6:   $��Center=�6T   $
��30 0q�6o   5�      5�      �� 5�      -   5        $ ProcessBulkItems��5�   '   $��50��6A   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ CommentProcessing=�6:   $��30��6T   $��25��6`   $	��5 0q�6k   �� 5�   "   $��12(�6,   $��Process Bulk Item��6:   $��Center=�6S   $
��30 0q�6n   5�      5�      �� 5�      -   5        $ 
Adjustment��5�   '   $��50��6;   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Adjust=�6:   $��30��6I   $��25��6U   $	��5 0q�6`   �� 5�   "   $��12(�6,   $��
Adjustment��6:   $��Center=�6L   $
��30 0q�6g   5�      5�      �� 5�      -   5        $ AdjustmentReasons��5�   '   $��50��6B   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ 	Subtitles=�6:   $��30��6L   $��25��6X   $	��5 0q�6c   �� 5�   "   $��12(�6,   $��Adjustment Reasons ��6:   $��Center=�6U   $
��30 0q�6p   5�      5�      �� 5�      -   5        $ PhysicalInventory��5�   '   $��50��6B   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Store24Hour=�6:   $��30��6N   $��25��6Z   $	��5 0q�6e   �� 5�   "   $��12(�6,   $��Physical Inventory��6:   $��Center=�6T   $
��30 0q�6o   5�      5�      �� 5�      -   5        $ Vendors��5�   '   $��50��68   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ UserOutline=�6:   $��30��6N   $��25��6Z   $	��5 0q�6e   �� 5�   "   $��12(�6,   $��Vendors��6:   $��Center=�6I   $
��30 0q�6d   5�      5�      �� 5�      -   5        $ Users��5�   '   $��50��66   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ AccountMultiple=�6:   $��30��6Q   $��25��6]   $	��5 0q�6h   �� 5�   "   $��12(�6.   $��Users��6<   $��Center=�6I   $
��30 0q�6d   5�      5�      �� 5�      -   5        $ Units��5�   '   $��50��66   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Unity=�6:   $��30��6G   $��25��6S   $	��5 0q�6^   �� 5�   "   $��12(�6.   $��Units��6<   $��Center=�6I   $
��30 0q�6d   5�      5�      �� 5�      6&   �� 5�      -   5        $ CenterSetup��5�   '   $��60��6<   .��5�      �� $ 
Horizontal=�6)   .+�5�   "     $ Settings=�6:   $��30��6K   $��30��6W   ���#85c1e9$��5 0 5 5q�6w   �� 5�   "   $
��Bold"�6,   $��16(�6>   $��Resturant Setup��6L   ���#85c1e9$��Center=�6x   $
��20 5q�6�   5�      5�      5�      5�      5�      5�      5�      5�      