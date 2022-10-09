using Godot;
using System;

public class OptionsMenu : CanvasLayer
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
  private void _on_Back_pressed()
  {
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  }
  
  private void _on_Fullscreen_pressed()
  {
    OS.WindowFullscreen = !OS.WindowFullscreen;
  }
}
