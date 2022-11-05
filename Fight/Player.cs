using Godot;
using Godot.Collections;
using System;

public class Player : KinematicBody2D
{
    [Export] public int speed = 200;
    public Vector2 velocity = new Vector2();
    Vector2 startpos;
    private static Dictionary _beastOptions = null;
    Globals g;
    Texture playerTexture;
    
    
    private Dictionary beastOptions {
      get {
        if (_beastOptions == null) {
          var file = new File();
          file.Open("res://Data/Beasts.json", File.ModeFlags.Read);
          var text = file.GetAsText();
          _beastOptions = JSON.Parse(text).Result as Dictionary;
        }
        return _beastOptions;
      }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Dictionary beast;
      
        startpos.x = GetViewport().Size.x/2;
        startpos.y = GetViewport().Size.y/2;
        this.Position = startpos;
        
        g = (Globals)GetNode("/root/Gm");
        
        beast = beastOptions[g.playerBeastIndex.ToString()] as Dictionary;
        playerTexture = ResourceLoader.Load((String)beast["texture"]) as Texture;
        GetNode<Sprite>("Texture").Texture = playerTexture;
    }
    
    // sets the velocity vector
    public void GetInput()
    {
      velocity = new Vector2();

      if (Input.IsActionPressed("right")){
        velocity.x += 1;
      }

      if (Input.IsActionPressed("left")){
        velocity.x -= 1;
      }

      if (Input.IsActionPressed("down")){
        velocity.y += 1;
      }

      if (Input.IsActionPressed("up")){
        velocity.y -= 1;
      }
      velocity = velocity.Normalized() * speed;
    }
    
    // Moves the character based off character input
    public override void _PhysicsProcess(float delta)
    {
      GetInput();
      velocity = MoveAndSlide(velocity);
    }
}
