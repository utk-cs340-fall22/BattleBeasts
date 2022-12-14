using Godot;
using System;

public class Enter_Name : Godot.LineEdit
{
  Globals g;
  Transition t;
  
  public override void _Ready()
  {
    //This is so the player doesn't have to click to type
    GrabFocus();
    GetNode<Button>("Button").Hide();
    g = (Globals)GetNode("/root/Gm");
    t = (Transition)GetNode("/root/Transition");
  }

  //When the player enters their name, they will get a message and a button to move to a different scene
  private void _on_LineEdit_text_entered(String new_text)
  {
    g.name = new_text;
    GetNode<Label>("Welcome").Hide();
    GetNode<Label>("Enter").Hide();
    GetNode<Label>("Exit").Text = "Hello " + new_text + ". Welcome to the world of Battle Beasts!";
    GetNode<Button>("Button").Text = "Let's Go " + new_text + "!";
    GetNode<Button>("Button").Show();
  }
  
  
  private async void _on_Button_pressed()
  {
    await t.ChangeScene("res://Menus/TeamSelect.tscn");
  }
}
