using Godot;
using System;

public class OptionsMenu2 : Control
{
    int bus_index;
    float val;
    private CheckButton fullscreen;
    private AudioStreamPlayer audio, se;
    private HSlider vol;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Hide();
        
        fullscreen = GetNode<CheckButton>("CenterContainer/VBoxContainer/FullscreenButton");
        audio = GetNode<AudioStreamPlayer>("/root/Gm/Music");
        vol = GetNode<HSlider>("CenterContainer/VBoxContainer/VolumeSlider");
        se = GetNode<AudioStreamPlayer>("/root/Gm/SoundEffects");
        
        if(OS.WindowFullscreen){
            fullscreen.Pressed = true;
        }else{
            fullscreen.Pressed = false;  
        }
        
        bus_index = AudioServer.GetBusIndex("Master");
        val = GD.Db2Linear(AudioServer.GetBusVolumeDb(bus_index));
        vol.Value = val*100;
    }
    
    private void _on_FullscreenButton_pressed()
    {
      se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
      se.Play();
      
        OS.WindowFullscreen = !OS.WindowFullscreen;
    }
    
    public void _Load_Options_Menu()
    {
        this.Show();     
    }
    
    private void _on_BackButton_pressed()
    {
        se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
        se.Play();
      
        this.Hide();
    }
    
    private void _on_VolumeSlider_value_changed(float value)
    {
        val = value / 100;
        AudioServer.SetBusVolumeDb(bus_index,GD.Linear2Db(val));
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
