# Sprint 4

- Name: Colin Canoaco
- GitHub: ColinC5
- Group: Battle Beasts

### Files you worked on

- [Bracket.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Bracket/Bracket.cs)
- [Bullet.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Bullet.cs)
- [BulletHell.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/BulletHell.cs)
- [Fight.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fight.cs)
- [Fighter.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fighter.cs)
- [Globals.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Globals.cs)
- [HealthInterface.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/HealthInterface.cs)
- [Player.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Player.cs)
- [QuickTimeKeyboard.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/QuickTimeKeyboard.cs)
- [Textbox.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Textbox.cs)
- [TeamSelect.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/TeamSelect.cs)
- [Tutorial.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/Tutorial.cs)

### What you accomplished

- [#71](https://github.com/utk-cs340-fall22/BattleBeasts/issues/71) Adjust attack damage values to be more diverse and deal more damage overall
  - Fights were taking too long to complete and attack stats were being repeated due to the low target for average total damage.
  - I increased the maximum health for all beasts and made all attacks deal more damage so most fights end by the 8th turn.
    - Increased attack damage allowed me to diversify attack stats. Total attack damage is determined by multiplying the strike damage by the strike count so targetting a higher average for total damage increased the number of values I could use.
- [#91](https://github.com/utk-cs340-fall22/BattleBeasts/issues/91) Minigame spawned depends on attack
  - I defined what minigame each attack spawns every time it is performed.
- [#99](https://github.com/utk-cs340-fall22/BattleBeasts/issues/99) Text appears describing what attack was performed by either the player or opponent and how much damage it dealt
  - I created a text box that displays information about what attack was just played, who performed it, who recieved the damage, and how much damage it dealt.
  - The text is quickly revealed one character at a time. Along with this I added a delay after every attack where neither the opponent nor the player can select their next attack. Without this attacks could be performed before the beast hit animations finished and the opponent would attack the instant the player chose an attack. This improved the pacing of the fights.
- [#104](https://github.com/utk-cs340-fall22/BattleBeasts/issues/104) More modifiers and beasts. Have modifiers give access to more attacks.
  - I gave each beast a unique set of modifiers they can take on.
  - I defined attacks that each modifiers adds to the set of allowed attacks for a beast.
  - I added another beast, another modifier, and attacks for the beast.
- Added correct beast customization logic to bracket
  - The player customizes their beast in the beast customization scene, so I implemented a modified version of that logic in the bracket so it customizes the opposing beasts following the same rules that apply to the player's customisation process.
- Rebalanced stats
  - I rebalanced the stats of some beasts, modifiers, and attacks. Most notably armor turned out to be very strong so I made changes considering that.
- Balanced quick time and bullet hell minigames
  - The quick time minigame was originally too difficult to score well on so I made the range of achievable results wider and I made it possible but challenging to score well.
  - Once the bullet hell minigame was finished I made it so each instance of the minigame either spawns walls or circles, but not both. I balanced the player speed, bullet speed, and formation spawn times to make it possible but challenging to complete perfectly.
- Bracket bug fixing
  - I fixed multiple bugs in the bracket. Many of which had to do with how information about the opposing beasts are stored and recalled.
- Added armor to fight name tags
  - I made the fight scene name tags display the armor value of its beast. I also changed the positions of things in the fight scene as well as the settings for the text in the nametags so that they can't extend off of the screen.
- Added more names for opponents
  - I added more names that the opponents can take on.
  - I made it so that every name is chosen at most once in every game.
- Added description of fight nametags to tutorial
  - The fight scene nametags contain the armor value of their beast, but it isn't labeled to minimize its footprint. I added a description of what the information in the nametags means to explain this.
