# BattleBeasts

## How to edit this code w/ Godot
Clone this repo. Open Godot and click Import. Type the path of the folder and click "Import&Edit".
To commit: In the highest level directory in the project on your machine do "git add ." to add any files changed by you or godot in response to things you did.

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
  - Player customizes their beast before each tournament
    - Base beast, type, attacks
  - Player fights through bracket. Winning advances through the bracket, losing is game over.
- ***Fights***
  - Player and AI opponent take turns performing actions
  - Minigames like in Undertale combat, Sonic real-time interaction, Sonic Chronicles combat
    - Successful completion boosts attack power or reduces damage taken
  - Minimal to no RNG
    - Quick time events/minigames as alternative for strength modifiers (ex. critical hits) (favor predictability and player agency)
  - Attacks have simple stats/effects
    - Label attacks with difficulty/type of minigame. Stronger attacks have harder minigames.
- ***Scenes***
  - Main menu
    - Play, options, credits, quit
  - Beast customization
    - Select beast, type, move set
  - Bracket
    - Short animation of moving up in bracket
    - Display player's beast and opponent's beast before each match
    - Button to enter fight
    - Transition between fight and bracket
  - Fight
    - Display involved beasts, attack options, beast health values
- ***Stretch***
  - Animations, sound effects
  - More sprites/abilties
  - Different tournament formats other than bo1 single elimination
  - More fight quick times/mini games. More exclusive to abilities
  - Difficulty settings
  - Unlock beasts, types, and attacks after each game to use in future games
    - Save progress

## Functionality
### Fights
The Fight scene instantiates two Fighter nodes so one is controlled by the player and the other by the computer. The Fighter nodes are instantiated with a beast texture and information about the beast.
Players can choose their beast/type as well as four attacks. Available types to choose from depend on the beast and attacks available depend on the beast and type.
Base stats (hp, armor, type...) for all beasts will be stored in one text file in a specific format. Making a program to modify this file may be helpful.
Information about attacks will all be stored in another similar text tile.
Information in these files will be coded with positive integer identifiers which will correspond to the order in which the beast/type/attack information appears in its resective file.

The beast, type, and attacks are chosen in menus before the Fight scene starts. The identifiers for the chosen stats will be passed from the menus to the Fight scene script, which will then pass the appropriate information to the initialization function in the Player and Opponent Fighter nodes. The Fighter nodes will then set their information.

The information about attacks will be stored in their respective Fighter nodes. When an attack is sent the information on that attack will be sent to the Fight scene script which will calculate the result of the attack and will update the defending Fighter.
