   M S B A M L   `   `   `  ���� @  <Gwent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GwentGwent      Gwent.ChouseCard   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     %localclr-namespace:Gwent       �� BitmapScalingMode$  HighQuality=� � 	Resources  �� 
TargetType   btnTemplate ��Name �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName	 �� Property
 �� Value��%�   &	        �� " ��.R��� $ buttonBorder��$ 3��#��z��#��z��#��z��._��� #��z��$��Center=�$��Center=� P�  � �� . . ��� $ buttonBorder��	 � ��

 �  ��.��� -    �Name$ grdMainGrid��.+�� -   $ grdChouseCard�� � RowDefinitions �� $��1* ��� $	��10* � � ColumnDefinitions �� $��2* ��� $��1* ��� $��2* �.+��� $��1�����#101010
���   �$��1q�$��3q�._��� -    ��Name$ scrvAllCards��$��1��$
��Auto=�.��� -   $ grdAllCards��$��10q�$	��Top=� �� �� �� �� $��1��$��2�����#101010
���   �$��1q�$��3q�._��� -   $ scrvUserCards��$
��Auto=�.��� -   $ grdUserCards��$	��Top=�$��10q� �� �� �� � -   $ 	grdMeadle��$��1��$��1�� �� �� �� .+��� -    ��Name$ lblCardsCount��$��1��
���  ��$��Arial)�$��50(�$��Center=�$	��Top=�$�� ���� -	   $ lblCurrCardCount��$��1��
�������$��Arial)�$��50(�$��Center=�$��Bottom=�$��0���� -
    ��Name$ 	btnToMenu��$��2��$��Bottom=�$��3,3,3,3q�$��30��$��В меню��$��18(�
���   ����	#FF8F8F8F� -   $ grdCouseFraction��$��1�� �� $��1* ��� $��2* ��� $��1* �.+��� -   $ lblFraction��$��20(�$��Center=�
�������$��Center=�$�� ��$��3���� -   $ btnNextFraction��#��[  $��2��$��0��$��Right=�$��20��$��50��$��3,3,3,3q�$��0q�����  �� ImageSource$ Resources/35.png���� -   $ btnPrevFraction��#��[  $��0��$��0��$
��left=�$��20��$��50��$��3,3,3,3q�$��0q����� $ Resources/34.png��� -   $ grdCard��