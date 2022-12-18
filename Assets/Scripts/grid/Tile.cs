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
    [SerializeField] private List<Bullet> bullets = new List<Bullet>(16);
    [SerializeField] private List<bulletDirs> dirs = new List<bulletDirs>(8);
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
        //GameManager.onMatchEnd += EraseGrid;
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
    public bool GetIfBulletOnAngle(Vector2 where)
    {
        bulletDirs bulletCheck;
        return true; //placeholder

        if (where == Vector2.right)
        {            
            bulletCheck = bulletDirs.R;
        }
        else if (where == Vector2.right + Vector2.up)
        {
            bulletCheck = bulletDirs.UR;
        }
        else if (where == Vector2.up)
        {
            bulletCheck = bulletDirs.U;
        }
        else if (where == Vector2.left + Vector2.up)
        {
            bulletCheck = bulletDirs.LU;
        }
        else if (where == Vector2.left)
        {
            bulletCheck = bulletDirs.L;
        }
        /*else if (where == Vector2.left + Vector2.down)
        {
            bulletCheck =
        }
        else if (dir == Vector2.down)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 270f, 0);
            anim.Play("bulletV");
            sr.flipX = false;
            sr.flipY = true;
        }
        else if (dir == Vector2.down + Vector2.right)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 315f, 0);
            anim.Play("bulletD");
            sr.flipX = false;
            sr.flipY = true;
        }*/
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
    public bool GetIfSameDirBullet(Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            if (dirs.Contains(bulletDirs.R))
            {
                return true;
            }
            return false;
        }
        else if (direction == Vector2.right + Vector2.up)
        {
            if (dirs.Contains(bulletDirs.UR))
            {
                return true;
            }
            return false;
        }
        else if (direction == Vector2.up)
        {
            if (dirs.Contains(bulletDirs.U))
            {
                return true;
            }
            return false;
        }
        else if (direction == Vector2.left + Vector2.up)
        {
            if (dirs.Contains(bulletDirs.LU))
            {
                return true;
            }
            return false;
        }
        else if (direction == Vector2.left)
        {
            if (dirs.Contains(bulletDirs.L))
            {
                return true;
            }
            return false;
        }
        else if (direction == Vector2.left + Vector2.down)
        {
            if (dirs.Contains(bulletDirs.LD))
            {
                return true;
            }
            return false;
        }
        else if (direction == Vector2.down)
        {
            if (dirs.Contains(bulletDirs.D))
            {
                return true;
            }
            return false;
        }
        else if (direction == Vector2.down + Vector2.right)
        {
            if (dirs.Contains(bulletDirs.DR))
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    public void SetAsBulletOn(PlayerTurns whose, Bullet theBullet, bulletDirs bulletDir)
    {
        isBullet = true;
        bulletOnThis = theBullet;
        bullets.Add(theBullet); //the new important one
        whoseBullet = whose;
        if (whose == PlayerTurns.Player1)
        {
            hasBulletFrom1 = true;
        }
        else
        {
            hasBulletFrom2 = true;
        }
        dirs.Add(bulletDir);
    }
    public void SetAsBulletOff(Bullet toRemove)
    {
        //isBullet = false;
        dirs.Remove(toRemove.GetDir());
        bullets.Remove(toRemove); //removes the bullet that moved out from the list
        if (bullets.Count == 0) //checks if theres no more bullets on the list after removing that one
        {
            isBullet = false;
        }
        else //WORTHLESS?????????????????????????????????????????????????????
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                hasBulletFrom1 = false;
                hasBulletFrom2 = false;

                if (bullets[i].GetWhose() == PlayerTurns.Player1)
                {
                    hasBulletFrom1 = true;
                }
                if (bullets[i].GetWhose() == PlayerTurns.Player2)
                {
                    hasBulletFrom2 = true;
                }
            }
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
