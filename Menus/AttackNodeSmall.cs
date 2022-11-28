using Godot;
using System;

public class AttackNodeSmall : Control
{
    private Label AttackName, AttackDmg, AttackNum;
    private StyleBoxFlat backgroundstyle;
    private Color bordercolor;
    private Panel background;
    private Node Fight;
    private int index;

    public void setup_AttackNode(String name, int dmg, int num, String type, int ind, object parent)
    {
      // get all the nodes
      AttackName = GetNode<Label>("AttackName");
      AttackDmg = GetNode<Label>("AttackDmg");
      AttackNum = GetNode<Label>("AttackNum");
      background = GetNode<Panel>("Panel");
      Fight = (Node) parent;
      
      // set all the values
      String d = dmg.ToString();
      String n = num.ToString();
      AttackName.Text = name;
      AttackDmg.Text = "ATK "+ d;
      AttackNum.Text = "HPA " + n;
      index = ind;
      
      // grab a duplicate of the style
      backgroundstyle = (StyleBoxFlat) background.GetStylebox("panel").Duplicate();
      
      // get the color of the border
      if(type == "Fire"){
        bordercolor = new Color((float).74,(float).21,(float).21,1);
      }else if(type == "Electric"){
        bordercolor = new Color((float).74,(float).67,(float).21,1);
      }else if(type == "Poison"){
        bordercolor = new Color((float).35,(float).74,(float).21,1);
      }else if(type == "Ice"){
        bordercolor = new Color((float).21,(float).74,(float).74,1);
      }else if(type == "Dark Magic"){
        bordercolor = new Color((float).47,(float).21,(float).74,1);
      }
      
      // set the border color and override the style
      backgroundstyle.BorderColor = bordercolor;
      background.AddStyleboxOverride("panel", backgroundstyle);
    }
    
    // Use the attack
    private void _on_TextureButton_pressed()
    {
        Fight.Call("_on_Attack_Selected",index);
    }
}
