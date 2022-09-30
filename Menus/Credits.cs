using Godot;
using System;

public class Credits : Control
{
	private HBoxContainer scrolling_text;
	private Label titles, names;
	private File file = new File();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scrolling_text = GetNode<HBoxContainer>("ScrollingText");
		titles = GetNode<Label>("ScrollingText/Margin/Titles");
		names = GetNode<Label>("ScrollingText/Margin2/Names");
		
		file.Open("credits.txt",File.ModeFlags.Read);
		GD.Print(file.GetLine());
	}
	
	private void _on_BackgroundVideo_finished()
	{
		// Replace with function body.
	}
	
	private void _on_MusicPlayer_finished()
	{
		// Replace with function body.
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
