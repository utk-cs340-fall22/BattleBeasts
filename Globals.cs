using Godot;
using System;

public class Globals : Node
{
    public string name = "Player";
    public string[] oppName = {"CPU", "CPU", "CPU", "CPU", "CPU", "CPU", "CPU"};
    public int[] oppBeast = {-1, -1, -1, -1, -1, -1, -1};
    public int[] oppMods = {0, 0, 0, 0, 0, 0, 0};
    public int[,] oppAttacks = new int[7, 4];
    public int level = 0; // stage of bracket
    public int bracketSize = -1;
    public int fightOutcome = -1;   
    public int currBeast = 0; // DEPRECATED | currBeast = oppBeast[currentOpponentIndex]
    public int currentOpponentIndex = 0; // index of current opponent's information in global arrays | [0, 7]
    public int playerBeastIndex = 0;
    public int playerModifierIndex = 0;
    public int[] playerAttackIndices = {0, 0, 0, 0};
}
