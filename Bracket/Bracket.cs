using Godot;
using System;
using Godot.Collections;


public class Bracket: Node2D
{
  Globals g;
  int size = 0;
  Sprite user, other;

  //Will be used for .json file
  private static Dictionary _beastsOps = null;
  private static Dictionary _opponentsOps = null;
  private static Dictionary _attackOptions = null;

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
  
  private void get_curr_beast(Dictionary beasts)
  {
    GD.Print(g.currBeast);
    select_beast("Other", g.currBeast, true);
    
   }
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

  //At the end of the game, this will be reset to default values.
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

  //This function hides/shows certain things when the buttons big or small are pressed
  private void for_button() 
  {
    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Label>("Welcome").Hide();
    GetNode<Sprite>("Sprite").Show();
    GetNode<Button>("Continue").Show();
  }

  //This function hides the continue and exit button
  public void hideall() 
  {
    GetNode<Button>("Continue").Hide();
    GetNode<Sprite>("Sprite").Hide();
    GetNode<Button>("Exit").Hide();

  }
  
  //This displays/hides certain things when the player wins
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

  //This function uses a random number generator to select a beast for the opponents
  private void get_random_beast(Dictionary opponents, int opp) 
  {
    Godot.Collections.Array attacks;
    Dictionary beast;
    Random rnd = new Random();
    int num = rnd.Next();
    opponents = opponentsOps[(num % 5).ToString()] as Dictionary;
    beast = beastsOps[(num % 5).ToString()] as Dictionary;
    attacks = (Godot.Collections.Array)beast["attacks"];
    g.oppName[opp] = (String) opponents["name"];
    g.oppBeast[opp] = Int32.Parse((String) opponents["beast"]);
    num = rnd.Next() % 3;
    g.oppMods[opp] = num;
    
    for (int i = 0; i < 4; i++) {
      num = rnd.Next();
      num %= 6;
      g.oppAttacks[opp,i] = (int)(float)attacks[num];
      for (int j = 0; j < i; j++) {
        if (g.oppAttacks[opp, i] == g.oppAttacks[opp, j]) {
          i--;
          break;
        }
      }
    }


    GD.Print((String) beast["name"] + ": " + g.oppAttacks[opp,0] + " " + g.oppAttacks[opp, 1] + " " + g.oppAttacks[opp, 2] + " " + g.oppAttacks[opp, 3]);
  }

  //This is what the user is greeted with when first entering the bracket
  private void display_welcome() 
  {
    hideall();
    GetNode<Button>("Small").Text = "Small";
    GetNode<Button>("Big").Text ="Big";
    GetNode<Label>("Welcome").Text = "Hi " + g.name + "! Do you want to enter the small or big tournament?";
  }
  
  //This function gets the player's name and beast from globals.cs and initializes them in bracket
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
      } else {
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
    
    GetNode<Button>("Exit").Hide();
    
    //initialize globals
    g = (Globals)GetNode("/root/Gm");

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
    var points4 = new Vector2[2];
    var points3 = new Vector2[100];
    var color = new Color((float) 0.941176, (float)0.972549, 1, 1 );
    int i, levels;
    levels = size;
    var left = new Vector2(500, 500);
    var right = new Vector2(875, 500);
    var center = new Vector2(650, 450);
    var col = new Color((float) 0.941176, (float)0.972549, 1, 1 );
    
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
  
  //When the player loses, this function is called
  //It makes the player exit back to the title menu
  private void Lost()
  {
    g.name = "Player";
    g.level = 0;
    g.bracketSize = -1;
    g.fightOutcome = 0;
    GetNode<Button>("Exit").Show();
    GetNode<Button>("Exit").Text = "Exit";
    GetNode<Button>("Continue").Hide();
    GetNode<Label>("Welcome").Text = "You lose! Press exit to return to the title menu";

  }

  private void _on_Continue_pressed()
  {
    GetTree().ChangeScene("res://Fight/Fight.tscn");
  }


  private void _on_Big_pressed()
  {
    g = (Globals)GetNode("/root/Gm");
    g.bracketSize = 1;
    size = 4;
    for_button();
    show_sprites(size);
    Update();
  }


  private void _on_Small_pressed()
  {
    g = (Globals)GetNode("/root/Gm");
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
    GetTree().ChangeScene("res://Menus/TitleMenu.tscn");
  }

  //This will be changed later to be cleaner (without switch statement)
  //But right now, this function is used to set the sprite's texture
  private void select_beast(string sprite, int opp, bool player) 
  {
    int pick;
    if (player) pick = opp;
    else pick = g.oppBeast[opp];
      
    
    switch (pick) {
      case 0:
        GetNode<Sprite>(sprite).Texture = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
        break;
      case 1:
        GetNode<Sprite>(sprite).Texture = ResourceLoader.Load("res://Assets/Character Sprites/Solanac-1.png") as Texture;
        break;
      case 2:
        GetNode<Sprite>(sprite).Texture = ResourceLoader.Load("res://Assets/Character Sprites/Alzrius-1.png") as Texture;
        break;
      case 3:
        GetNode<Sprite>(sprite).Texture = ResourceLoader.Load("res://Assets/Character Sprites/Glabbagool.png") as Texture;
        break;
      case 4:
        GetNode<Sprite>(sprite).Texture = ResourceLoader.Load("res://Assets/Character Sprites/Bunpir.png") as Texture;
        break;
      default:
        GetNode<Sprite>(sprite).Texture = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
        break;
    } 
  }

}
