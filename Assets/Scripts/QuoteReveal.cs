using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuoteReveal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textWindow;
    [SerializeField] private string quote;
    [SerializeField] private float letterShowTime = 0.1f;
    //[SerializeField] private char[] debugDisplay;
    private WaitForSeconds letterTime;
    public QuoteReveal SetQuote(string line)
    {
        quote = line;
        return this;
    }
    public QuoteReveal PlayQuote()
    {
        StartCoroutine(RevealText());
        return this;
    }

    private IEnumerator RevealText()
    {
        letterTime = new WaitForSeconds(letterShowTime);
        //yield return new WaitForSeconds(5f); //debug
        char[] letters = quote.ToCharArray();
        textWindow.text = "";
        //debugDisplay = letters;  //debug
        for (int i = 0; i < letters.Length; i++)
        {
            textWindow.text += letters[i];
            yield return letterTime;
        }

    }

    // Start is called before the first frame update
    /*void Start()
    {
        DebugRev(); //debug, delete start and update after
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
