using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Specials", fileName = "SpecialMove")]
public class SpecialAction : ScriptableObject
{
    public virtual void SetupSpecial()
    {

    }
    public virtual void SetupSpecial(Player player)
    {

    }
    public virtual void SetupSpecial(GameObject gobj)
    {

    }
    public virtual void SetupSpecial(Player player, GameObject gobj)
    {

    }
    //public SpecialAction specialScript;
    public virtual void ActivateSpecial()
    {

    }
    public virtual void ActivateSpecial(Vector2 dir)
    {

    }
    public virtual void ActivateSpecial(Vector2 dir, PlayerTurns whichPlayer)
    {

    }
    public virtual void ActivateSpecial(Vector2 dir, PlayerTurns whichPlayer, Player sourcePlayer)
    {

    }

    public virtual void EndSpecial()
    {

    }

    public virtual void UndoSetupSpecial() //this is necesary for the game to work on repeated matches
    {

    }
}
