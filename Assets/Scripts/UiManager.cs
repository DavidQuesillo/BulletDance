using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerTurn;
    [SerializeField] private TextMeshProUGUI turnNumber;

    [Header("Player 1 side")]
    [SerializeField] private Image icon1;
    [SerializeField] private TextMeshProUGUI hp1;
    [SerializeField] private TextMeshProUGUI ActionCount1;
    [SerializeField] private Image glow1;
    [SerializeField] private Image[] buttons1 = new Image[3];
    [SerializeField] private TextMeshProUGUI shotsCount1;

    [SerializeField] private TextMeshProUGUI charName1;

    [Header("Player 2 side")]
    [SerializeField] private Image icon2;
    [SerializeField] private TextMeshProUGUI hp2;
    [SerializeField] private TextMeshProUGUI ActionCount2;
    [SerializeField] private Image glow2;
    [SerializeField] private Image[] buttons2 = new Image[3];
    [SerializeField] private TextMeshProUGUI shotsCount2;

    [SerializeField] private TextMeshProUGUI charName2;

    [Header("Win Screen")]
    [SerializeField] private Image wPortrait;
    [SerializeField] private TextMeshProUGUI wName;
    [SerializeField] private QuoteReveal wQuote;

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
            icon1.sprite = data.miniPortrait;
        }
        else
        {
            charName2.text = data.charName;
            icon2.sprite = data.miniPortrait;
        }
    }
    public void SetVictorInfo(CharBase wInfo)
    {
        wPortrait.sprite = wInfo.portrait;
        wName.text = wInfo.charName + " wins!";
        wQuote.SetQuote(wInfo.winQuote).PlayQuote(); //FIRST APPLICATION OF THIS FOR ME EVER
    }

    public void LockButton(int action, PlayerTurns whichPlayer)
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            buttons1[action].DOColor(new Color(buttons1[action].color.r, buttons1[action].color.g, buttons1[action].color.b, 0.2f), 0.1f);
        }
        else
        {
            buttons2[action].DOColor(new Color(buttons2[action].color.r, buttons2[action].color.g, buttons2[action].color.b, 0.2f), 0.1f);
        }
    }
    public void UnlockButton(int action, PlayerTurns whichPlayer)
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            buttons1[action].DOColor(new Color(buttons1[action].color.r, buttons1[action].color.g, buttons1[action].color.b, 255f), 0.1f);
        }
        else
        {
            buttons2[action].DOColor(new Color(buttons2[action].color.r, buttons2[action].color.g, buttons2[action].color.b, 255f), 0.1f);
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
    public void UpdateActions(int actionsLeft, PlayerTurns whoseActions)
    {
        //print("func update ran");
        if (whoseActions == PlayerTurns.Player1)
        {
            ActionCount1.text = actionsLeft.ToString();
            //print("1 ran");
        }
        else
        {
            ActionCount2.text = actionsLeft.ToString();
            //print("2 ran");
        }
    }
    public void UpdateShots(int shotsLeft, PlayerTurns whoseShots)
    {
        if (whoseShots == PlayerTurns.Player1)
        {
            shotsCount1.text = shotsLeft.ToString();
        }
        else
        {
            shotsCount2.text = shotsLeft.ToString();
        }
    }
    public void UpdateTurn(int turnNum, PlayerTurns turnWho)
    {
        turnNumber.text = "Turn " + turnNum.ToString();

        if (turnWho == PlayerTurns.Player1)
        {
            playerTurn.text = "Player 1";
            glow1.gameObject.SetActive(true);
            glow2.gameObject.SetActive(false);
            //TurnSwitchAnimations(PlayerTurns.Player2);
        }
        else
        {
            playerTurn.text = "Player 2";
            glow1.gameObject.SetActive(false);
            glow2.gameObject.SetActive(true);
            //TurnSwitchAnimations(PlayerTurns.Player1);
        }
    }

    public void TurnSwitchAnimations(PlayerTurns playerOut)
    {
        if (playerOut == PlayerTurns.Player1)
        {
            //remove last player's things
            buttons1[0].rectTransform.DOAnchorPos(Vector2.left * 500f, 1f);
            buttons1[1].rectTransform.DOAnchorPos(Vector2.left * 500f, 1f);
            buttons1[2].rectTransform.DOAnchorPos(Vector2.left * 500f, 1f);
            glow1.DOColor(Color.clear, 1f);

            //enter new player's things
            buttons2[0].rectTransform.DOAnchorPos(Vector2.zero * 500f, 1f);
            buttons2[1].rectTransform.DOAnchorPos(Vector2.zero * 500f, 1f);
            buttons2[2].rectTransform.DOAnchorPos(Vector2.zero * 500f, 1f);
            glow2.DOColor(Color.red, 1f);
        }
        else
        {
            //remove last player's things
            buttons2[0].rectTransform.DOAnchorPos(Vector2.right * 500f, 1f);
            buttons2[1].rectTransform.DOAnchorPos(Vector2.right * 500f, 1f);
            buttons2[2].rectTransform.DOAnchorPos(Vector2.right * 500f, 1f);
            glow2.DOColor(Color.clear, 1f);

            //enter new player's things
            buttons1[0].rectTransform.DOAnchorPos(Vector2.zero, 1f);
            buttons1[1].rectTransform.DOAnchorPos(Vector2.zero, 1f);
            buttons1[2].rectTransform.DOAnchorPos(Vector2.zero, 1f);
            glow1.DOColor(Color.cyan, 1f);
        }
    }

}
