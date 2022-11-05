# Sprint 2

Name: Colin Canonaco  
GitHub ID: ColinC5  
Group: BattleBeasts

### Planned to do

- [#8](https://github.com/utk-cs340-fall22/BattleBeasts/issues/8) Start enemy beast AI
- [#25](https://github.com/utk-cs340-fall22/BattleBeasts/issues/25) Create JSON files describing beast stats, type modifiers, attacks. Read from these files
- [#44](https://github.com/utk-cs340-fall22/BattleBeasts/issues/44) Create accessor for minigames to indicate their completion to the Fight scene

### Did not do

- I completed everything I planned to work on

### Problems encountered

- None

### Issues worked on

- [#8](https://github.com/utk-cs340-fall22/BattleBeasts/issues/8) Start enemy beast AI
- [#17](https://github.com/utk-cs340-fall22/BattleBeasts/issues/17) Figure out how fight health labels will be instantiated
- [#25](https://github.com/utk-cs340-fall22/BattleBeasts/issues/25) Create JSON files describing beast stats, type modifiers, attacks. Read from these files
- [#26](https://github.com/utk-cs340-fall22/BattleBeasts/issues/26) Update README.md with game design plan
- [#43](https://github.com/utk-cs340-fall22/BattleBeasts/issues/43) Fix spacebar pressing previously pressed GUI button
- [#44](https://github.com/utk-cs340-fall22/BattleBeasts/issues/44) Create accessor for minigames to indicate their completion to the Fight scene

### Files worked on

- [BattleBeasts/Fight/Fighter.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fighter.cs)
- [BattleBeasts/Fight/Fight.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fight.cs)
- [BattleBeasts/Bracket/Bracket.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Bracket/Bracket.cs)
- [BattleBeasts/Menus/TeamSelect.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/TeamSelect.cs)
- [BattleBeasts/Globals.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Globals.cs)
- [BattleBeasts/Username/Enter_Name.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Username/Enter_Name.cs)
- [BattleBeasts/Data/Attacks.json](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Data/Attacks.json)
- [BattleBeasts/Data/Beasts.json](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Data/Beasts.json)
- [BattleBeasts/Data/Modifiers.json](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Data/Modifiers.json)


### Accomplished

- [#8](https://github.com/utk-cs340-fall22/BattleBeasts/issues/8) Start enemy beast AI
  - Enemy beast can make decisions for what attack to submit. Currently picks an attack randomly.
- [#17](https://github.com/utk-cs340-fall22/BattleBeasts/issues/17) Figure out how fight health labels will be instantiated
  - Researched a good way to implement the health bars and communicated that to whoever would take up that task.
- [#25](https://github.com/utk-cs340-fall22/BattleBeasts/issues/25) Create JSON files describing beast stats, type modifiers, attacks. Read from these files
  - Created JSON files to store information about beast stats, modifiers, and attacks. Godot has functionality for parsing these files and since I have them organized with a simple integer index they are easy to use.
  - The beast customization records the indices of the choices the player made and the bracket and fight scenes use those indices to find the information they need from the JSON files.
- [#26](https://github.com/utk-cs340-fall22/BattleBeasts/issues/26) Update README.md with game design plan
  - Made the existing description of the game's design definitive. I also added a description for how the the JSON files are organized.
- [#43](https://github.com/utk-cs340-fall22/BattleBeasts/issues/43) Fix spacebar pressing previously pressed GUI button
  - Fixed this small issue that prevented the fight scene from functioning properly after a minigame had been started.
- [#44](https://github.com/utk-cs340-fall22/BattleBeasts/issues/44) Create accessor for minigames to indicate their completion to the Fight scene
  - Created a function in the fight scene that minigames can use to set a variable in the fight scene used to indicate the status and result of the most recently instanced minigame.