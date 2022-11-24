using Godot;
using System;

public class PauseMenu2 : Control
{
    bool is_paused, options_open;
    private Control Options;
    private Globals globals;
    private AudioStreamPlayer music, musicP;

    // initialize the scene to be hidden
    public override void _Ready()
    {
        is_paused = false;
        options_open = false;
        this.Hide();
    }

    private void _on_QuitButton_pressed()
    {
        _SetPaused(false);
        globals = (Globals)GetNode("/root/Gm");
        music = globals.GetNode<AudioStreamPlayer>("Music");
        musicP = globals.GetNode<AudioStreamPlayer>("MusicPlayer");
        music.Stop(); 
        musicP.Stop();
        
        globals.oppAttacks = new int[7, 4];
        globals.level = 0;
        globals.bracketSize = -1;
        globals.fightOutcome = -1;   
        globals.currBeast = -1;
        globals.playerBeastIndex = 0;
        globals.playerModifierIndex = 0;
        for(int i = 0; i < 7; i++ ) globals.oppBeast[i] = -1;
        
        
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
        options_open = true;
        Options.Call("_Load_Options_Menu",true);
    }
    
    public void _set_options_open(bool i){
        options_open = i;
    }

    public override void _Process(float delta)
    {
        
    }
    
    // on escape key pressed we pull up the pause menu
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("pause") && options_open == false)
        {
            if(is_paused == true){
                _SetPaused(false);
            }else{
                _SetPaused(true);
            }
        }
    }
}
