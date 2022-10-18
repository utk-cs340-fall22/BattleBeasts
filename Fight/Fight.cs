using Godot;
using System;

public class Fight : Node
{
#pragma warning disable 649
  [Export]
  public PackedScene Fighter;
#pragma warning restore 649
  Globals g;
  int playerMaxHealth, opponentMaxHealth, isPlayerTurn, aiAttackChoice;
  Fighter player, opponent;
  Texture playerTexture, opponentTexture;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    g = (Globals)GetNode("/root/Gm");
    int[] attackSet = new int[4];
    playerMaxHealth = 100;
    opponentMaxHealth = 132;
    isPlayerTurn = 1;
    GD.Randomize();

    /* Initialize player character */

    player = (Fighter)Fighter.Instance();
    AddChild(player);
    playerTexture = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
    player.GetNode<Sprite>("Texture").Texture = playerTexture;
    player.Position = new Vector2(190, 280);
    player.Scale = new Vector2(6, 6);
    attackSet = new int[] {1, 2, 13, 1000};
    player.Init("player", attackSet, playerMaxHealth);

    /* Initialize opponent character */

    opponent = (Fighter)Fighter.Instance();
    AddChild(opponent);
    opponentTexture = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
    opponent.GetNode<Sprite>("Texture").Texture = opponentTexture;
    opponent.Position = new Vector2(850, 170);
    opponent.Scale = new Vector2(6, 6);
    attackSet = new int[] {1, 2, 13, 50};
    opponent.Init("opponent", attackSet, opponentMaxHealth);

    // debug
    GD.Print("oppoonent health: ", opponent.GetHealth());
  }

  public void AITakeTurn() {
    /* Ensure method is called on opponent's turn */

    if (isPlayerTurn == 1) {
      GD.Print("AI cannot take a turn on the player's turn");
      return;
    }

    /* Choose attack */

    aiAttackChoice = (int)(GD.Randi() % 4); // random number between 0 and 3
    GD.Print("opponent attack ", aiAttackChoice, " to player. player health remaining: ", player.ReduceHealth(opponent.GetAttackStrength(aiAttackChoice)));
    isPlayerTurn = 1;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    /* End fight once a beast has 0 health */

    if (player.GetHealth() <= 0) {
      GD.Print("opponent defeted player");
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }
    else if (opponent.GetHealth() <= 0) {
      GD.Print("player defeted opponent");
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }

    /* AI turn */

    // it seems signals can be recieved in the middle of _Process() so checking health here is necessary
    if (isPlayerTurn == 0 && opponent.GetHealth() != 0) {
      AITakeTurn();
    }
  }

  /* Signals */

  private void _on_B0_pressed() {
    if (isPlayerTurn == 0) {
      GD.Print("Cannot attack on opponent's turn.");
      return;
    }
    GD.Print("player attack 0 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(0)));
    isPlayerTurn = 0;
  }

  private void _on_B1_pressed() {
    if (isPlayerTurn == 0) {
      GD.Print("Cannot attack on opponent's turn.");
      return;
    }
    GD.Print("player attack 1 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(1)));
    isPlayerTurn = 0;
  }

  private void _on_B2_pressed() {
    if (isPlayerTurn == 0) {
      GD.Print("Cannot attack on opponent's turn.");
      return;
    }
    GD.Print("player attack 2 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(2)));
    isPlayerTurn = 0;
  }

  private void _on_B3_pressed() {
    if (isPlayerTurn == 0) {
      GD.Print("Cannot attack on opponent's turn.");
      return;
    }
    GD.Print("player attack 3 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(3)));
    isPlayerTurn = 0;
  }
}
