using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerTurns { Player1, Player2, transitioning}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public PlayerTurns playerPlaying;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private int turn;
    [SerializeField] private int actionsInTurn;

    public delegate void OnPlayerMoved();
    public static event OnPlayerMoved onPlayedMoved;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        UiManager.instance.UpdateActions(actionsInTurn);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        actionsInTurn = 5; //placeholder
        UiManager.instance.UpdateActions(actionsInTurn);
    }


    private IEnumerator TurnSwitchAnimation(PlayerTurns which)
    {
        yield return new WaitForSeconds(1f);
        SwitchPlayerTurns(which);
    }
}
