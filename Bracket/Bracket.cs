using Godot;
using System;


public class Bracket: Node2D
{

  public override void _Ready() {
	Update();
  }

  public override void _Process(float delta)
  {

  }

  //public RandomNumberGenerator rng = new RandomNumberGenerator();
  public override void _Draw()
  {
	var points = new Vector2[100];
	/*LEVEL 1*/
	
	//First Bracket
	points[0] = new Vector2(100, 100);
	points[1] = new Vector2(200, 100);
	
	points[2] = new Vector2(100, 150);
	points[3] = new Vector2(200, 150);
	
	points[4] = new Vector2(200, 100);
	points[5] = new Vector2(200, 150);
	
	//Second Bracket
	points[6] = new Vector2(100, 200);
	points[7] = new Vector2(200, 200);
	
	points[8] = new Vector2(100, 250);
	points[9] = new Vector2(200, 250);
	
	points[10] = new Vector2(200, 200);
	points[11] = new Vector2(200, 250);
	
	//Third Bracket
	points[12] = new Vector2(100, 300);
	points[13] = new Vector2(200, 300);
	
	points[14] = new Vector2(100, 350);
	points[15] = new Vector2(200, 350);
	
	points[16] = new Vector2(200, 300);
	points[17] = new Vector2(200, 350);
	
	//4th Bracket
	points[18] = new Vector2(100, 400);
	points[19] = new Vector2(200, 400);
	
	points[20] = new Vector2(100, 450);
	points[21] = new Vector2(200, 450);
	
	points[22] = new Vector2(200, 400);
	points[23] = new Vector2(200, 450);
	
	/*LEVEL TWO*/
	
	//Bracket 1
	points[24] = new Vector2(200, 125);
	points[25] = new Vector2(300, 125);
	
	points[26] = new Vector2(200, 225);
	points[27] = new Vector2(300, 225);
	
	points[28] = new Vector2(300, 125);
	points[29] = new Vector2(300, 225);
	
	//Bracket 2
	
	points[30] = new Vector2(200, 325);
	points[31] = new Vector2(300, 325);
	
	points[32] = new Vector2(200, 425);
	points[33] = new Vector2(300, 425);
	
	points[34] = new Vector2(300, 325);
	points[35] = new Vector2(300, 425);
	
	/*Level 3*/
	points[36] = new Vector2(300, 175);
	points[37] = new Vector2(400, 175);
	
	points[38] = new Vector2(300, 375);
	points[39] = new Vector2(400, 375);
	
	points[40] = new Vector2(400, 175);
	points[41] = new Vector2(400, 375);
	
	/*LEVEL 4*/
	points[42] = new Vector2(400, 275);
	points[43] = new Vector2(500, 275);
	
	
	var color = new Color(100, 100, 100);

	DrawMultiline(points, color, (float) 15.0, false );
  }
}
