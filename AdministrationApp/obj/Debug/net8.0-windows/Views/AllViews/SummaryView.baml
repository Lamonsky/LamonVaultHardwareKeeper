   M S B A M L   `   `   `  ����L  HAdministrationApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdministrationApp.ViewsAdministrationApp.Views  5      2    ,AdministrationApp.Views.AllViews.SummaryView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:AdministrationApp.Views     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��0��6   $��1��6   $
��auto��6.   $
��auto��6=   ���	#FF49A0EC��6J   �� *ShowComputersCommand+.��5      �� .+�5      �� $��24(�6   $	��Top=�6*   $
��Left=�6B   $��Komputery: ��6]   �� 5      $��24(�6   $��Bottom=�6*   $��Right=�6E   ��6a   �� *ComputerCount+5      5      �� 5   
   $��0��6   $��2��6   $
��auto��6.   $
��auto��6=   ���	#FF49A0EC��6J   �� *ShowMonitorCommand+.��5       �� .+�5!      �� $��24(�6   $	��Top=�6*   $
��Left=�6B   $��
Monitory: ��6]   �� 5"      $��24(�6   $��Bottom=�6*   $��Right=�6E   ��6a   �� *monitorCount+5#      5$      �� 5%   
   $��1��6   $��1��6   $
��auto��6.   $
��auto��6=   ���	#FF49A0EC��6J   �� *ShowLocationCommand+.��5'      �� .+�5(      �� $��24(�6   $	��Top=�6*   $
��Left=�6B   $��Lokalizacje: ��6]   �� 5)      $��24(�6   $��Bottom=�6*   $��Right=�6E   ��6a   �� *locationCount+5*      5+      �� 5,   
   $��1��6   $��2��6   $
��auto��6.   $
��auto��6=   ���	#FF49A0EC��6J   �� *ShowUserCommand+.��5.      �� .+�5/      �� $��24(�6   $	��Top=�6*   $
��Left=�6B   $��Użytkownicy: ��6]   �� 50      $��24(�6   $��Bottom=�6*   $��Right=�6E   ��6a   �� *
usersCount+51      52      53      54      