using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/SpecialAdam", fileName = "SpecialAdam")]
public class SpecialAdamH : SpecialAction
{
    private void SideBullet(Vector2 dirr, PlayerTurns whichPlayerr, Player sourcePlayerr, Tile Rtile)
    {
        if (Rtile == null) { return;}
        //GameObject r = BulletPool.Instance.RequestPoolObject();
        //same necessary checks as a normal shot
        if (Rtile.GetIfSameDirBullet(dirr))
        {
            //print("had same dir");
            return;
        }
        if (Rtile.GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
        {
            if (Rtile.GetPlayerOnThis() == this)
            {
                Debug.Log("its the same playu7er");
                return;
            }
            Rtile.GetPlayerOnThis().TakeDamage();
            //GameManager.instance.SpendAction();
            return;
        }

        GameObject r = BulletPool.Instance.RequestPoolObject();
        //GameManager.instance.SpendAction();
        //GameManager.instance.SpendShot();
        r.transform.position = Rtile.transform.position;
        r.SetActive(true);
        r.GetComponent<Bullet>().BulletInit(whichPlayerr, dirr, Rtile, sourcePlayerr);
    }

    public override bool ActivateSpecial(Vector2 dir, PlayerTurns whichPlayer, Player sourcePlayer)
    {
         Tile tilehit = Physics2D.Raycast(sourcePlayer.transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();

        if (tilehit != null)
        {
            #region The central bullet

            GameObject b = BulletPool.Instance.RequestPoolObject();
            //b.SetActive(false);

            if (tilehit.GetIfSameDirBullet(dir))
            {
                //print("had same dir");
             //   return;
                b.SetActive(false);
                return false;
            }
            else if (tilehit.GetIfPlayerOn()) //check if the enemy is on the tile you're shooting
            {
                if (tilehit.GetPlayerOnThis() == this)
                {
                    Debug.Log("its the same playu7er");
                    return false;
                }
                tilehit.GetPlayerOnThis().TakeDamage();
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
                b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit, sourcePlayer);
            }
            
            
            #endregion
            
            //Debug.Log(b.GetComponent<Bullet>().GetDir().ToString());


            //The "left" and "right" are as clockwise (at 12, 11 is left and 1 is right)
            ////the cardinal direction are gotten from the player's cardinals. Diagonals are gotten from the bullet's
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.L)
            {
                Tile leftTile = Physics2D.Raycast(b.transform.position + Vector3.right * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(b.transform.position + Vector3.right * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);
                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;
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
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/                
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

                //same necessary checks as a normal shot
                if (Rtile.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    //print("had same dir");
                    return false;
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
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.LD)
            {
                Tile leftTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);
                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;
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
                }*/

                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;
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
                */
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.D) //THIS IS WHERE WE LEFT OFF!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! no clue what this means anymore, make better comments asshole
            {
                Tile leftTile = Physics2D.Raycast(b.transform.position + Vector3.up * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(b.transform.position + Vector3.up * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);

                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.DR)
            {
                Tile leftTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);

                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.R)
            {
                Tile leftTile = Physics2D.Raycast(b.transform.position + Vector3.left * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(b.transform.position + Vector3.left * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);

                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.down * 0.5f, Vector2.down, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.UR)
            {
                Tile leftTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);

                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.U)
            {
                Tile leftTile = Physics2D.Raycast(b.transform.position + Vector3.down * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(b.transform.position + Vector3.down * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);

                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(b.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(b.transform.position + Vector3.right * 0.5f, Vector2.right, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
            }
            if (b.GetComponent<Bullet>().GetDir() == bulletDirs.LU)
            {
                Tile leftTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, leftTile);
                Tile rightTile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
                SideBullet(dir, whichPlayer, sourcePlayer, rightTile);

                #region The Left Bullet
                //THE LEFT SHOT
                /*Collider2D Ltile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.left * 0.5f, Vector2.left, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                l.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
                #region The Right Bullet
                //THE RIGHT SHOT
                /*Collider2D Rtile = Physics2D.Raycast(sourcePlayer.transform.position + Vector3.up * 0.5f, Vector2.up, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

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
                r.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
                #endregion
            }            
            EndSpecial();
            UiManager.instance.LockButton(2, whichPlayer);
            return true;
        }
        return false;
    }

    public override void EndSpecial()
    {
        //base.EndSpecial();
        //GameManager.instance.SpendSpecial();
    }
}
