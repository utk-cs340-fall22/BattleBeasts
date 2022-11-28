using Godot;
using System;

public class Tutorial : Node2D
{
  private AudioStreamPlayer se;
  private Globals globals;
  private Transition t;
  private RichTextLabel Welcome, Continue, Info;
  private HSlider PowerSlider;
  //private Vector2 s,e;
  //private Color c;
  //private Line2D line1, line2, line3, line4, line5;
  private Control Attack, AttackSmall;
  private Sprite Fire, Ice, Electric, Poison, DM;
  private int step/*, ATK, HPA*/;
  //private string name, type;


  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    // transition and global
    t = (Transition)GetNode("/root/Transition");
    globals = GetNode<Globals>("/root/Gm");
    
    // starting text nodes
    Welcome = GetNode<RichTextLabel>("ColorRect/CenterContainer/Welcome");
    Continue = GetNode<RichTextLabel>("ColorRect/Continue");
    
    // attack nodes
    Attack = GetNode<Control>("ColorRect/Attack");
    AttackSmall = GetNode<Control>("ColorRect/AttackSmall");
    
    // type nodes (the sprites)
    Fire = GetNode<Sprite>("ColorRect/Fire");
    Ice = GetNode<Sprite>("ColorRect/Ice");
    Electric = GetNode<Sprite>("ColorRect/Electric");
    Poison = GetNode<Sprite>("ColorRect/Poison");
    DM = GetNode<Sprite>("ColorRect/Dark Magic");
    
    // big text section
    Info = GetNode<RichTextLabel>("ColorRect/Info");
    //Info.BBCodeEnabled = true;
    
    PowerSlider = GetNode<HSlider>("ColorRect/HSlider");
    
    // do the first step of the tutorial
    _DoStep(0);
  }
  
  // Return to the previous step of the tutorial or to the Main Menu
  private void _on_Back_pressed()
  {
    se = globals.GetNode<AudioStreamPlayer>("SoundEffects");
    se.Stream = ResourceLoader.Load("res://Assets/Music/BackSound.tres") as AudioStream;
    se.Play();
    
    if(step == 0){
      t.ChangeScene("res://Menus/MainMenu.tscn");
    }else{
      _DoStep(step-1);
    }
    
  }

  // Handle the SpaceBar presses
  public override void _Input(InputEvent inputEvent)
  {
    if (inputEvent.IsActionPressed("ui_accept"))
    {
      _DoStep(step+1);
    }
  }
  
  // makes everything for the required step of the turoial visible
  private void _DoStep(int stepnum){
    if(stepnum <= 2) _HideAll();
    if(stepnum == 0){
      Continue.Visible = true;
      Welcome.Visible = true;
      step = 0;
    }else if(stepnum == 1){
      Welcome.Visible = false;
      Continue.Visible = true;
      Fire.Visible = true;
      Ice.Visible = true;
      Electric.Visible = true;
      Poison.Visible = true;
      DM.Visible = true;
      Info.Visible = true;
      Info.BbcodeText = "There are 5 types in Battle Beasts.\n\n[color=#bd3636]Fire[/color], [color=#36bdbd]Ice[/color], [color=#bdab36]Electric[/color], [color=#59bd36]Poison[/color], and [color=#7836bd]Dark Magic[/color].\n\n\nIn Battle Beasts an attack contains a name, type, attack damage (ATK), and hits per attack (HPA).\n\nThe name, ATK, and HPA are always listed. The type however can be either the dots from above or the border of the attack.\n\n\n\n\n\n\n\n\n\n\n\nThere are 3 Mini-Games in Battle Beasts.\n\nPower Slider, Quick Time, and Bullet Hell.\n\nHow you perform on these Mini-Games will affect how strong your attack is. Continue to learn more about them.";
      Attack.Show();
      AttackSmall.Show();
      step = 1;
    }else if(stepnum == 2){
      Info.Visible = true;
      Continue.Visible = true;
      PowerSlider.Visible = true;
      
      Info.Text = "Power Slider is a Mini-Game where your goal is to stop the tick of the slider as close to the center as possible. To stop the slider you press the spacebar.\n\n\n\n\nQuick Time is a Mini-Game where you press a key as fast as possible.\n\n\nBullet Hell is a Mini-Game where you dodge bullets and your finishing health is what decides your score. You are capable of using either WASD or the arrow keys to move your beast.";
      step = 2;
    }
    if(stepnum > 2) step = 2;
  }
  
  // hides everything
  private void _HideAll(){
    Fire.Visible = false;
    Ice.Visible = false;
    Electric.Visible = false;
    Poison.Visible = false;
    DM.Visible = false;
    Info.Visible = false;
    Continue.Visible = false;
    Welcome.Visible = false;
    PowerSlider.Visible = false;
    Attack.Hide();
    AttackSmall.Hide();
  }
}
