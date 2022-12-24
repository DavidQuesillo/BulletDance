using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SelectableCharacters { Adam, Mercuria, Thompson}
public class CharSelect : MonoBehaviour
{
    private bool started = false;
    [SerializeField] private bool p1Picked;    
    [SerializeField] private bool p2Picked;
    [SerializeField] private SelectableCharacters p1Char;
    [SerializeField] private SelectableCharacters p2Char;

    private void OnEnable()
    {
        started = false;
        p1Picked = false;
        p2Picked = false;
        p1Char = SelectableCharacters.Adam;
        p2Char = SelectableCharacters.Mercuria; //whoever is the furthest right on the screen
    }

    public void SelectP1Char(InputAction.CallbackContext ctx, SelectableCharacters selection)
    {
        if (ctx.started)
        {
            p1Char = selection;
            p1Picked = true;
        }
    }
    public void SelectP2Char(InputAction.CallbackContext ctx, SelectableCharacters selection)
    {
        if (ctx.started)
        {
            p2Char = selection;
            p2Picked = true;
        }
    }

    public void StartGame(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (!started && p1Picked && p2Picked)
            {
                print("started game");
                PanelsManager.instance.StartGame();
                GameManager.instance.InitMatch();
                //will be filled with charselect data later

                started = true;
            }
        }       
    }
}
