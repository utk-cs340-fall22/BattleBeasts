using Godot;
using System;

public class TitleMenu : CanvasLayer
{
  // Declare member variables here. Examples:
  private AnimationPlayer titleAnim;
  private AudioStreamPlayer music, se;
  private Globals globals;
  
  int playcount;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    titleAnim = GetNode<AnimationPlayer>("AnimationPlayer");
    globals = GetNode<Globals>("/root/Gm");
    music = globals.GetNode<AudioStreamPlayer>("Music");
  
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
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  
  }
}
