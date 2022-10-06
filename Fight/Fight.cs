using Godot;
using System;

public class Fight : Node
{
#pragma warning disable 649
  [Export]
  public PackedScene Fighter;
#pragma warning restore 649

  int playerMaxHealth, opponentMaxHealth;
  Fighter player, opponent;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    int[] attackSet = new int[4];
    playerMaxHealth = 100;
    opponentMaxHealth = 132;

    // initialize player character
    player = (Fighter)Fighter.Instance();
    attackSet = new int[] {1, 2, 3, 4};
    player.Init("player", attackSet, playerMaxHealth);

    // initialize opponent character
    opponent = (Fighter)Fighter.Instance();
    attackSet = new int[] {2, 4, 3, 1};
    opponent.Init("opponent", attackSet, opponentMaxHealth);

    // debug
    GD.Print("oppoonent health: ", opponent.Reduce_Health(0));
  }

  /*
  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	// stuff
  }
  */

  private void _on_B1_pressed()
  {
	GD.Print("opponent health remaining: ", opponent.Reduce_Health(player.Get_Attack_Strength(0)));
  }
}
