using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/SpecialAdam", fileName = "SpecialAdam")]
public class SpecialAdamH : SpecialAction
{
    public override void ActivateSpecial(Vector2 dir, PlayerTurns whichPlayer, Player sourcePlayer)
    {
        Collider2D tilehit = Physics2D.Raycast(sourcePlayer.transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

        if (tilehit != null)
        {
            #region The central bullet

            GameObject b = BulletPool.Instance.RequestPoolObject();
            //b.SetActive(false);

            if (tilehit.GetComponent<Tile>().GetIfSameDirBullet(dir))
            {
                //print("had same dir");
             //   return;
                b.SetActive(false);
            }
            else if (tilehit.GetComponent<Tile>().GetIfPlayerOn()) //check if the enemy is on the tile you're shooting
            {
                if (tilehit.GetComponent<Tile>().GetPlayerOnThis() == this)
                {
                    Debug.Log("its the same playu7er");
                    return;
                }
                tilehit.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                b.SetActive(false);
                //GameManager.instance.SpendAction();
                //return;
            }
            else
            {
                //the bullet straight ahead
                //GameObject b = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();

                b.transform.position = tilehit.transform.position;
                b.SetActive(true);
                b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
            }
            
            
            #endregion
            
            //Debug.Log(b.GetComponent<Bullet>().GetDir().ToString());


            //The "left" and "right" are as clockwise (at 12, 11 is left and 1 is right)
            ////the cardinal direction are gotten from the player's cardinals. Diagonals are gotten from the bullet's
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.L)
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;
                if (Ltile == tilehit)
                {  Debug.Log("hitting the same"); }
                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject l = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                l.transform.position = Ltile.transform.position;
                l.SetActive(true);
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);                
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject r = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                r.transform.position = Rtile.transform.position;
                r.SetActive(true);
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.LD)
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;
                GameObject l = BulletPool.Instance.RequestPoolObject();

                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    //return;
                    l.SetActive(false);
                }
                else if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    //return;
                    l.SetActive(false);
                }
                else
                {
                    //GameObject l = BulletPool.Instance.RequestPoolObject();
                    //GameManager.instance.SpendAction();
                    //GameManager.instance.SpendShot();
                    l.transform.position = Ltile.transform.position;
                    l.SetActive(true);
                    l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                }

                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;
                GameObject r = BulletPool.Instance.RequestPoolObject();

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    //return;
                    r.SetActive(false);
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    //return;
                    r.SetActive(false);
                }
                else
                {
                    //GameObject r = BulletPool.Instance.RequestPoolObject();
                    //GameManager.instance.SpendAction();
                    //GameManager.instance.SpendShot();
                    r.transform.position = Rtile.transform.position;
                    r.SetActive(true);
                    r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                }
                
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.D) //THIS IS WHERE WE LEFT OFF!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject l = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                l.transform.position = Ltile.transform.position;
                l.SetActive(true);
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject r = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                r.transform.position = Rtile.transform.position;
                r.SetActive(true);
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.DR)
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject l = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                l.transform.position = Ltile.transform.position;
                l.SetActive(true);
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject r = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                r.transform.position = Rtile.transform.position;
                r.SetActive(true);
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.R)
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject l = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                l.transform.position = Ltile.transform.position;
                l.SetActive(true);
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject r = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                r.transform.position = Rtile.transform.position;
                r.SetActive(true);
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.UR)
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject l = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                l.transform.position = Ltile.transform.position;
                l.SetActive(true);
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject r = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                r.transform.position = Rtile.transform.position;
                r.SetActive(true);
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.U)
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject l = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                l.transform.position = Ltile.transform.position;
                l.SetActive(true);
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject r = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                r.transform.position = Rtile.transform.position;
                r.SetActive(true);
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.LU)
            {
                #region The Left Bullet
                //THE LEFT SHOT
                Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Ltile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Ltile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Ltile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Ltile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject l = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                l.transform.position = Ltile.transform.position;
                l.SetActive(true);
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return;
                }
                if (Rtile.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (Rtile.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    Rtile.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();
                    //GameManager.instance.SpendAction();
                    return;
                }

                GameObject r = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                r.transform.position = Rtile.transform.position;
                r.SetActive(true);
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                #endregion
            }
            EndSpecial();
            UiManager.instance.LockButton(2, whichPlayer);
        }
    }

    public override void EndSpecial()
    {
        base.EndSpecial();
        GameManager.instance.SpendSpecial();
    }
}
