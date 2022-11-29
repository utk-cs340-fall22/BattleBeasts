# Sprint 4

* Christopher Canaday
* ChrisCanaday
* Battle Beasts

### Files you worked on

* [Fight/Bullet.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Bullet.cs)
* [Fight/Bullet.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Bullet.tscn)
* [Fight/BulletHell.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/BulletHell.cs)
* [Fight/BulletHell.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/BulletHell.tscn)
* [Fight/Fight.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fight.cs)
* [Fight/Fight.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Fight.tscn)
* [Fight/Player.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Fight/Player.cs)
* [Menus/AttackNode.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/AttackNode.cs)
* [Menus/AttackNodeSmall.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/AttackNodeSmall.cs)
* [Menus/AttackNodeSmall.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/AttackNodeSmall.tscn)
* [Menus/Credits.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/Credits.cs)
* [Menus/Credits.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/Credits.tscn)
* [Menus/MainMenu.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/MainMenu.cs)
* [Menus/PauseMenu2.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/PauseMenu2.cs)
* [Menus/TeamSelect.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/TeamSelect.cs)
* [Menus/Tutorial.cs](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/Tutorial.cs)
* [Menus/Tutorial.tscn](https://github.com/utk-cs340-fall22/BattleBeasts/blob/main/Menus/Tutorial.tscn)

### What you accomplished
(Give a description of the features you added or tasks you accomplished. Provide some detail here. This section will be a little longer than the bulleted lists above) 

* [[#84] Beast creation doesn't reload moves.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/84)
    - Made the TeamSelect scene reload your moves upon changing the beast or modifier after having already made an original selection.
* [[#89] Change font on main menu buttons.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/89)
    - Changed the font on the Main Menu buttons to the universal font for the game.
* [[#90] Create simple tutorial scene to explain the game.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/90)
    - Created a Tutorial scene that explains how the game cycle works, how minigames work, and how the damage is calculated.
* [[#93] Change how circles work in Bullet Hell.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/93)
    - Originally circles were full circles and were inescapable by the user.
    - The circles were redesigned to have a safe spot so that the user could escape the circle.
    - This safe spot is in a random position on the circles perimeter.
* [[#94] Give player small time of invincibility when hit by a bullet in bullet hell.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/94)
    - Gave player a small instance of invincibility after being hit by a bullet so they can't be hit by multiple bullets at a time.
* [[#95] Create small information menu for the attack scene.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/95)
    - Created the AttackNodeSmall scene to display an attacks information on the attack screne.
    - Displays name, ATK, HPA, and the border of the attack is the attacks type.
* [[#106] Bullet Hell Spawns character and bullets in wrong location when in full screen.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/106)
    - When in fullscreen the player and bullets would be spawned in the bottom right of the screen instead of the middle.
    - This was caused by the godot handles the scenes themselves.
    - Fixed the player and bullets to spawn off the center of screen. (Static numbers instead of screen dependant numbers)
* [[#109] Types do not transfer to fight scene correctly.](https://github.com/utk-cs340-fall22/BattleBeasts/issues/109)
    - Changed how the border is changed in the AttackNodeSmall to allow for different types on the screen at the same time.