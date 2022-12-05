# Battle Beasts

## Group Members

| Name | GitHub Username | GitHub ID |
| ---- | --------------- | --------- |
| Christopher Canaday | ChrisCanaday | 89429907 |
| Colin Canonaco | ColinC5 | 89421772 |
| Noah Dahle | noahd15 | 91710565 |
| Stephanie Hill | stephhill77 | 89418194 |
| Jake Looney | 6a6c | 10680332 |

![Battle Beasts Logo](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Assets/Logo.png)

## Description

Battle Beasts is a turn based game where you customize a beast to compete in a tournament by fighting against other beasts.

### **Beast Customization**
You choose a beast, modifier, and four attacks for each tournament. Your beast changes the modifiers you can choose from, and your beast-modifier combo determines the set of attacks you can choose from.  
Every beast has base stats for maximum health and armor. Your beast is the primary factor in what attacks and modifiers you can select from.  
Your modifier changes the health and armor values of your beast as well as granting you access to a larger attack selection.  
Your attack options are presented with their name, damage per strike, and the number of strikes per attack. These will be used against your opponent to deplete their health pool to win the match.

### **Bracket**
Between matches in the tournament you can view the bracket to see your progress and what beasts you might encounter next.

### **Fight**
You and your opponent take turns attacking each other's beast. The match ends once a beast's health has reached zero resulting in the other beast winning the match.  
The damage an attack deals is determined by the armor value of the defending beast as well as the result of a minigame completed by the attacker. The damage per strike of an attack is reduced by the armor value of the defending beast, then this new damage per strike is multiplied by the number of strikes for the given attack, and finally this value is changed by the result of the minigame performed by the attacker.

### **Minigames**
After selecting an attack to perform on your turn you must complete a short minigame that changes the strength of your attack depending on your performance in the minigame. At worst your attack's strength is decreased by 50%, and at best your attack's strength is increased by 20%.  
There are three possible minigames:
- Power Bar: Press the spacebar to stop the arrow. Your performance is better the closer the arrow stops to the middle.
- Bullet Hell: Use WASD to move and avoid the bullets. The fewer bullets that hit you the better.
- Quick Time: Press the indicated key as quick as possible for the best performance.

### **Game Loop**
The player advances through the bracket by winning matches with their beast.
Winning the final match or losing at any point returns the player to the main menu.

## Installation Instructions
We have precompiled binaries for Windows, macOS, Linux, and a link to the web version on our [releases](https://github.com/utk-cs340-fall22/BattleBeasts/releases) page if you want to quickly jump into the game.

If you want to look at the game you first need to download Godot.
You can download Godot [here](https://godotengine.org/download). Make sure to download the mono version as BattleBeasts uses C#.
You may also need to download [.NET](https://dotnet.microsoft.com/en-us/download) from Microsoft in order to build.

After downloading Godot go ahead and clone the repository in the folder of your choice. After you have cloned open up Godot. On the right of the menu click **Import**. Click **Browse** and navigate the cloned repository. Select **project.godot**. Click **Import & Edit**. The Godot editor will now open and you can look around at all of the game scenes.

To build the game in Godot you click the play button in the top right of the screen. If you wish to build only the current scene you can press F6 on your keyboard.

**Have Fun!**

## Usage Instructions
To navigate in BattleBeasts you need to use the mouse to click on buttons on the screen. During a battle you will play minigames and these minigames have their own controls.

* PowerSlider: Press SpaceBar to stop the slider.
* QuickTime: Press the prompted button to end the game.
* BulletHell: Press W to move up, S to move down, A to move left, D to move right.

## Licensing
[LICENSE](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/LICENSE)
