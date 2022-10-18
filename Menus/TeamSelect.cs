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
  
  private OptionButton beasts, type;
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
      return beastOptions;
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
      return modifierOptions;
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
      return attackOptions;
    }
  }
  
  private void InitMenus()
  {
    int i;
    Dictionary beast;

    /* Add beast selections */

    GD.Print("test1");

    for (i = 0; i < beastOptions.size(); i++) {
      GD.Print("test2");
      beast = beastOptions[i.ToString()] as Dictionary;
      GD.Print("test3");
      beasts.AddItem((String) beast["name"]);
      GD.Print("test4");
    }

    GD.Print("test5");
    
    //beasts.AddItem("Alzrius"); beasts.AddItem("Auril"); beasts.AddItem("Solanac");
    
    /* Add modifier selections */
    
    type.AddItem("Fire"); type.AddItem("Ice"); type.AddItem("Poison");
    
    /* Add attack selections */
    
    /* There is a better way of doing this. This is just
       for temporary to see how the menu looks */
    attack0.AddItem("Attack 1"); attack0.AddItem("Attack 2");
    attack0.AddItem("Attack 3"); attack0.AddItem("Attack 4");
    attack1.AddItem("Attack 1"); attack1.AddItem("Attack 2");
    attack1.AddItem("Attack 3"); attack1.AddItem("Attack 4");
    attack2.AddItem("Attack 1"); attack2.AddItem("Attack 2");
    attack2.AddItem("Attack 3"); attack2.AddItem("Attack 4");
    attack3.AddItem("Attack 1"); attack3.AddItem("Attack 2");
    attack3.AddItem("Attack 3"); attack3.AddItem("Attack 4");
  }

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    beasts = GetNode<OptionButton>("Beasts");
    type = GetNode<OptionButton>("Type");
    
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

    GD.Print("here1");
    
    // beasts.connect("ItemSelected", this, "OnBeastSelected");
    
    GD.Print("here2");
    InitMenus();
    GD.Print("here3");
  }
  
  

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

  
  private void _on_Beasts_item_selected(int index)
  {
    string s;
    
    s = beasts.GetItemText(beasts.GetSelectedId());
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
