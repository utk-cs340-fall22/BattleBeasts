using Godot;
using System;

public class Tutorial : Node2D
{
  private AudioStreamPlayer se;
  private Globals globals;
  private Transition t;
  private RichTextLabel Welcome, Continue;
  private bool start;
  private Vector2 s,e;
  private Color c;
  private Line2D line;


  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    t = (Transition)GetNode("/root/Transition");
    globals = GetNode<Globals>("/root/Gm");
    Welcome = GetNode<RichTextLabel>("ColorRect/CenterContainer/Welcome");
    Continue = GetNode<RichTextLabel>("ColorRect/CenterContainer/Continue");
    line = GetNode<Line2D>("Line2D");
    start = true;
  }
  
  // Return to the Main Menu
  private void _on_Back_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/BackSound.tres") as AudioStream;
    se.Play();
    
    t.ChangeScene("res://Menus/MainMenu.tscn");
  }

  // Handle the SpaceBar presses
  public override void _Input(InputEvent inputEvent)
  {
    if (inputEvent.IsActionPressed("ui_accept") && start)
    {
      Welcome.Text = "";
      Continue.Text = "";
      start = false;
      s.x = 100;
      s.y = 50;
      
      e.x = 400;
      e.y = 90;
      
      line.AddPoint(s,0);
      line.AddPoint(e,1);
      //DrawLine(s,e,c);
      GD.Print("MOVE FORWARD");
    }
  }
}
