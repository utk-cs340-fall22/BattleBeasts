using Godot;
using System;
using Godot.Collections;


public class Bracket: Node2D
{
  Globals g;
  int size = 0;
  Sprite user, other;
  Transition t;

  Texture tex;

  //Will be used for .json file
  private static Dictionary _beastsOps = null;
  private static Dictionary _opponentsOps = null;
  private static Dictionary _attackOps = null;
  private static Dictionary _modifierOps = null;

  private AudioStreamPlayer music, musicP, se;

  private Dictionary opponentsOps 
  {
    get {
      if (_opponentsOps == null) {
        var file = new File();
        file.Open("res://Data/Opponents.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _opponentsOps = JSON.Parse(text).Result as Dictionary;
      }
      return _opponentsOps;
    }
  }

  private Dictionary beastsOps 
  {
    get {
      if (_beastsOps == null) {
        var file = new File();
        file.Open("res://Data/Beasts.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _beastsOps = JSON.Parse(text).Result as Dictionary;
      }
      return _beastsOps;
    }
  }

  private Dictionary attackOptions 
  {
    get {
      if (_attackOps == null) {
        var file = new File();
        file.Open("res://Data/Attacks.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _attackOps = JSON.Parse(text).Result as Dictionary;
      }
      return _attackOps;
    }
  }
  
  private Dictionary modifierOps 
  {
    get {
      if (_modifierOps == null) {
        var file = new File();
        file.Open("res://Data/Modifiers.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _modifierOps = JSON.Parse(text).Result as Dictionary;
      }
      return _modifierOps;
    }
  }
  /*                      */
  private void get_curr_beast(Dictionary beasts)
  {
    select_beast("Other", g.currBeast, true);
  }

  /* Draws a circle around the two opposing beasts */
  public void DrawCircleArc(Vector2 center, float radius, float angleFrom, float angleTo, Color color)
  {
    int nbPoints = 32;
    var pointsArc = new Vector2[nbPoints];

    for (int i = 0; i < nbPoints; ++i)
    {
      float anglePoint = Mathf.Deg2Rad(angleFrom + i * (angleTo - angleFrom) / nbPoints - 90f);
      pointsArc[i] = center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius;
    }

    for (int i = 0; i < nbPoints - 1; ++i)
      DrawLine(pointsArc[i], pointsArc[i + 1], color);
}

  /* At the end of the game, resets all values to default */
    private void reset_all() 
  {
    g.name = "Player";
    for (int i = 0; i < 7; i++) g.oppName[i] = "CPU";
    for (int i = 0; i < 7; i++) g.oppBeast[i] = -1;
    g.playerBeastIndex = -1;
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
  private void get_random_beast(Dictionary opponents, int opp) 
  {
    Dictionary oppBeast;
    Random rnd = new Random();
    int num = rnd.Next();
    
    opponents = opponentsOps[(num % opponentsOps.Count).ToString()] as Dictionary;
    oppBeast = beastsOps[(num % beastsOps.Count).ToString()] as Dictionary;
    g.oppName[opp] = (String) opponents["name"];
    g.oppBeast[opp] = Int32.Parse((String) opponents["beast"]);
    g.oppMods[opp] = rnd.Next() % modifierOps.Count;

  }

  /* Generates the modifier and attacks for each opponenent */
  private void define_opponent(int opp)
  {
    int[] attacksAllowed, used;
    Godot.Collections.Array beastGArray, modifierGArray;
    Dictionary beast, modifier, attack;
    Random rnd = new Random();
    int num = rnd.Next();
    int i, j;
    beast = beastsOps[opp.ToString()] as Dictionary;
    modifier = modifierOps[opp.ToString()] as Dictionary;
    beastGArray = (Godot.Collections.Array)beast["attacks"];
    modifierGArray = (Godot.Collections.Array)modifier["attacks"];
    attacksAllowed = new int[beastGArray.Count + modifierGArray.Count];
    
    for (i = 0; i < attacksAllowed.Length; i++) attacksAllowed[i] = -1;
    for (i = 0; i < beastGArray.Count; i++) attacksAllowed[i] = (int)(float)beastGArray[i];
    for (j = 0; j < modifierGArray.Count; j++) if (IsInArray(attacksAllowed, (int)(float)modifierGArray[j]) == 0) attacksAllowed[i + j] = (int)(float)modifierGArray[j];
   /*
    used = new int[attacksAllowed.Length];
    for (i = 0; i < used.Length; i++) used[i] = -1;
    for (i = 0; i < attacksAllowed.Length; i++) {
      if (IsInArray(used, i) == 0 && i != -1) {   
        used[i] = attacksAllowed[i];
        attack = attackOptions[attacksAllowed[i].ToString()] as Dictionary;
      }
    } */
    //attack = (Godot.Collections.Dictionary)beast["attacks"];

    for (i = 0; i < 4; i++) {
      num = rnd.Next();
      int index = num % attacksAllowed.Length;
      g.oppAttacks[opp,i] = attacksAllowed[num % attacksAllowed.Length];
      GD.Print("Inside the loop " +g.oppAttacks[opp,i]);
      for (j = index-1; j<attacksAllowed.Length - 1; j++){
        attacksAllowed[j] = attacksAllowed[j + 1];
      }      
    }
    GD.Print((String) beast["name"] + ": " + g.oppAttacks[opp,0] + " " + g.oppAttacks[opp, 1] + " " + g.oppAttacks[opp, 2] + " " + g.oppAttacks[opp, 3]);
  }
  /* Used for attack selection */
  private int IsInArray(int[] array, int num) {
    foreach (int i in array) if (i == num) return 1;
    return 0;
  }

  /* This is what the user is greeted with when first entering the bracket */
  private void display_welcome() 
  {
    hideall();
    GetNode<Button>("Small").Text = "Small";
    GetNode<Button>("Big").Text ="Big";
    GetNode<Label>("Welcome").Text = "Hi " + g.name + "! Do you want to enter the small or big tournament?";
  }
  
  /* This function gets the player's name and beast from globals.cs and initializes them in bracket */
  private void initialize_player(Globals g, Dictionary beasts) 
  {
    if (g.level == 0) beasts = beastsOps[(g.playerBeastIndex).ToString()] as Dictionary;
    select_beast("Sprite", g.playerBeastIndex, true);
    select_beast("Player", g.playerBeastIndex, true);
    GetNode<Label>("Sprite/Name").Text = g.name;
    GetNode<Label>("Sprite/Name").Show();
    GetNode<Sprite>("Sprite").Position = new Vector2(90 + 100*g.level, 55+50*g.level);
    
  }

  //This gets the opponents that were randomly generated and initializes their names and beasts
  private void initialize_opponents(Globals g) 
  {

    for (int i = 0; i < 7; i++) {
      select_beast("Sprite" + (i+2).ToString(), i, false);
      GetNode<Label>("Sprite" + (i+2).ToString() + "/Name").Text = g.oppName[i];
      GetNode<Label>("Sprite" + (i+2).ToString() + "/Name").Show();
      if (g.level == 0) GetNode<Sprite>("Sprite" + (i+2).ToString()).Hide();
      if (g.level == 0) GetNode<Sprite>("Sprite" + (i+2).ToString()).Position = new Vector2(110, 40+ 50*(i+2));
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
  private void show_on_bracket() 
  {
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

    private void StartMusic(){
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
  public override void _Ready() 
  {

    GetNode<TextureRect>("Background").Texture = ResourceLoader.Load("res://Assets/tournament.png") as Texture;
    //I hide all sprites to begin with
    hide_sprites();
    
    Dictionary opponents = null;
    Dictionary beasts = null;
    Dictionary modifiers = null;
    
    GetNode<Button>("Exit").Hide();
    
    //initialize globals
    g = (Globals)GetNode("/root/Gm");
    t = (Transition)GetNode("/root/Transition");

    //Display welcome if it's the first time
    if (g.bracketSize == -1) {
      display_welcome();
    }

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
      }
    }
    //then initialize them
    initialize_opponents(g);
    if (g.level == 0) g.currBeast = g.oppBeast[0];
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
  private void Won()
  {
    GetNode<Sprite>("Sprite").Position = new Vector2(100 + 100*(g.level+1), 50+50*(g.level+1));

    if (g.level == 0) {

      GetNode<Sprite>("Sprite3").Position = new Vector2(120 + 100*(g.level+1), 180 + 50*(g.level+1));
      g.currBeast = g.oppBeast[1];
      if (g.bracketSize == 1) {
        GetNode<Sprite>("Sprite5").Position = new Vector2(120 + 100*(g.level+1), 210 + 100*(g.level+1));
        GetNode<Sprite>("Sprite7").Position = new Vector2(120 + 100*(g.level+1), 250 + 150*(g.level+1));
      }

    }
  
    if (g.level == 1 && g.bracketSize == 1) {
       g.currBeast = g.oppBeast[6];
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

  private void _on_Continue_pressed()
  {
    t.ChangeScene2("res://Fight/Fight.tscn");
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

  private void _on_Exit_pressed()
  {
    GetNode<Sprite>("Sprite").Position = new Vector2(0,0);
    reset_all();
    GetNode<Sprite>("Sprite").Hide();
    t.ChangeScene("res://Menus/TitleMenu.tscn");
    
  }

  /* Sets the beasts texture */
  private void select_beast(string sprite, int opp, bool player) 
  {
    Dictionary beast;
    int pick;

    if (player) pick = opp;
    else pick = g.oppBeast[opp];
    
    beast = beastsOps[pick.ToString()] as Dictionary;
    GetNode<Sprite>(sprite).Texture = ResourceLoader.Load((String)beast["texture"]) as Texture;
  }
}
