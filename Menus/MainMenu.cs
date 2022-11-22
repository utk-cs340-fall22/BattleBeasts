using Godot;
using System;

public class MainMenu : CanvasLayer
{
  private Globals globals;
  private Transition t;
  private Control Options;
  private AudioStreamPlayer music, se;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    globals = GetNode<Globals>("/root/Gm");
    t = (Transition)GetNode("/root/Transition");
    music = globals.GetNode<AudioStreamPlayer>("Music");
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    if(!music.IsPlaying()) {
      music.Stream = ResourceLoader.Load("res://Assets/Music/TitleMusic.mp3") as AudioStream;
      music.Play();
    }
  }
  
  private void _on_Exit_pressed()
  {
    // Replace with function body.
    GetTree().Quit();
  }


  private void _on_Settings_pressed()
  {
    
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
     
    Options = GetNode<Control>("OptionsMenu2");
    Options.Call("_Load_Options_Menu",false);
  }


  private void _on_Play_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();  
    
  t.ChangeScene("res://Username/username.tscn", "res://Assets/username.png");
  }
  
  private void _on_Credits_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    
  GetTree().ChangeScene("res://Menus/Credits.tscn");
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
