   M S B A M L   `   `   `  ����L  HAdministrationApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:AdministrationApp.ViewModels.AllViewModel)AdministrationApp.ViewModels.AllViewModel  5   	   W0clr-namespace:AdministrationApp.Views.AllWindows"AdministrationApp.Views.AllWindows  5   	   7    1AdministrationApp.Views.AllWindows.AllUsersWindow   5   	   ?vma7clr-namespace:AdministrationApp.ViewModels.AllViewModel  W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Blocal0clr-namespace:AdministrationApp.Views.AllWindows     5   	   $	��450��5
   	   $	��800��6   .��5   
   &   System.Windows.Controls.DataGrid      AutoGenerateColumns$  False��60      
IsReadOnly$
 True��6�   ��6   �� *List+ �� SelectedItem 6L   ��  �� Path$ 
ChosenUser� �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� $ Id�  5      $ Nazwa Użytkownika��6%    6@   �� $ 	Usersname�  5      $ Imię��6%    63   �� $ 	FirstName�  5      $ Nazwisko��6%    67   �� $ LastName�  5      $ Lokalizacja��6%    6:   �� $ Location�  5      $ Email��6%    64   �� $ Email�  5      $ Numer telefonu��6%    6=   �� $ Phone1�  5      $ Numer telefonu 2��6%    6?   �� $ Phone2�  5      $ Numer wewnętrzny��6%    6?   �� $ InternalNumber�  5      $ 
Stanowisko��6%    69   �� $ Position�5      5      