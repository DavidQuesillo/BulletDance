using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerTurn;
    [SerializeField] private TextMeshProUGUI turnNumber;

    [Header("Player 1 side")]
    [SerializeField] private TextMeshProUGUI hp1;
    [SerializeField] private TextMeshProUGUI ActionCount1;

    [SerializeField] private TextMeshProUGUI charName1;

    [Header("Player 2 side")]
    [SerializeField] private TextMeshProUGUI hp2;
    [SerializeField] private TextMeshProUGUI ActionCount2;

    [SerializeField] private TextMeshProUGUI charName2;

    public static UiManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
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

    public void ShowCharatersData(CharBase data, PlayerTurns whichPlayer)
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            charName1.text = data.charName;
        }
        else
        {
            charName2.text = data.charName;
        }
    }

    public void UpdateHP(PlayerTurns whose, int howMuch)
    {
        if (whose == PlayerTurns.Player1)
        {
            hp1.text = howMuch.ToString();
        }
        else
        {
            hp2.text = howMuch.ToString();
        }
    }
    public void UpdateActions(int actionsLeft)
    {
        if (GameManager.instance.playerPlaying == PlayerTurns.Player1)
        {
            ActionCount1.text = actionsLeft.ToString();
        }
        else
        {
            ActionCount2.text = actionsLeft.ToString();
        }
    }
    public void UpdateTurn(int turnNum, PlayerTurns turnWho)
    {
        turnNumber.text = "Turn " + turnNum.ToString();
        
        if (turnWho == PlayerTurns.Player1)
        {
            playerTurn.text = "Player 1";
        }
        else
        {
            playerTurn.text = "Player 2";
        }
    }
}
