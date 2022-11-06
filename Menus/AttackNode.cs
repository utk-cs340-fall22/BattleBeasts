using Godot;
using System;

public class AttackNode : Control
{
  
    private Label AttackName, AttackDmg, AttackNum;
    private CanvasLayer TeamSelect;
    private Panel Cover;
    private Sprite DmgType;
    public bool selected = false;
    private int index;
      
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AttackName = GetNode<Label>("AttackName");
        AttackDmg = GetNode<Label>("AttackDmg");
        AttackNum = GetNode<Label>("AttackNum");
        DmgType = GetNode<Sprite>("DmgType");
        TeamSelect = GetNode<CanvasLayer>("../../..");
        Cover = GetNode<Panel>("Panel2");
        Cover.Hide();
    }
    
    public void setup_AttackNode(string name, int dmg, int num, string type, int ind, CanvasLayer parent)
    {
      AttackName = GetNode<Label>("AttackName");
      AttackDmg = GetNode<Label>("AttackDmg");
      AttackNum = GetNode<Label>("AttackNum");
      DmgType = GetNode<Sprite>("DmgType");
      TeamSelect = parent;
      Cover = GetNode<Panel>("Panel2");
      Cover.Hide();
      String d = dmg.ToString();
      String n = num.ToString();
      AttackName.Text = name;
      AttackDmg.Text = "ATK "+ d;
      AttackNum.Text = "HPA " + n;
      index = ind;
      
      if(type == "Fire"){
        DmgType.Texture = ResourceLoader.Load("res://Assets/Attack Type Sprites/fire-icon.png") as Texture;  
      }else if(type == "Electric"){
        DmgType.Texture = ResourceLoader.Load("res://Assets/Attack Type Sprites/electric-icon.png") as Texture; 
      }else if(type == "Poison"){
        DmgType.Texture = ResourceLoader.Load("res://Assets/Attack Type Sprites/poison-icon.png") as Texture; 
      }else if(type == "Ice"){
        DmgType.Texture = ResourceLoader.Load("res://Assets/Attack Type Sprites/ice-icon.png") as Texture; 
      }else if(type == "Dark Magic"){
        DmgType.Texture = ResourceLoader.Load("res://Assets/Attack Type Sprites/darkmagic-icon.png") as Texture; 
      }
    }
    
    private void _on_TextureButton_pressed()
    {
        bool val;
        
        val = (bool) TeamSelect.Call("_on_Attack_selected",index);
        
        if(selected && val){
          Cover.Hide();
          selected = false; 
        }else if(val){
          Cover.Show();
          selected = true;
        }
    }
    
}
