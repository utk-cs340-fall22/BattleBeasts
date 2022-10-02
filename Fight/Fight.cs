using Godot;
using System;

public class Fight : Node
{
#pragma warning disable 649
  [Export]
  public PackedScene Fighter;
#pragma warning restore 649
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    int[] attackSet = new int[4];

    // initialize player character
    Fighter player = (Fighter)Fighter.Instance();
    attackSet = new int[] {1, 2, 3, 4};
    player.Init("player", attackSet);

    // initialize opponent character
    Fighter opponent = (Fighter)Fighter.Instance();
    attackSet = new int[] {2, 4, 3, 1};
    opponent.Init("opponent", attackSet);
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
