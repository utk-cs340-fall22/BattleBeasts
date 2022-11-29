# Product Requirements Document
Name: Christopher Canaday

Product Name: Battle Beasts

## Background
In the current market there are many games that allow you to battle with different beasts but the customization of these beasts is very limited.
This coupled with the repetitiveness of the different battles leaves these games with a low amount of replayability.
This leaves a gap in the market for a game with a high range of customization and a large amount of replayability.

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
1. **Pause Menu** As a player, I want the ability to stop playing the game in the middle of a fight, so that I don't have to finish the tournament to exit the game.
1. **Volume Setting** As a player, I want the ability to change the audio settings of the games, so that I can listen to the game without getting ear damage.
1. **Main Menu** As a player, I want the ability to seamlessly move throughout the games menus, so that I can have a good experience.
1. **Tutorial** As a player, I want a tutorial, so that I can understand how the game works.
1. **Fullscreen Setting** As a player, I want the ability to change the screen to fullscreen, so that I can be fully immersed in the game.
1. **Detailed Attack Nodes** As a player, I want the ability to at a glance tell what an ability does and what type it is, so that I can strategize against enemy beasts.
1. **BulletHell Minigame** As a player, I want a unrepetitive minigame to play, so that I do not get bored of the game.
1. **Change Options in Pause Menu** As a player, I want the ability to change my settings in the middle of a fight, so that I don't have to wait till the end of the tournament to update my settings.

## Technologies to be used
Godot will be used as the game engine and C# will be used as the programming language that all the logic will be in.