   M S B A M L   `   `   `  ����H  DKP_Balashova_, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:KP_Balashova_.PageMainKP_Balashova_.PageMain  5      '    !KP_Balashova_.PageMain.PageOplata   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:KP_Balashova_.PageMain     5        N� Title$  
PageOplata��5	      .��5      � ���	#FFF5F5F5 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   
������� �� Padding$ 12q�61   ._�5      �� $��Оплата заказа��6   $��18(�5      $
��Bold"�5      $��Center=�5      5      �� 5   
   $��1��6   $��20q�6"   .+�5      �� -   5         ��Name$ QrCodeImage��5      $	��250��5      $	��250��5      $��Uniform=�5       $��Center=�5!      $��0,0,0,20q�5"      $y�	Collapsed=�5#      �� 5&      -   5         ��Name$ CardPaymentText��5&      $9��3Приложите карту к терминалу��5'      $��18(�5(      $
��Bold"�5)      $��Center=�5*      $y�	Collapsed=�5+      $��0,0,0,20q�5,      �� 5/      -   5        $ 
AmountText��5/      $��16(�51      $��Center=�52      ��50      �� *TotalAmount+ �� StringFormat$ К оплате: {0} ₽��52      �� 55      -   5        $ SuccessText��55      $/��)Оплата прошла успешно!��56      $��18(�57      $
��Bold"�58      
��� � �$��Center=�5:      $y�	Collapsed=�5;      $��0,20,0,0q�5<      5=      �� 5@   
   -   5        $��2��5@      $#��Отменить оплату��5A         ButtonY#��[  5C      $��	0,20,0,20q�5D      $��Center=�5E      $	��108��5F      5G      5H      