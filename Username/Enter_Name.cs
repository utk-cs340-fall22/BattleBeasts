using Godot;
using System;

public class Enter_Name : Godot.LineEdit
{
	string name;
	public override void _Ready()
	{
		GrabFocus();
		GetNode<Button>("Button").Hide();
		
	}


	private void _on_LineEdit_text_entered(String new_text)
	{
		name = new_text;
		GetNode<Label>("Exit").SetText("Hello " + new_text + ". Welcome to the world of Battle Beasts.");
		GetNode<Label>("press_here").SetText("Press here to select your team.");
		GetNode<Button>("Button").SetText("Let's Go " + new_text + "!");
		GetNode<Button>("Button").Show();
	}
	
	
	private void _on_Button_pressed()
{
	GetTree().ChangeScene("res://Bracket/Bracket.tscn");
}
}
