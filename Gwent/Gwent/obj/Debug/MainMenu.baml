   M S B A M L   `   `   `  ����@  <Gwent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GwentGwent  5          Gwent.MainMenu   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Gwent     5        � 	Resources  5	       �� 
TargetType   btnTemplate ��Name �� CornerRadius �� Triggers P� Property P� Value �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect	   Color
   ShadowDepth   
BlurRadius �� 
TargetName��%�  &	        �� 5
   
   " ��6   .R�5      �� $ buttonBorder��6   $ 3��6+   #��z��5      #��z��5      #��z��5      ._�5      �� #��z��6!   $��Center=�5   )   $��Center=�5   )   5       5      P� 5       �@J�IsMouseOver . . �5      ��  �@J�Effect 5        5      $	 	BurlyWood��6/   $
 0Q�6A   $ 25Q�6Q   5      5      P� 5       � �� . . �5      �� $ buttonBorder��6    � ��
 �  ��6N   5      5      5       .��5!      �  � RowDefinitions 5"   
   �� 5#      �� 5$      �� 5%       � ColumnDefinitions 5'   
   �� 5(      .+�5*   
   �� -   5         ��Name$ btnStartGame��5*      #��[  6'   $��0��6O   $��0��6_   $��Times New Roman)�6l   $��70(�6�   
�������$��Начать игру��5+      $��30,20,30,20q�6'   ��5,      �� 5-       �� ImageSource$ Resources/Button3.jpg��6   5/      �� 51   
   -   5        $ btnChouseCard��51      #��[  6(   $��0��6P   $��1��6`   $��Times New Roman)�6m   $��70(�6�   
�������$��Выбрать карты��52      $��30,20,30,20q�6)   ��53      �� 54      $ Resources/Button2.jpg��6   56      �� 58   
   -   5        $ btnClose��58      #��[  6#   $��0��6K   $��2��6[   $��Times New Roman)�6h   $��70(�6�   
�������$��
Выйти��59      $��30,20,30,20q�6!   ��5:      �� 5;      $ Resources/Button1.jpg��6   5=      5@      5A      