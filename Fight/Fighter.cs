using Godot;
using System;

public class Fighter : Sprite
{
  string controller; // fighter is controlled by player or the opponent (computer)
  int health; // current health
  int maxHealth; // max health
  int[] attackStats = new int[4]; // attack damage for the 4 different attacks
  public Sprite texture;
  Texture tex;

  /*
  initialize as player or opponent and load stats
  controller: what controls this fighter, "player" or "opponent"  
  attackSet: array of attack IDs indicating what attacks this fighter has
  maxHealth: maximum health value
  */
  public void Init(string controller, int[] attackSet, int maxHealth) {
    int i;
    
    tex = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
    GetNode<Sprite>("Texture").Texture = tex;

    // Load attack identifiers
    this.controller = controller;
    this.maxHealth = maxHealth;
    health = maxHealth;
    for (i = 0; i < attackSet.Length; i++) {
      attackStats[i] = attackSet[i];
    }
    

    // debugging
    GD.Print("controller: ", controller);
    for (i = 0; i < attackSet.Length; i++) {
      GD.Print("attack", i, ": ", attackSet[i]);
    }
  }

  /*
  Return how much damage an attack does
  attackID: ID of the attack
  return: Damage value of attack
  */
  public int GetAttackStrength(int attackID) {
    return attackStats[attackID];
  }

  /*
  Return this fighter's current health value
  Return: Fighter's current health value
  */
  public int GetHealth() {
    return health;
  }

  /*
  Reduce this fighter's health by a given amount
  damage: How much to reduce the fighter's health by
  return: Resulting health value
  */
  public int ReduceHealth(int damage) {
    if (damage >= health) {
      health = 0;
    }
    else {
      health -= damage;
    }

    return health;
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
