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
    if(!music.Playing) {
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


  private async void _on_Play_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();  
    
    await t.ChangeScene("res://Username/username.tscn");
  }
  
  private async void _on_Credits_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    
    await t.ChangeScene("res://Menus/Credits.tscn");
  }


  private async void _on_Tutorial_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    
    await t.ChangeScene("res://Menus/Tutorial.tscn");
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
