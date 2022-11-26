using Godot;
using System;

public class OptionsMenu2 : Control
{
    int bus_index_Music, bus_index_SE;
    float val_Music, val_SE;
    bool parent;
    private CheckButton fullscreen;
    private AudioStreamPlayer audio, se;
    private HSlider volM, volS;
    private Control Pause;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Hide();
        parent = false;
        
        fullscreen = GetNode<CheckButton>("CenterContainer/VBoxContainer/FullscreenButton");
        audio = GetNode<AudioStreamPlayer>("/root/Gm/Music");
        volM = GetNode<HSlider>("CenterContainer/VBoxContainer/VolumeSliderMusic");
        volS = GetNode<HSlider>("CenterContainer/VBoxContainer/VolumeSliderSE");
        se = GetNode<AudioStreamPlayer>("/root/Gm/SoundEffects");
        
        if(OS.WindowFullscreen){
            fullscreen.Pressed = true;
        }else{
            fullscreen.Pressed = false;  
        }
        
        bus_index_Music = AudioServer.GetBusIndex("Music");
        bus_index_SE = AudioServer.GetBusIndex("SoundEffects");
        val_Music = GD.Db2Linear(AudioServer.GetBusVolumeDb(bus_index_Music));
        val_SE = GD.Db2Linear(AudioServer.GetBusVolumeDb(bus_index_SE));
        volM.Value = val_Music*100;
        volS.Value = val_SE*100;
    }
    
    private void _on_FullscreenButton_pressed()
    {
      se.Stream = ResourceLoader.Load("res://Assets/Music/MenuClick.tres") as AudioStream;
      se.Play();
      
      OS.WindowFullscreen = !OS.WindowFullscreen;
    }
    
    public void _Load_Options_Menu(bool main)
    {
        parent = main;
        this.Show();     
    }
    
    private void _on_BackButton_pressed()
    {
        se.Stream = ResourceLoader.Load("res://Assets/Music/BackSound.tres") as AudioStream;
        se.Play();
        if(parent){
          Pause = GetNode<Control>("..");
          Pause.Call("_set_options_open",false);
        }
      
        this.Hide();
    }
    
    private void _on_VolumeSliderMusic_value_changed(float value)
    {
        val_Music = value / 100;
        AudioServer.SetBusVolumeDb(bus_index_Music,GD.Linear2Db(val_Music));
    }
    
    private void _on_VolumeSliderSE_value_changed(float value)
    {
        val_SE = value / 100;
        AudioServer.SetBusVolumeDb(bus_index_SE,GD.Linear2Db(val_SE));
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
