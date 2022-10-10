using Godot;
using System;

public class TitleMenu : CanvasLayer
{
  // Declare member variables here. Examples:
  private AnimationPlayer titleAnim;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    titleAnim = GetNode<AnimationPlayer>("AnimationPlayer");
  
    titleAnim.Play("test");
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if( titleAnim.IsPlaying() == false ){
      titleAnim.Play("test");
    } 
  }

  private void _on_Button_pressed()
  {
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  
  }
}
