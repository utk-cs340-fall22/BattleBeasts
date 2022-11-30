using Godot;
using System;

public class PowerSliderMiniGame : Control
{
    // position of the hslider
    float position = 0;
    
    // direction bar is going false is right true is left
    bool direction = false;
    private HSlider vol;
    private Node fight;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        vol = GetNode<HSlider>("CenterContainer/PowerSlider");
        fight = GetNode<Node>("/root/Main");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // moves the position of the slider back and forth
    public override void _Process(float delta)
    {
        if(position <= 200 && direction == false && position >= 0){
            vol.Value = position;
            position += 300*delta;
        }else if(position <= 200 && direction == true && position >= 0){
            vol.Value = position;
            position -= 300*delta;
        }else if(position < 0){
            position = 0;
            direction = false;
        }else if(position > 200){
            position = 200;
            direction = true;
        }
    }
    
    // records the score on input event
    public override void _Input(InputEvent inputEvent)
    {
        double ans = 0;
        if (inputEvent.IsActionPressed("ui_accept"))
        {
            if(vol.Value > 100){
                ans = 200 - vol.Value;
            }else{
                ans = vol.Value;
            }
            fight.Call("MinigameReturn",ans);
            QueueFree();
        }
    }
}
