using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sRend;
    //[SerializeField] private GameObject hl;
    public bool off;
    public PlayerTurns isPlayer;
    private bool isBullet = false;
    private PlayerTurns whoseBullet;
    private Bullet bulletOnThis;
    public void TileInit(bool isOffset)
    {
        off = isOffset;
        /*if (isOffset)
        {
            transform.position = transform.position + Vector3.up * 0.5f;
        }*/
        
    }

    /*private void OnMouseEnter()
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
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Bullet GetBulletOnThis()
    {
        return bulletOnThis;
    }
    public bool GetIfBullet()
    {
        return isBullet;
    }
    public PlayerTurns GetWhoseBullet()
    {
        return whoseBullet;
    }
    public void SetAsBulletOn(PlayerTurns whose, Bullet theBullet)
    {
        isBullet = true;
        bulletOnThis = theBullet;
        whoseBullet = whose;
    }
    public void SetAsBulletOff()
    {
        isBullet = false;
    }

    public void SetAsPlayerOn(PlayerTurns who) //is it player int 1 or player int 2?
    {
        isPlayer = who;
        //transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
    public void SetAsPlayerOff() //no player means player zero (??????)
    {
        isPlayer = 0;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
