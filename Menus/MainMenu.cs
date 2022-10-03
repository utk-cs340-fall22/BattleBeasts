using Godot;
using System;

public class MainMenu : CanvasLayer
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
	
  }
  
  private void _on_Exit_pressed()
  {
	// Replace with function body.
	GetTree().Quit();
  }


  private void _on_Settings_pressed()
  {
	GetTree().ChangeScene("res://Menus/OptionsMenu.tscn");
  }


  private void _on_Play_pressed()
  {
	GetTree().ChangeScene("res://Fight/Fight.tscn");
  }
  
  private void _on_Credits_pressed()
  {
	GetTree().ChangeScene("res://Menus/Credits.tscn");
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
