   M S B A M L   `   `   `  ����M  IConnectToSQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ConnectToSQLServerConnectToSQLServer  5   	       ConnectToSQLServer.PrintW   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ConnectToSQLServer     5   	     � Title$  Друк данних��5   	   $	��300��6   $	��600��6*   #��� ��66    � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�6�   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��0.40* �6   �� 5       � RowDefinitions 5   
   �� 5      $
��1.1* �6   �� 5      �� 5      �� 5      $
��0.8* �6   �� 5      $
��0.8* �6   �� 5      $
��0.8* �6   .+�5   
   �� -   5         ��Name$ 
PrintItems��5      $��1��6(   $��Center=�65   $��Center=�6R   $	��140��6m   $��25��6y   $��	0,-25,0,0q�6�   $��Times New Roman)�6�   $��14(�6�    �� MaxDropDownHeight$	 289��6�   .@�5      �� $��Адвокати��6   $��14(�6.   �� 5      $��Клієнти��6   $��14(�6-   �� 5      $��Журнал��6   $��14(�6,   �� 5      $��Запит №1��6   $��14(�6.   �� 5      $��Запит №2��6   $��14(�6.   �� 5      $��Запит №3��6   $��14(�6.   �� 5      $��Запит №4��6   $��14(�6.   �� 5      $��Запит №5��6   $��14(�6.   �� 5       $��Запит №6��6   $��14(�6.   5!      �� 5"   
   -   5         ��Name$ PickBtn��5"      $��Center=�6"   $��Center=�6?   $��3��6[   $��Вибрати запит��6h   $	��130��6�   $��25��6�   $��0,0,0,0q�6�   $��Times New Roman)�6�   $��16(�6�   6�   �� 5#   
   -   5        $ PrintBtn��5#      $��Center=�6#   $��Center=�6@   $��4��6\   $��Роздрукувати!��6i   $	��140��6�   $��25��6�   $��Times New Roman)�6�   $��16(�6�   6�   �� 5$   
   -   5        $ ExitBtn��5$      $��Center=�6"   $��Center=�6?   $��5��6[   $!��Попереднє меню��6h   $	��150��6�   $��25��6�   $��Times New Roman)�6�   $��16(�6�   6�   &   System.Windows.Controls.DataGrid  5%   
   -   5          Name$ MainGrid��5%      $��1��6%   $��6��65   $	��267��6F   $	��419��6S   �� 5&   
   $��0��6   $F��@Виберіть таблицю для 
             друку:��6   $��Center=�6U   $��Center=�6r   $��Times New Roman)�6�   $��15(�6�   5'      5(      