using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color colorA;
    [SerializeField] private Color colorB;
    [SerializeField] private SpriteRenderer sRend;

    [SerializeField] private GameObject highlight;

    public void TileInit(bool isOffset)
    {
        if (isOffset)
        {
            sRend.color = colorB;
        }
        else
        {
            sRend.color = colorA;
        }
    }

    private void OnMouseDown()
    {
        UnitManager.instance.SetSelectedTile(gameObject);
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
