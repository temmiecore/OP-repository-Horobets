   M S B A M L   `   `   `  ����M  IConnectToSQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ConnectToSQLServerConnectToSQLServer  5   	   &     ConnectToSQLServer.UserConfigWin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ConnectToSQLServer     5   	   -   5          � Title$  UserConfigWin��5   	   $	��300��6   $	��250��6,    � WindowStartupLocation$ CenterScreen=�68    � 
ResizeMode$ NoResize=�6]   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.7* �6   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $4��.Оновити дані користувача��6   $��Center=�6@   $��Center=�6]   $��Times New Roman)�6x   $��18(�6�   �� 5   
   $��1��6   $��Ім'я:��6   $
��Left=�6-   $��Center=�6H   $��Times New Roman)�6c   $��15(�6�   �� 5   
   $��2��6   $��Прізвище:��6   $
��Left=�61   $��Center=�6L   $��Times New Roman)�6g   $��15(�6�   �� 5   
   $��3��6   $��Новий пароль:��6   $
��Left=�65   $��Center=�6P   $��Times New Roman)�6k   $��14(�6�   �� 5   
   $��4��6   $&�� Повторіть пароль:��6   $
��Left=�69   $��Center=�6T   $��Times New Roman)�6o   $��14(�6�   �� 5   
   -   5         ��Name$
 Name��5      $��Center=�6    $��23��6=    �� TextWrapping$
 Wrap=�6I   $��Times New Roman)�6]   $��1��6z   $��Center=�6�   $	��120��6�   $��65,0,0,0q�6�   �� 5   
   -   5        $ Surname��5      $��Center=�6#   $��23��6@   $
 Wrap=�6L   $��Times New Roman)�6`   $��2��6}   $��Center=�6�   $	��120��6�   $��80,0,0,0q�6�   �� 5   
   -   5         ��Name$ 	UpdateBtn��5      $��5��6$   $��Оновити дані��61   $��Center=�6H   $��Center=�6e   $	��130��6�   $��30��6�   $��Times New Roman)�6�   $��16(�6�   6�   G� 5   
   -   5         G�Name$ NewPass��5      $��Center=�6'   $��23��6D   $��3��6P   $��Center=�6]   $	��120��6x   $��95,0,0,0q�6�   G� 5   
   -   5        $ NewPassCheck��5      $��Center=�6,   $��23��6I   $��4��6U   $��Center=�6b   $	��120��6}   $��	115,0,0,0q�6�   5      5      