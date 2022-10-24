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
  
  private OptionButton beastSelector, modifierSelector;
  private OptionButton attack0Selector, attack1Selector, attack2Selector, attack3Selector;
  private Fighter player;

  private static Dictionary _beastOptions = null;
  private static Dictionary _modifierOptions = null;
  private static Dictionary _attackOptions = null;
  
  private AudioStreamPlayer se;
  
  Globals g;
  
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
    Dictionary beast, modifier, attack;

    /* Add beast selections */

    for (i = 0; i < beastOptions.Count; i++) {
      beast = beastOptions[i.ToString()] as Dictionary;
      beastSelector.AddItem((String) beast["name"], i);

      // debug
      GD.Print("Added: ", beast["name"]);
    }

    /* Add modifier selections */

    for (i = 0; i < modifierOptions.Count; i++) {
      modifier = modifierOptions[i.ToString()] as Dictionary;
      modifierSelector.AddItem((String) modifier["name"], i);

      // debug
      GD.Print("Added: ", modifier["name"]);
    }
    
    /* Add attack selections */

    for (i = 0; i < attackOptions.Count; i++) {
      attack = attackOptions[i.ToString()] as Dictionary;
      attack0Selector.AddItem((String) attack["name"], i);
      attack1Selector.AddItem((String) attack["name"], i);
      attack2Selector.AddItem((String) attack["name"], i);
      attack3Selector.AddItem((String) attack["name"], i);

      // debug
      GD.Print("Added: ", attack["name"]);
    }
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    g = (Globals)GetNode("/root/Gm");
    
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    
    beastSelector = GetNode<OptionButton>("Beasts");
    modifierSelector = GetNode<OptionButton>("Modifier");
    
    /* You can't make arrays of structs in c# */
    attack0Selector = GetNode<OptionButton>("Attack0");
    attack1Selector = GetNode<OptionButton>("Attack1");
    attack2Selector = GetNode<OptionButton>("Attack2");
    attack3Selector = GetNode<OptionButton>("Attack3");
    
    player = GetNode<Fighter>("Fighter");
    attackSet = new int[] {1, 1, 1, 1};
    playerMaxHealth = 100;
    player.Init("player", attackSet, playerMaxHealth);
    player.Position = new Vector2(500, 200);
    player.Scale = new Vector2(6, 6);
    tex = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
    player.GetNode<Sprite>("Texture").Texture = tex;

    // beastSelector.connect("ItemSelected", this, "OnBeastSelected");
    
    InitMenus();
  }
  
  

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

  
  private void _on_Beasts_item_selected(int index)
  {    
    Dictionary beast;
    
    beast = beastOptions[beastSelector.GetSelectedId().ToString()] as Dictionary;
    tex = ResourceLoader.Load((String)beast["texture"]) as Texture;
  
    player.GetNode<Sprite>("Texture").Texture = tex;
    
    /* Play menu sound */
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  /* These all just play the menu sound */
  private void _on_Modifier_item_selected(int index)
  {
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack0_item_selected(int index)
  {
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack1_item_selected(int index)
  {
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack2_item_selected(int index)
  {
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack3_item_selected(int index)
  {
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Back_pressed()
  {
    /* Plays sound */
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  }
  
  private void _on_Go_pressed()
  {
    // moved this to _Ready()
    //  g = (Globals)GetNode("/root/Gm");
    
    g.playerBeastIndex = beastSelector.GetSelectedId();
    g.playerModifierIndex = modifierSelector.GetSelectedId();
    g.playerAttackIndices[0] = attack0Selector.GetSelectedId();
    g.playerAttackIndices[1] = attack1Selector.GetSelectedId();
    g.playerAttackIndices[2] = attack2Selector.GetSelectedId();
    g.playerAttackIndices[3] = attack3Selector.GetSelectedId();
    
    /* Play sound effect */
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    
    GetTree().ChangeScene("res://Bracket/Bracket.tscn");
  }
}


