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

    // initialize player character
    player = (Fighter)Fighter.Instance();
    AddChild(player);
    attackSet = new int[] {1, 2, 13, 50};
    player.Init("player", attackSet, playerMaxHealth);
    player.Position = new Vector2(190, 280);
    player.Scale = new Vector2(6, 6);
    playerTexture = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
    player.GetNode<Sprite>("Texture").Texture = playerTexture;

    // initialize opponent character
    opponent = (Fighter)Fighter.Instance();
    AddChild(opponent);
    attackSet = new int[] {2, 4, 3, 1};
    opponent.Init("opponent", attackSet, opponentMaxHealth);
    opponent.Position = new Vector2(850, 170);
    opponent.Scale = new Vector2(6, 6);
    opponentTexture = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
    opponent.GetNode<Sprite>("Texture").Texture = opponentTexture;

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

  private void _on_B0_pressed() {
    GD.Print("player attack 0 to opponent. opponent health remaining: ", opponent.Reduce_Health(player.Get_Attack_Strength(0)));
  }

  private void _on_B1_pressed() {
    GD.Print("player attack 1 to opponent. opponent health remaining: ", opponent.Reduce_Health(player.Get_Attack_Strength(1)));
  }

  private void _on_B2_pressed() {
    GD.Print("player attack 2 to opponent. opponent health remaining: ", opponent.Reduce_Health(player.Get_Attack_Strength(2)));
  }

  private void _on_B3_pressed() {
    GD.Print("player attack 3 to opponent. opponent health remaining: ", opponent.Reduce_Health(player.Get_Attack_Strength(3)));
  }
}
