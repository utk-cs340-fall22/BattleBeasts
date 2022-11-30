# Product Requirements Document
Name: Colin Canonaco

Product Name: Battle Beasts

## Background
Turn based combat in video games often lacks depth, interactivity, and mechanical skill expression. Pokemon games for example attempt to create depth by introducing a lot of complexity in Pokemon stats, but this isn't appreciated by inexperienced players and requires extensive study to understand. Pokemon also lacks interactivity in that the player may only make decisions, not actively engage with the game. Undertale combat is the opposite where combat is not complex but there is ample opportunity to actively engage with the game through minigames. Sonic Chronicles is an exmaple of a game that attempted to find a balance of complexity and interactivity through team customization, plenty of combat options, team synergies, and minigames that impact the fight. The execution of this left plenty of room for improvement, and Battle Beasts takes advantage of this.

## Project Overview
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

## Features
1. **Customize beast.** As a player, I want to customize my beast so that I can try out different builds and strategies as well as make replaying the game more fun.
2. **Play minigames.**  As a player, I want to play a minigame for each attack so that I can be actively engaged in the game and influence the battle.
3. **Transparent attack stats.** As a player, I want to see the stats of the attacks I can choose from so that I can strategize with my attack set.
4. **Transparent opponent stats.** As a player, I want to see the stats of my opponent's beast so that I can make an informed decision on what attacks to play.
5. **Select attack.** As a player, in the fight I want to see the attacks I chose, their stats, and select one to be played so that I can fight my opponent my way.
5. **Tutorial.** As a player, I want a tutorial to teach me how the game works so that I can learn quickly and start playing and having fun sooner.
7. **Visual and auditory feedback.** As a player, I want to recieve visual and auditory feedback so that the game feels responive to my actions.
8. **See progress in tournament.** As a player, I want to see where I am in a tournament so that I can see my potential future opponents and know what's at stake.
9. **Pause menu.** As a player, I want to be able to pause the game in the middle of a fight so that I can take a quick break, change some game settings, or exit to the main menu.
10. **Fight text description.** As a player, I want to see what attacks my opponent is playing and how much damage I'm dealing and recieving so that I know how effective certain attacks are.

## Technologies to be used
The project will be created using the Godot game engine and programmed in C#. Graphical assets will be created using programs capable of editing 2D bitmap images, such as Paint.NET.