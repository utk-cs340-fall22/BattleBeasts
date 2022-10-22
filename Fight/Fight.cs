using Godot;
using System;

public class Fight : Node
{
#pragma warning disable 649
  [Export]
  public PackedScene Fighter;
  [Export]
  public PackedScene PowerSliderMinigame;
#pragma warning restore 649
  Globals g;
  int playerMaxHealth, opponentMaxHealth, isPlayerTurn, aiAttackChoice;
  int queuedAttack; // index of submitted attack in attackSet of attacking fighter | -1 for no queued attack | integer in [0, 3] for player | integer in [10, 13] for opponent
  int minigameResult; // -1: minigame active | [0, 100]: result of previous minigame, no minigame active
  Fighter player, opponent;
  Texture playerTexture, opponentTexture;
  
  // Called when the node enters the scene tree for the first time.
  public override void _Ready() {
    g = (Globals)GetNode("/root/Gm");
    int[] attackSet = new int[4];
    playerMaxHealth = 100;
    opponentMaxHealth = 132;
    isPlayerTurn = 1;
    minigameResult = 0;
    queuedAttack = -1;
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

  // AI chooses and performs an attack
  public void AITakeTurn() {
    /* Ensure method is called on opponent's turn */

    if (isPlayerTurn == 1) {
      GD.Print("AI cannot take a turn on the player's turn");
      return;
    }

    /* Choose attack */

    aiAttackChoice = (int)(GD.Randi() % 4); // random number between 0 and 3
    queuedAttack = aiAttackChoice + 10;
    minigameResult = -1;
    // CREATE MINIGAME
    GD.Print("opponent attack ", aiAttackChoice, " to player. player health remaining: ", player.ReduceHealth(opponent.GetAttackStrength(aiAttackChoice))); // to remove
    minigameResult = 100; // to remove
    isPlayerTurn = 1; // to remove
  }

  // Returns 1 if attack button signals shouldn't be obeyed
  public int CheckAttackSignalPermission () {
    if (isPlayerTurn == 0) {
      GD.Print("Cannot attack on opponent's turn.");
      return 1;
    }
    if (minigameResult == -1) {
      GD.Print("Cannot attack during active minigame.");
      return 1;
    }

    return 0;
  }

  public void MinigameReturn(int result) {
    if (result < 0 || result > 100) {
      GD.Print("Minigames may only return integer values in [0, 100]");
      return;
    }
    minigameResult = result;
  }

  // Performs the queued attack whether its from the player or the opponent
  public void PerformQueuedAttack() {
    // no queued attack
    if (queuedAttack == -1) return;

    // opponent attacking
    if (queuedAttack >= 10) {
      GD.Print("opponent attack ", queuedAttack - 10, " to player. player health remaining: ", player.ReduceHealth(opponent.GetAttackStrength(queuedAttack - 10)));
      isPlayerTurn = 1;
      queuedAttack = -1;
    }

    // player attacking
    else {
      GD.Print("player attack ", queuedAttack, " to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(queuedAttack)));
      isPlayerTurn = 0;
      queuedAttack = -1;
    }
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    /* End fight once a beast has 0 health */

    if (player.GetHealth() <= 0) {
      GD.Print("opponent defeated player");
      g.fight_outcome = 0;
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }
    else if (opponent.GetHealth() <= 0) {
      GD.Print("player defeated opponent");
      g.fight_outcome = 1;
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }

    /* Everything below is skipped if a minigame is active */

    if (minigameResult == -1) return;

    /* Perform queued attack */

    //PerformQueuedAttack(); // UNCOMMENT WHEN MINIGAMES DONT SOFTLOCK THE GAME

    /* AI turn */

    // it seems signals can be recieved in the middle of _Process() so checking health here is necessary
    if (isPlayerTurn == 0 && opponent.GetHealth() != 0) {
      AITakeTurn();
    }
  }

  /* Signals */

  private void _on_B0_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 0;
    GD.Print("player attack 0 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(0))); // to remove
    isPlayerTurn = 0; // to remove
    minigameResult = -1;
    // CREATE MINIGAME
    minigameResult = 100; // to remove
  }

  private void _on_B1_pressed() { // DEBUG: spawns slider minigame
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 1;
    GD.Print("player attack 1 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(1))); // to remove
    isPlayerTurn = 0; // to remove
    minigameResult = -1;
    AddChild(PowerSliderMinigame.Instance()); // some logic to determine what minigame to create will be needed eventually
    minigameResult = 100; // to remove
  }

  private void _on_B2_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 2;
    GD.Print("player attack 2 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(2))); // to remove
    isPlayerTurn = 0; // to remove
    minigameResult = -1;
    // CREATE MINIGAME
    minigameResult = 100; // to remove
  }

  private void _on_B3_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 3;
    GD.Print("player attack 3 to opponent. opponent health remaining: ", opponent.ReduceHealth(player.GetAttackStrength(3))); // to remove
    isPlayerTurn = 0; // to remove
    minigameResult = -1;
    // CREATE MINIGAME
    minigameResult = 100; // to remove
  }
}
