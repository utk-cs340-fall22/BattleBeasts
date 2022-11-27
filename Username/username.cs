using Godot;
using System;

public class username : Node2D
{

  public override void _Ready()
  {
    GetNode<TextureRect>("TextureRect").Texture = ResourceLoader.Load("res://Assets/lab.png") as Texture;
  }
  
}
