using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject select;
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject result;

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
    }
    public void ReturnToTitle()
    {
        game.SetActive(false);
        result.SetActive(false);
        title.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }    
}
