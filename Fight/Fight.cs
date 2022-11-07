using Godot;
using Godot.Collections;
using System;

public class Fight : Node
{
#pragma warning disable 649
  [Export]
  public PackedScene Fighter;
  [Export]
  public PackedScene PowerSliderMinigame;
  [Export]
  public PackedScene HPinterface;

#pragma warning restore 649
  Globals g;
  int isPlayerTurn, aiAttackChoice;
  int queuedAttack; // index of submitted attack in attack dictionary of attacking fighter | -1 for no queued attack | integer in [0, 3] for player | integer in [10, 13] for opponent
  int minigameResult; // -1: minigame active | [0, 100]: result of previous minigame, no minigame active
  Fighter player, opponent;
  Texture playerTexture, opponentTexture;
  HealthInterface pHealthBar, oHealthBar;
  private AudioStreamPlayer music, musicP, musicO, se;
  private static Dictionary _beastOptions = null;
  private static Dictionary _modifierOptions = null;
  private static Dictionary _attackOptions = null;
  
  /* Make JSON files accessible */
  
  private Dictionary beastOptions {
    get {
      if (_beastOptions == null) {
        var file = new File();
        file.Open("res://Data/Beasts.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _beastOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _beastOptions;
    }
  }

  private Dictionary modifierOptions {
    get {
      if (_modifierOptions == null) {
        var file = new File();
        file.Open("res://Data/Modifiers.json", File.ModeFlags.Read); // WILL BE MODIFIERS.JSON
        var text = file.GetAsText();
        _modifierOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _modifierOptions;
    }
  }

  private Dictionary attackOptions {
    get {
      if (_attackOptions == null) {
        var file = new File();
        file.Open("res://Data/Attacks.json", File.ModeFlags.Read);
        var text = file.GetAsText();
        _attackOptions = JSON.Parse(text).Result as Dictionary;
      }
      return _attackOptions;
    }
  }
  
  /* Called when the node enters the scene tree for the first time. */
  public override void _Ready() {
    Dictionary beast, modifier;
    Dictionary[] attacks = new Dictionary[4];
    int i;
    
    g = (Globals)GetNode("/root/Gm");
    isPlayerTurn = 1;
    minigameResult = 0;
    queuedAttack = -1;
    GD.Randomize();

    /* Initialize player character */
    player = (Fighter)Fighter.Instance();
    AddChild(player);
    beast = beastOptions[g.playerBeastIndex.ToString()] as Dictionary;
    modifier = modifierOptions[g.playerModifierIndex.ToString()] as Dictionary;
    for (i = 0; i < g.playerAttackIndices.Length; i++) attacks[i] = attackOptions[g.playerAttackIndices[i].ToString()] as Dictionary;
    playerTexture = ResourceLoader.Load((String)beast["texture"]) as Texture;
    player.GetNode<Sprite>("Texture").Texture = playerTexture;
    player.Position = new Vector2(190, 280);
    player.Scale = new Vector2(6, 6);
    player.Init("player", beast, modifier, attacks);

    /* Initialize player health bar to bottom right*/
    pHealthBar = (HealthInterface)HPinterface.Instance();
    AddChild(pHealthBar);
    pHealthBar.CreateLabel(g.name, (String)modifier["name"]);
   

    /* Initialize opponent character */
    opponent = (Fighter)Fighter.Instance();
    AddChild(opponent);
    beast = beastOptions[g.currBeast.ToString()] as Dictionary;
    modifier = modifierOptions[g.oppMods[g.currBeast].ToString()] as Dictionary;
    opponentTexture = ResourceLoader.Load((String) beast["texture"]) as Texture;
    opponent.GetNode<Sprite>("Texture").Texture = opponentTexture;
    opponent.Position = new Vector2(850, 170);
    opponent.Scale = new Vector2(6, 6);
    opponent.Init("opponent", beast, modifier, attacks);

    /* Initialize opponent health bar to top left*/
    oHealthBar = (HealthInterface)HPinterface.Instance();
    AddChild(oHealthBar);
    oHealthBar.CreateLabel(g.oppName[g.currBeast], (String)modifier["name"]); 
    Vector2 oHpBar = new Vector2(-600, -500);
    oHealthBar.SetPosition(oHpBar, false);
    

    // debug
    GD.Print("opponent health: ", opponent.GetHealth());
    
    /* Music */

    StartMusic();
  }

  /* AI chooses and performs an attack */
  public void AITakeTurn() {
    /* Ensure method is called on opponent's turn */

    if (isPlayerTurn == 1) {
      GD.Print("AI cannot take a turn on the player's turn");
      return;
    }

    /* Choose attack */

    aiAttackChoice = (int)(GD.Randi() % 4); // random number between 0 and 3
    queuedAttack = aiAttackChoice + 10;
    minigameResult = -1;
    // CREATE MINIGAME
    minigameResult = 100; // to remove
    isPlayerTurn = 1; // to remove
  }

  // Returns 1 if attack button signals shouldn't be obeyed
  public int CheckAttackSignalPermission () {
    if (isPlayerTurn == 0) {
      GD.Print("Cannot attack on opponent's turn.");
      return 1;
    }
    if (minigameResult == -1) {
      GD.Print("Cannot attack during active minigame.");
      return 1;
    }

    return 0;
  }

  public void MinigameReturn(int result) {
    if (result < 0 || result > 100) {
      GD.Print("Minigames may only return integer values in [0, 100]");
      return;
    }
    minigameResult = result;
  }

  // Performs the queued attack whether its from the player or the opponent
  public void PerformQueuedAttack() {
    int damage;

    // no queued attack
    if (queuedAttack == -1) return;

    // opponent attacking
    if (queuedAttack >= 10) {
      GD.Print("opponent attack ", queuedAttack - 10, " to player. player health remaining: ", player.ReduceHealth(opponent.GetAttackStrength(queuedAttack - 10)));
      isPlayerTurn = 1;
      queuedAttack = -1;
    }

    // player attacking
    else {
      damage = player.GetAttackStrength(queuedAttack) - player.GetArmor();
      if (damage < 1) damage = 1; // lowest damage dealt per strike is 1
      opponent.ReduceHealth(damage * player.GetAttackCount(queuedAttack));
      GD.Print("player attack ", queuedAttack, " dealt ", damage * player.GetAttackCount(queuedAttack), " damage. opponent health remaining: ", opponent.GetHealth());
      isPlayerTurn = 0;
      queuedAttack = -1;
    }
  }
  
  private void StartMusic(){
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    music = g.GetNode<AudioStreamPlayer>("Music");
    musicP = g.GetNode<AudioStreamPlayer>("MusicPlayer");
    musicO = g.GetNode<AudioStreamPlayer>("MusicOpponent");
    music.Stop();
    music.Stream = ResourceLoader.Load("res://Assets/Music/BattleThemeBase.mp3") as AudioStream;

        
    /* Is this how I should be determing what beasts are playing? */
    if(g.playerBeastIndex == 0)
      musicP.Stream = ResourceLoader.Load("res://Assets/Music/AurilMelody.mp3") as AudioStream;
    if(g.playerBeastIndex == 1)
      musicP.Stream = ResourceLoader.Load("res://Assets/Music/SolanacMelody.mp3") as AudioStream;
    if(g.playerBeastIndex == 2)
      musicP.Stream = ResourceLoader.Load("res://Assets/Music/AlzriusMelody.mp3") as AudioStream;
    if(g.playerBeastIndex == 3) {}
      musicP.Stream = ResourceLoader.Load("res://Assets/Music/BunpirMelody.mp3") as AudioStream;
    if(g.playerBeastIndex == 4) {}
      musicP.Stream = ResourceLoader.Load("res://Assets/Music/GlabbagoolMelody.mp3") as AudioStream;

    music.Play();
    musicP.Play();
    musicO.Play();
    
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
    /* End fight once a beast has 0 health */

    if (player.GetHealth() <= 0) {
      GD.Print("opponent defeated player");
      g.fightOutcome = 0;
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }
    else if (opponent.GetHealth() <= 0) {
      GD.Print("player defeated opponent");
      g.fightOutcome = 1;
      GetTree().ChangeScene("res://Bracket/Bracket.tscn");
    }
    
    pHealthBar.AdjustHealth((player.GetHealth() * 100) / player.GetMaxHealth()); // adjusts the player's HP bar
    pHealthBar.UpdateHealthFrac(player.GetMaxHealth(), player.GetHealth()); // adjusts players's HP fraction
    oHealthBar.AdjustHealth((opponent.GetHealth() * 100) / opponent.GetMaxHealth()); // adjusts the opponent's HP bar
    oHealthBar.UpdateHealthFrac(opponent.GetMaxHealth(), opponent.GetHealth()); // adjusts opponent's HP fraction
    
    /* Everything below is skipped if a minigame is active */

    if (minigameResult == -1) return;

    /* Perform queued attack */

    PerformQueuedAttack();

    /* AI turn */

    // it seems signals can be recieved in the middle of _Process() so checking health here is necessary
    if (isPlayerTurn == 0 && opponent.GetHealth() != 0) {
      AITakeTurn();
    }
  }

  /* Signals */

  private void _on_B0_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 0;
    minigameResult = -1;
    AddChild(PowerSliderMinigame.Instance());
  }

  private void _on_B1_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 1;
    minigameResult = -1;
    AddChild(PowerSliderMinigame.Instance()); // some logic to determine what minigame to create will be needed eventually
  }

  private void _on_B2_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 2;
    minigameResult = -1;
    AddChild(PowerSliderMinigame.Instance());
  }

  private void _on_B3_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 3;
    minigameResult = -1;
    AddChild(PowerSliderMinigame.Instance());
  }
}
