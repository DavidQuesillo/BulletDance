using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerTurns { Player1, Player2}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PlayerTurns playerPlaying;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private int turn;
    [SerializeField] private int actionsInTurn;

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
        }
        else
        {
            playerPlaying = PlayerTurns.Player2;
        }
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
        if (actionsInTurn <= 0)
        {
            TurnEnd();
        }
    }

    public void TurnEnd()
    {
        if (playerPlaying == PlayerTurns.Player1)
        {
            player1.GetComponent<PlayerInput>().enabled = false;
            player2.GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            player2.GetComponent<PlayerInput>().enabled = false;
            player1.GetComponent<PlayerInput>().enabled = true;
        }
    }
}
