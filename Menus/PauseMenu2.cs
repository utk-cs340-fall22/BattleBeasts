using Godot;
using System;

public class PauseMenu2 : Control
{
    bool is_paused;
    private Control Options;

    // initialize the scene to be hidden
    public override void _Ready()
    {
        is_paused = false;
        this.Hide();
    }

    private void _on_QuitButton_pressed()
    {
        _SetPaused(false);
        GetTree().ChangeScene("res://Menus/MainMenu.tscn");
    }

    private void _on_ResumeButton_pressed()
    {
        _SetPaused(false);
    }

    // pauses the scene tree and shows the pause menu
    private void _SetPaused(bool val)
    {
        is_paused = val;
        GetTree().Paused = is_paused;
        if (val == true) this.Show();
        if (val == false) this.Hide();
    }
    
    private void _on_OptionButton_pressed()
    {
        Options = GetNode<Control>("OptionsMenu2");
        //Options._Load_Options_Menu();
        Options.Call("_Load_Options_Menu");
    }

    // on escape key pressed we pull up the pause menu
    public override void _Process(float delta)
    {
        
    }
    
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("pause"))
        {
            if(is_paused == true){
                _SetPaused(false);
            }else{
                _SetPaused(true);
            }
        }
    }
}
