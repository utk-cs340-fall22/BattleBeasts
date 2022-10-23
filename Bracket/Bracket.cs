using Godot;
using System;
using Godot.Collections;


public class Bracket: Node2D
{
  Globals g;
  int size = 0;
  string name;

  private static Dictionary _beastsOps = null;
  private static Dictionary _opponentsOps = null;

  private Dictionary opponentsOps {
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

  private Dictionary beastsOps {
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


  private void reset_all() {
    g.name = "Player";
    for (int i = 0; i < 7; i++) g.opp_name[i] = "CPU";
    for (int i = 0; i < 7; i++) g.opp_beast[i] = -1;
    g.player_beast = -1;
    g.level = 0;
    g.bracket_size = -1;
    g.fight_outcome = -1;
  }

  private void for_button() {
    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Label>("Welcome").Hide();
    GetNode<Sprite>("Sprite").Show();
    GetNode<Sprite>("Sprite2").Show();
    GetNode<Button>("Continue").Show();
  }

  public void hideall() {
    GetNode<Sprite>("Sprite").Hide();
    GetNode<Sprite>("Sprite2").Hide();
    GetNode<Button>("Continue").Hide();
    GetNode<Button>("Exit").Hide();

  }

  public void hideforwin() {
    GetNode<Button>("Continue").Hide();
  }

  public void display_win() {
    GetNode<Label>("Welcome").Text = "You Win! Press exit to return to title menu.";
    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Button>("Exit").Show();
    GetNode<Button>("Exit").Text = "Exit";
    GetNode<Sprite>("Sprite").Show();
    GetNode<Sprite>("Sprite2").Show();
    GetNode<Sprite>("Sprite3").Show();
  }


  private void get_random_beast(Dictionary opponents, int opp) {
    Random rnd = new Random();
    int num = rnd.Next();
    opponents = opponentsOps[(num % 5).ToString()] as Dictionary;
    g.opp_name[opp] = (String) opponents["name"];
    g.opp_beast[opp] = Int32.Parse((String) opponents["beast"]);
    if (g.opp_beast[opp] == g.player_beast) g.opp_beast[opp] = (g.opp_beast[opp] + 1)  % 5;
  }

  private void display_welcome() {
    hideall();
    GetNode<Button>("Small").Text = "Small";
    GetNode<Button>("Big").Text ="Big";
    GetNode<Label>("Welcome").Text = "Hi " + g.name + "! Do you want to enter the small or big tournament?";
  }
  
  private void initialize_player(Globals g, Dictionary beasts) {
    g.player_beast = 4;
    if (g.level == 0) beasts = beastsOps[(g.player_beast).ToString()] as Dictionary;
    select_beast("Sprite", g.player_beast);
    GetNode<Sprite>("Sprite").Texture = ResourceLoader.Load("res://Assets/Character Sprites/Bunpir.png") as Texture;
    GetNode<Label>("Sprite/Name").Text = g.name;
    GetNode<Label>("Sprite/Name").Show();
    GetNode<Sprite>("Sprite").Position = new Vector2(90 + 100*g.level, 55+50*g.level);
  }

  private void initialize_opponents(Globals g) {

    for (int i = 0; i < 7; i++) {
      select_beast("Sprite" + (i+2).ToString(), i);
      GetNode<Label>("Sprite" + (i+2).ToString() + "/Name").Text = g.opp_name[i];
      GetNode<Label>("Sprite" + (i+2).ToString() + "/Name").Show();
      GetNode<Sprite>("Sprite" + (i+2).ToString()).Hide();
      if (g.level == 0) GetNode<Sprite>("Sprite" + (i+2).ToString()).Position = new Vector2(110, 40+ 50*(i+2));

    }

    if (g.level == 1) {
      GetNode<Sprite>("Sprite3").Show();
      if (g.bracket_size == 1) {
        GetNode<Sprite>("Sprite5").Show();
        GetNode<Sprite>("Sprite7").Show();
      }
    }

    if (g.level == 2 && g.bracket_size == 1) {
      GetNode<Sprite>("Sprite7").Position = new Vector2(120 + 100*(g.level+1), 250 + 150*(g.level+1));
      GetNode<Sprite>("Sprite7").Show(); 
    }
  }

  private void show_sprites(int size) {
    GetNode<Sprite>("Sprite").Show();
    GetNode<Sprite>("Sprite2").Show();
    GetNode<Sprite>("Sprite3").Show();
    GetNode<Sprite>("Sprite4").Show();

    if (size == 4) {
      GetNode<Sprite>("Sprite5").Show();
      GetNode<Sprite>("Sprite6").Show();
      GetNode<Sprite>("Sprite7").Show();
      GetNode<Sprite>("Sprite8").Show();
    }

  }

  public override void _Ready() {
    Dictionary opponents = null;
    Dictionary beasts = null;
    GetNode<Button>("Exit").Hide();
    g = (Globals)GetNode("/root/Gm");

    if (g.bracket_size == -1) {
      display_welcome();
    }

    if (g.bracket_size == 0) {
      size = 2;
      GetNode<Button>("Small").Hide();
      GetNode<Button>("Big").Hide();
      Update();
    } else if (g.bracket_size == 1) {
      GetNode<Button>("Small").Hide();
      GetNode<Button>("Big").Hide();
      size = 4;
      Update();
    }


    initialize_player(g, beasts);

    if (g.fight_outcome == 1) Won();
    else if (g.fight_outcome == 0) Lost();

    if (g.level == 0) {
      for (int i = 0; i < 7; i++) {
        get_random_beast(opponents, i);
      }
    }
    initialize_opponents(g);

  }

  public override void _Draw()
  {
    if (size == 0) return;

    var points = new Vector2[100];
    var points2 = new Vector2[100];
    var points4 = new Vector2[2];
    var points3 = new Vector2[100];
    var color = new Color(0, 3, 1);
    int i, levels;
    levels = size;

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




  private void Won()
  {
    Dictionary opponents = null;
    GetNode<Sprite>("Sprite").Position = new Vector2(100 + 100*(g.level+1), 50+50*(g.level+1));

    if (g.level == 0) {
      GetNode<Sprite>("Sprite2").Hide();
      GetNode<Sprite>("Sprite3").Position = new Vector2(120 + 100*(g.level+1), 180 + 50*(g.level+1));
      if (g.bracket_size == 1) {
        GetNode<Sprite>("Sprite5").Position = new Vector2(120 + 100*(g.level+1), 210 + 100*(g.level+1));
        GetNode<Sprite>("Sprite7").Position = new Vector2(120 + 100*(g.level+1), 250 + 150*(g.level+1));
      }

    }



    if (g.level == 2 && g.bracket_size == 1) {
      display_win();
      hideforwin();


    }
    if (g.level == 1 && g.bracket_size == 0) {
      display_win();
      hideforwin();


    }
    g.level++;


  }

  private void Lost()
  {
    g.name = "Player";
    g.level = 0;
    g.bracket_size = -1;
    g.fight_outcome = 0;
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
    g.bracket_size = 1;
    size = 4;
    for_button();
    show_sprites(size);
    Update();
  }


  private void _on_Small_pressed()
  {
    g = (Globals)GetNode("/root/Gm");
    g.bracket_size = 0;
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

  private void select_beast(string sprite, int opp) {
    switch (g.opp_beast[opp]) {
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
