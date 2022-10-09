using Godot;
using System;

public class PauseMenu2 : Control
{
	bool is_paused;

	// initialize the scene to be hidden
	public override void _Ready()
	{
		is_paused = false;
		this.Hide();
	}
	
	private void _on_QuitButton_pressed()
	{
	  _SetPaused(false);
	  GetTree().ChangeScene("res://Menus/MainMenu.tscn");
	}
	
	private void _on_ResumeButton_pressed()
	{
	  _SetPaused(false);
	}
	
	// pauses the scene tree and shows the pause menu
	private void _SetPaused(bool val)
	{
	  is_paused = val;
	  GetTree().Paused = is_paused;
	  if(val == true) this.Show();
	  if(val == false) this.Hide();
	}
  
	// on escape key pressed we pull up the pause menu
	public override void _Process(float delta)
	{
	  if(Input.IsActionPressed("pause")) _SetPaused(true);
	}
}
