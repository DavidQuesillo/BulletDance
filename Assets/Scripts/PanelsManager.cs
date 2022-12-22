using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject select;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject result;
    
    [Header("Default Panels Buttons")]
    [SerializeField] private GameObject returnButton;
    [SerializeField] private GameObject playButton;

    public static PanelsManager instance;

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

    public void GoToSelect()
    {
        title.SetActive(false);
        select.SetActive(true);
    }
    public void StartGame()
    {
        select.SetActive(false);
        game.SetActive(true);
    }
    public void ShowResult()
    {
        game.SetActive(false);
        result.SetActive(true);
        
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(returnButton);
    }
    public void ReturnToTitle()
    {
        game.SetActive(false);
        result.SetActive(false);
        title.SetActive(true);
        MusicManager.instance.StopPlaying();
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(playButton);
    }

    public void ExitGame()
    {
        Application.Quit();
    }    
}
