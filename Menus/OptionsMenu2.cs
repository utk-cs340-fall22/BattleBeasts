using Godot;
using System;

public class OptionsMenu2 : Control
{
    bool is_paused;
    private CheckButton fullscreen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Hide();
        
        fullscreen = GetNode<CheckButton>("CenterContainer/VBoxContainer/FullscreenButton");
        
        if(OS.WindowFullscreen){
            fullscreen.Pressed = true;
        }else{
            fullscreen.Pressed = false;  
        }
    }
    
    private void _on_FullscreenButton_pressed()
    {
        OS.WindowFullscreen = !OS.WindowFullscreen;
    }
    
    public void _Load_Options_Menu()
    {
        this.Show();     
    }
    
    private void _on_BackButton_pressed()
    {
        this.Hide();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
