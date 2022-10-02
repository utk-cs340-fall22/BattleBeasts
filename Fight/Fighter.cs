using Godot;
using System;

public class Fighter : Sprite
{
  string controller;
  int health, maxHealth;
  int[] attackStats = new int[4]; // attack damage
  
  // initialize as player/opponent, load stats
  public void Init(string controller, int[] attackSet) {
    int i;

    // load attack identifiers
    this.controller = controller;
    for (i = 0; i < attackSet.Length; i++) {
      attackStats[i] = attackSet[i];
    }

    // debugging
    GD.Print("controller: ", controller);
    for (i = 0; i < attackSet.Length; i++) {
      GD.Print("attack", i, ": ", attackSet[i]);
    }
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
