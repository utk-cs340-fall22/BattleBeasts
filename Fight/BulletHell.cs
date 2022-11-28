using Godot;
using System;

public class BulletHell : Node2D
{
    RandomNumberGenerator rng = new RandomNumberGenerator();
    private Random _random = new Random();
    PathFollow2D BulletSpawnLocation;
    public PackedScene Bullet;
    private int PlayerHealth;
    private float StartTime;
    private float GameTime;
    private Control HPBar;
    private float delay, itime;
    private Node fight;
    private bool invincible;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // defaults
        PlayerHealth = 100;
        StartTime = 1;
        GameTime = 6;
        delay = (float) .2;
        invincible = false;
        itime = 0;
        rng.Randomize();
        
        // grab scenes
        BulletSpawnLocation = GetNode<PathFollow2D>("BulletSpawn/SpawnLocation");
        Bullet = GD.Load<PackedScene>("res://Fight/Bullet.tscn");
        fight = GetNode<Node>("/root/Main");
        HPBar = GetNode<Control>("HealthInterface");
        
        // set up health bar
        HPBar.Call("AdjustHealth",100);
        HPBar.Call("UpdateHealthFrac",100,100);
    }
    
    public override void _Process(float delta)
    { 
        // invincibility time handler
        if(itime > 0){
          itime -= delta;
          if(itime <= 0) invincible = false;
          return;
        }
        
        // creates new bullets if needed
        if(StartTime < 0 && GetChildCount() < 200){
          if(delay > 0){
            delay -= delta;
          }else{
            _MakeBullet();
            delay = (float) rng.RandfRange((float) .2,(float) .5);
          }
        }
        
        // handle game time
        if(StartTime > 0) StartTime -= delta;
        if(StartTime < 0) GameTime -= delta;
        if(PlayerHealth < 0 || GameTime < 0)  _GameOver();
    }
    
    // returns value to fight scene and exits
    private void _GameOver(){
      if(PlayerHealth > 0){
        fight.Call("MinigameReturn",PlayerHealth);
      }else{
        fight.Call("MinigameReturn",0);
      }
      QueueFree(); 
    }
    
    // creates new bullets
    private void _MakeBullet()
    {
      BulletSpawnLocation.Offset = _random.Next();
      Random tmp_random = new Random();
      double start = BulletSpawnLocation.Offset;
      float radius = 1024;
      float theta = 0;
      float stat = 2;
      Vector2 dt;
      
      // randomize either wall or circle
      int todo = tmp_random.Next() % 2;
      
      // make a wall with 10 bullets
      if(todo == 0){
        for(int i = 0; i < 10; i++){
          var BulletInstance = (Area2D) Bullet.Instance();
          AddChild(BulletInstance);
          BulletInstance.Position = BulletSpawnLocation.Position;
          
          // make bullet shoot perpendicular to spawn zone
          float direction = BulletSpawnLocation.Rotation + Mathf.Pi / 2;
          BulletInstance.Rotation = direction;
          BulletSpawnLocation.Offset += 22;
        }
      }
      
      // creates circle
      if(todo == 1){
        // get middle of screen
        dt.x = 512;
        dt.y = 300;
        
        // get random start position
        theta = rng.RandfRange((float) 0,(float) 1.7);
        stat = theta;
        
        // spawn bullets in circle
        while(theta < (1.7+stat)){
          Vector2 dx;
          
          // get spawn point
          dx.x = dt.x + radius * Mathf.Cos(theta * Mathf.Pi);
          dx.y = dt.y + radius * Mathf.Sin(theta * Mathf.Pi);
          
          // spawn bullet
          var BulletInstance = (Area2D) Bullet.Instance();
          AddChild(BulletInstance);
          BulletInstance.Position = dx;
          BulletInstance.LookAt(dt);
          BulletInstance.Call("set_destroy_on_collide",true);
          
          // increment
          theta += (float) .03;
        }
      }
    }
    
    public void _ChangePlayerHealth(int change)
    {
      if(invincible) return;
      
      PlayerHealth += change;
      HPBar.Call("UpdateHealthFrac",100,PlayerHealth);
      HPBar.Call("AdjustHealth",PlayerHealth);
      invincible = true;
      itime = (float) .5;    
    }
    
    private float RandRange(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min;
    }
}
