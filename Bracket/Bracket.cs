using Godot;
using System;
using System.Threading;


public class Bracket: Node2D
{
  Globals g;
  int size = 0;

  public override void _Ready() {
  g = (Globals)GetNode("/root/Gm");
  GetNode<Sprite>("Sprite").Hide();
  GetNode<Sprite>("Sprite2").Hide();
  GetNode<Button>("Continue").Hide();
  GetNode<Button>("Win").Hide();
  GetNode<Button>("Lose").Hide();
  GetNode<Label>("Label").Hide();
  
  GetNode<Button>("Small").SetText("Small");
  GetNode<Button>("Big").SetText("Big");
  GetNode<Label>("Welcome").SetText("Hi " + g.name + "! Do you want to enter the small or big tournament?");
  
  GetNode<Sprite>("Sprite").Position = new Vector2(100, 60);
  GetNode<Label>("Label").SetText("Did you win or lose?\nIf this is your first time here, press continue.\n");
  }

  public override void _Process(float delta)
  {
  
  }

  //public RandomNumberGenerator rng = new RandomNumberGenerator();
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

  


private void _on_Win_pressed()
{
  GetNode<Sprite>("Sprite").Position = new Vector2(225, 85);
  GetNode<Button>("Win").Hide();
  GetNode<Button>("Lose").Hide();
  GetNode<Label>("Label").Hide();
  
  
}


private void _on_Lose_pressed()
{
  GetTree().ChangeScene("res://Menus/TitleMenu.tscn");
  GetNode<Button>("Win").Hide();
  GetNode<Button>("Lose").Hide();
  GetNode<Label>("Label").Hide();
}

private void _on_Continue_pressed()
{
  GetTree().ChangeScene("res://Fight/Fight.tscn");
}


private void _on_Big_pressed()
{
  size = 4;
  GetNode<Button>("Big").Hide();
  GetNode<Button>("Small").Hide();
  GetNode<Label>("Welcome").Hide();
  GetNode<Sprite>("Sprite").Show();
  GetNode<Sprite>("Sprite2").Show();
  GetNode<Button>("Win").Show();
  GetNode<Button>("Lose").Show();
  GetNode<Label>("Label").Show();
  GetNode<Button>("Continue").Show();
  Update();
}


private void _on_Small_pressed()
{
  size = 2;
  GetNode<Button>("Big").Hide();
  GetNode<Button>("Small").Hide();
  GetNode<Label>("Welcome").Hide();
  GetNode<Sprite>("Sprite").Show();
  GetNode<Sprite>("Sprite2").Show();
  GetNode<Button>("Win").Show();
  GetNode<Button>("Lose").Show();
  GetNode<Label>("Label").Show();
  GetNode<Button>("Continue").Show();
  Update();
}

}
