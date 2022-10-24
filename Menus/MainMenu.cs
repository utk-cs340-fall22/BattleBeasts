using Godot;
using System;

public class MainMenu : CanvasLayer
{
  private Globals globals;
  private Control Options;
  private AudioStreamPlayer se;
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    globals = GetNode<Globals>("/root/Gm");
  }
  
  private void _on_Exit_pressed()
  {
  // Replace with function body.
  GetTree().Quit();
  }


  private void _on_Settings_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
     
  //GetTree().ChangeScene("res://Menus/OptionsMenu.tscn");
        Options = GetNode<Control>("OptionsMenu2");
        //Options._Load_Options_Menu();
        Options.Call("_Load_Options_Menu");
  }


  private void _on_Play_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();  
    
  GetTree().ChangeScene("res://Username/username.tscn");
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
