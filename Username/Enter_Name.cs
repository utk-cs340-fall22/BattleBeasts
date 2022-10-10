using Godot;
using System;

public class Enter_Name : Godot.LineEdit
{
  Globals g;
  public override void _Ready()
  {
    GrabFocus();
    GetNode<Button>("Button").Hide();
    g = (Globals)GetNode("/root/Gm");
  }


  private void _on_LineEdit_text_entered(String new_text)
  {
    g.name = new_text;
    GetNode<Label>("Exit").SetText("Hello " + new_text + ". Welcome to the world of Battle Beasts.");
    GetNode<Label>("press_here").SetText("Press here to enter the tournament.");
    GetNode<Button>("Button").SetText("Let's Go " + new_text + "!");
    GetNode<Button>("Button").Show();
  }
  
  
  private void _on_Button_pressed()
{
  GetTree().ChangeScene("res://Bracket/Bracket.tscn");
}

  public override void _Process(float delta)
  {

  }

}
