using Godot;
using System;
using System.Threading.Tasks;

public class Transition : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      
    }
    
    public async Task ChangeScene(string target, string texture) 
    {
      
        GetNode<TextureRect>("ColorRect").Texture = ResourceLoader.Load(texture) as Texture;
        var st = (AnimationPlayer) GetNode<AnimationPlayer>("animate");
        st.Play("dissolve");
        await ToSignal(st, "animation_finished");
        GetTree().ChangeScene(target);
        st.Play("RESET");
      
     }
    
        public async Task ChangeScene2(string target) 
    {
        //GetNode<ColorRect>("Color").Color = new Color(0,0,0,1);
        var st = (AnimationPlayer) GetNode<AnimationPlayer>("animate");
        st.Play("dissolve2");
        await ToSignal(st, "animation_finished");
        GetTree().ChangeScene(target);
        st.Play("RESET");
     }
    
    
    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
