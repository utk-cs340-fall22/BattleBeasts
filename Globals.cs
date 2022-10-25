using Godot;
using System;

public class Globals : Node
{
    public string name = "Player";
    public string[] oppName = {"CPU", "CPU", "CPU", "CPU", "CPU", "CPU", "CPU"};
    public int[] oppBeast = {-1, -1, -1, -1, -1, -1, -1};
    public int level = 0;
    public int bracketSize = -1;
    public int fightOutcome = -1;   
    public int currBeast = -1;
    public int playerBeastIndex = 0;
    public int playerModifierIndex = 0;
    public int[] playerAttackIndices = {0, 0, 0, 0};
}
