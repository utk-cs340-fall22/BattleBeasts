using Godot;
using Godot.Collections;
using System;

public class Fighter : Sprite
{
  public Sprite texture;
  string controller; // fighter is controlled by player or the opponent (computer)

  string beastName; // beast name
  int maxHealth; // max health
  int health; // current health
  int armor; // beast armor
  string type; // beast type

  string modifierName; // modifier name
  int healthModifier, armorModifier;

  Dictionary[] attacksDict = new Dictionary[4];

  /*
  Initialize as player or opponent and load stats
  controller: what controls this fighter, "player" or "opponent"  
  attackSet: array of attack IDs indicating what attacks this fighter has
  maxHealth: maximum health value
  */
  public void Init(string controller, Dictionary beastDict, Dictionary modifierDict, Dictionary[] attacksDict) {
    int i;
    
    // Load initialization data
    this.controller = controller;
    beastName = (string)beastDict["name"];
    modifierName = (string)modifierDict["name"];
    healthModifier = Convert.ToInt32(modifierDict["health_modifier"]);
    armorModifier = Convert.ToInt32(modifierDict["armor_modifier"]);
    maxHealth = Convert.ToInt32(beastDict["health"]) + healthModifier;
    health = maxHealth;
    armor = Convert.ToInt32(beastDict["armor"]) + armorModifier;
    type = (string)beastDict["type"];
    this.attacksDict = attacksDict;

    // debugging
    GD.Print("controller: ", controller);
    GD.Print("maxHealth: ", maxHealth, " | armor: ", armor, " | type: ", type);
    GD.Print("modifierName: ", modifierName, " | healthModifier: ", healthModifier, " | armorModifier: ", armorModifier);
    for (i = 0; i < attacksDict.Length; i++) GD.Print("attack", i, ": ", attacksDict[i]["name"]);
  }

  /*
  Return how much damage an attack does
  attackID: ID of the attack
  return: Damage value of attack
  */
  public int GetAttackStrength(int attackID) {
    return Convert.ToInt32(attacksDict[attackID]["strike_damage"]);
  }

  /*
  Return how many strikes an attack performs
  attackID: ID of the attack
  return: Strike count of attack
  */
  public int GetAttackCount(int attackID) {
    return Convert.ToInt32(attacksDict[attackID]["strike_count"]);
  }

  /*
  Return this fighter's current health value
  Return: Fighter's current health value
  */
  public int GetHealth() {
    return health;
  }

  /*
  Return this fighter's maximum health value
  Return: Fighter's maximum health value
  */
  public int GetMaxHealth() {
    return maxHealth;
  }

  /*
  Return this fighter's armor value
  Return: Fighter's armor value
  */
  public int GetArmor() {
    return armor;
  }

  /*
  Return minigame of attack with given ID
  attackID: ID of the attack
  return: Minigame index of attack
  */
  public int GetAttackMinigame(int attackID) {
    return Convert.ToInt32(attacksDict[attackID]["minigame"]);
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
