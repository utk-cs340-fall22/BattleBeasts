using Godot;
using Godot.Collections;
using System;
//using System.Collections.Generic;

public class TeamSelect : CanvasLayer
{
#pragma warning disable 649
  [Export]
  public PackedScene Fighter;
#pragma warning restore 649
  
  private OptionButton beast, modifier;
  private OptionButton attack0, attack1, attack2, attack3;
  private Fighter player;

  private static Dictionary _beastOptions = null;
  private static Dictionary _modifierOptions = null;
  private static Dictionary _attackOptions = null;
  private Dictionary beastStats;
  private Dictionary modifierStats;
  private Dictionary attack0Stats;
  private Dictionary attack1Stats;
  private Dictionary attack2Stats;
  private Dictionary attack3Stats;
  
  int playerMaxHealth;
  int[] attackSet;
  Texture tex;
  
  private Dictionary beastOptions {
    get {
      if (_beastOptions == null) {
        var file = new File();
        file.Open("res://Data/Beasts.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _beastOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _beastOptions;
    }
  }

  private Dictionary modifierOptions {
    get {
      if (_modifierOptions == null) {
        var file = new File();
        file.Open("res://Data/Modifiers.json", File.ModeFlags.Read); // WILL BE MODIFIERS.JSON
        var text = file.GetAsText();
        _modifierOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _modifierOptions;
    }
  }

  private Dictionary attackOptions {
    get {
      if (_attackOptions == null) {
        var file = new File();
        file.Open("res://Data/Attacks.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _attackOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _attackOptions;
    }
  }
  
  private void InitMenus()
  {
    int i;
    Dictionary beasts, modifiers, attacks;

    /* Add beast selections */

    for (i = 0; i < beastOptions.Count; i++) {
      beasts = beastOptions[i.ToString()] as Dictionary;
      beast.AddItem((String) beasts["name"]);

      // debug
      GD.Print("Added: ", beasts["name"]);
    }

    /* Add modifier selections */

    for (i = 0; i < modifierOptions.Count; i++) {
      modifiers = modifierOptions[i.ToString()] as Dictionary;
      modifier.AddItem((String) modifiers["name"]);

      // debug
      GD.Print("Added: ", modifiers["name"]);
    }
    
    /* Add attack selections */

    for (i = 0; i < attackOptions.Count; i++) {
      attacks = attackOptions[i.ToString()] as Dictionary;
      attack0.AddItem((String) attacks["name"]);
      attack1.AddItem((String) attacks["name"]);
      attack2.AddItem((String) attacks["name"]);
      attack3.AddItem((String) attacks["name"]);

      // debug
      GD.Print("Added: ", attacks["name"]);
    }
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    beast = GetNode<OptionButton>("Beasts");
    modifier = GetNode<OptionButton>("Modifier");
    
    /* You can't make arrays of structs in c# */
    attack0 = GetNode<OptionButton>("Attack0");
    attack1 = GetNode<OptionButton>("Attack1");
    attack2= GetNode<OptionButton>("Attack2");
    attack3 = GetNode<OptionButton>("Attack3");
    
    player = GetNode<Fighter>("Fighter");
    attackSet = new int[] {1, 1, 1, 1};
    playerMaxHealth = 100;
    player.Init("player", attackSet, playerMaxHealth);
    player.Position = new Vector2(500, 200);
    player.Scale = new Vector2(6, 6);
    tex = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
    player.GetNode<Sprite>("Texture").Texture = tex;

    // beast.connect("ItemSelected", this, "OnBeastSelected");
    
    InitMenus();
  }
  
  

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

  
  private void _on_Beasts_item_selected(int index)
  {
    string s;
    
    s = beast.GetItemText(beast.GetSelectedId());
    if(s == "Alzrius"){
        tex = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
    } else if (s == "Auril"){
        tex = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
    } else if (s == "Solanac"){
        tex = ResourceLoader.Load("res://Assets/Character Sprites/Solanac-1.png") as Texture;
    } else{
      tex = null;
    }
  
    player.GetNode<Sprite>("Texture").Texture = tex;
  }
  
  private void _on_Back_pressed()
  {
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  }
  
  private void _on_Go_pressed()
  {
    /* Pass Fighter here */
    GetTree().ChangeScene("res://Bracket/Bracket.tscn");
  }
  
}
