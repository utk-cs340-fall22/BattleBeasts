using Godot;
using System;

public class AttackNodeSmall : Control
{
    private Label AttackName, AttackDmg, AttackNum;
    //private Sprite DmgType;
    private Node Fight;
    private int index;
    private StyleBoxFlat backgroundstyle;
    private Panel background;
    private Color bordercolor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void setup_AttackNode(string name, int dmg, int num, string type, int ind, object parent)
    {
      AttackName = GetNode<Label>("AttackName");
      AttackDmg = GetNode<Label>("AttackDmg");
      AttackNum = GetNode<Label>("AttackNum");
      background = GetNode<Panel>("Panel");
      Fight = (Node) parent;
      String d = dmg.ToString();
      String n = num.ToString();
      AttackName.Text = name;
      AttackDmg.Text = "ATK "+ d;
      AttackNum.Text = "HPA " + n;
      index = ind;
      
      //background.Panel;
      backgroundstyle = (StyleBoxFlat) background.GetStylebox("panel");
      
      if(type == "Fire"){
        bordercolor.a = 1;
        bordercolor.b = (float) .21;
        bordercolor.g = (float) .21;
        bordercolor.r = (float) .74;
      }else if(type == "Electric"){
        bordercolor.a = 1;
        bordercolor.b = (float) .21;
        bordercolor.g = (float) .67;
        bordercolor.r = (float) .74;
      }else if(type == "Poison"){
        bordercolor.a = 1;
        bordercolor.b = (float) .21;
        bordercolor.g = (float) .74;
        bordercolor.r = (float) .35;
      }else if(type == "Ice"){
        bordercolor.a = 1;
        bordercolor.b = (float) .74;
        bordercolor.g = (float) .74;
        bordercolor.r = (float) .21;
      }else if(type == "Dark Magic"){
        bordercolor.a = 1;
        bordercolor.b = (float) .74;
        bordercolor.g = (float) .21;
        bordercolor.r = (float) .47;
      }
      
      
      backgroundstyle.BorderColor = bordercolor;
    }
    
    private void _on_TextureButton_pressed()
    {
        Fight.Call("_on_Attack_Selected",index);
    }
}
