using Godot;
using System;

public class Globals : Node
{

    public string name = "Player";
    public string[] opp_name = {"CPU", "CPU", "CPU", "CPU", "CPU", "CPU", "CPU"};
    public int[] opp_beast = {-1,-1,-1,-1,-1,-1,-1};
    public int player_beast = -1;
    public int level = 0;
    public int bracket_size = -1;
    public int fight_outcome = -1;
    
}
