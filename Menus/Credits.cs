using Godot;
using System;
using System.Collections.Generic;

public class Credits : Control
{
  private HBoxContainer scrolling_text;
  private Label titles, names;
  private ItemList titlest, namest;
  private MarginContainer margin, margin2;
  private File file = new File();
  private List<String> lines = new List<string>();

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
	string tmp;
	int i;
	scrolling_text = GetNode<HBoxContainer>("ScrollingText");
	titles = GetNode<Label>("ScrollingText/Margin/Titles");
	names = GetNode<Label>("ScrollingText/Margin2/Names");
	margin = GetNode<MarginContainer>("ScrollingText/Margin");
	margin2 = GetNode<MarginContainer>("ScrollingText/Margin2");
	namest = GetNode<ItemList>("ScrollingText/Margin2/NamesT");
	titlest = GetNode<ItemList>("ScrollingText/Margin/TitlesT");
	
	file.Open("res://Menus/credits.txt",File.ModeFlags.Read);
	//GD.Print(file.GetLine());
	//tmp = file.Getline();
	while(!file.EofReached()){
	  lines.Add(file.GetLine());
	}
	//file.close();
	
	for(i = 0; i < lines.Count; i++){
	  GD.Print(lines[i]);
	}
	
	for(i = 0; i < lines.Count; i++){
	  if(lines[i].Length != 0 && lines[i][0] == '['){
		titlest.AddItem(lines[i]);
		//titles.Text = lines[i];
	  }else if(lines[i].Length != 0){
		//names.Text = lines[i];
		namest.AddItem(lines[i]);
	  }
	}
  }
  
  private void _on_BackgroundVideo_finished()
  {
	// Replace with function body.
  }
  
  private void _on_MusicPlayer_finished()
  {
	// Replace with function body.
  }

  private void _on_Back_pressed()
  {
	GetTree().ChangeScene("res://Menus/MainMenu.tscn");
  }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
