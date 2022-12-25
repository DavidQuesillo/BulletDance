using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public enum SelectableCharacters { Adam, Mercuria, Thompson, Shouko, Arlene}
public class CharSelect : MonoBehaviour
{
    private bool started = false;
    [SerializeField] private bool p1Picked;    
    [SerializeField] private bool p2Picked;
    [SerializeField] private SelectableCharacters p1Char;
    [SerializeField] private SelectableCharacters p2Char;
    [SerializeField] private Image startButton;

    [Header("Characters Data")]
    [SerializeField] private CharBase CharAdam;
    [SerializeField] private CharBase CharMercuria;

    [Header("Select Display")]
    [SerializeField] private Image[] buttons1 = new Image[2];
    [SerializeField] private Image[] buttons2 = new Image[2];
    [SerializeField] private Image p1Hl;
    [SerializeField] private Image p2Hl;

    [Header("Select Info P1")]
    [SerializeField] private Image p1Portrait;
    [SerializeField] private TextMeshProUGUI p1Name;
    [SerializeField] private Image p1Sprite;
    [SerializeField] private TextMeshProUGUI p1SpecName;
    [SerializeField] private Image p1SpecImage;
    [SerializeField] private TextMeshProUGUI p1SpecDesc;
    [SerializeField] private TextMeshProUGUI p1hp;
    [SerializeField] private TextMeshProUGUI p1steps;
    [SerializeField] private TextMeshProUGUI p1shots;
    [Header("Select Info P2")]
    [SerializeField] private Image p2Portrait;
    [SerializeField] private TextMeshProUGUI p2Name;
    [SerializeField] private Image p2Sprite;
    [SerializeField] private TextMeshProUGUI p2SpecName;
    [SerializeField] private Image p2SpecImage;
    [SerializeField] private TextMeshProUGUI p2SpecDesc;
    [SerializeField] private TextMeshProUGUI p2hp;
    [SerializeField] private TextMeshProUGUI p2steps;
    [SerializeField] private TextMeshProUGUI p2shots;

    private void OnEnable()
    {
        started = false;
        p1Picked = false;
        p2Picked = false;
        p1Char = SelectableCharacters.Adam;
        p2Char = SelectableCharacters.Mercuria; //whoever is the furthest right on the screen? doesnt really matter
        startButton.gameObject.SetActive(false);

        MakeInfoVisible(false, PlayerTurns.Player1);
        MakeInfoVisible(false, PlayerTurns.Player2);
        //MakeInfoVisible(false, PlayerTurns.Player2);
    }
   
    /*public void GetP1SeLectInput(InputAction.CallbackContext ctx)
    {
        p1Char = selection;
        p1Picked = true;
        DisplaySelection(PlayerTurns.Player1, selection);
    }*/
    /*public void SelectP1Char(SelectableCharacters selection)
    {        
        p1Char = selection;
        p1Picked = true;
        DisplaySelection(PlayerTurns.Player1, selection);
    }*/
    public void SelectionHighlight(int charIndex, PlayerTurns whichPlayer)
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            //p1Hl.rectTransform.position = buttons[charIndex].rectTransform.position;
            p1Hl.enabled = true;
            p1Hl.rectTransform.anchorMax = buttons1[charIndex].rectTransform.anchorMax;
            p1Hl.rectTransform.anchorMin = buttons1[charIndex].rectTransform.anchorMin;
            p1Hl.rectTransform.anchoredPosition = buttons1[charIndex].rectTransform.anchoredPosition;
        }
        else
        {
            //p2Hl.rectTransform.position = buttons1[charIndex].rectTransform.position;
            p2Hl.enabled = true;
            p2Hl.rectTransform.anchorMax = buttons2[charIndex].rectTransform.anchorMax;
            p2Hl.rectTransform.anchorMin = buttons2[charIndex].rectTransform.anchorMin;
            p2Hl.rectTransform.anchoredPosition = buttons2[charIndex].rectTransform.anchoredPosition;
        }
    }
    public void SelectP1Adam(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            p1Picked = true;
            DisplaySelection(PlayerTurns.Player1, SelectableCharacters.Adam);
            SelectionHighlight(0, PlayerTurns.Player1);
            p1Char = SelectableCharacters.Adam;
            CheckIfBothSelected();
        }        
    }
    public void SelectP1Mercuria(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            p1Picked = true;
            DisplaySelection(PlayerTurns.Player1, SelectableCharacters.Mercuria);
            SelectionHighlight(1, PlayerTurns.Player1);
            p1Char = SelectableCharacters.Mercuria;
            CheckIfBothSelected();
        }        
    }
    public void SelectP2Adam(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            p2Picked = true;
            DisplaySelection(PlayerTurns.Player2, SelectableCharacters.Adam);
            SelectionHighlight(0, PlayerTurns.Player2);
            p2Char = SelectableCharacters.Adam;
            CheckIfBothSelected();
        }        
    }
    public void SelectP2Mercuria(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            p2Picked = true;
            DisplaySelection(PlayerTurns.Player2, SelectableCharacters.Mercuria);
            SelectionHighlight(1, PlayerTurns.Player2);
            p2Char = SelectableCharacters.Mercuria;
            CheckIfBothSelected();
        }        
    }

    public void CheckIfBothSelected()
    {
        if (p1Picked && p2Picked)
        {
            startButton.gameObject.SetActive(true);
        }
    }

    /*public void SelectP2Char(InputAction.CallbackContext ctx, SelectableCharacters selection)
    {
        if (ctx.started)
        {
            p2Char = selection;
            p2Picked = true;
            DisplaySelection(PlayerTurns.Player2, selection);
        }
    }*/
    public void MakeInfoVisible(bool yesno, PlayerTurns whichPlayer)
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            p1Portrait.enabled = yesno;
            p1Sprite.enabled = yesno;
            p1SpecImage.enabled = yesno;

            //p1Name.enabled = yesno;
            if (!yesno) { p1Name.text = "Select your Character!"; }
            
            p1SpecName.gameObject.SetActive(yesno);
            p1SpecDesc.enabled = yesno;
            p1hp.enabled = yesno;
            p1steps.gameObject.SetActive(yesno);
            p1shots.enabled = yesno;
            p1Hl.enabled = yesno;
        }
        else
        {
            p2Portrait.enabled = yesno;
            p2Sprite.enabled = yesno;
            p2SpecImage.enabled = yesno;

            //p2Name.enabled = yesno;
            if (!yesno) { p1Name.text = "Select your Character!"; }
            
            p2SpecName.gameObject.SetActive(yesno);
            p2SpecDesc.enabled = yesno;
            p2hp.enabled = yesno;
            p2steps.gameObject.SetActive(yesno);
            p2shots.enabled = yesno;
            p2Hl.enabled = yesno;
        }
    }
    public void DisplaySelection(PlayerTurns whichPlayer, SelectableCharacters whichChar)
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            /*p1Portrait.color = Color.white;
            p1Sprite.color = Color.white;
            p1SpecImage.color = Color.white;*/

            /*p1Portrait.enabled = true;
            p1Sprite.enabled = true;
            p1SpecImage.enabled = true;

            p1Name.enabled = true;
            p1SpecName.enabled = true;
            p1SpecDesc.enabled = true;
            p1hp.enabled = true;*/
            MakeInfoVisible(true, whichPlayer);

            switch (whichChar)
            {
                case SelectableCharacters.Adam:
                    SetSelectedData(PlayerTurns.Player1, CharAdam);
                    /*p1Portrait.sprite = CharAdam.portrait;
                    p1Name.text = CharAdam.charName;
                    p1SpecName.text = CharAdam.specialName;
                    p1SpecImage.sprite = CharAdam.specialImage;*/
                    break;
                case SelectableCharacters.Mercuria:
                    SetSelectedData(PlayerTurns.Player1, CharMercuria);
                    break;
                case SelectableCharacters.Thompson:
                    break;
                case SelectableCharacters.Shouko:
                    break;
                case SelectableCharacters.Arlene:
                    break;
                default:
                    MakeInfoVisible(false, whichPlayer);
                    /*p1Portrait.enabled = false;
                    p1Sprite.enabled = false;
                    p1SpecImage.enabled = false;

                    p1Name.enabled = false;
                    p1SpecName.enabled = false;
                    p1SpecDesc.enabled = false;
                    p1hp.enabled = false;*/
                    break;
            }
        }
        else
        {
            /*p2Portrait.enabled = true;
            p2Sprite.enabled = true;
            p2SpecImage.enabled = true;

            p2Name.enabled = true;
            p2SpecName.enabled = true;
            p2SpecDesc.enabled = true;*/
            MakeInfoVisible(true, whichPlayer);

            switch (whichChar)
            {
                case SelectableCharacters.Adam:
                    SetSelectedData(PlayerTurns.Player2, CharAdam);
                    /*p1Portrait.sprite = CharAdam.portrait;
                    p1Name.text = CharAdam.charName;
                    p1SpecName.text = CharAdam.specialName;
                    p1SpecImage.sprite = CharAdam.specialImage;*/
                    break;
                case SelectableCharacters.Mercuria:
                    SetSelectedData(PlayerTurns.Player2, CharMercuria);
                    break;
                case SelectableCharacters.Thompson:
                    break;
                case SelectableCharacters.Shouko:
                    break;
                case SelectableCharacters.Arlene:
                    break;
                default:
                    MakeInfoVisible(false, whichPlayer);
                    /*p2Portrait.enabled = false;
                    p2Sprite.enabled = false;
                    p2SpecImage.enabled = false;

                    p2Name.enabled = false;
                    p2SpecName.enabled = false;
                    p2SpecDesc.enabled = false;
                    p2hp.enabled = false;*/
                    break;
            }
        }
    }
    public void SetSelectedData(PlayerTurns whichSide, CharBase charData)
    {
        if (whichSide == PlayerTurns.Player1)
        {
            p1Portrait.sprite = charData.portrait;
            p1Name.text = charData.charName;
            p1SpecName.text = charData.specialName;
            p1SpecImage.sprite = charData.specialImage;
            p1SpecDesc.text = charData.specialDesc;
            p1Sprite.sprite = charData.gameSprite;
            p1hp.text = "HP: " + charData.hp;
            p1steps.text = "Steps: " + charData.baseActions;
            p1shots.text = "Shots: " + charData.baseShots;
        }
        else
        {
            p2Portrait.sprite = charData.portrait;
            p2Name.text = charData.charName;
            p2SpecName.text = charData.specialName;
            p2SpecImage.sprite = charData.specialImage;
            p2SpecDesc.text = charData.specialDesc;
            p2Sprite.sprite = charData.gameSprite;
            p2hp.text = charData.hp + " HP";
            p2steps.text = charData.baseActions + " steps";
            p2shots.text = charData.baseShots + " steps";
        }
    }
    public void StartGame(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (!started && p1Picked && p2Picked)
            {
                Player one = GameManager.instance.player1.GetComponent<Player>();
                Player two = GameManager.instance.player2.GetComponent<Player>();
                print("started game on charselect");
                switch (p1Char)
                {
                    case SelectableCharacters.Adam:
                        //one.SetCharData(CharAdam);
                        one.charData = CharAdam;
                        break;
                    case SelectableCharacters.Mercuria:
                        //one.SetCharData(CharMercuria);
                        one.charData = CharMercuria;
                        break;
                    case SelectableCharacters.Thompson:
                        break;
                    case SelectableCharacters.Shouko:
                        break;
                    case SelectableCharacters.Arlene:
                        break;
                    default:
                        break;
                }
                switch (p2Char)
                {
                    case SelectableCharacters.Adam:
                        //two.SetCharData(CharAdam);
                        two.charData = CharAdam;
                        break;
                    case SelectableCharacters.Mercuria:
                        //two.SetCharData(CharMercuria);
                        two.charData = CharMercuria;
                        break;
                    case SelectableCharacters.Thompson:
                        break;
                    case SelectableCharacters.Shouko:
                        break;
                    case SelectableCharacters.Arlene:
                        break;
                    default:
                        break;
                }


                PanelsManager.instance.StartGame();
                //GameManager.instance.InitMatch();

                //will be filled with charselect data later (is this still relevant??)
                

                started = true;
            }
        }       
    }
}
