   M S B A M L   `   `   `  ����H  DYWDE_Launcher, Version=1.2.3.1, Culture=neutral, PublicKeyToken=null-clr-namespace:YWDE_LauncherYWDE_Launcher  5   	       YWDE_Launcher.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:YWDE_Launcher     5   	     � Title$  YWDE Launcher��5   	   $	��520��6   $	��890��6,    � WindowStartupLocation$ CenterScreen=�68    � Icon$ YWDE_LauncherIcon.ico��6]   .��5	      � -   5         �Name$ LauncherWindow��5	       � ColumnDefinitions 5
   
   �� 5      $
��173* �6   �� 5      $
��371* �6   �� 5      $
��339* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ Black��6    � Offset$ 1Q�6-   � 5      $ 	#FF3C3C3C��6   5      .+�5   
   �� -   5        	 ��Name$	 	NewsLabel��5      $
��News��6#   $��10,0,0,0q�62   
 J� RenderTransformOrigin$
 -1.612,-1.238&�6D   $
��Bold"�6j   $��24(�6|   ���	#FF09B800$��38��6�   $	��Top=�6�   $
��Left=�6�   $��75��6�   ��6�   Z� ��5      �� 5      $ 0.5,1&�6&   $ 0.5,0&�67   .��5      � $ Black��6#   $ 0Q�61   � 5      $ White��6#   $ 1Q�61   5      5      �� 5   
   -   5        $	 ClientVersionLabel��5      $��Client Version:��6,   $��Italic$�6F   ���	#FF09B800$��26��6p   $��Bottom=�6|   $
��Left=�6�   $��85��6�   �� 5   
   -   5        $	 LauncherVersionLabel��5      $��Launcher Version:��6.   $��0,0,0,18q�6J   $��Italic$�6\   ���	#FF09B800$��26��6�   $��Bottom=�6�   $
��Left=�6�   $	��102��6�   (  "System.Windows.Controls.WebBrowser  5   
   -   5          Name$ 	Newsource��5      $��10,38,10,44q�6(      Source$5 /https://zhakalendk.github.io/Launcher_Page.html?�6=   $��3��6v   �� 5    
   -   5         ��Name$ LaunchButton��5       $��Launch��6'   $��	0,0,10,10q�68   $
��Bold"�6}   $��14(�6�   ���	#FF168F10$	��0,1q�6�   $
 1.003,0.332&�6�   $��25��6�   $��Bottom=�6�   $��Right=�6  $	��117��61  $��2��6=  ��6K   Z� ��5!      
� 5"      .��5#      � $ 	#FF383838��6#   $ 1Q�65   � 5$      $ 	#FF005B08��6#   $ 0.598Q�65   5%       J� Effect 5'      3  -System.Windows.Media.Effects.DropShadowEffect  5(         	Direction$ -2Q�6#      Opacity$ 0Q�62   5*      �� 5+   
   -   5        $ LaunchWebsiteButton��5+      $��Website��6.   $��0,8,10,0q�6@   $
��Bold"�6�   $��14(�6�   $	��0,1q�6�   $
 1.003,0.332&�6�   $��25��6�   $	��Top=�6�   $��Right=�6	  $	��117��6%  $��2��61  ��6R   Z� ��5,      �� 5-       �� Color$ 	#FF08B000��6"   ��5/      
� 50      .��51      � $ 	#FF383838��6#   $ 1Q�65   � 52      $ 	#FF005B08��6#   $ 0.598Q�65   53       55        56      $ -2Q�6#   $ 0Q�62   58      �� 59   
   -   5         ��Name$ closeOnLaunch��59      $��Close on Launch��6*   $��
103,0,0,14q�6D   ���	#FF09B800$��16��6o   $��Bottom=�6{   $
��Left=�6�   $	��103��6�   $��Italic$�6�   $��2��6�   �� 5:   
   -	   5        $	 LauncherVersion��5:      $��1.0.0.0��6)   $
��Left=�6;   $��
102,0,0,19q�6V   $��Bottom=�6j   ���	#FFFFB900$��10(�6�   �� 5;   
   -
   5        $	 ClientVersion��5;      $��1.4.1.0��6'   $
��Left=�69   $��85,0,0,1q�6T   $��Bottom=�6f   ���	#FFFFB900$��10(�6�   5=      5>      