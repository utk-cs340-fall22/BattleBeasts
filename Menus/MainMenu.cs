using Godot;
using System;

public class MainMenu : CanvasLayer
{

  private Control Options;
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
  //GetTree().ChangeScene("res://Menus/OptionsMenu.tscn");
        Options = GetNode<Control>("OptionsMenu2");
        //Options._Load_Options_Menu();
        Options.Call("_Load_Options_Menu");
  }


  private void _on_Play_pressed()
  {
  GetTree().ChangeScene("res://Username/username.tscn");
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
