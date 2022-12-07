using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sRend;
    //[SerializeField] private GameObject hl;
    public bool off;
    public bool isPlayer;
    private bool isBullet = false;
    private PlayerTurns whoseBullet;
    private Player playerOnThis;
    private Bullet bulletOnThis;
    private List<Bullet> bullets = new List<Bullet>(16);
    private bool hasBulletFrom1;
    private bool hasBulletFrom2;

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
        GameManager.onMatchEnd += EraseGrid;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GetIfPlayerOn()
    {
        return isPlayer;
    }
    public Player GetPlayerOnThis()
    {
        return playerOnThis;
    }
    public Bullet GetBulletOnThis()
    {
        return bulletOnThis;
    }
    public bool GetIfBullet()
    {
        return isBullet;
    }

    public bool GetIfEnemyBullet(PlayerTurns whoChecking)
    {
        //return whoseBullet;

        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].GetWhose() != whoChecking) //check bullet list for enemy bullet, if theres one it hurts
            {
                return true;
            }
        }
        return false;
    }
    public void SetAsBulletOn(PlayerTurns whose, Bullet theBullet)
    {
        isBullet = true;
        bulletOnThis = theBullet;
        bullets.Add(theBullet); //the new important one
        whoseBullet = whose;
    }
    public void SetAsBulletOff(Bullet toRemove)
    {
        //isBullet = false;

        bullets.Remove(toRemove); //removes the bullet that moved out from the list
        if (bullets.Count == 0) //checks if theres no more bullets on the list after removing that one
        {
            isBullet = false;
        }
    }

    public void SetAsPlayerOn(Player who) //is it player int 1 or player int 2?
    {
        isPlayer = true;
        playerOnThis = who;
        //transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
    public void SetAsPlayerOff() //no player means player zero (??????)
    {
        isPlayer = false;
        playerOnThis = null;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void EraseGrid()
    {
        Destroy(gameObject);
    }
}
