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
  public PackedScene BulletHellMinigame;
  [Export]
  public PackedScene QuickTimeMinigame;
  [Export]
  public PackedScene HPinterface;
  [Export]
  public PackedScene Textbox;

#pragma warning restore 649
  Globals g;
  Transition t;
  int isPlayerTurn, aiAttackChoice;
  int queuedAttack; // index of submitted attack in attack dictionary of attacking fighter | -1 for no queued attack | integer in [0, 3] for player | integer in [10, 13] for opponent
  int queuedMinigame; // index of minigame queued in switch statement in CallMinigame() | -1 for no queued minigame | integer >= 0 for minigame index
  int minigameResult; // -2: no minigame running, result used, minigame may be instantiated | -1: minigame active | [0, 100]: result of previous minigame, no minigame active, result not used
  float turnDelay = 1.5f; // seconds until opponent attacks after player attacks
  Fighter player, opponent, f;
  Texture playerTexture, opponentTexture;
  HealthInterface pHealthBar, oHealthBar;
  Textbox textbox;
  Timer timer;
  Vector2 playerHealthBarPosition, opponentHealthBarPosition;
  private AudioStreamPlayer music, musicP, musicO, se;
  private static Dictionary _beastOptions = null;
  private static Dictionary _modifierOptions = null;
  private static Dictionary _attackOptions = null;
  private bool playerAnim, opponentAnim;
  private int animTickP, animTickO;
  private Vector2 right, left;
  private Control Attack0, Attack1, Attack2, Attack3;
  
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
    Dictionary playerBeastD, playerModiferD, opponentBeastD, opponentModiferD;
    Dictionary[] playerAttacksD = new Dictionary[4];
    Dictionary[] opponentAttacksD = new Dictionary[4];
    int i;
    
    g = (Globals)GetNode("/root/Gm");
    t = (Transition)GetNode("/root/Transition");
    timer = (Timer)GetNode("Timer");
    timer.SetOneShot(true);
    isPlayerTurn = 1;
    minigameResult = -2;
    queuedAttack = -1;
    GD.Randomize();

    /* Initialize player character */
    player = (Fighter)Fighter.Instance();
    AddChild(player);
    playerBeastD = beastOptions[g.playerBeastIndex.ToString()] as Dictionary;
    playerModiferD = modifierOptions[g.playerModifierIndex.ToString()] as Dictionary;
    for (i = 0; i < g.playerAttackIndices.Length; i++) playerAttacksD[i] = attackOptions[g.playerAttackIndices[i].ToString()] as Dictionary;
    playerTexture = ResourceLoader.Load((String)playerBeastD["texture"]) as Texture;
    player.GetNode<Sprite>("Texture").Texture = playerTexture;
    player.Position = new Vector2(190, 250);
    player.Scale = new Vector2(6, 6);
    player.Init("player", playerBeastD, playerModiferD, playerAttacksD);

    /* Initialize player health bar to bottom right*/
    pHealthBar = (HealthInterface)HPinterface.Instance();
    AddChild(pHealthBar);
    pHealthBar.CreateLabel(g.name, (String)playerModiferD["name"]);
    Vector2 playerHealthBarPosition = new Vector2(-630, -450);
    pHealthBar.SetPosition(playerHealthBarPosition, false);

    /* Initialize player attack options bottom right */
    Attack0 = GetNode<Control>("Action Console/VBoxContainer/Top Row/Attack0");
    Attack1 = GetNode<Control>("Action Console/VBoxContainer/Top Row/Attack1");
    Attack2 = GetNode<Control>("Action Console/VBoxContainer/Bottom Row/Attack2");
    Attack3 = GetNode<Control>("Action Console/VBoxContainer/Bottom Row/Attack3");
    
    Attack0.Call("setup_AttackNode",(String) playerAttacksD[0]["name"], Convert.ToInt32(playerAttacksD[0]["strike_damage"]), Convert.ToInt32(playerAttacksD[0]["strike_count"]),(String) playerAttacksD[0]["type"], 0, this);
    Attack1.Call("setup_AttackNode",(String) playerAttacksD[1]["name"], Convert.ToInt32(playerAttacksD[1]["strike_damage"]), Convert.ToInt32(playerAttacksD[1]["strike_count"]),(String) playerAttacksD[1]["type"], 1, this);
    Attack2.Call("setup_AttackNode",(String) playerAttacksD[2]["name"], Convert.ToInt32(playerAttacksD[2]["strike_damage"]), Convert.ToInt32(playerAttacksD[2]["strike_count"]),(String) playerAttacksD[2]["type"], 2, this);
    Attack3.Call("setup_AttackNode",(String) playerAttacksD[3]["name"], Convert.ToInt32(playerAttacksD[3]["strike_damage"]), Convert.ToInt32(playerAttacksD[3]["strike_count"]),(String) playerAttacksD[3]["type"], 3, this);
    
    /* Initialize opponent character */
    GD.Print("g.currentOpponentIndex: ", g.currentOpponentIndex);
    opponent = (Fighter)Fighter.Instance();
    AddChild(opponent);
    opponentBeastD = beastOptions[g.oppBeast[g.currentOpponentIndex].ToString()] as Dictionary;
    opponentModiferD = modifierOptions[g.oppMods[g.currentOpponentIndex].ToString()] as Dictionary;
    for (i = 0; i < g.oppAttacks.GetLength(1); i++) opponentAttacksD[i] = attackOptions[g.oppAttacks[g.currentOpponentIndex, i].ToString()] as Dictionary;
    opponentTexture = ResourceLoader.Load((String) opponentBeastD["texture"]) as Texture;
    opponent.GetNode<Sprite>("Texture").Texture = opponentTexture;
    opponent.Position = new Vector2(850, 180);
    opponent.Scale = new Vector2(6, 6);
    opponent.Init("opponent", opponentBeastD, opponentModiferD, opponentAttacksD);

    /* Initialize opponent health bar */
    oHealthBar = (HealthInterface)HPinterface.Instance();
    AddChild(oHealthBar);
    oHealthBar.CreateLabel(g.oppName[g.currentOpponentIndex], (String)opponentModiferD["name"]);
    Vector2 opponentHealthBarPosition = new Vector2(30, -510);
    oHealthBar.SetPosition(opponentHealthBarPosition, false);
        
    /* Vectors for anims: */
    right = new Vector2(1,0);
    left = new Vector2(-1,0);
        
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
    minigameResult = 100;
    isPlayerTurn = 1;
  }

  // Returns 1 if attack button signals shouldn't be obeyed
  public int CheckAttackSignalPermission () {
    if (isPlayerTurn == 0) {
      GD.Print("Cannot select an attack on opponent's turn.");
      return 1;
    }
    if (minigameResult == -1) {
      GD.Print("Cannot select an attack during active minigame.");
      return 1;
    }
    if (timer.GetTimeLeft() != 0) {
      GD.Print("Cannot select an attack during an attack.");
      return 1;
    }

    return 0;
  }

  public void CallMinigame() {
    if (queuedAttack > 3 || queuedAttack < 0 || minigameResult != -2) return;
    queuedMinigame = player.GetAttackMinigame(queuedAttack);
    minigameResult = -1;
    switch (queuedMinigame) {
      case 0:
        AddChild(PowerSliderMinigame.Instance());
        break;
      case 1:
        AddChild(BulletHellMinigame.Instance());
        break;
      case 2:
        AddChild(QuickTimeMinigame.Instance());
        break;
      default:
        AddChild(PowerSliderMinigame.Instance());
        break;
    }
  }
  
  public void MinigameReturn(int result) {
    if (result < 0 || result > 100) {
      GD.Print("Minigames may only return integer values in [0, 100]");
      minigameResult = 0;
      return;
    }
    minigameResult = result;
  }

  // Performs the queued attack whether its from the player or the opponent
  public void PerformQueuedAttack() {
    Dictionary attackD;
    int damage, damageDealt;

    // no queued attack
    if (queuedAttack == -1) return;

    // opponent attacking
    if (queuedAttack >= 10) {
      damage = opponent.GetAttackStrength(queuedAttack - 10) - player.GetArmor();
      minigameResult = 85; // nerf the opponent
      if (damage < 1) damage = 1; // lowest damage dealt per strike is 1
      damageDealt = damage * opponent.GetAttackCount(queuedAttack - 10) * minigameResult / 100;
      player.ReduceHealth(damageDealt);

      // text box
      if (IsInstanceValid(textbox)) textbox.QueueFree();
      textbox = (Textbox)Textbox.Instance();
      AddChild(textbox);
      attackD = attackOptions[g.oppAttacks[g.currentOpponentIndex, queuedAttack - 10].ToString()] as Dictionary;
      textbox.Init(g.oppName[g.currentOpponentIndex], (String)attackD["name"], damageDealt.ToString());
      textbox.AnimateText();

      GD.Print("opponent attack ", queuedAttack - 10, " dealt ", damage * opponent.GetAttackCount(queuedAttack - 10) * minigameResult / 100, " damage.");
      isPlayerTurn = 1;
      queuedAttack = -1;
      PlayerHurtAnim();
    }

    // player attacking
    else {
      damage = player.GetAttackStrength(queuedAttack) - opponent.GetArmor();
      if (damage < 1) damage = 1;
      if (minigameResult >= 90) minigameResult = 120;
      if (minigameResult <= 50) minigameResult = 50;
      damageDealt = damage * player.GetAttackCount(queuedAttack) * minigameResult / 100;
      opponent.ReduceHealth(damageDealt);

      // text box
      if (IsInstanceValid(textbox)) textbox.QueueFree();
      textbox = (Textbox)Textbox.Instance();
      AddChild(textbox);
      attackD = attackOptions[g.playerAttackIndices[queuedAttack].ToString()] as Dictionary;
      textbox.Init(g.name, (String)attackD["name"], damageDealt.ToString());
      textbox.AnimateText();

      isPlayerTurn = 0;
      queuedAttack = -1;
      OpponentHurtAnim();
    }

    // ready for next minigame
    minigameResult = -2;

    // set delay before next attack can be performed
    timer.Start(turnDelay);
  }
  
  private void PlayerHurtAnim(){
    playerAnim = true;
    animTickP = 0;
    se.Stream = ResourceLoader.Load("res://Assets/Music/HitSound.tres") as AudioStream;
    se.Play();
  }
  
  private void OpponentHurtAnim(){
    opponentAnim = true;
    animTickO = 0;
    se.Stream = ResourceLoader.Load("res://Assets/Music/HitSound.tres") as AudioStream;
    se.Play();
  }
  
  private void StartMusic(){
    Dictionary beast;
    
    se = g.GetNode<AudioStreamPlayer>("SoundEffects");
    music = g.GetNode<AudioStreamPlayer>("Music");
    musicP = g.GetNode<AudioStreamPlayer>("MusicPlayer");
    music.Stop(); musicP.Stop();
    
    music.Stream = ResourceLoader.Load("res://Assets/Music/BattleThemeBase.mp3") as AudioStream;
    beast = beastOptions[g.playerBeastIndex.ToString()] as Dictionary;
    musicP.Stream = ResourceLoader.Load((String)beast["music"]) as AudioStream;

    music.Play();
    musicP.Play();
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
  

    /* Update health bars */
    
    pHealthBar.AdjustHealth((player.GetHealth() * 100) / player.GetMaxHealth()); // adjusts the player's HP bar
    pHealthBar.UpdateHealthFrac(player.GetMaxHealth(), player.GetHealth()); // adjusts players's HP fraction
    oHealthBar.AdjustHealth((opponent.GetHealth() * 100) / opponent.GetMaxHealth()); // adjusts the opponent's HP bar
    oHealthBar.UpdateHealthFrac(opponent.GetMaxHealth(), opponent.GetHealth()); // adjusts opponent's HP fraction
    
    /* Call minigame */

    if (minigameResult == -1) return;
    CallMinigame();

    /* Perform queued attack */

    if (minigameResult == -1) return;
    if (timer.GetTimeLeft() == 0) PerformQueuedAttack();

    /* AI turn */

    // it seems signals can be recieved in the middle of _Process() so checking health here is necessary
    if (isPlayerTurn == 0 && opponent.GetHealth() != 0) {
      AITakeTurn();
    }
    
    if(playerAnim){
      if(animTickP > 80) {
        player.Visible = true;
        playerAnim = false;
      }
      
      if(animTickP < 10) player.Translate(2 * right);
      else if(animTickP < 30) player.Translate(2 * left);
      else if(animTickP < 50) player.Translate(2 * right);
      else if(animTickP < 70) player.Translate(2 * left);
      else player.Translate(2 * right);
 
      if(animTickP % 30 < 5) player.Visible = false;
      else player.Visible = true;
      
      animTickP += 1;
    }
    
    if(opponentAnim){
      if(animTickO > 80) {
        opponent.Visible = true;
        opponentAnim = false;
      }
      
      if(animTickO < 10) opponent.Translate(2 * right);
      else if(animTickO < 30) opponent.Translate(2 * left);
      else if(animTickO < 50) opponent.Translate(2 * right);
      else if(animTickO < 70) opponent.Translate(2 * left);
      else opponent.Translate(2 * right);
 
      if(animTickO % 29 < 5) opponent.Visible = false;
      else opponent.Visible = true;
      
      animTickO += 1;
    }
    
  }

  /* Signals */

  private void _on_B0_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 0;
  }

  private void _on_B1_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 1;
  }

  private void _on_B2_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 2;
  }

  private void _on_B3_pressed() {
    if (CheckAttackSignalPermission() == 1) return;
    queuedAttack = 3;
  }

  public void _on_Attack_Selected(int index){
    if(CheckAttackSignalPermission() == 1) return;
    queuedAttack = index;
  }
}
