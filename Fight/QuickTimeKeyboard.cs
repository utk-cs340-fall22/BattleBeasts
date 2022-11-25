using Godot;
using System;

public class QuickTimeKeyboard : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    private Label press, letter;
    private Node fight;
    private int tick, ltr, wait, score;
    private int ret;
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private Random rnd;
    
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        tick = 0;
        press = GetNode<Label>("Container/Press");
        letter = GetNode<Label>("Container/Letter");
        fight = GetNode<Node>("/root/Main");
        
        rnd = new Random();
        wait = rnd.Next(65,120);
        ltr = rnd.Next(0,25);
        GD.Print("Wait: " + wait.ToString() + " Letter: " + alphabet[ltr].ToString() );
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
      tick += 1;
      
      if(tick == wait){
          press.SetText("Press:");
          letter.SetText(alphabet[ltr].ToString());
      }
    }
    
     public override void _Input(InputEvent input){
     
      if (input is InputEventKey keyEvent && keyEvent.Pressed)
      {
        if(OS.GetScancodeString(keyEvent.Scancode) == alphabet[ltr].ToString()){
          ret = 120 - ((tick - wait) / 3);
          if(ret < 0) ret = 0;
          GD.Print("QT return: ", ret);
          fight.Call("MinigameReturn", ret);
          QueueFree();
        }
        
      }
      
     }
}
