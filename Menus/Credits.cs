using Godot;
using System;
using System.Collections.Generic;

public class Credits : Control
{
  private VBoxContainer credits;
  private File file = new File();
  private List<String> lines = new List<string>();
  private AudioStreamPlayer se;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    int i;
    Label tmp;
    
    // grab your node
    credits = GetNode<VBoxContainer>("ScrollContainer/ScrollingText");
  
    // open the credits file
    file.Open("res://Menus/credits.txt",File.ModeFlags.Read);
    
    // read all the lines from the file
    while(!file.EofReached()){
      lines.Add(file.GetLine());
    }
    
    // close the file
    file.Close();
  
    // add all the lines to the container
    for(i = 0; i < lines.Count; i++){
        tmp = new Label();
        tmp.Text = lines[i];
        tmp.Autowrap = true;
        credits.AddChild(tmp);
    }
    
    // set the lines list to null so garbage collector will grab it
    lines = null;
  }
  
  private void _on_BackgroundVideo_finished()
  {
    // Replace with function body.
  }
  
  private void _on_MusicPlayer_finished()
  {
    // Replace with function body.
  }

  private void _on_Back_pressed()
  {
    se = GetNode<AudioStreamPlayer>("/root/Gm/SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/BackSound.tres") as AudioStream;
    se.Play();
    
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
