using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelect : MonoBehaviour
{
    
    public void StartGame()
    {
        PanelsManager.instance.StartGame();
        GameManager.instance.InitMatch();
        //will be filled with data later

    }
}
