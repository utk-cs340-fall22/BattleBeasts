using Godot;
using System;

public class Fighter : Sprite
{
  string controller; // fighter is controlled by player or the opponent (computer)
  int health; // current health
  int maxHealth; // max health
  int[] attackStats = new int[4]; // attack damage for the 4 different attacks

  /*
  initialize as player or opponent and load stats
  controller: what controls this fighter, "player" or "opponent"  
  attackSet: array of attack IDs indicating what attacks this fighter has
  maxHealth: maximum health value
  */
  public void Init(string controller, int[] attackSet, int maxHealth) {
    int i;

    // load attack identifiers
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
  return how much damage an attack does
  attackID: id of the attack
  return: damage value of attack
  */
  public int Get_Attack_Strength(int attackID) {
    return attackStats[attackID];
  }

  /*
  reduce this fighter's health by a given amount
  damage: how much to reduce the fighter's health by
  return: resulting health value
  */
  public int Reduce_Health(int damage) {
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
