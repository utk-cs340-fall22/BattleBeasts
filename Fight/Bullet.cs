using Godot;
using System;

public class Bullet : Area2D
{
    Vector2 velocity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    
    public override void _Process(float delta)
    {
      this.Position += velocity * delta;
      //Rotate((float) .05);
    }
}
