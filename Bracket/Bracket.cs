using Godot;
using System;
using Godot.Collections;
//using System.Collections.Generic;


public class Bracket: Node2D
{
  Globals g;
  int size = 0;
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
  
  private void select_beast(string sprite) {
    switch (g.opp_beast) {
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

private void get_random_beast(Dictionary opponents) {
      Random rnd = new Random();
      int num = rnd.Next();
      opponents = opponentsOps[(num % 5).ToString()] as Dictionary;
      g.opp_name = (String) opponents["name"];
      g.opp_beast = Int32.Parse((String) opponents["beast"]);
 }

  public override void _Ready() {
    
    Dictionary opponents = null;
    Dictionary beasts = null;    
    
    GetNode<Button>("Exit").Hide();

    g = (Globals)GetNode("/root/Gm");

    GetNode<Label>("Sprite/Name").Text = g.name;
    GetNode<Label>("Sprite/Name").Show();
    GetNode<Label>("Sprite2/Name").Show();
    GetNode<Sprite>("Sprite").Position = new Vector2(100 + 100*g.level, 50+50*g.level);
    GetNode<Sprite>("Sprite").Texture = ResourceLoader.Load("res://Assets/Character Sprites/Bunpir.png") as Texture;

    if (g.bracket_size == -1) {
      hideall();
      GetNode<Button>("Small").Text = "Small";
      GetNode<Button>("Big").Text ="Big";
      GetNode<Label>("Welcome").Text = "Hi " + g.name + "! Do you want to enter the small or big tournament?";
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

    if (g.fight_outcome == 1) Won();
    else if (g.fight_outcome == 0) Lost();

    if (g.level == 0) get_random_beast(opponents);

    select_beast("Sprite2");
    GetNode<Label>("Sprite2/Name").Text = g.opp_name;
    if (g.level != 0) GetNode<Sprite>("Sprite2").Hide();

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
      get_random_beast(opponents);
      select_beast("Sprite3");
      GetNode<Label>("Sprite3/Name").Text = g.opp_name;
      //GetNode<Sprite>("Sprite3").Texture = ResourceLoader.Load("res://Assets/Character Sprites/Auril-1.png") as Texture;
      GetNode<Label>("Sprite3/Name").Show(); 
      GetNode<Sprite>("Sprite3").Show();
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

    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Label>("Welcome").Hide();
    GetNode<Sprite>("Sprite").Show();
    GetNode<Sprite>("Sprite2").Show();
    GetNode<Button>("Continue").Show();
    Update();
  }


  private void _on_Small_pressed()
  {
    g = (Globals)GetNode("/root/Gm");
    g.bracket_size = 0;
    size = 2;
    GetNode<Button>("Big").Hide();
    GetNode<Button>("Small").Hide();
    GetNode<Label>("Welcome").Hide();
    GetNode<Sprite>("Sprite").Show();
    GetNode<Sprite>("Sprite2").Show();
    GetNode<Button>("Continue").Show();
    Update();
  }

  private void _on_Exit_pressed()
  {
    GetNode<Sprite>("Sprite").Position = new Vector2(0,0);
    g.name = "Player";
    g.level = 0;
    g.bracket_size = -1;
    g.fight_outcome = -1;
    GetTree().ChangeScene("res://Menus/TitleMenu.tscn");
  }

}
