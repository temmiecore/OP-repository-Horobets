   M S B A M L   `   `   `  ����M  IConnectToSQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ConnectToSQLServerConnectToSQLServer  5   	       ConnectToSQLServer.CellW   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ConnectToSQLServer     5   	     � Title$'  !Однаковість камер��5   	   $	��300��6#   $	��350��60   #��� ��6<    � WindowStartupLocation$ CenterScreen=�6�    � 
ResizeMode$ NoResize=�6�   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5         ��Name$ IDBox1��5      $��Right=�6"   $��0��6>   $��23��6K    �� TextWrapping$
 Wrap=�6W   $��Center=�6l   $��70��6�   $��Times New Roman)�6�   $��16(�6�   $��0,0,60,0q�6�   �� 5   
   -   5        $ IDBox2��5      $��Right=�6"   $��1��6>   $��23��6K   $
 Wrap=�6W   $��Center=�6l   $��70��6�   $��Times New Roman)�6�   $��16(�6�   $��0,0,60,0q�6�   �� 5   
   $$��ID першого в'язня:��6   $��0��6-   $
��Left=�6:   $��Center=�6U   $��40,0,0,0q�6p   $��Times New Roman)�6�   $��19(�6�   �� 5   
   $$��ID другого в'язня:��6   $��1��6-   $
��Left=�6:   $��Center=�6U   $��40,0,0,0q�6p   $��Times New Roman)�6�   $��19(�6�   �� 5   
   -   5         ��Name$ 
CheckLabel��5      $�� ��6$   $��3��6/   $��Center=�6<   $��Center=�6Y   $��Times New Roman)�6t   $��17(�6�   $
��Bold"�6�   
���   �6�   �� 5   
   -   5         ��Name$ CheckBtn��5      $'��!Перевірити камери��6#   $��2��6?   $��Center=�6L   $��Center=�6i   $��30��6�   $	��200��6�   $��Times New Roman)�6�   $��16(�6�   6�   �� 5   
   -   5        $ ExitBtn��5      $$��У попереднє меню��6"   $��4��6=   $��Center=�6J   $��Center=�6g   $��35��6�   $	��250��6�   $��Times New Roman)�6�   $��16(�6�   $��0q�6�   6�   �� 5   
   $��3��6   $��Center=�6!   $��55��6>   $��2��6J   
���   �$	��Top=�6j   $	��360��6�   5      5      