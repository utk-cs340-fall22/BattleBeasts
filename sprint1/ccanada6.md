# Sprint 1

Name: Christopher Canaday
Github ID: ChrisCanaday
Group Name: BattleBeasts

### Planned to do

* #4 Create the Main Menu
* #9 Create the Pause Menu
* #12 Create the Options Menu
* #18 Create the Credits Page

### Did not do

I did all of my assigned work.

### Problems I encountered

The only real problem I encountered was learning how Godot operated.
Almost all of the resources for it are in GDSCript so it can be someone challenging to figure
out exactly how you should do things. It is much easier once you figure out that the 
only huge difference is the naming of the functions used. Although some of the functions
are depreciated and that can be pretty annoying.

### Issues I worked on

* #4 Create the Main Menu
* #9 Create the Pause Menu
* #12 Create the Options Menu
* #18 Create the Credits Page

### File worked on

* BattleBeasts/Menus/MainMenu.tscn
* BattleBeasts/Menus/MainMenu.cs
* BattleBeasts/Menus/OptionsMenu.tscn
* BattleBeasts/Menus/OptionsMenu.cs
* BattleBeasts/Menus/PauseMenu2.tscn
* BattleBeasts/Menus/PauseMenu2.cs
* BattleBeasts/Menus/Credits.tscn
* BattleBeasts/Menus/Credits.cs
* BattleBeasts/Menus/Credits.txt
* BattleBeasts/getcommits.sh

### Accomplished

* #4 Create the Main Menu
    - User enters this screen from the title screen and is prompted with the options Play, Options, Credits, and Exit.
    - Play sends you to the username screen which will lead you to the bracket and fight scenes.
    - Options sends you to the OptionsMenu.
    - Credits sends you to the CreditsMenu.
    - Exit closes the program.

* #9 Create the Pause Menu
    - Easily integratable into any scene. Can be linked into any scene and will work as is.
    - On the Escape key being pressed the Pause Menu will be pulled up and all scenes will be paused.
    - The user will have the option to resume or to return to the main menu.
    - On resume all scenes are unpaused and the pause menu disappears.
    - On return to main menu all scenes are unpaused and it switches to the main menu.

* #12 Create the Options Menu
    - User enters this menu from MainMenu and it allows them to change variables throughout the whole program.
    - Currently allows for the user to enable and disable fullscreen mode.

* #18 Create the Credits Page
    - User enters this menu from MainMenu and it credits the developers, Godot, and Godot's libraries.
    - Reads in the file Credits.txt to allow for easy changes to the credits.
    - User is capable of scrolling through the credits.

* Create getcommits.sh
    - I really did not want to git blame for this assignment by hand so I wrote a shell script for it instead.
    - Runs git blame -w on all the C# files in our repository and greps them with a persons name.
    - Everything gets saved to a commits.txt file so that it is easy to submit.