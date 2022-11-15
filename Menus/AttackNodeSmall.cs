using Godot;
using System;

public class AttackNodeSmall : Control
{
    private Label AttackName, AttackDmg, AttackNum;
    //private Sprite DmgType;
    private Node Fight;
    private int index;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void setup_AttackNode(string name, int dmg, int num, string type, int ind, object parent)
    {
      AttackName = GetNode<Label>("AttackName");
      AttackDmg = GetNode<Label>("AttackDmg");
      AttackNum = GetNode<Label>("AttackNum");
      //DmgType = GetNode<Sprite>("DmgType");
      Fight = (Node) parent;
      String d = dmg.ToString();
      String n = num.ToString();
      AttackName.Text = name;
      AttackDmg.Text = "ATK "+ d;
      AttackNum.Text = "HPA " + n;
      index = ind;
    }
    
    private void _on_TextureButton_pressed()
    {
        Fight.Call("_on_Attack_Selected",index);
    }
}
