   M S B A M L   `   `   `  ���� @  <Gwent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GwentGwent      Gwent.Battlefield   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     %localclr-namespace:Gwent     .��� .+�� -     �Name$  grdPlayGround�� � ColumnDefinitions �� $
��200* ��� $��1000* ��� $
��200* �.+�� $��1�� � RowDefinitions �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1.25* �.+��� -    ��Name$ brdLine6��$��0�����#101010
���   �$��1q�$��3q�._�� -   $  grdLine6��$��1q�$��Center=��� -   $ brdLine5��$��1�����#101010
���   �$��1q�$��3q�._�� -   $  grdLine5��$��1q�$��Center=��� -   $ brdLine4��$��2�����#101010
���   �$��1q�$��3q�._�� -   $  grdLine4��$��1q�$��Center=��� -   $ brdLine1��$��3�����#101010
���   �$��1q�$��3q�._�� -	   $  grdLine1��$��1q�$��Center=��� -
   $ brdLine2��$��4�����#101010
���   �$��1q�$��3q�._�� -   $  grdLine2��$��1q�$��Center=��� -   $ brdLine3��$��5�����#101010
���   �$��1q�$��3q�._�� -   $  grdLine3��$��1q�$��Center=��� $��6�����#101010
���   �$��1q�$��3q�._�� -   $  grdInHandCards��$��3q�$��Center=��  �� $
��124* ��� $
��175* ��� $	��98* ��� $
��199* ��� $
��124* �.+��� $��2�����#101010
���   �$��1q�$��	30,3,30,3q�._�� -   $  
grdWeather��$��Center=�$��1q��� -    ��Name$ lblUserCardsPower��$��4��
�������$��0��$��Center=�$��Center=�$��60(�$��Times New Roman)��� -   $ lblOponentCardsPower��$��0��
�������$��0��$��Center=�$��Center=�$��60(�$��Times New Roman)��� -   $ lblOponentInHandCardCount��$��1��
�������$��0��$��Bottom=�$��Center=�$��50(�$��Times New Roman)��� -   $ lblInHandCardCount��$��3��
�������$��0��$	��Top=�$��Center=�$��50(�$��Times New Roman)��� -   $ lblOponentPassed��$��1��
�������$��Пасс��$	��Top=�$��Center=�$��50(�$��Times New Roman)�$y�Hidden=��� -   $ 	lblPassed��$��3��
�������$��Пасс��$��Bottom=�$��Center=�$��50(�$��Times New Roman)�$y�Hidden=�� $��2�� �� �� �� �� �� .+�� $��4��$��25,10,25,10q�.+��� -    ��Name$ imgUserStack��$
��Fill=�$��bin/Debug/Cards/North.png���� -   $ lblInDeckCards��$��Bottom=�$��Center=�$��0��$��24(�
��������� $��3�����#101010
���   �$��1q�$��25,10,25,10q�._�� -   $  grdUserUsedCards���� $��1�����#101010
���   �$��1q�$��25,10,25,10q�._�� -   $  grdOponentUsedCards��� $��25,10,25,10q�$��0��.+��� -   $ imgOponentStack��$
��Fill=�$��bin/Debug/Cards/North.png���� -   $ lblOponentInDeckCards��$��Bottom=�$��Center=�$��0��$��24(�
�������� $��2�� �� �� .+��� -    ��Name$ btnPassGame��$��0��$��15q�$��35��$��Пасс��$��30(�$��Times New Roman)��� -   $ btnLeaveGame��$��1��$��15q�$��35��$��
Выйти��$��30(�$��Times New Roman)�� -   $  grdNotification��.+��� -    ��Name$ tbNotification��$��Center=�$��Center=�$��60(�
�������$�� ��$	��900��$
��Wrap=� �� TextAlignment$ Center=�