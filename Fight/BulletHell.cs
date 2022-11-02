using Godot;
using System;

public class BulletHell : Node2D
{
    //Area2D bullets = Load("res://Fight/Bullet.tscn");
    public PackedScene Bullet;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Bullet = (PackedScene) ResourceLoader.Load("res:://Fight/Bullet.tscn");
        Bullet = GD.Load<PackedScene>("res://Fight/Bullet.tscn");
    }
    
    public override void _Process(float delta)
    {
        var tmp = Bullet.Instance();
        AddChild(tmp);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
