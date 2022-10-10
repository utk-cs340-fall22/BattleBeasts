using Godot;
using System;

public class Beasts : Node2D
{
  private int hp = 100;
  private int attack = 100;
  private int defence = 100;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    Hide();
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
   public override void _Process(float delta)
   {
    
    }
}
