   M S B A M L   `   `   `  ����M  IConnectToSQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ConnectToSQLServerConnectToSQLServer  5   	   $    ConnectToSQLServer.AttHistoryW   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ConnectToSQLServer     5   	     � Title$0  *Історія справ адвоката��5   	   $	��250��6(   $	��600��65   #��� ��6A    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�6�   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��0.40* �6   �� 5       � RowDefinitions 5   
   �� 5      $
��1.1* �6   �� 5      �� 5      �� 5      $
��0.8* �6   �� 5      $
��0.8* �6   �� 5      $
��0.8* �6   .+�5   
   �� -   5         ��Name$ PickBtn��5      $��Center=�6"   $��Center=�6?   $��2��6[   $%��Вибрати адвоката��6h   $	��130��6�   $��30��6�   $��0,0,0,-8q�6�   $��Times New Roman)�6�   $��16(�6�   6�   �� 5   
   -   5        $ ExitBtn��5      $��Center=�6"   $��Center=�6?   $��5��6[   $!��Попереднє меню��6h   $	��150��6�   $��35��6�   $��Times New Roman)�6�   $��16(�6�   $��	0,-26,0,0q�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ MainGrid��5      $��1��6%   $��6��65   $	��213��6F   $	��419��6S   �� 5   
   $��0��6   $-��'Виберіть адвоката (ID):��6   $��Center=�6?   $��Center=�6\   $��Times New Roman)�6w   $��15(�6�   $��	0,0,0,-12q�6�   �� 5   
   -   5         ��Name$ IDBox��5      $��Center=�6!   $��1��6>   $��30��6L    �� TextWrapping$
 Wrap=�6X   $�� ��6l   $��Center=�6t   $��80��6�   $��Times New Roman)�6�   $��17(�6�   5      5      