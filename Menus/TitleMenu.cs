using Godot;
using Godot.Collections;
using System;

public class TitleMenu : CanvasLayer
{
  // Declare member variables here. Examples:
  private AnimationPlayer titleAnim;
  private AudioStreamPlayer music, musicP, musicO, se;
  private Globals globals;
  
  int playcount;
  
  private static Dictionary _beastOptions = null;
  
  private Dictionary beastOptions {
    get {
      if (_beastOptions == null) {
        var file = new File();
        file.Open("res://Data/Beasts.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _beastOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _beastOptions;
    }
  }
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    titleAnim = GetNode<AnimationPlayer>("AnimationPlayer");
    globals = GetNode<Globals>("/root/Gm");
    music = globals.GetNode<AudioStreamPlayer>("Music");
    musicP = globals.GetNode<AudioStreamPlayer>("MusicPlayer");
    musicO = globals.GetNode<AudioStreamPlayer>("MusicOpponent");
    
  
    playcount = 0;
  
    titleAnim.Play("test");
    music.Stop();
    musicP.Stop();
    music.Stream = ResourceLoader.Load("res://Assets/Music/TitleMusic.mp3") as AudioStream;
    music.Play();
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    if( titleAnim.IsPlaying() == false ){
      playcount++;
      
      Dictionary beast;
      Texture tex;
    
      beast = beastOptions[(playcount % 5).ToString()] as Dictionary;
      tex = ResourceLoader.Load((String) beast["texture"]) as Texture;
      
      GetNode<Sprite>("AnimationPlayer/Sprite").Texture = tex;
      
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
