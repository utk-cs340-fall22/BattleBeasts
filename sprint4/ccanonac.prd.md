# Product Requirements Document
Name: Colin Canonaco

Product Name: Battle Beasts

## Background
(Explain the user need or market opportunity)

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
(Give at least 8 user stories to describe required features. These can come from the issues assigned to you during the 4 sprints, or you 
can create new items. Give a title or feature name for each story. Example: 
1. **Account holder withdraws cash.** As a customer,	I want to withdraw cash from an ATM,	so that I don’t have to wait in line at the bank.
2. **Create Account.** As a new user, I want to register by creating a username and password so that the system can remember me and my data)

## Technologies to be used
Describe any tools and technologies to be used in the project. Include the languages, third-party libraries, and tools that will be used.