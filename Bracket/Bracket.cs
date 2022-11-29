using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Bracket: Node2D
{
  Globals g;
  int size = 0;
  Transition t;

  //Will be used for .json file
  private static Dictionary _opponentOptions = null;
  private static Dictionary _beastOptions = null;
  private static Dictionary _attackOptions = null;
  private static Dictionary _modifierOptions = null;

  private AudioStreamPlayer music, musicP, se;

  private Dictionary opponentOptions {
    get {
      if (_opponentOptions == null) {
        var file = new File();
        file.Open("res://Data/Opponents.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _opponentOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _opponentOptions;
    }
  }

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
  
  private Dictionary modifierOptions {
    get {
      if (_modifierOptions == null) {
        var file = new File();
        file.Open("res://Data/Modifiers.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _modifierOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _modifierOptions;
    }
  }

  /**/
  private void get_curr_beast(Dictionary beasts) {
    select_beast("Other", g.currBeast, true);
  }

  /* Draws a circle around the two opposing beasts */
  public void DrawCircleArc(Vector2 center, float radius, float angleFrom, float angleTo, Color color) {
    int nbPoints = 32;
    var pointsArc = new Vector2[nbPoints];

    for (int i = 0; i < nbPoints; ++i) {
      float anglePoint = Mathf.Deg2Rad(angleFrom + i * (angleTo - angleFrom) / nbPoints - 90f);
      pointsArc[i] = center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius;
    }

    for (int i = 0; i < nbPoints - 1; ++i) DrawLine(pointsArc[i], pointsArc[i + 1], color);
  }

  /* At the end of the game, resets all values to default */
  private void reset_all() {
    g.name = "Player";
    for (int i = 0; i < 7; i++) g.oppName[i] = "CPU";
    for (int i = 0; i < 7; i++) g.oppBeast[i] = -1;
    g.playerBeastIndex = 0;
    g.level = 0;
    g.bracketSize = -1;
    g.fightOutcome = -1;
  }

  /* This function hides/shows certain things when the buttons ("big" or "small") are pressed */
  private void for_button() 
  {
    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Label>("Welcome").Hide();
    GetNode<Sprite>("Sprite").Show();
    GetNode<Button>("Continue").Show();
  }

  /* This function hides the continue and exit button */
  public void hideall() 
  {
    GetNode<Button>("Continue").Hide();
    GetNode<Sprite>("Sprite").Hide();
    GetNode<Button>("Exit").Hide();
  }
  
  /* This displays/hides certain things when the player wins */
  public void display_win() 
  {
    GetNode<Button>("Continue").Hide();
    GetNode<Label>("Welcome").Text = "You Win! Press exit to return to title menu.";
    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Button>("Exit").Show();
    GetNode<Button>("Exit").Text = "Exit";
    GetNode<Sprite>("Sprite").Show();
    hide_sprites();
  }

 /* Use RNG to select opponents for the bracket */
  private void get_random_beast(Dictionary opponents, int opp) {
    int choice;
    
    // choose name
    do {
      choice = (int)(GD.Randi() % opponentOptions.Count);
      opponents = opponentOptions[choice.ToString()] as Dictionary;
    } while (IsInArrayS(g.oppName, (String)opponents["name"]) == 1);
    g.oppName[opp] = (String)opponents["name"];

    // choose beast
    choice = (int)(GD.Randi() % beastOptions.Count);
    g.oppBeast[opp] = choice;
  }

  /* For 1 dimensional arrays. Returns 0 if num isn't in array. Returns 1 if num is in array */
  private int IsInArray(int[] array, int num) {
    foreach (int i in array) if (i == num) return 1;
    return 0;
  }

  private int IsInArrayS(string[] array, string str) {
    foreach (string i in array) if (i == str) return 1;
    //for (int i = 0; i < array.Length; i++) if 
    return 0;
  }

  /* For one subarray index in 2 dimensional arrays. Returns 0 if num isn't in subarray. Returns 1 if num is in subarray */
  private int IsInArray2(int[,] array, int index, int num) {
    for (int i = 0; i < array.GetLength(1); i++) if (array[index, i] == num) return 1;
    return 0;
  }

  /* Assign allowed modifiers and four unique attacks to an opposing beast */
  private void CustomizeOpponent(int index) {
    int i, choice;
    int[] modifiersAllowed, attacksAllowed;
    Dictionary beast, modifier;
    Godot.Collections.Array modifiersGArray, beastAttacksGArray, modifierAttacksGArray;

    // assign random allowed modifier
    beast = beastOptions[g.oppBeast[index].ToString()] as Dictionary;
    modifiersGArray = (Godot.Collections.Array)beast["modifiers"];
    modifiersAllowed = new int[modifiersGArray.Count];
    for (i = 0; i < modifiersAllowed.Length; i++) modifiersAllowed[i] = (int)(float)modifiersGArray[i];
    g.oppMods[index] = modifiersAllowed[GD.Randi() % modifiersAllowed.Length];

    // find allowed attacks
    modifier = modifierOptions[g.oppMods[index].ToString()] as Dictionary;
    beastAttacksGArray = (Godot.Collections.Array)beast["attacks"];
    modifierAttacksGArray = (Godot.Collections.Array)modifier["attacks"];
    attacksAllowed = new int[beastAttacksGArray.Count + modifierAttacksGArray.Count];
    for (i = 0; i < beastAttacksGArray.Count + modifierAttacksGArray.Count; i++) attacksAllowed[i] = -1;
    for (i = 0; i < beastAttacksGArray.Count; i++) attacksAllowed[i] = (int)(float)beastAttacksGArray[i];
    for (i = 0; i < modifierAttacksGArray.Count; i++) {
      if (IsInArray(attacksAllowed, (int)(float)modifierAttacksGArray[i]) == 0) {
        attacksAllowed[beastAttacksGArray.Count + i] = (int)(float)modifierAttacksGArray[i];
      }
    }

    // assign four random unique allowed attacks
    for (i = 0; i < 4; i++) g.oppAttacks[index, i] = -1;
    for (i = 0; i < 4; i++) {
      // find unique attack (not already chosen)
      for (choice = attacksAllowed[GD.Randi() % attacksAllowed.Length]; IsInArray2(g.oppAttacks, index, choice) == 1 || choice == -1; choice = attacksAllowed[GD.Randi() % attacksAllowed.Length]);

      // assign unique attack
      g.oppAttacks[index, i] = choice;
    }
  }

  /* This is what the user is greeted with when first entering the bracket */
  private void display_welcome() {
    hideall();
    GetNode<Button>("Small").Text = "Regular";
    GetNode<Button>("Big").Text = "Ultimate";
    GetNode<Label>("Welcome").Text = "Hi " + g.name + "! Choose your bracket!";
  }
  
  /* This function gets the player's name and beast from globals.cs and initializes them in bracket */
  private void initialize_player(Globals g, Dictionary beasts) {
    if (g.level == 0) beasts = beastOptions[(g.playerBeastIndex).ToString()] as Dictionary;
    select_beast("Sprite", g.playerBeastIndex, true);
    select_beast("Player", g.playerBeastIndex, true);
    GetNode<Label>("Sprite/Name").Text = g.name;
    GetNode<Label>("Sprite/Name").Show();
    GetNode<Sprite>("Sprite").Position = new Vector2(90 + 100*g.level, 55+50*g.level);
  }

  //This gets the opponents that were randomly generated and initializes their names and beasts
  private void initialize_opponents(Globals g) {
    string name;
    
    for (int i = 0; i < 7; i++) {
      name = "Sprite" + (i+2).ToString();
      select_beast(name, i, false);
      GetNode<Label>(name + "/Name").Text = g.oppName[i];
      GetNode<Label>(name + "/Name").Show();
      if (g.level == 0) {
        GetNode<Sprite>(name).Hide();
        GetNode<Sprite>(name).Position = new Vector2(110, 40+ 50*(i+2));
      }
    }
  }

  //Function shows the sprites based on which bracket the player chooses
  private void show_sprites(int size) 
  {
    GetNode<Sprite>("Sprite").Show();
    GetNode<Sprite>("Sprite2").Show();
    GetNode<Sprite>("Sprite3").Show();
    GetNode<Sprite>("Sprite4").Show();
    GetNode<Sprite>("Player").Show();
    GetNode<Sprite>("Other").Show();

    if (size == 4) {
      GetNode<Sprite>("Sprite5").Show();
      GetNode<Sprite>("Sprite6").Show();
      GetNode<Sprite>("Sprite7").Show();
      GetNode<Sprite>("Sprite8").Show();
    }
  }
  
  //This function hides all the sprites
  private void hide_sprites() 
  {
    GetNode<Sprite>("Player").Hide();
    GetNode<Sprite>("Other").Hide();
    GetNode<Sprite>("Sprite2").Hide();
    GetNode<Sprite>("Sprite3").Hide();
    GetNode<Sprite>("Sprite4").Hide();
    GetNode<Sprite>("Sprite5").Hide();
    GetNode<Sprite>("Sprite6").Hide();
    GetNode<Sprite>("Sprite7").Hide();
    GetNode<Sprite>("Sprite8").Hide();
  }
  
  //Shows the beasts that one the last bracket depending on the level
  private void show_on_bracket() {
    if (g.level == 1) {
      GetNode<Sprite>("Sprite3").Show();
      select_beast("Other", 2, false);
      if (g.bracketSize == 1) {
        GetNode<Sprite>("Sprite5").Show();
        select_beast("Other", 4, false);
        GetNode<Sprite>("Sprite7").Show();
        select_beast("Other", 6, false);
      }else {
        select_beast("Other", 1, false);
       }
    }
    
    if (g.level >= 1) {
      GetNode<Sprite>("Player").Show();
      GetNode<Sprite>("Other").Show();
    }
  }

  private void StartMusic() {
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    music = g.GetNode<AudioStreamPlayer>("Music");
    musicP = g.GetNode<AudioStreamPlayer>("MusicPlayer");
    music.Stop();
    musicP.Stop();
    
    if (g.fightOutcome == 0)
      return;
    else if(g.level - 1 == 2 && g.bracketSize == 1)
      music.Stream = ResourceLoader.Load("res://Assets/Music/VictoryTheme.mp3") as AudioStream;
    else if (g.level - 1 == 1 && g.bracketSize == 0)
      music.Stream = ResourceLoader.Load("res://Assets/Music/VictoryTheme.mp3") as AudioStream;
    else
      music.Stream = ResourceLoader.Load("res://Assets/Music/BracketTheme.mp3") as AudioStream;

    music.Play();
  }

  //This is the function that gets called first
  public override void _Ready() {
    Dictionary opponents = null;
    Dictionary beasts = null;
    
    //initialize globals
    g = (Globals)GetNode("/root/Gm");
    t = (Transition)GetNode("/root/Transition");

    GD.Randomize();

    GetNode<TextureRect>("Background").Texture = ResourceLoader.Load("res://Assets/tournament.png") as Texture;
    //I hide all sprites to begin with
    hide_sprites();
    
    GetNode<Button>("Exit").Hide();

    //Display welcome if it's the first time
    if (g.bracketSize == -1) display_welcome();

    //Set size depending on which bracket they chose
    if (g.bracketSize == 0) {
      size = 2;
      GetNode<Button>("Small").Hide();
      GetNode<Button>("Big").Hide();
      Update();
    } else if (g.bracketSize == 1) {
      GetNode<Button>("Small").Hide();
      GetNode<Button>("Big").Hide();
      size = 4;
      Update();
    }
    
    initialize_player(g, beasts);
    
    //Tests to see if player won or lost
    if (g.fightOutcome == 1) Won();
    else if (g.fightOutcome == 0) Lost();
    
    //if it's the first time, create random beasts
    if (g.level == 0) {
      for (int i = 0; i < 7; i++) {
        get_random_beast(opponents, i);
        CustomizeOpponent(i);
      }
      // debug
      GD.Print("g.oppName.Length: ", g.oppName.Length);
      for (int i = 0; i < g.oppName.Length; i++) {
        GD.Print("i: ", i);
        GD.Print(g.oppName[i], " ", g.oppBeast[i], " ", g.oppMods[i]);
        for (int j = 0; j < 4; j++) {
          GD.Print(g.oppAttacks[i, j]);
        }
        GD.Print("");
      }
    }

    //then initialize them
    initialize_opponents(g);
    if (g.level == 0) {
      g.currBeast = g.oppBeast[0];
      g.currentOpponentIndex = 0;
    }

    get_curr_beast(beasts);
    
    show_on_bracket();
    
    StartMusic();
  }
  
 //This draws the bracket
  public override void _Draw()
  {
    //If a size hasn't been selected, don't do anything
    if (size == 0) return;

    //This creates the vectors necessary for making the bracket
    var points = new Vector2[100];
    var points2 = new Vector2[100];
    var points3 = new Vector2[100];
    var points4 = new Vector2[2];
    var left = new Vector2(500, 500);
    var right = new Vector2(875, 500);
    var center = new Vector2(650, 450);
    var color = new Color(1, 1, 1, 1 );
    var col = new Color(1, 1, 1, 1 );
    int i, levels;
    levels = size;
    
    GetNode<TextureRect>("TextureRect").RectPosition = center;
    GetNode<TextureRect>("TextureRect").Texture = ResourceLoader.Load("res://Assets/Logo.png") as Texture;

    DrawCircleArc(left, 80, 0, 380, col);
    DrawCircleArc(right, 80, 0, 380, col);
    GetNode<Sprite>("Player").Position = left;
    GetNode<Sprite>("Other").Position = right;
  
    /*Level 1*/
    for (i = 0; i < levels; i++) {
      points[i*6]     = new Vector2(100 , 100 + i * 100);
      points[i*6 + 1] = new Vector2(200, 100 + i * 100);
      points[i*6 + 2] = new Vector2(100, 150 + i * 100);
      points[i*6 + 3] = new Vector2(200, 150 + i * 100);
      points[i*6 + 4] = new Vector2(200, 100 + 100* i);
      points[i*6 + 5] = new Vector2(200, 150 + 100 * i);
    }

    levels = levels / 2;
    
    /*Level 2*/
    for (i = 0; i <  levels; i++) {
      points2[i*6]   = new Vector2(200, 125 + i*200);
      points2[i*6+1] = new Vector2(300, 125 + i*200);
      points2[i*6+2] = new Vector2(200, 225 + i*200);
      points2[i*6+3] = new Vector2(300, 225 + i*200);
      points2[i*6+4] = new Vector2(300, 125 + i*200);
      points2[i*6+5] = new Vector2(300, 225 + i*200);
    }

    if (levels < 2) {
      points4[0] = new Vector2(300, 175);
      points4[1] = new Vector2(400, 175);

      DrawMultiline(points, color, (float) 15.0, false );
      DrawMultiline(points2, color, (float) 15.0, false );
      DrawMultiline(points3, color, (float) 15.0, false );
      DrawMultiline(points4, color, (float) 15.0, false );

      return;
    }
    
    levels = levels / 2;
    
    /*Level 3*/
    for (i = 0; i < levels; i++) {
      points3[i*6] = new Vector2(300, 175);
      points3[i*6+1] = new Vector2(400, 175);
      points3[i*6+2] = new Vector2(300, 375);
      points3[i*6+3] = new Vector2(400, 375);
      points3[i*6+4] = new Vector2(400, 175);
      points3[i*6+5] = new Vector2(400, 375);
    }

    /*LEVEL 4*/
    points4[0] = new Vector2(400, 275);
    points4[1] = new Vector2(500, 275);

    DrawMultiline(points, color, (float) 15.0, false );
    DrawMultiline(points2, color, (float) 15.0, false );
    DrawMultiline(points3, color, (float) 15.0, false );
    DrawMultiline(points4, color, (float) 15.0, false );
  }


  //This is called when the player won the last fight
  private void Won() {
    GetNode<Sprite>("Sprite").Position = new Vector2(100 + 100*(g.level+1), 50+50*(g.level+1));

    if (g.level == 0) {

      GetNode<Sprite>("Sprite3").Position = new Vector2(120 + 100*(g.level+1), 180 + 50*(g.level+1));
      g.currBeast = g.oppBeast[1];
      g.currentOpponentIndex = 1;
      if (g.bracketSize == 1) {
        GetNode<Sprite>("Sprite5").Position = new Vector2(120 + 100*(g.level+1), 210 + 100*(g.level+1));
        GetNode<Sprite>("Sprite7").Position = new Vector2(120 + 100*(g.level+1), 250 + 150*(g.level+1));
      }

    }
  
    if (g.level == 1 && g.bracketSize == 1) {
       g.currBeast = g.oppBeast[6];
       g.currentOpponentIndex = 5;
       GetNode<Sprite>("Sprite7").Position = new Vector2(120 + 100*(g.level+1), 350);
       GetNode<Sprite>("Sprite7").Show();
     }

    if (g.level == 2 && g.bracketSize == 1) {
      display_win();
    }
    
    if (g.level == 1 && g.bracketSize == 0) {
      display_win();
    }
    
    g.level++;
  }
  
  /* When the player loses, this function is called
     It makes the player exit back to the title menu */
  private void Lost()
  {
    g.name = "Player";
    g.level = 0;
    g.bracketSize = -1;
    g.fightOutcome = 0;
    GetNode<Button>("Exit").Show();
    GetNode<Button>("Exit").Text = "Exit";
    GetNode<Button>("Continue").Hide();
    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Label>("Welcome").Text = "You lose! Press exit to return to the title menu";
  }

  private async void _on_Continue_pressed()
  {
    await t.ChangeScene2("res://Fight/Fight.tscn");
  }

  private void _on_Big_pressed()
  {
    g.bracketSize = 1;
    size = 4;
    for_button();
    show_sprites(size);
    Update();
  }

  private void _on_Small_pressed()
  {
    g.bracketSize = 0;
    size = 2;
    for_button();
    show_sprites(size);
    Update();
  }

  private async void _on_Exit_pressed()
  {
    GetNode<Sprite>("Sprite").Position = new Vector2(0,0);
    reset_all();
    GetNode<Sprite>("Sprite").Hide();
    await t.ChangeScene("res://Menus/TitleMenu.tscn");
  }

  /* Sets the beasts texture */
  private void select_beast(string sprite, int opp, bool player) {
    Dictionary beast;
    int pick;

    if (player) pick = opp;
    else pick = g.oppBeast[opp];
    
    beast = beastOptions[pick.ToString()] as Dictionary;
    GetNode<Sprite>(sprite).Texture = ResourceLoader.Load((String)beast["texture"]) as Texture;
  }
}
