using Godot;
using System;

public class Globals : Node
{
    public string[] names = {"Noah", "Jake", "Steph", "Chris", "Colin", "Reid", "Adam", "CPU"};
    public string name = "Player";
    public string opp_name;
    public int level = 0;
    public int bracket_size = -1;
    public int fight_outcome = -1;
    public Texture beast = ResourceLoader.Load("res://Assets/Character Sprites/Bunpir.png") as Texture;
    public Texture opp_beast = ResourceLoader.Load("res://Assets/Character Sprites/Glabbagool.png") as Texture;
}
