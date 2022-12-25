using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum Screens { Title, Select, Gameplay, Victory}
public class PanelsManager : MonoBehaviour
{
    [SerializeField] private Screens currentScreen = Screens.Title;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject select;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject result;
    [SerializeField] private GameObject pause;
    private bool charSelReady = false; //TESTING, MUST BE FALSE
    [SerializeField] private bool paused;
    [Header("Default Panels Buttons")]
    [SerializeField] private GameObject returnButton;
    [SerializeField] private GameObject playButton;

    public static PanelsManager instance;
    [Header("CharSelect Script")]
    [SerializeField] private CharSelect selectScript;

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

    public void EnterKey(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            switch (currentScreen)
            {
                case Screens.Title:
                    print("title thru here");
                    GoToSelect();
                    break;
                case Screens.Select:
                    /*if (charSelReady)
                    {
                        //StartGame();
                        selectScript.StartGame(ctx);
                    }*/
                    selectScript.StartGame(ctx);
                    break;
                case Screens.Gameplay:
                    if (paused)
                    {
                        ReturnToTitle();
                    }
                    break;
                case Screens.Victory:
                    ReturnToTitle();
                    break;
                default:
                    break;
            }
        
        }
    }
    public void EscapeKey(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            print("escape pressed");
            switch (currentScreen)
            {
                case Screens.Title:
                    Application.Quit();
                    break;
                case Screens.Select:
                    ReturnToTitle();
                    break;
                case Screens.Gameplay:
                    if (!paused) { Pause();}
                    else { Unpause();}
                    break;
                case Screens.Victory:
                    break;
                default:
                    break;
            }
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        GameManager.instance.paused = true;
        paused = true;
        pause.SetActive(true);
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
        GameManager.instance.paused = false;
        paused = false;
        pause.SetActive(false);
    }
    public void SetSelectReady()
    {
        charSelReady = true;
    }
    public void GoToSelect()
    {
        title.SetActive(false);
        select.SetActive(true);
        currentScreen = Screens.Select;
    }
    public void StartGame()
    {
        print("pressed too");
        select.SetActive(false);
        game.SetActive(true);
        Time.timeScale = 1f;
        currentScreen = Screens.Gameplay;
        GameManager.instance.InitMatch();
    }
    public void ShowResult()
    {
        game.SetActive(false);
        result.SetActive(true);
        currentScreen = Screens.Victory;
        Time.timeScale = 1f;
        //var eventSystem = EventSystem.current;
        //eventSystem.SetSelectedGameObject(returnButton);
    }
    public void ReturnToTitle()
    {
        //undo pause state
        paused = false;
        pause.SetActive(false);

        //the rest
        print("called title");
        currentScreen = Screens.Title;
        game.SetActive(false);
        select.SetActive(false);
        result.SetActive(false);
        title.SetActive(true);
        Time.timeScale = 1f;
        //MusicManager.instance.StopPlaying();
        GameManager.instance.ClearForTitle();
        //var eventSystem = EventSystem.current;
        //eventSystem.SetSelectedGameObject(playButton);
    }

    public void ExitGame()
    {
        Application.Quit();
    }    
}
