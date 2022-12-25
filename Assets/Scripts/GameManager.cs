using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerTurns { Player1, Player2, transitioning}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameStarted = false;
    [SerializeField] public PlayerTurns playerPlaying;
    public bool paused;
    [SerializeField] public GameObject player1;
    [SerializeField] public GameObject player2;
    [SerializeField] private GameObject endExplosion;
    [SerializeField] private int turn = 1;
    [SerializeField] private int actionsInTurn;
    [SerializeField] private int shotsInturn;
    private bool usedSpecial;
    //[SerializeField] private bool paused;
    [SerializeField] private int actionsRange = 6;

    private Player winner; //may not stay Player class

    public delegate void OnPlayerMoved();
    public static event OnPlayerMoved onPlayedMoved;

    public delegate void OnMatchEnd();
    public static event OnMatchEnd onMatchEnd;

    public delegate void OnTurnSwitch();
    public static event OnTurnSwitch onTurnSwitch;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        



        /*if (Random.value < 0.5f)
        {
            playerPlaying = PlayerTurns.Player1;
            //player2.GetComponent<PlayerInput>().enabled = false;
            //player1.GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            playerPlaying = PlayerTurns.Player2;
            //player1.GetComponent<PlayerInput>().enabled = false;
            //player2.GetComponent<PlayerInput>().enabled = true;
        }*/        
    }

    void Start()
    {
        //UiManager.instance.UpdateActions(actionsInTurn);
        
        //InitMatch(); //placeholder
    }

    void Update()
    {
        
    }

    public void InitMatch()
    {
        //print("initing match");
        if (gameStarted) {return; }

        turn = 1;
        UiManager.instance.UnlockButton(0, PlayerTurns.Player1);
        UiManager.instance.UnlockButton(1, PlayerTurns.Player1);
        UiManager.instance.UnlockButton(2, PlayerTurns.Player1);
        UiManager.instance.UnlockButton(0, PlayerTurns.Player2);
        UiManager.instance.UnlockButton(1, PlayerTurns.Player2);
        UiManager.instance.UnlockButton(2, PlayerTurns.Player2);

        //randomizes which player starts
        if (Random.value < 0.5f)
        {
            playerPlaying = PlayerTurns.Player1;

            /*actionsInTurn = player1.GetComponent<Player>().GetCharData().baseActions;
            shotsInturn = player1.GetComponent<Player>().GetCharData().baseShots;*/
            
            //player2.GetComponent<PlayerInput>().enabled = false;
            //player1.GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            playerPlaying = PlayerTurns.Player2;
            /*actionsInTurn = player2.GetComponent<Player>().GetCharData().baseActions;
            shotsInturn = player2.GetComponent<Player>().GetCharData().baseShots;*/

            //player1.GetComponent<PlayerInput>().enabled = false;
            //player2.GetComponent<PlayerInput>().enabled = true;
        }

        //GridManager.instance.GenerateGrid(); //first generate grid COMMENTED FOR TESTING HERE IT IS CHANGE IT IF NOT WORK!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        GridManager.instance.DisplayGrid();                 //IT WORKS FOR NOW


        player1.SetActive(true); //then players are enabled and place themselves in grid
        player2.SetActive(true);

        if (playerPlaying == PlayerTurns.Player1)
        {
            actionsInTurn = player1.GetComponent<Player>().GetCharData().baseActions;
            shotsInturn = player1.GetComponent<Player>().GetCharData().baseShots;
        }
        else
        {
            actionsInTurn = player2.GetComponent<Player>().GetCharData().baseActions;
            shotsInturn = player2.GetComponent<Player>().GetCharData().baseShots;

        }
                
        UiManager.instance.UpdateActions(actionsInTurn, playerPlaying); //display actions of the player
        UiManager.instance.UpdateShots(shotsInturn, playerPlaying);
        usedSpecial = false;
        UiManager.instance.UpdateTurn(turn, playerPlaying);
        UiManager.instance.UpdateHP(PlayerTurns.Player1, player1.GetComponent<Player>().GetCharData().hp);
        UiManager.instance.UpdateHP(PlayerTurns.Player2, player2.GetComponent<Player>().GetCharData().hp);
        UiManager.instance.ShowCharatersData(player1.GetComponent<Player>().GetCharData(), PlayerTurns.Player1);
        UiManager.instance.ShowCharatersData(player2.GetComponent<Player>().GetCharData(), PlayerTurns.Player2);
        
        if (playerPlaying == PlayerTurns.Player1)   { UiManager.instance.TurnSwitchAnimations(PlayerTurns.Player2); print("player1 starts"); }
        else                                        { UiManager.instance.TurnSwitchAnimations(PlayerTurns.Player1); print("player2 starts"); }

        MusicManager.instance.PlayBattleMusic();

        gameStarted = true;
    }

    /*public void DebugActivePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            print(playerPlaying.ToString());
        }        
    }*/

    public void SpendAction()
    {
        actionsInTurn -= 1;
        UiManager.instance.UpdateActions(actionsInTurn, playerPlaying);
        if (onPlayedMoved != null)
        {
            onPlayedMoved();
            //print("calling action");
        }
        
        if (actionsInTurn <= 0)
        {
            UiManager.instance.LockButton(1, playerPlaying);
        }
        if (actionsInTurn <= 0 && shotsInturn <= 0 && usedSpecial)
        {
            TurnEnd();
        }
    }
    public void SpendShot()
    {
        shotsInturn -= 1;
        UiManager.instance.UpdateShots(shotsInturn, playerPlaying);

        if (shotsInturn <= 0)
        {
            UiManager.instance.LockButton(0, playerPlaying);
        }
        if (actionsInTurn <= 0 && shotsInturn <= 0 && usedSpecial)
        {
            TurnEnd();
        }
    }
    public void SpendSpecial()
    {
        usedSpecial = true;
        if (shotsInturn <= 0 && actionsInTurn <= 0 && usedSpecial)
        {
            TurnEnd();
        }
    }
    public int GetActionsInTurn()
    {
        return actionsInTurn;
    }
    public int GetShotsInTurn()
    {
        return shotsInturn;
    }

    public void TurnEnd()
    {
        StartCoroutine(TurnSwitchAnimation(playerPlaying));
        playerPlaying = PlayerTurns.transitioning;
        turn++;
        
        /*if (playerPlaying == PlayerTurns.Player1)
        {
            playerPlaying = PlayerTurns.transitioning;
            //player1.GetComponent<PlayerInput>().enabled = false;
            //player2.GetComponent<PlayerInput>().enabled = true;
            playerPlaying = PlayerTurns.Player2;
        }
        else
        {
            playerPlaying = PlayerTurns.transitioning;
            //player2.GetComponent<PlayerInput>().enabled = false;
            //player1.GetComponent<PlayerInput>().enabled = true;
            playerPlaying = PlayerTurns.Player1;
        }
        actionsInTurn = 5; //placeholder
        UiManager.instance.UpdateActions(actionsInTurn);*/
    }
    public void SwitchPlayerTurns(PlayerTurns which)
    {
        if (which == PlayerTurns.Player1)
        {
            playerPlaying = PlayerTurns.Player2;
        }
        else
        {
            playerPlaying = PlayerTurns.Player1;
        }
        UiManager.instance.UpdateTurn(turn, playerPlaying);

        //actionsInTurn = 5; //placeholder

        //ActionDice(playerPlaying);
        ActionsSet(playerPlaying); //replacing the dice for determined number per char

        UiManager.instance.UpdateActions(actionsInTurn, playerPlaying);

        if (onTurnSwitch != null) onTurnSwitch();
        UiManager.instance.UpdateShots(shotsInturn, playerPlaying);
    }

    public void MatchEnd(PlayerTurns whoLost)
    {
        if (whoLost == PlayerTurns.Player1)
        {
            winner = player2.GetComponent<Player>();
            endExplosion.transform.position = player1.transform.position;
            endExplosion.GetComponent<Animator>().Play("death_explosion");
        }
        else
        {
            winner = player1.GetComponent<Player>();
            endExplosion.transform.position = player2.transform.position;
            endExplosion.GetComponent<Animator>().Play("death_explosion");
        }
        playerPlaying = PlayerTurns.transitioning;
        SoundManager.instance.PlayDeathSound();
        StartCoroutine(MatchEndDelay()); //moving things here for match end feel & feedback
        //    MOVED TO COROUTINE
        //disable players
        /*player1.SetActive(false);
        player2.SetActive(false);
        
        //call event for anything else to close
        onMatchEnd();
        
        //switch to result screen
        PanelsManager.instance.ShowResult();
        gameStarted = false;*/
    }
    public void ClearForTitle()
    {
        MusicManager.instance.StopBattleMusic();
        //disable players
        player1.SetActive(false);
        player2.SetActive(false);

        //call event for anything else to close
        onMatchEnd();
        gameStarted = false;
    }

    public void ActionDice(PlayerTurns whoseActions)
    {
        if (whoseActions == PlayerTurns.Player1)
        {
            actionsInTurn = player1.GetComponent<Player>().GetCharData().baseActions + Random.Range(1, actionsRange + 1);
            shotsInturn = player1.GetComponent<Player>().GetCharData().baseShots + Random.Range(1, actionsRange + 1);
        }
        else
        {
            actionsInTurn = player2.GetComponent<Player>().GetCharData().baseActions + Random.Range(1, actionsRange + 1);
            shotsInturn = player2.GetComponent<Player>().GetCharData().baseShots + Random.Range(1, actionsRange + 1);
        }

        //actionsInTurn = Random.Range(1, actionsRange + 1);
    } //outdated, no longer doing luck
    public void ActionsSet(PlayerTurns whose) //replacement, deterministic
    {
        usedSpecial = false;
        if (whose == PlayerTurns.Player1)
        {
            actionsInTurn = player1.GetComponent<Player>().GetCharData().baseActions;
            shotsInturn = player1.GetComponent<Player>().GetCharData().baseShots;
        }
        else
        {
            actionsInTurn = player2.GetComponent<Player>().GetCharData().baseActions;
            shotsInturn = player2.GetComponent<Player>().GetCharData().baseShots;
        }
    }

    private IEnumerator TurnSwitchAnimation(PlayerTurns whichOut)
    {
        if (whichOut == PlayerTurns.Player1)
        {
            UiManager.instance.UnlockButton(0, PlayerTurns.Player2);
            UiManager.instance.UnlockButton(1, PlayerTurns.Player2);
            UiManager.instance.UnlockButton(2, PlayerTurns.Player2);
            UiManager.instance.TurnSwitchAnimations(whichOut);
            player2.GetComponent<Player>().RefreshSpecial();
        }
        else
        {
            UiManager.instance.UnlockButton(0, PlayerTurns.Player1);
            UiManager.instance.UnlockButton(1, PlayerTurns.Player1);
            UiManager.instance.UnlockButton(2, PlayerTurns.Player1);
            UiManager.instance.TurnSwitchAnimations(whichOut);
            player1.GetComponent<Player>().RefreshSpecial();
        }

        yield return new WaitForSeconds(1f);
        SwitchPlayerTurns(whichOut);
    }
    private IEnumerator MatchEndDelay()
    {
        MusicManager.instance.StopBattleMusic();
        yield return new WaitForSeconds(4.5f);
        //disable players
        player1.SetActive(false);
        player2.SetActive(false);
        
        //call event for anything else to close
        onMatchEnd();
                
        MusicManager.instance.PlayVictoryMusic();

        //switch to result screen
        PanelsManager.instance.ShowResult();
        //get the win screen info from the winning player
        UiManager.instance.SetVictorInfo(winner.GetCharData());
        gameStarted = false;
    }
}
