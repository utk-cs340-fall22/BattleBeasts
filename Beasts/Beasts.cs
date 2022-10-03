using Godot;
using System;

public class Beasts : Node2D
{
	private int hp = 10;
	private int attack = 10;
	private int defence = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
 	public override void _Process(float delta)
 	{
	  
  	}
}
