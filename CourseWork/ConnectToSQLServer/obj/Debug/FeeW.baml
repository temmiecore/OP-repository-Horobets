   M S B A M L   `   `   `  ����M  IConnectToSQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ConnectToSQLServerConnectToSQLServer  5   	       ConnectToSQLServer.FeeW   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ConnectToSQLServer     5   	     � Title$.  (Вікно підрахунку чека��5   	   $	��210��6'   $	��400��64   #��� ��6@    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $
��1.1* �6   �� 5      .+�5   
   �� -   5         ��Name$ IDBox��5      $��0��6!   $
��Left=�6.   $��23��6I    �� TextWrapping$
 Wrap=�6U   $��Times New Roman)�6i   $��16(�6�   $��Center=�6�   $��40��6�   $��37,0,0,0q�6�   �� 5   
   $��0��6   $
��ID: ��6   $
��Left=�6,   $��Center=�6G   $��Times New Roman)�6b   $��16(�6   $��5,0,0,0q�6�   �� 5   
   -   5         ��Name$ FeeLabel��5      $��3��6"   $��1��63   $��$000000��6C   $��Center=�6U   $��Times New Roman)�6r   $��16(�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ 	OneAttBtn��5      $��0��6$   $��Адвокат��62   $��Right=�6D   $��Center=�6`   $��Times New Roman)�6{   $��16(�6�   $��27��6�   $	��100��6�   $��0,0,13,0q�6�   6�   �� 5   
   -   5        $ 	AllAttBtn��5      $��1��6$   $0��*Усі незавершені справи��62   $��Center=�6S   $��Times New Roman)�6p   $��16(�6�   $��Center=�6�   $	��180��6�   $��30��6�   6�   �� 5   
   -   5        $ AllCasesBtn��5      $��2��6&   $(��"Усі справи загалом��64   $��Center=�6Q   $��Times New Roman)�6n   $��16(�6�   $��Center=�6�   $��30��6�   $	��150��6�   6�   �� 5   
   -   5        $ ExitBtn��5      $��3��6"   $��2��6/   $!��Попереднє меню��6D   $��Center=�6]   $��Times New Roman)�6z   $��16(�6�   $��Center=�6�   $��35��6�   $	��220��6�   6�   �� 5   
   $��3��6   $��1��6%   $
��Left=�65   $��55��6P   
���   �$	��Top=�6k   $	��130��6�   $��	29,36,0,0q�6�   �� 5   
   $��3��6   $��1��6!   $��Чек на оплату:��61   $��Center=�6J   $��Times New Roman)�6g   $��16(�6�   $��Center=�6�   $��	0,-3,0,86q�6�   �� 5   
   -   5        $ PrintBtn��5      $��2��6#   $��1��60   $��Роздрукувати��6@   $��Center=�6W   $��Times New Roman)�6t   $��16(�6�   $��Center=�6�   $��27��6�   $	��120��6�   $��28.2,6.2,28.2,6q�6�   �� 5   
   $��3��6   $
��Left=�6%   $	��135��6@   
���   �$	��Top=�6\   $	��188��6t   $��206,0,-0.4,0q�6�   $��2��6�   5       5!      