using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sRend;
    [SerializeField] private GameObject hl;
    public bool off;

    public void TileInit(bool isOffset)
    {
        off = isOffset;
        /*if (isOffset)
        {
            transform.position = transform.position + Vector3.up * 0.5f;
        }*/
        
    }

    private void OnMouseEnter()
    {
        hl.SetActive(true);
    }
    private void OnMouseExit()
    {
        hl.SetActive(false);
    }

    private void OnMouseDown()
    {
        //UnitManager.instance.SetSelectedTile(gameObject);
        print(off.ToString());
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
