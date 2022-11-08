# Sprint 2

* Name: Christopher Canaday
* Github ID: ChrisCanaday
* Group Name: BattleBeasts

## Planned to do

* [#59](https://github.com/utk-cs340-fall22/BattleBeasts/issues/59) Can leave pause menu while in options menu
* [#66](https://github.com/utk-cs340-fall22/BattleBeasts/issues/66) Recreate beast creation and attack choice screen
* [#68](https://github.com/utk-cs340-fall22/BattleBeasts/issues/68) Options menu back button crashes
* [#69](https://github.com/utk-cs340-fall22/BattleBeasts/issues/69) Create a bullet hell minigame

## Did not do

I did all of my assigned work.

## Problems I encountered

I had a very smooth time during this sprint and encountered no real problems. Only 

## Issues I worked on

* [#59](https://github.com/utk-cs340-fall22/BattleBeasts/issues/59) Can leave pause menu while in options menu
* [#66](https://github.com/utk-cs340-fall22/BattleBeasts/issues/66) Recreate beast creation and attack choice screen
* [#68](https://github.com/utk-cs340-fall22/BattleBeasts/issues/68) Options menu back button crashes
* [#69](https://github.com/utk-cs340-fall22/BattleBeasts/issues/69) Create a bullet hell minigame

## File worked on

* [BattleBeasts/Fight/Bullet.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Bullet.cs)
* [BattleBeasts/Fight/Bullet.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Bullet.tscn)
* [BattleBeasts/Fight/BulletHell.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/BulletHell.cs)
* [BattleBeasts/Fight/BulletHell.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/BulletHell.tscn)
* [BattleBeasts/Fight/Player.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Player.cs)
* [BattleBeasts/Fight/Player.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Player.tscn)
* [BattleBeasts/Menus/AttackNode.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/AttackNode.cs)
* [BattleBeasts/Menus/AttackNode.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/AttackNode.cs)
* [BattleBeasts/Menus/AttackNode.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/AttackNode.cs)
* [BattleBeasts/Menus/MainMenu.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/MainMenu.cs)
* [BattleBeasts/Menus/OptionsMenu2.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/OptionsMenu2.cs)
* [BattleBeasts/Menus/PauseMenu2.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/PauseMenu2.cs)
* [BattleBeasts/Menus/TeamSelect.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/TeamSelect.cs)
* [BattleBeasts/Menus/TeamSelect.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/TeamSelect.tscn)

## Accomplished

* [#59](https://github.com/utk-cs340-fall22/BattleBeasts/issues/59) Can leave pause menu while in options menu
    - We had a bug where when you pressed escape in the options menu it would leave the pause menu.
    - This was fixed by adding in a check to make sure that you are on the pause menu before you can leave.

* [#66](https://github.com/utk-cs340-fall22/BattleBeasts/issues/66) Recreate beast creation and attack choice screen
    - The old attack choice screen was underwhelming and gave no information about the attack itself.
    - I reworked the attack choice screen to have a list that we can change based off the beast and modifier selection.
    - I then created an AttackNode which contained all the information and was visually appealing.
    - Once the player selects the beast and modifier the attacks are loaded onto the right and they are able to select the attacks.
    - They are capable of selecting up to 4 attacks (there is 6 for each beast type currently).
    - They are able to unselect and select other attacks as well.
    - Once they have selected their 4 attacks they are able to move on to the bracket screen.


* [#68](https://github.com/utk-cs340-fall22/BattleBeasts/issues/68) Options menu back button crashes
    - We had a bug where when you pressed the back button in the options menu it would crash.
    - This only happened in the MainMenu.
    - This was fixed by adding an indicator to whether you entered the options menu from the MainMenu or the PauseMenu.

* [#69](https://github.com/utk-cs340-fall22/BattleBeasts/issues/69) Create a bullet hell minigame
    - We currently only had one minigame (that being the PowerSliderMinigame) so I made another.
    - This minigame spawns in a player character and then spawns bullets around them.
    - The objective is to get hit by as few bullets as possible.
    - You have a health bar that starts at 100 and it is the indicator of your success.
    - Your health value at the end of the game is what determines your score on the minigame.
    - To finish the game you either last for 10 seconds or die.