   M S B A M L   `   `   `  ����A  =Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Player.ViewPlayer.View  5      clr-namespace:PlayerPlayer  5          Player.View.PlayListView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:Player.View     5      %listclr-namespace:Player     5      .��5
      � $��
0,0,-129,0q�6   .+�5   
      Player.CustomListView    �� SelectionMode$  Extended=�5      $��19,-1,15,78q�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   ���	#FF29292A���	#FFF39F13$��Comic Sans MS)�6�   ��5      ��  �� Path$ PlayList� �� UpdateSourceTrigger$ PropertyChanged=�   SelectedItemsList 5   -   �� *SelectedModels+ �� Mode$ TwoWay=�$ PropertyChanged=�z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -0.394Q�6&   T� 5      5       �� View 5      �� 5      .P�5      ��  �� DisplayMemberBinding 6%   �� $ ID�.��6N   ID6R   �� 5       6%   �� $ Title�.��6Q   Title6X   �� 5       6%   �� $ Artist�.��6R   Artist6Z   �� 5       6%   �� $ Genre�.��6Q   Genre6X   �� 5       6%   �� $ Rating�.��6R   Rating6Z   �� 5       6%   �� $ Length�.��6R   Length6Z   5      5       �� 5!   
   -   5        	 ��Name$
	 play��5!      $
��Left=�6u   $��
83,238,0,0q�6�   $	��Top=�6�   $��75��6  ���	#FFE2A90E
���   ���6   �� *PlayButtonContent+$ TwoWay=�$ PropertyChanged=�{�6�   �� *ButtonEnabled+��6�   �� *PlayCommand+6#  � 5"   
   -   5        
 �Name$
 progress��5"       � Minimum$ 0Q�6s   $
��Left=�6�   $��
24,271,0,0q�6�   $	��Top=�6�   $	��395��6  $��19��6#  ���	#FFEEB10B���	#FF2D2C2C � Value 6)   �� *Value+$ TwoWay=�$ PropertyChanged=� � Maximum 6�   �� *	Maximum+$ TwoWay=�$ PropertyChanged=�6F  �� 5#   
   -   5        $
	 stop��5#      $
��Stop��6   $
��Left=�6.   $��180,238,0,0q�6j   $	��Top=�6   $��75��6�   ���	#FFE2A90E
���   ���6I   �� *StopCommand+6�   5$      5%      