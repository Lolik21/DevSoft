   M S B A M L   `   `   `  ���� @  <Gwent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GwentGwent      Gwent.MainMenu   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     %localclr-namespace:Gwent       � 	Resources   �� 
TargetType   btnTemplate ��Name �� CornerRadius �� Triggers P� Property P� Value �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect	   Color
   ShadowDepth   
BlurRadius �� 
TargetName��%  &	        �� " ��.R��� $ buttonBorder��$ 3��#��z��#��z��#��z��._��� #��z��$��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  �@J�Effect   $	 	BurlyWood��$
 0Q�$ 25Q�P�  � �� . . ��� $ buttonBorder�� � ��
 �  ��.���  � RowDefinitions �� �� ��  � ColumnDefinitions �� .+��� -    ��Name$ btnStartGame��#��[  $��0��$��0��$��Times New Roman)�$��70(�
�������$��Начать игру��$��30,20,30,20q�����  �� ImageSource$ Resources/Button3.jpg���� -   $ btnChouseCard��#��[  $��0��$��1��$��Times New Roman)�$��70(�
�������$��Выбрать карты��$��30,20,30,20q����� $ Resources/Button2.jpg���� -   $ btnClose��#��[  $��0��$��2��$��Times New Roman)�$��70(�
�������$��
Выйти��$��30,20,30,20q����� $ Resources/Button1.jpg��