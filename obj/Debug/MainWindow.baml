   M S B A M L   `   `   `  ����J  FUnrealPakHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:UnrealPakHelperUnrealPakHelper  5   	   O,clr-namespace:UnrealPakHelper.ControlLibraryUnrealPakHelper.ControlLibrary  5   	        UnrealPakHelper.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:UnrealPakHelper     5   	   GcontrolLibrary,clr-namespace:UnrealPakHelper.ControlLibrary     5   	   -   5          � Title$  UnrealPak辅助器��5	   	   $	��700��6   $	��500��6-   
������  � AllowsTransparency .  � WindowStyle$
 None=�5   	   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 30Q�6    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $( "Themes/ScrollViewerDictionary.xaml?�6%   %    5      .��5      � .+�5   
   �� ���#2b2b2b �� CornerRadius$ 25��6&   	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      
   Color$

 Gray��6#      	Direction$ 0Q�60      ShadowDepth$ 0Q�6>      
BlurRadius$ 0Q�6N      Opacity$
 0.25Q�5   #   5      � 5    
   }�.  � RowDefinitions 5!      �� 5"      $��* �6    .+�5$      � $��1��6   $��20q�6     5%      �� 5&      $
��Auto �6$   �� 5'      $��8 �6$   �� 5(      $��* �6$   �� 5)      $
��Auto �6$   .+�5+      �� $��0��6   $	��Top=�6,   $��Stretch=�6D   $
��Auto=�5,      $	��500��6D   .��5-      �� $
��Auto��6!   .+�5.      ��  �� Orientation$ 
Horizontal=�6%   .+�5/      �� $��初始设置��6$   $��24(�63   �� 50      -   5         ��Name$ Button_DefaultSet_Fold��50   %   $��Center=�51   %   $��Center=�6@   $	��⋘��6]   $��18(�6i   52      �� 53      -   5         ��Name$ StackPanel_DefaultSetArea��53   %   $ Vertical=�6H   .+�54      6   0UnrealPakHelper.ControlLibrary.SelectPathControl  -   5          Name$$ SelectPathControl_UnrealPakExe��54   ?   6   0UnrealPakHelper.ControlLibrary.SelectPathControl   SelectButtonClicked$: 4SelectPathControl_UnrealPakExe_OnSelectButtonClicked��55   ?      
SelectMode$ 
SelectFile=�56   ?      Filter$ 	EXE|*.exe��6W      TitleContent$ 选择UnrealPak.exe路径��57   ?     58      -   5        $ SelectPathControl_WorkDir��58   ?   $5 /SelectPathControl_WorkDir_OnSelectButtonClicked��59   ?   $ SelectFolder=�5:   ?   $ 选择工作目录��6Y   � 5;       � ColumnDefinitions 5<   "   �� 5=   &   $
��Auto �67   �� 5>   &   $��* �67   �� 5?   &   $
��Auto �67   .+�5A   "   �� $��密钥��6(   �� 5B   "   -   5         ��Name$ TextBox_Key��5B   :   $��1��6*   6:   5C      5D      �� 5G      $��8��6!   ���#4545456,   �� 5H      $ 
Horizontal=�6%   .+�5I      �� $��查看��6$   $��24(�61   �� 5J      -   5        $ Button_View_Fold��5J   %   $��Center=�5K   %   $��Center=�6@   $	��⋘��6]   $��18(�6i   5L      �� 5M      -   5        $ StackPanel_ViewArea��5M   %   $ Vertical=�6B   .+�5N        -	   5        $&  SelectPathControl_ViewTargetPath��5N   ?   $ 
SelectFile=�5O   ?   $ 选择目标文件��6W   �� 5P      $ Vertical=�6)   .+�5Q   "   �� $��8��6)   �� 5R   "   $ 
Horizontal=�6-   .+�5S   &   �� -
   5        $ Button_PakTest��5S   -   $��测试包体��6E   $
��Left=�6T   5T   -   5U   #   �� 5V   "   $��8��6)   �� 5W   "   $ 
Horizontal=�6-   .+�5X   &   �� -   5        $ Button_PakVerify��5X   -   $��验证包体��6G   $
��Left=�6V   5Y   -   5Z   #   �� 5[   "   $��8��6)   �� 5\   "   $ 
Horizontal=�6-   .+�5]   &   �� -   5        $ Button_PakInfo��5]   -   $��查看包体信息��6E   $
��Left=�6V   5^   -   5_   #   �� 5`   "   $��8��6)   �� 5a   "   $ 
Horizontal=�6-   .+�5b   &   �� -   5        $ Button_PakList��5b   -   $��查看包体内容��6E   $
��Left=�6V   5c   -   �� 5d   &   -   5         ��Name$% CheckBox_PakList_ExcludeDeleted��5d   /   $&��  -ExcludeDeleted 排除已删除��6X   5e   #   5f      5g      �� 5i      $��8��6!   ���#4545456,   �� 5j      $ 
Horizontal=�6%   .+�5k      �� $��打包��6$   $��24(�61   �� 5l      -   5        $ Button_Pak_Fold��5l   %   $��Center=�6^   $��Center=�6y   $	��⋘��5m   %   $��20(�61   5n      �� 5o      -   5        $ StackPanel_PakArea��5o   %   $ Vertical=�6A   .+�5p        -   5        $$ SelectPathControl_ResponseFile��5p   ?   $ SelectFolder=�5q   ?   $ 选择目标文件夹��6Y     5s      -   5        $# SelectPathControl_PakFilename��5s   ?   $ SaveFile=�5t   ?   $ 选择输出��5u   ?   �� 5v      $��8��6+   �� 5w      $ Vertical=�6)   .+�5x   "   �� $ 
Horizontal=�6-   .+�5y   &   �� -   5        $ 
Button_Pak��5y   -   $��执行打包指令��6\   �� 5z   &   $��8��6-   �� 5{   &   -   5        $ CheckBox_Pak_Compress��5{   /   $��使用Options��6N   5|   #   �� 5}   "   $ 
Horizontal=�6-   .+�5~   &   �� -   5        $ Button_RePak��5~   -   $��执行重打包命令��6`   �� 5   &   -   5        $ CheckBox_Repak_Output��5   /   $��-Output=Path��6N   �� 5�   &   -   5        $# CheckBox_RePak_ExcludeDeleted��5�   /   $��-ExcludeDeleted��6V   5�   #   5�      5�      �� 5�      $��8��6!   ���#4545456,   �� 5�      $ 
Horizontal=�6%   .+�5�      �� $��解包��6$   $��24(�61   �� 5�      -   5        $ Button_Extract_Fold��5�   %   $��Center=�6f   $��Center=�5�   %   $	��⋘��6B   $��20(�6N   5�      �� 5�      -   5        $ StackPanel_ExtractArea��5�   %   .+�5�        -   5        $) #SelectPathControl_ExtractTargetFile��5�   ?   $ 
SelectFile=�6l   $ 选择目标文件��6�     5�      -   5        $" SelectPathControl_ExtractDir��5�   ?   $ SelectFolder=�6e   $ 选择输出文件夹��6   �� 5�      $ 
Horizontal=�6)   .+�5�   "   �� -   5        $ Button_Extract��5�   )   $��执行提取命令��6`   $
��Left=�6q   5�      �� 5�      $ 
Horizontal=�6)   .+�5�   "   �� -   5        $ Button_MountPoint��5�   )   $��修改挂载点��6D   6T   �� 5�   "   $��8��6/   
������ 69   �� 5�   "   -   5        $ TextBox_MountPoint��5�   *   $	��300��6F   $��5q�6U   5�      5�      5�      5�      �� 5�      $��1��6   $��Stretch=�6,   
�������6J   �� 5�      -   5         ��Name$ ScrollViewer_Log��5�   U   $��2��6   
���� K�$��2q�6A   .��5�      �� -    5        $ TextBox_Log��5�       �� IsUndoEnabled .   �� 
IsReadOnly . 
���   �
�������5�      5�      �� 5�      $��3��6   .+�5�      �� $��8��6#   $��Stretch=�6.   ���#2b2b2b6L   � 5�       5�      �� 5�      $��* �6/   �� 5�      $
��Auto �6/   .+�5�      �� $ 
Horizontal=�6%   .+�5�      �� -!   5        $ Button_ClearLog��5�   %   $��清除日志��6^   $��Bottom=�5�   %   $��Center=�5�   %   $	��120��6B   $��30��6N   �� 5�      -"   5        $ Button_SaveLog��5�   %   $��保存日志��6\   $��Bottom=�5�   %   $��Center=�5�   %   $	��120��6B   $��30��6N   5�      �� 5�      $��1��6%   $ 
Horizontal=�65   $��Right=�6N   .+�5�      �� -#   5        $ Button_Minimized��5�   %   $��	最小化��6?   $��Bottom=�6M   $��Center=�5�   %   $	��120��6B   $��30��6N   5�   %   �� 5�      $��8��6+   $��Stretch=�65   ���#2b2b2b6S   �� 5�      -$   5        $ Button_Maximized��5�   %   $��	最小化��6?   $��Bottom=�6M   $��Center=�5�   %   $	��120��6B   $��30��6N   �� 5�      $��8��6+   $��Stretch=�65   ���#2b2b2b6S   �� 5�      -%   5        $ Button_CloseWindow��5�   %   $��退出��6A   $��Bottom=�6N   $��Center=�5�   %   $	��120��6B   $��30��6N   5�   %   5�      5�      5�      5�      5�      5�      5�      