   M S B A M L   `   `   `  ����F  BExcelToFile, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ExcelToFileExcelToFile  5   	       ExcelToFile.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ExcelToFile     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��525��6)   .��5	      � $��	0,0,0,-88q�6   $	��457��6   $	��511��6+   .+�5
   
   �� -   5         ��Name$ btnSelectFile��5
      $��Carica uno o più file��6(   $
��Left=�6H   $��10,5,0,0q�6c   $	��Top=�6u   $	��175��6�   ���	#FFEEEEEE���	#FF333333$��48��6�   �� 5   
   -   5         ��Name$ txtBoxSelectDestination��5      $��Stretch=�63   $��30��6Q   $��10,216,10,0q�6]   $��5q�6r   $	��Top=�6~   ���	#FFABADB3
������ {�.  6�   �� 5   
   -   5        $ btnSelectDestination��5      $��Seleziona destinazione��6/   $
��Left=�6P   $��
10,163,0,0q�6k   $	��Top=�6   $	��175��6�   ���	#FFEEEEEE���	#FF333333$��48��6�   �� 5   
   -   5        $ btnStartProcess��5      $��Processa��6*   $��Center=�6=   $��166,261,170,0q�6Z   $	��Top=�6q   $	��175��6�   $��48��6�   ���	#FFEEEEEE���	#FF3333336�   �� 5   
   -   5         ��Name$ lstSelectedFiles��5      $��Stretch=�6-   $	��100��6K   $��
10,58,10,0q�6X   $	��Top=�6l   ��5      �� 5      .R�5      �� ��6   �� *
FileName+5      5      �� 5   
   -   5         ��Name$ label��5      $��Import Listini��6   $
��Left=�68   $��190,-92,0,0q�6S   $	��Top=�6h   $��30(�6�   ���	#FF006AA46�   �� 5   
   -   5        $ lstLog��5      $��Stretch=�6#   $	��133��6A   $��10,314,10,0q�6N   $	��Top=�6c   
�������5      �� 5   
   -   5         ��Name$ image��5      $
��Left=�6   $��74��6:   $��
10,-92,0,0q�6F   $	��Top=�6Z   $	��175��6r   $��/Image/ITNEXT.png��6~   5      5      