   M S B A M L   `   `   `  ����H  DKP_Balashova_, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:KP_Balashova_.PageMainKP_Balashova_.PageMain  5      %    KP_Balashova_.PageMain.PageMenu   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:KP_Balashova_.PageMain     5      -   5          N� Title$  PageMenu��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��1 �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��1 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ DtGridTovar��5         AutoGenerateColumns .     
IsReadOnly . $��0��6V   $��2��6f      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Наименование��6%      FontSize$ 20Q�6Z   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 20* 6h   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *
DishesName+6v     5      $ Ингредиенты��6%   $ 20Q�6Z   $		 20* 6h   
 6:   �� *Ingredients+   ElementStyle 5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value$
 Wrap=�69   5      5        5      $ Цена��6%   $ 31Q�6M   $	 5* 6[   
 63   �� *Price+6[     5      $ Каллории��6%   $ 31Q�6T   $	 5* 6b   
 67   �� *
Calories+6b   4  .System.Windows.Controls.DataGridTemplateColumn  5       $ Изображение��6)   $		 25* 6>      CellTemplate 5!      �� 5"      .R�5#      �� $
��Fill=�6D   $	��200��6S   $	��200��6_   ��6$   �� *ProductImage+6_   5$      5&        5'      $
	 auto 6)    5(      �� 5)      .R�5*      �� $��BtnDb��68   -   5        $��Добавить��5*   %      ButtonT#��[  6Z   5+      5-      5/      51      52      