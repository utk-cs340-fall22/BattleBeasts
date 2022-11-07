using Godot;
using System;

public class Bullet : Area2D
{
    public int speed = 400;
    Vector2 velocity;
    float time = 0;
    public bool destroy_on_collide = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        velocity.x = 1;
        velocity.y = 0;
    }
    
    public override void _Process(float delta)
    {
      // move bullet foward
      this.Position += velocity.Rotated(Rotation) * speed * delta;
      time += delta;
      
      // delete the bullet if its off screen
      if((this.Position.x > OS.WindowSize.x ||
         this.Position.y > OS.WindowSize.y ||
         this.Position.x < 0 ||
         this.Position.y < 0) && time > 2){
        QueueFree();
      }
    }
    
    // when the bullet hits a player subtract from health
    private void _on_Bullet_body_entered(KinematicBody2D body)
    {
        GetParent().Call("_ChangePlayerHealth",-1);
    }
    
    public void set_destroy_on_collide(bool b)
    {
      destroy_on_collide = b;  
    }
    
    public bool get_destroy_on_collide()
    {
      return destroy_on_collide;
    }
    
    private void _on_Bullet_area_entered(Area2D area)
    {
      bool t = (bool) area.Call("get_destroy_on_collide");
      if(destroy_on_collide && t) QueueFree();
    }
}
