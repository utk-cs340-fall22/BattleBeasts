using Godot;
using System;

public class HealthInterface : Control
{
    /* Controls the health bar's progress */
    public void AdjustHealth(int health)
    {
        GetNode<ProgressBar>("HealthBar").Value = health;
    }

    /* Controls the fraction the appears in the health bar*/
    public void UpdateHealthFrac(int maxHealth, int currHealth)
    {
        GetNode<Label>("HealthFrac").Text = currHealth + " / " + maxHealth;
    }

    /* Creates the label for player/opponent name and modifier */
    public void CreateLabel(string name, string modifier, string armor)
    {
      GetNode<Label>("UserMods").Text = name + " | " + modifier + " | " + armor + " armor";
      GetNode<Label>("UserMods").SetAlign((Godot.Label.AlignEnum)2);
    }

    
    
}
