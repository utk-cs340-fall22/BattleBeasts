using Godot;
using System;

public class Textbox : Node
{
    int cumulativeLength;
    float charReadRate;
    string beast, attack, damage;
    VBoxContainer vbox;
    HBoxContainer topRow, bottomRow;
    Label beastL, usedL, attackL, dealingL, damageValueL, damageL;
    Label[] labelArray;
    Tween tween;

    public int TextAnimationStatus;

    // this is hardcoded but this textbox literally has a single purpose so it works
    // this is being done 5 days before the presentation too
    public void Init(string s1, string s2, string s3) {
      //vbox = GetTree().Root.GetNode("Textbox").GetNode("MarginContainer").GetNode("MarginContainer").GetNode<VBoxContainer>("VBoxContainer");
      vbox = GetNode("MarginContainer").GetNode("MarginContainer").GetNode<VBoxContainer>("VBoxContainer");
      topRow = vbox.GetNode<HBoxContainer>("TopRow");
      bottomRow = vbox.GetNode<HBoxContainer>("BottomRow");
      beastL = topRow.GetNode<Label>("Beast");
      usedL = topRow.GetNode<Label>("Used");
      attackL = vbox.GetNode<Label>("AttackName");
      dealingL = bottomRow.GetNode<Label>("Dealing");
      damageValueL = bottomRow.GetNode<Label>("DamageValue");
      damageL = bottomRow.GetNode<Label>("Damage");
      tween = GetNode<Tween>("Tween");

      beast = s1;
      attack = s2;
      damage = s3;
      charReadRate = 0.02f;

      labelArray = new Label[6] {beastL, usedL, attackL, dealingL, damageValueL, damageL};
      beastL.Text = beast;
      usedL.Text = "used";
      attackL.Text = attack;
      dealingL.Text = "dealing";
      damageValueL.Text = damage;
      damageL.Text = "damage!";
    }

    public void AnimateText() {
      
      foreach (Label label in labelArray) label.PercentVisible = 0;
      tween.Start();
      cumulativeLength = 0;
      foreach (Label label in labelArray) {
        tween.InterpolateProperty(label, "percent_visible", 0.0, 1.0, label.Text.Length * charReadRate, Tween.TransitionType.Linear, Tween.EaseType.InOut, cumulativeLength * charReadRate);
        cumulativeLength += label.Text.Length;
      }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
      
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
