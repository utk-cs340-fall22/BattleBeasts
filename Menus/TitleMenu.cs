using Godot;
using System;

public class TitleMenu : CanvasLayer
{
  // Declare member variables here. Examples:
  private AnimationPlayer titleAnim;
  private AudioStreamPlayer music;
  
  int playcount;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    titleAnim = GetNode<AnimationPlayer>("AnimationPlayer");
    music = GetNode<AudioStreamPlayer>("Music");
  
    playcount = 0;
  
    titleAnim.Play("test");
    music.Play();
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if( titleAnim.IsPlaying() == false ){
      playcount++;
      if(playcount % 3 == 0)
        GetNode<Sprite>("AnimationPlayer/Sprite").Texture = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
      else if(playcount % 3 == 1)
        GetNode<Sprite>("AnimationPlayer/Sprite").Texture = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
      else
        GetNode<Sprite>("AnimationPlayer/Sprite").Texture = ResourceLoader.Load("res://Assets/Character Sprites/Solanac-1.png") as Texture;
      
      titleAnim.Play("test");
    } 
  }

  private void _on_Button_pressed()
  {
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  
  }
}
