using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
    private bool started = false;

    private void OnEnable()
    {
        started = false;
    }
    public void StartGame()
    {
        if (!started)
        {
            print("started game");
            PanelsManager.instance.StartGame();
            GameManager.instance.InitMatch();
            //will be filled with charselect data later

            started = true;
        }


    }
}
