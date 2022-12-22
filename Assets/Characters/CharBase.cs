using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/PlayerCharacters", fileName = "PlayerChar")]
public class CharBase : ScriptableObject
{
    [Header("Gameplay elements")]    
    public int hp;
    public SpecialAction charSpecial;
    //public Object specialScript;
    //public MonoBehaviour specialScript;
    public int baseActions;
    public int baseShots;
    public RuntimeAnimatorController animations;
    public AudioClip specialClip;

    [Header("Character info")]
    public string charName;
    public Sprite portrait;
    public Sprite miniPortrait;
    public string winQuote;
    
}
