using Godot;
using System;

public class Fighter : Sprite
{
	string controller;
	
	// initialize as player/opponent, load stats
	public void Init(string controller) {
		this.controller = controller;
		GD.Print("controller: ", controller);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
