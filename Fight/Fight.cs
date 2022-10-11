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
  Texture playerTexture, opponentTexture;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    int[] attackSet = new int[4];
    playerMaxHealth = 100;
    opponentMaxHealth = 132;

    /* Initialize player character */

    player = (Fighter)Fighter.Instance();
    AddChild(player);
    playerTexture = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
    player.GetNode<Sprite>("Texture").Texture = playerTexture;
    player.Position = new Vector2(190, 280);
    player.Scale = new Vector2(6, 6);
    attackSet = new int[] {1, 2, 13, 50};
    player.Init("player", attackSet, playerMaxHealth);

    /* Initialize opponent character */

    opponent = (Fighter)Fighter.Instance();
    AddChild(opponent);
    opponentTexture = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
    opponent.GetNode<Sprite>("Texture").Texture = opponentTexture;
    opponent.Position = new Vector2(850, 170);
    opponent.Scale = new Vector2(6, 6);
    attackSet = new int[] {2, 4, 3, 1};
    opponent.Init("opponent", attackSet, opponentMaxHealth);

    // debug
    GD.Print("oppoonent health: ", opponent.GetHealth());
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    /* End fight once a beast is defeated */

    if (player.GetHealth() <= 0) {
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }
    else if (opponent.GetHealth() <= 0) {
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }
  }

  /* Signals */

  private void _on_B0_pressed() {
    GD.Print("player attack 0 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(0)));
  }

  private void _on_B1_pressed() {
    GD.Print("player attack 1 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(1)));
  }

  private void _on_B2_pressed() {
    GD.Print("player attack 2 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(2)));
  }

  private void _on_B3_pressed() {
    GD.Print("player attack 3 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(3)));
  }
}
