# BattleBeasts

## How to edit this code w/ Godot
Clone this repo. Open Godot and click Import. Type the path of the folder and click "Import&Edit".

### Github usernames:

 - Noah Dahle - noahd15
 - Colin Canonaco - ColinC5
 - Jake Looney - 6a6c
 - Stephanie Hill - stephhill77
 - Christopher Canaday - ChrisCanaday

## Game Design
- ***Constraints***
  - Short game loop
    - The game needs to have a game loop that can be experienced in a substantial quantity in a few minutes (in the time we can present the game)
      - Tournament bracket for multiple fights with similar base functionality
    - Other parts of the game may be well served to be understood without much demonstration
      - "In our game we also have a progression system, a short campaign with exploration..."
  - Requires minimal onboarding
    - Very transparent game mechanics (no hidden stats)
    - Simple set of actions for player
  - Reuse assets
    - Diminishing returns with creating additional sprites
    - Recoloring (one sprite can represent different types)
- ***Game Loop***
  - Game consists of the player trying to advance through a tournament bracket
  - Player customizes their beast(s) before each tournament
    - Cards?
    - Skill points?
    - See opponents' lineups?
  - Player fights through bracket
    - Damage to beast/team persists through tournament? Have to strategize usage of resources?
    - Each fight is isolated? Customization before each fight?
  - Earn more beasts/abilities after each tournament? Access harder tournaments? Save playthrough information to file to load at another time?
- ***Fights***
  - Players (and computers) take turns performing actions
  - Minigames like in Undertale combat
    - Create a mix of turn-based and real-time gameplay
  - Minimal to no RNG
    - Quick time events/minigames as alternative for strength modifiers (favor player agency)
  - Attacks have simple stats/effects
    - Label attacks with difficulty of minigame/quick time events? Stronger attacks have harder minigames/quick time events?
- ***Scenes***
  - Main menu
    - Play, options, credits, quit
  - Beast customization
    - Select beast, type, move set, skill categories
  - Bracket
    - Short animation of moving up in bracket
    - Display randomly generated opposing beasts
    - Button to enter fight
    - Transition between fight and bracket
  - Fight
    - Display involved beasts, attack options
- ***Stretch***
  - Campaign/adventure like Pokemon
  - More sprites/abilties
  - Different tournament formats other than bo1 single elimination
  - More fight quick times/mini games. More exclusive to abilities
  - Difficulty settings
