# Sprint 3

Name: Colin Canonaco  
GitHub ID: ColinC5  
Group: BattleBeasts

### Planned to do

- [#40](https://github.com/utk-cs340-fall22/BattleBeasts/issues/40) Beast creation dropdown box options change/disappear depending on what beast, modifier, and attacks have been chosen
- [#63](https://github.com/utk-cs340-fall22/BattleBeasts/issues/63) Fight scene reads modifier and attacks chosen by player/bracket and applies them to the fighters FunctionalityBack-end logic implementation
- [#65](https://github.com/utk-cs340-fall22/BattleBeasts/issues/65) Beast attack damage considers armor and minigame result
- [#67](https://github.com/utk-cs340-fall22/BattleBeasts/issues/67) Opponent performs an attack twice every time the player attacks
- [#71](https://github.com/utk-cs340-fall22/BattleBeasts/issues/71) 
Adjust attack damage values to be more diverse and deal more damage overall

### Did not do

- [#71](https://github.com/utk-cs340-fall22/BattleBeasts/issues/71) 
Adjust attack damage values to be more diverse and deal more damage overall

### Problems encountered

- Spent five hours figuring out how Godot arrays and dictionaries work with its JSON parser so I could get the information into drop down boxes

### Issues worked on

- [#40](https://github.com/utk-cs340-fall22/BattleBeasts/issues/40) Beast creation dropdown box options change/disappear depending on what beast, modifier, and attacks have been chosen
- [#63](https://github.com/utk-cs340-fall22/BattleBeasts/issues/63) Fight scene reads modifier and attacks chosen by player/bracket and applies them to the fighters FunctionalityBack-end logic implementation
- [#65](https://github.com/utk-cs340-fall22/BattleBeasts/issues/65) Beast attack damage considers armor and minigame result
- [#67](https://github.com/utk-cs340-fall22/BattleBeasts/issues/67) Opponent performs an attack twice every time the player attacks

### Files worked on

- [BattleBeasts/Fight/Fighter.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fighter.cs)
- [BattleBeasts/Fight/Fight.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fight.cs)
- [BattleBeasts/Fight/Bullet.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Bullet.cs)
- [BattleBeasts/Fight/BulletHell.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/BulletHell.cs)
- [BattleBeasts/Menus/TeamSelect.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/TeamSelect.cs)
- [BattleBeasts/Data/Attacks.json](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Data/Attacks.json)
- [BattleBeasts/Data/Beasts.json](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Data/Beasts.json)
- [BattleBeasts/Data/Modifiers.json](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Data/Modifiers.json)


### Accomplished

- [#40](https://github.com/utk-cs340-fall22/BattleBeasts/issues/40) Beast creation dropdown box options change/disappear depending on what beast, modifier, and attacks have been chosen
  - I made it so that the player has to choose a beast, modifier, and attacks in that order. The attacks they can choose from depend on the beast and modifier they chose, so when the beast or modifier fields is changed their attack selections get reset. The set of attacks the player can choose from is the union of the set of attacks compatible with the selected beast and the selected modifier. This compatibility information is read from json files and is accessed using dictionaries.
- [#63](https://github.com/utk-cs340-fall22/BattleBeasts/issues/63) Fight scene reads modifier and attacks chosen by player/bracket and applies them to the fighters
  - The beast customization and bracket indicate the player's selecions in Globals.cs and the fight scene reads this information. It applies this information to the fighters and is now used in the fight.
- [#65](https://github.com/utk-cs340-fall22/BattleBeasts/issues/65) Beast attack damage considers armor and minigame result
  - The damage dealt by a beast's attack is now determined by the stats of the attack but also the defending beast's armor stat and the player's performance in the minigame. Some attacks strike multiple times when performed and armor reduces the strength of each of those strikes, so attacks with more strikes are affected by armor more than attacks with fewer strikes.
- [#67](https://github.com/utk-cs340-fall22/BattleBeasts/issues/67) Opponent performs an attack twice every time the player attacks
  - Fixed a bug where the opponent attacked twice for each time the player attacked. This was due to a leftover line of code from a previous design for how the player and opponent take turns.