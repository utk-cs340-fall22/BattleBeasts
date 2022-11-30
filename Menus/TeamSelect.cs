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
  private VBoxContainer AttacksList;
  public PackedScene Attack;
  private BaseButton goButton;
  private Sprite player;

  private static Dictionary _beastOptions = null;
  private static Dictionary _modifierOptions = null;
  private static Dictionary _attackOptions = null;
  
  private AudioStreamPlayer se;
  
  Globals g;
  Transition t;
  
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
    modifierSelector.AddItem("Modifier", 1000);
    modifierSelector.Select(modifierSelector.GetItemIndex(1000));
    modifierSelector.Disabled = true;
  }

  private void MakeModifierSelection() {
    int i, selectedBeastIndex;
    int[] modifiersAllowed;
    Godot.Collections.Array beastGArray;
    Dictionary beast, modifier;

    // set up default selector values for attacks then disable
    attack0Prev = -1;
    attack1Prev = -1;
    attack2Prev = -1;
    attack3Prev = -1;
    
    // remove old attacks
    foreach (Control n in AttacksList.GetChildren()){
      AttacksList.RemoveChild(n);
      n.QueueFree();  
    }
    
    // enable modifier selector, disable attack selectors, set up up modifier selector, get selected beast index
    modifierSelector.Clear();
    modifierSelector.Disabled = false;
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
    
    // remove old moves
    foreach (Control n in AttacksList.GetChildren()){
      AttacksList.RemoveChild(n);
      n.QueueFree();  
    }
    
    attack0Prev = -1;
    attack1Prev = -1;
    attack2Prev = -1;
    attack3Prev = -1;
    
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
      if (IsInArray(used, attacksAllowed[i]) == 0 && attacksAllowed[i] != -1) {
        used[i] = attacksAllowed[i];
        attack = attackOptions[attacksAllowed[i].ToString()] as Dictionary;
        
        var AttackInstance = (Control) Attack.Instance();
        AttackInstance.Call("setup_AttackNode",(String) attack["name"],Convert.ToInt32(attack["strike_damage"]),Convert.ToInt32(attack["strike_count"]),(String) attack["type"],attacksAllowed[i],this);
        AttacksList.AddChild(AttackInstance);
      }
    }
  }

  // enables the Go button when all selections are valid
  private void areSelectionsValid() {
    bool badBeast, badModifier, badAttack0, badAttack1, badAttack2, badAttack3;

    badBeast = beastSelector.IsItemDisabled(beastSelector.GetItemIndex(beastSelector.GetSelectedId()));
    badModifier = modifierSelector.IsItemDisabled(modifierSelector.GetItemIndex(modifierSelector.GetSelectedId()));
    badAttack0 = (attack0Prev == -1);
    badAttack1 = (attack1Prev == -1);
    badAttack2 = (attack2Prev == -1);
    badAttack3 = (attack3Prev == -1);

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
    t = (Transition)GetNode("/root/Transition");

    goButton = GetNode<BaseButton>("Go");
    goButton.Disabled = true;
    
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    
    beastSelector = GetNode<OptionButton>("Beasts");
    modifierSelector = GetNode<OptionButton>("Modifier");
    
    /* You can't make arrays of structs in c# */
    AttacksList = GetNode<VBoxContainer>("Attacks/List");
    Attack = GD.Load<PackedScene>("res://Menus/AttackNode.tscn");
    
    player = GetNode<Sprite>("Beast Preview");
    player.Position = new Vector2(200, 300);
    player.Scale = new Vector2(6, 6);

    // beastSelector.connect("ItemSelected", this, "OnBeastSelected");
    
    InitMenus();
  }

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
  
  // handles attack selection
  public bool _on_Attack_selected(int index)
  {
    int i,roomi = -1;
    bool rest = true;
    bool hasroom = false;
    bool alreadyselected = false;
    int alreadyselectedi = -1;
    int[] nums = new int[4];
    
    nums[0] = attack0Prev;
    nums[1] = attack1Prev;
    nums[2] = attack2Prev;
    nums[3] = attack3Prev;
    
    for(i = 0; i < 4; i++){
      if(nums[i] == -1) hasroom = true;
      if(nums[i] == index){
        alreadyselected = true;
        alreadyselectedi = i;
      }
    }
    
    if(alreadyselected){
      nums[alreadyselectedi] = -1;
      rest = false;
      hasroom = true;
    }
    
    if(rest && hasroom){
        for(i = 0; i < 4; i++){
          if(nums[i] == -1){
            roomi = i;
            break;
          }  
        }
        
        nums[roomi] = index;
    }else if(!hasroom){
      return false;
    }
    
    attack0Prev = nums[0];
    attack1Prev = nums[1];
    attack2Prev = nums[2];
    attack3Prev = nums[3];
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    areSelectionsValid();
    return true;
  }
  
  private async void _on_Back_pressed()
  {
    /* Plays sound */
    se.Stream = ResourceLoader.Load("res://Assets/Music/BackSound.tres") as AudioStream;
    se.Play();
    
    await t.ChangeScene("res://Menus/MainMenu.tscn");
  }
  
  private async void _on_Go_pressed()
  {    
    g.playerBeastIndex = beastSelector.GetSelectedId();
    g.playerModifierIndex = modifierSelector.GetSelectedId();
    g.playerAttackIndices[0] = attack0Prev;
    g.playerAttackIndices[1] = attack1Prev;
    g.playerAttackIndices[2] = attack2Prev;
    g.playerAttackIndices[3] = attack3Prev;
    
    /* Play sound effect */
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
    se.Play();
    
    await t.ChangeScene("res://Bracket/Bracket.tscn");
  }
}
