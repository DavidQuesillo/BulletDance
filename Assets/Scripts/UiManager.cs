using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Player 1 side")]
    [SerializeField] private TextMeshProUGUI hp1;
    [SerializeField] private TextMeshProUGUI ActionCount1;

    [Header("Player 2 side")]
    [SerializeField] private TextMeshProUGUI hp2;
    [SerializeField] private TextMeshProUGUI ActionCount2;

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
    public void UpdateActions(int actionsLeft)
    {
        if (GameManager.instance.playerPlaying == PlayerTurns.Player1)
        {
            ActionCount1.text = actionsLeft.ToString();
        }
        else
        {
            ActionCount2.text = actionsLeft.ToString();
        }
    }
}
