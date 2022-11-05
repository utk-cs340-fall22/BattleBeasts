using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class TeamSelect : CanvasLayer
{
#pragma warning disable 649
  [Export]
  public PackedScene Fighter;
#pragma warning restore 649
  
  private OptionButton beastSelector, modifierSelector;
  private OptionButton attack0Selector, attack1Selector, attack2Selector, attack3Selector;
  private BaseButton goButton;
  private Sprite player;

  private static Dictionary _beastOptions = null;
  private static Dictionary _modifierOptions = null;
  private static Dictionary _attackOptions = null;
  
  private AudioStreamPlayer se;
  
  Globals g;
  
  int attack0Prev, attack1Prev, attack2Prev, attack3Prev;
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

  private int IsInArray(int[] array, int num) {
    foreach (int i in array) if (i == num) return 1;
    return 0;
  }

  private void MakeBeastSelection() {
    int i;
    Dictionary beast;

    beastSelector.AddItem("Beast", 1000);
    beastSelector.AddSeparator();
    for (i = 0; i < beastOptions.Count; i++) {
      beast = beastOptions[i.ToString()] as Dictionary;
      beastSelector.AddItem((String) beast["name"], i);
    }
    beastSelector.Select(beastSelector.GetItemIndex(1000));
    beastSelector.SetItemDisabled(beastSelector.GetItemIndex(1000), true);

    // set up default selector values for modifier and attacks then disable them
    modifierSelector.Clear();
    attack0Selector.Clear();
    attack1Selector.Clear();
    attack2Selector.Clear();
    attack3Selector.Clear();
    modifierSelector.AddItem("Modifier", 1000);
    attack0Selector.AddItem("Attack 1", 1000);
    attack1Selector.AddItem("Attack 2", 1000);
    attack2Selector.AddItem("Attack 3", 1000);
    attack3Selector.AddItem("Attack 4", 1000);
    modifierSelector.Select(modifierSelector.GetItemIndex(1000));
    attack0Selector.Select(attack0Selector.GetItemIndex(1000));
    attack1Selector.Select(attack1Selector.GetItemIndex(1000));
    attack2Selector.Select(attack2Selector.GetItemIndex(1000));
    attack3Selector.Select(attack3Selector.GetItemIndex(1000));
    modifierSelector.Disabled = true;
    attack0Selector.Disabled = true;
    attack1Selector.Disabled = true;
    attack2Selector.Disabled = true;
    attack3Selector.Disabled = true;
  }

  private void MakeModifierSelection() {
    int i, selectedBeastIndex;
    int[] modifiersAllowed;
    Godot.Collections.Array beastGArray;
    Dictionary beast, modifier;

    // set up default selector values for attacks then disable
    attack0Selector.Clear();
    attack1Selector.Clear();
    attack2Selector.Clear();
    attack3Selector.Clear();
    attack0Selector.AddItem("Attack 1", 1000);
    attack1Selector.AddItem("Attack 2", 1000);
    attack2Selector.AddItem("Attack 3", 1000);
    attack3Selector.AddItem("Attack 4", 1000);
    attack0Selector.Select(attack0Selector.GetItemIndex(1000));
    attack1Selector.Select(attack1Selector.GetItemIndex(1000));
    attack2Selector.Select(attack2Selector.GetItemIndex(1000));
    attack3Selector.Select(attack3Selector.GetItemIndex(1000));
    attack0Selector.Disabled = true;
    attack1Selector.Disabled = true;
    attack2Selector.Disabled = true;
    attack3Selector.Disabled = true;
    
    // enable modifier selector, disable attack selectors, set up up modifier selector, get selected beast index
    modifierSelector.Clear();
    modifierSelector.Disabled = false;
    attack0Selector.Disabled = true;
    attack1Selector.Disabled = true;
    attack2Selector.Disabled = true;
    attack3Selector.Disabled = true;
    modifierSelector.AddItem("Modifier", 1000);
    modifierSelector.AddSeparator();
    selectedBeastIndex = beastSelector.GetItemIndex(beastSelector.GetSelectedId());
    
    // find allowed options
    beast = beastOptions[beastSelector.GetSelectedId().ToString()] as Dictionary;
    beastGArray = (Godot.Collections.Array)beast["modifiers"];
    modifiersAllowed = new int[beastGArray.Count];
    for (i = 0; i < modifiersAllowed.Length; i++) modifiersAllowed[i] = (int)(float)beastGArray[i];
    
    // add options
    foreach (int item in modifiersAllowed) {
      modifier = modifierOptions[item.ToString()] as Dictionary;
      modifierSelector.AddItem((String) modifier["name"], item);
    }
    modifierSelector.Select(modifierSelector.GetItemIndex(1000));
    modifierSelector.SetItemDisabled(modifierSelector.GetItemIndex(1000), true);
  }

  private void MakeAttackSelection() {
    int i, j;
    int[] attacksAllowed, used;
    Godot.Collections.Array beastGArray, modifierGArray;
    Dictionary beast, modifier, attack;

    // set up default attack selections
    attack0Selector.Disabled = false;
    attack1Selector.Disabled = false;
    attack2Selector.Disabled = false;
    attack3Selector.Disabled = false;
    attack0Selector.Clear();
    attack1Selector.Clear();
    attack2Selector.Clear();
    attack3Selector.Clear();
    attack0Selector.AddItem("Attack 1", 1000);
    attack1Selector.AddItem("Attack 2", 1000);
    attack2Selector.AddItem("Attack 3", 1000);
    attack3Selector.AddItem("Attack 4", 1000);
    attack0Selector.AddSeparator();
    attack1Selector.AddSeparator();
    attack2Selector.AddSeparator();
    attack3Selector.AddSeparator();
    attack0Selector.Select(attack0Selector.GetItemIndex(1000));
    attack1Selector.Select(attack1Selector.GetItemIndex(1000));
    attack2Selector.Select(attack2Selector.GetItemIndex(1000));
    attack3Selector.Select(attack3Selector.GetItemIndex(1000));
    attack0Selector.SetItemDisabled(attack0Selector.GetItemIndex(1000), true);
    attack1Selector.SetItemDisabled(attack1Selector.GetItemIndex(1000), true);
    attack2Selector.SetItemDisabled(attack2Selector.GetItemIndex(1000), true);
    attack3Selector.SetItemDisabled(attack3Selector.GetItemIndex(1000), true);
    
    // find allowed options
    beast = beastOptions[beastSelector.GetSelectedId().ToString()] as Dictionary;
    modifier = modifierOptions[modifierSelector.GetSelectedId().ToString()] as Dictionary;
    beastGArray = (Godot.Collections.Array)beast["attacks"];
    modifierGArray = (Godot.Collections.Array)modifier["attacks"];
    attacksAllowed = new int[beastGArray.Count + modifierGArray.Count];
    for (i = 0; i < attacksAllowed.Length; i++) attacksAllowed[i] = -1;
    for (i = 0; i < beastGArray.Count; i++) attacksAllowed[i] = (int)(float)beastGArray[i];
    for (j = 0; j < modifierGArray.Count; j++) if (IsInArray(attacksAllowed, (int)(float)modifierGArray[j]) == 0) attacksAllowed[i + j] = (int)(float)modifierGArray[j];

    // add options
    used = new int[attacksAllowed.Length];
    for (i = 0; i < used.Length; i++) used[i] = -1;
    for (i = 0; i < attacksAllowed.Length; i++) {
      if (IsInArray(used, i) == 0 && i != -1) {
        used[i] = attacksAllowed[i];
        attack = attackOptions[attacksAllowed[i].ToString()] as Dictionary;
        attack0Selector.AddItem((String) attack["name"], attacksAllowed[i]);
        attack1Selector.AddItem((String) attack["name"], attacksAllowed[i]);
        attack2Selector.AddItem((String) attack["name"], attacksAllowed[i]);
        attack3Selector.AddItem((String) attack["name"], attacksAllowed[i]);
      }
    }
  }

  // enables the Go button when all selections are valid
  private void areSelectionsValid() {
    bool badBeast, badModifier, badAttack0, badAttack1, badAttack2, badAttack3;

    badBeast = beastSelector.IsItemDisabled(beastSelector.GetItemIndex(beastSelector.GetSelectedId()));
    badModifier = modifierSelector.IsItemDisabled(modifierSelector.GetItemIndex(modifierSelector.GetSelectedId()));
    badAttack0 = attack0Selector.IsItemDisabled(attack0Selector.GetItemIndex(attack0Selector.GetSelectedId()));
    badAttack1 = attack1Selector.IsItemDisabled(attack1Selector.GetItemIndex(attack1Selector.GetSelectedId()));
    badAttack2 = attack2Selector.IsItemDisabled(attack2Selector.GetItemIndex(attack2Selector.GetSelectedId()));
    badAttack3 = attack3Selector.IsItemDisabled(attack3Selector.GetItemIndex(attack3Selector.GetSelectedId()));

    goButton.Disabled = badBeast || badModifier || badAttack0 || badAttack1 || badAttack2 || badAttack3 ? true : false;
  }
  
  private void InitMenus()
  {
    attack0Prev = -1;
    attack1Prev = -1;
    attack2Prev = -1;
    attack3Prev = -1;

    // Create selection options
    MakeBeastSelection();
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    g = (Globals)GetNode("/root/Gm");

    goButton = GetNode<BaseButton>("Go");
    goButton.Disabled = true;
    
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    
    beastSelector = GetNode<OptionButton>("Beasts");
    modifierSelector = GetNode<OptionButton>("Modifier");
    
    /* You can't make arrays of structs in c# */
    attack0Selector = GetNode<OptionButton>("Attack0");
    attack1Selector = GetNode<OptionButton>("Attack1");
    attack2Selector = GetNode<OptionButton>("Attack2");
    attack3Selector = GetNode<OptionButton>("Attack3");
    
    player = GetNode<Sprite>("Beast Preview");
    player.Position = new Vector2(500, 300);
    player.Scale = new Vector2(6, 6);

    // beastSelector.connect("ItemSelected", this, "OnBeastSelected");
    
    InitMenus();
  }
  
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

  /* Signals */
  
  private void _on_Beasts_item_selected(int index)
  {    
    Dictionary beast;

    MakeModifierSelection();
    areSelectionsValid();
    
    beast = beastOptions[beastSelector.GetSelectedId().ToString()] as Dictionary;
    tex = ResourceLoader.Load((String)beast["texture"]) as Texture;
  
    player.Texture = tex;
    
    /* Play menu sound */
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  /* These all just play the menu sound */
  private void _on_Modifier_item_selected(int index)
  {
    MakeAttackSelection();
    areSelectionsValid();

    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack0_item_selected(int index)
  {
    areSelectionsValid();

    if (attack0Prev != -1) {
      attack0Selector.SetItemDisabled(attack0Prev, false);
      attack1Selector.SetItemDisabled(attack0Prev, false);
      attack2Selector.SetItemDisabled(attack0Prev, false);
      attack3Selector.SetItemDisabled(attack0Prev, false);
    }
    attack1Selector.SetItemDisabled(index, true);
    attack2Selector.SetItemDisabled(index, true);
    attack3Selector.SetItemDisabled(index, true);
    attack0Prev = index;

    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack1_item_selected(int index)
  {
    areSelectionsValid();

    if (attack1Prev != -1) {
      attack0Selector.SetItemDisabled(attack1Prev, false);
      attack1Selector.SetItemDisabled(attack1Prev, false);
      attack2Selector.SetItemDisabled(attack1Prev, false);
      attack3Selector.SetItemDisabled(attack1Prev, false);
    }
    attack0Selector.SetItemDisabled(index, true);
    attack2Selector.SetItemDisabled(index, true);
    attack3Selector.SetItemDisabled(index, true);
    attack1Prev = index;

    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack2_item_selected(int index)
  {
    areSelectionsValid();

    if (attack2Prev != -1) {
      attack0Selector.SetItemDisabled(attack2Prev, false);
      attack1Selector.SetItemDisabled(attack2Prev, false);
      attack2Selector.SetItemDisabled(attack2Prev, false);
      attack3Selector.SetItemDisabled(attack2Prev, false);
    }
    attack0Selector.SetItemDisabled(index, true);
    attack1Selector.SetItemDisabled(index, true);
    attack3Selector.SetItemDisabled(index, true);
    attack2Prev = index;
    
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Attack3_item_selected(int index)
  {
    areSelectionsValid();

    if (attack3Prev != -1) {
      attack0Selector.SetItemDisabled(attack3Prev, false);
      attack1Selector.SetItemDisabled(attack3Prev, false);
      attack2Selector.SetItemDisabled(attack3Prev, false);
      attack3Selector.SetItemDisabled(attack3Prev, false);
    }
    attack0Selector.SetItemDisabled(index, true);
    attack1Selector.SetItemDisabled(index, true);
    attack2Selector.SetItemDisabled(index, true);
    attack3Prev = index;

    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
  }
  
  private void _on_Back_pressed()
  {
    /* Plays sound */
    se.Stream = ResourceLoader.Load("res://Assets/Music/BackSound.tres") as AudioStream;
    se.Play();
    
    GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  }
  
  private void _on_Go_pressed()
  {    
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


