using Godot;
using System;

public class Bullet : Area2D
{
    public int speed = 400;
    Vector2 velocity;
    Vector2 screen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        velocity.x = 1;
        velocity.y = 0;
    }
    
    public override void _Process(float delta)
    {
      this.Position += velocity.Rotated(Rotation) * speed * delta;
      if(this.Position.x > GetViewport().Size.x ||
         this.Position.y > GetViewport().Size.y){
        GD.Print("Deleting Bullet");
        QueueFree();
      }
    }
    
    private void _on_Bullet_body_entered(KinematicBody2D body)
    {
        GetParent().Call("_ChangePlayerHealth",-1);
    }
}
