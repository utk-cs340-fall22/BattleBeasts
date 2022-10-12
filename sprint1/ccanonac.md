# Sprint 1

Name: Colin Canonaco  
GitHub ID: ColinC5  
Group: BattleBeasts

### Planned to do

- [#6](https://github.com/utk-cs340-fall22/BattleBeasts/issues/6) Create game state for fight between two beasts
- [#7](https://github.com/utk-cs340-fall22/BattleBeasts/issues/7) Allow two beasts to damage each other and be defeated when their HP reaches 0
- [#8](https://github.com/utk-cs340-fall22/BattleBeasts/issues/8) Start enemy beast AI
- [#17](https://github.com/utk-cs340-fall22/BattleBeasts/issues/17) Fight health label

### Did not do

- [#8](https://github.com/utk-cs340-fall22/BattleBeasts/issues/8) Start enemy beast AI
- [#17](https://github.com/utk-cs340-fall22/BattleBeasts/issues/17) Fight health label

### Problems encountered

- Didn't have access to the GitHub repository for half of the first week
- Underestimated amount of learning required to use a new game engine

### Issues worked on

- [#6](https://github.com/utk-cs340-fall22/BattleBeasts/issues/6) Create game state for fight between two beasts
- [#7](https://github.com/utk-cs340-fall22/BattleBeasts/issues/7) Allow two beasts to damage each other and be defeated when their HP reaches 0

### Files worked on

- [BattleBeasts/README.md](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/README.md)
- [BattleBeasts/Fight/Fighter.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fighter.cs)
- [BattleBeasts/Fight/Fighter.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fighter.cs)
- [BattleBeasts/Fight/Fight.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fight.cs)
- [BattleBeasts/Fight/Fight.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fight.tscn)

### Accomplished

- [BattleBeasts/README.md](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/README.md)
  - Organized our ideas and plans for the functionality and design of the game
  - Described how our constraints may impact our design decisions

- [#6](https://github.com/utk-cs340-fall22/BattleBeasts/issues/6) Fight game state
  - Created the fighter template. The beasts for the player and the opponent are both fighter instances with the same base functionality.
  - A fighter instance stores information about its stats like its controller, texture, health, and attack information.
  - Contains public methods for accessing the information it stores.

- [#7](https://github.com/utk-cs340-fall22/BattleBeasts/issues/7) Fight scene
  - Created the fight scene which coordinates everything that happens in the fight scene.
  - The fight sceen instantiates the player and opponent fighters. It passes all information stored by a fighter to the fighters while controlling their positions and scale.
  - Created a basic GUI for the player to select an attack.
  - Connected to signals sent by the GUI buttons that trigger the use of the public methods of the fighters to modify the opponent's health value.
  - Changes the scene back to the bracket scene once the opponent's beast's health is 0.