using Godot;
using System;

public class TitleMenu : CanvasLayer
{
  // Declare member variables here. Examples:
  private AnimationPlayer _titleAnim;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
  _titleAnim = GetNode<AnimationPlayer>("AnimationPlayer");
  
  _titleAnim.Play("test");
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
  if( _titleAnim.IsPlaying() == false ){
    _titleAnim.Play("test");
  } 
  }

  private void _on_Button_pressed()
  {
  GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  
  }
}
