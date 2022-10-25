# Sprint 2

* Name: Christopher Canaday
* Github ID: ChrisCanaday
* Group Name: BattleBeasts

## Planned to do

* [#32](https://github.com/utk-cs340-fall22/BattleBeasts/issues/32) Rewrite options menu to be portable
* [#33](https://github.com/utk-cs340-fall22/BattleBeasts/issues/33) Add Volume change functionality to options menu
* [#34](https://github.com/utk-cs340-fall22/BattleBeasts/issues/34) Create Power Bar minigame
* [#36](https://github.com/utk-cs340-fall22/BattleBeasts/issues/36) Update the getcommits.sh
* [#37](https://github.com/utk-cs340-fall22/BattleBeasts/issues/37) Add options menu link to the pause menu
* [#38](https://github.com/utk-cs340-fall22/BattleBeasts/issues/38) Link MainMenu Settings Button to new OptionsMenu

## Did not do

I did all of my assigned work.

## Problems I encountered

I had a very smooth time during this sprint and encountered no real problems. The only thing that I would
consider was that I was taking input the wrong way. I didn't realize that upon an input event in godot
it actually calls a predefined function. I was instead checking every frame for input and that caused
a lot of issues. But I now know how that works.

## Issues I worked on

* [#32](https://github.com/utk-cs340-fall22/BattleBeasts/issues/32) Rewrite options menu to be portable
* [#33](https://github.com/utk-cs340-fall22/BattleBeasts/issues/33) Add Volume change functionality to options menu
* [#34](https://github.com/utk-cs340-fall22/BattleBeasts/issues/34) Create Power Bar minigame
* [#36](https://github.com/utk-cs340-fall22/BattleBeasts/issues/36) Update the getcommits.sh
* [#37](https://github.com/utk-cs340-fall22/BattleBeasts/issues/37) Add options menu link to the pause menu
* [#38](https://github.com/utk-cs340-fall22/BattleBeasts/issues/38) Link MainMenu Settings Button to new OptionsMenu

## File worked on

* [BattleBeasts/Menus/MainMenu.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/MainMenu.cs)
* [BattleBeasts/Menus/OptionsMenu2.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/OptionsMenu2.tscn)
* [BattleBeasts/Menus/OptionsMenu2.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/OptionsMenu2.cs)
* [BattleBeasts/Menus/PauseMenu2.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/PauseMenu2.tscn)
* [BattleBeasts/Menus/PauseMenu2.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/PauseMenu2.cs)
* [BattleBeasts/getcommits.sh](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/getcommits.sh)
* [BattleBeasts/Fight/PowerSliderMiniGame.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/PowerSliderMiniGame.tscn)
* [BattleBeasts/Fight/PowerSliderMiniGame.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/PowerSliderMiniGame.cs)

## Accomplished

* [#32](https://github.com/utk-cs340-fall22/BattleBeasts/issues/32) Rewrite options menu to be portable
    - The old OptionsMenu was setup in a way that required you to completely leave a scene to use it.
    - This causes problems when trying to change settings midgame.
    - Changed so that it is now a child of a scene and can be unhidden to allow for the user to change settings.
    - Currently implemented as part of the MainMenu and the OptionsMenu.

* [#33](https://github.com/utk-cs340-fall22/BattleBeasts/issues/33) Add Volume change functionality to options menu
    - We now have music in the game!
    - Having music requires the user to be able to change the volume.
    - Is implemented as a sliding bar that allows you to change the volume by from 0% to 100%.

* [#34](https://github.com/utk-cs340-fall22/BattleBeasts/issues/34) Create Power Bar minigame
    - The Power Bar minigame is similar to what you use in a game like golf for the Wii.
    - You have a slider with a tick moving back and forth.
    - Your goal is to press when the tick is in the very center.
    - After the user presses SpaceBar the minigame records your score and sends it to the fight scene to be used to increase or decrease your damage.

* [#36](https://github.com/utk-cs340-fall22/BattleBeasts/issues/36) Update the getcommits.sh
    - For Sprint 2 we need all of the commits to only be commits from the last two weeks.
    - I updated the shellscript to accomidate this and also cleaned it up a little bit.

* [#37](https://github.com/utk-cs340-fall22/BattleBeasts/issues/37) Add options menu link to the pause menu
    - Any good game allows you to change your settings midgame.
    - I added a button to the pause menu called options.
    - When pressed it opens the options menu scene and allows you to change options.

* [#38](https://github.com/utk-cs340-fall22/BattleBeasts/issues/38) Link MainMenu Settings Button to new OptionsMenu
    - I removed the link to the old options menu and connected the new one.