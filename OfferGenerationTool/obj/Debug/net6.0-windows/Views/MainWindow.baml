   M S B A M L   `   `   `  ����N  JOfferGenerationTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:OfferGenerationToolOfferGenerationTool  5   	   $    OfferGenerationTool.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:OfferGenerationTool     5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	     � Title$  
MainWindow��5
   	   $	��300��6   $	��800��6)    � DataContext 5   	   ��  �� RelativeSource �� *Self+.��5      � .+�5   
   �� $��Выберите дату��6   $
��Left=�6(   $��
107,25,0,0q�6C   $	��Top=�6W   $��16(�6o   $
��Bold"�6}   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
datePicker��5      $
��Left=�6O   $��	90,82,0,0q�6j   $	��Top=�6}   $	��158��6�   $��16(�6�      SelectedDate 6)   �� *SelectedDate+6�   �� 5   
   -   5         ��Name$ 
sendButton��5      $��Отправить��6s   $
��Left=�6�   $��113,154,0,0q�6�   $	��Top=�6�   $��16(�6�    J� RenderTransformOrigin$ 0.499,0.489&�6�   ��6%   �� *SendDateCommand+��6I   �� *SelectedDate+6�   �� 5   
   -   5         ��Name$ textBox��5      $
��Left=�6<   $��
322,28,0,0q�6W    �� TextWrapping$
 Wrap=�6k   $	��Top=�6   $	��422��6�   $	��189��6�   $��16(�6�   ��6#   �� *	TextBox+6�   5      5      