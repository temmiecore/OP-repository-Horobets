   M S B A M L   `   `   `  ����M  IConnectToSQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ConnectToSQLServerConnectToSQLServer  5   	   #    ConnectToSQLServer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:ConnectToSQLServer     5   	     � Title$  
MainWindow��5   	   $	��200��6   $	��760��6)    � WindowStartupLocation$ CenterScreen=�66    � 
ResizeMode$ NoResize=�6[   .��5	      � .+�5
   
   �� -   5         ��Name$ 	AdminMode��5
      $-��'Режим Адміністратора��6$   $
��Left=�5      $��	25,63,0,0q�6   $	��Top=�5      $	��232��6   $��38��6%   $��18(�61   $��Italic$�5      $��Times New Roman)�6,   �� 5   
   $����Розмежування повноважень користувачів на основі парольної автентифікації��6   $
��Left=�5      $��	10,10,0,0q�5      $	��Top=�6   $��41.533��6,   $��730.4��5      $��Book Antiqua)�6   $��18(�6)   $
��Bold"�5       � IsSharedSizeScope . $��Center=�5      $��Center=�5       � UseLayoutRounding .  6#   �� 5   
   -   5        $ UserMode��5      $'��!Режим Користувача��6#   $
��Left=�5      $��
283,63,0,0q�6   $	��Top=�5      $	��247��6   $��38��6%   $��18(�61   $��Italic$�5      $��Times New Roman)�6   5      �� 5   
   -   5        $ UserMode_Copy��5      $#��Закрити додаток��6(   $
��Left=�5      $��
25,120,0,0q�6   $	��Top=�5       $	��704��6   $��24��6%   $��18(�61   $��Italic$�5!      $��Times New Roman)�6   5"      �� 5$   
   -   5        $ AboutDev��5$      $!��Про розробника��6#   $
��Left=�5%      $��
556,63,0,0q�6   $	��Top=�5&      $	��173��6   $��38��6%   $��18(�61   $��Italic$�5'      $��Times New Roman)�6   5(      5)      5*      