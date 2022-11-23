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

    public delegate void OnActionPassed();
    public static event OnActionPassed onActionPassed;

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
            player2.GetComponent<PlayerInput>().enabled = false;
            player1.GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            playerPlaying = PlayerTurns.Player2;
            player1.GetComponent<PlayerInput>().enabled = false;
            player2.GetComponent<PlayerInput>().enabled = true;
        }

        UiManager.instance.UpdateActions(actionsInTurn);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpendAction()
    {
        actionsInTurn -= 1;
        UiManager.instance.UpdateActions(actionsInTurn);
        if (onActionPassed != null)
        {
            onActionPassed();
        }
        
        if (actionsInTurn <= 0)
        {
            TurnEnd();
        }
    }

    public void TurnEnd()
    {
        if (playerPlaying == PlayerTurns.Player1)
        {
            playerPlaying = PlayerTurns.transitioning;
            player1.GetComponent<PlayerInput>().enabled = false;
            player2.GetComponent<PlayerInput>().enabled = true;
            playerPlaying = PlayerTurns.Player2;
        }
        else
        {
            playerPlaying = PlayerTurns.transitioning;
            player2.GetComponent<PlayerInput>().enabled = false;
            player1.GetComponent<PlayerInput>().enabled = true;
            playerPlaying = PlayerTurns.Player1;
        }
        actionsInTurn = 5; //placeholder
        UiManager.instance.UpdateActions(actionsInTurn);
    }
}
