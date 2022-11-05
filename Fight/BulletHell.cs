using Godot;
using System;

public class BulletHell : Node2D
{
    //Area2D bullets = Load("res://Fight/Bullet.tscn");
    public PackedScene Bullet;
    private int PlayerHealth;
    private float StartTime;
    private float GameTime;
    private float delay;
    RandomNumberGenerator rng = new RandomNumberGenerator();
    private Random _random = new Random();
    PathFollow2D BulletSpawnLocation;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PlayerHealth = 100;
        StartTime = 1;
        GameTime = 30;
        delay = (float) .2;
        //rng.New();
        rng.Randomize();
        BulletSpawnLocation = GetNode<PathFollow2D>("BulletSpawn/SpawnLocation");
        Bullet = GD.Load<PackedScene>("res://Fight/Bullet.tscn");
    }
    
    public override void _Process(float delta)
    {
        if(StartTime < 0 && GetChildCount() < 500){
          if(delay > 0){
            delay -= delta;
          }else{
            _MakeBullet();
            GD.Print(PlayerHealth);
            delay = (float) rng.RandfRange((float) .01,(float) .1);
          }
        }
        
        if(StartTime > 0) StartTime -= delta;
        if(StartTime < 0) GameTime -= delta;
        if(PlayerHealth < 0 || GameTime < 0)  _GameOver();
    }
    
    private void _GameOver(){
      QueueFree(); 
    }
    
    private void _MakeBullet()
    {
      BulletSpawnLocation.Offset = _random.Next();
      
      var BulletInstance = (Area2D) Bullet.Instance();
      AddChild(BulletInstance);
      BulletInstance.Position = BulletSpawnLocation.Position;
      
      float direction = BulletSpawnLocation.Rotation + Mathf.Pi / 2;
      
      direction += RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
      BulletInstance.Rotation = direction;
    }
    
    public void _ChangePlayerHealth(int change)
    {
      PlayerHealth += change;  
    }
    
    private float RandRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }
}
