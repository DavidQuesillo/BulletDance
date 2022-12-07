using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerTurns { Player1, Player2, transitioning}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public PlayerTurns playerPlaying;
    [SerializeField] public GameObject player1;
    [SerializeField] public GameObject player2;
    [SerializeField] private int turn;
    [SerializeField] private int actionsInTurn;
    [SerializeField] private int actionsRange = 6;

    private Player winner; //may not stay Player class

    public delegate void OnPlayerMoved();
    public static event OnPlayerMoved onPlayedMoved;

    public delegate void OnMatchEnd();
    public static event OnMatchEnd onMatchEnd;

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

    // Start is called before the first frame update
    void Start()
    {
        //UiManager.instance.UpdateActions(actionsInTurn);
        
        InitMatch(); //placeholder
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitMatch()
    {
        //GridManager.instance.GenerateGrid();
        if (Random.value < 0.5f)
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
        }

        GridManager.instance.GenerateGrid(); //first generate grid

        player1.SetActive(true); //then players are enabled and place themselves in grid
        player2.SetActive(true);
                
        UiManager.instance.UpdateActions(actionsInTurn); //display actions of the player
    }

    public void DebugActivePlayer(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            print(playerPlaying.ToString());
        }        
    }

    public void SpendAction()
    {
        actionsInTurn -= 1;
        UiManager.instance.UpdateActions(actionsInTurn);
        if (onPlayedMoved != null)
        {
            onPlayedMoved();
            //print("calling action");
        }
        
        if (actionsInTurn <= 0)
        {
            TurnEnd();
        }
    }

    public void TurnEnd()
    {
        StartCoroutine(TurnSwitchAnimation(playerPlaying));
        playerPlaying = PlayerTurns.transitioning;        
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
        
        //actionsInTurn = 5; //placeholder
        ActionDice(playerPlaying);

        UiManager.instance.UpdateActions(actionsInTurn);
    }

    public void MatchEnd(PlayerTurns whoLost)
    {
        if (whoLost == PlayerTurns.Player1)
        {
            winner = player2.GetComponent<Player>();
        }
        else
        {
            winner = player1.GetComponent<Player>();
        }
        //disable players
        player1.SetActive(false);
        player2.SetActive(false);
        
        //call event for anything else to close
        onMatchEnd();
        
        //switch to result screen
        PanelsManager.instance.ShowResult();
    }

    public void ActionDice(PlayerTurns whoseActions)
    {
        /*if (whoseActions == PlayerTurns.Player1)
        {
            
        }*/

        actionsInTurn = Random.Range(1, actionsRange + 1);
    }

    private IEnumerator TurnSwitchAnimation(PlayerTurns which)
    {
        yield return new WaitForSeconds(1f);
        SwitchPlayerTurns(which);
    }
}
