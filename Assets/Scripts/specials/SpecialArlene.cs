using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/SpecialArlene", fileName = "SpecialArlene")]
public class SpecialArlene : SpecialAction
{
    public override bool ActivateSpecial(Vector2 dir, PlayerTurns whichPlayer, Player sourcePlayer)
    {
        //return base.ActivateSpecial(dir, whichPlayer, sourcePlayer);
        
        //+first time doing this right
        Tile tilehit = Physics2D.Raycast(sourcePlayer.transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
        Tile tilehit2 = tilehit;
        if (tilehit != null)
        {
            tilehit2 = Physics2D.Raycast((Vector2)(tilehit.transform?.position + (Vector3)dir * 0.5f), dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
        }
        
        if (tilehit != null && tilehit2 != null)
        {
            Debug.Log("tile1: " + tilehit.name);
            Debug.Log("tile2: " + tilehit2.name);
        }        

        if (tilehit != null && tilehit2 != null && tilehit2 != tilehit)
        {
            bool b1hit = false;

            if (tilehit.GetIfSameDirBullet(dir))
            {
                Debug.Log("had same dir");
                return false;
            }
            if (tilehit2 != null)
            {
                if (tilehit2.GetIfSameDirBullet(dir))
                {
                    Debug.Log("had same dir");
                    return false;
                }
            }
            if (tilehit.GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
            {
                if (tilehit.GetPlayerOnThis() == this)
                {
                    Debug.Log("its the same playu7er");
                    return false;
                }
                tilehit.GetPlayerOnThis().TakeDamage();
                b1hit = true;
                //from bullet destroy
                GameObject p = PoofPool.Instance.RequestPoolObject();
                p.transform.position = tilehit.transform.position;
                p.GetComponent<SpriteRenderer>().color = sourcePlayer.GetSpriteRend().color;
                p.GetComponent<Animator>().Play("bulletPoof");

                //SoundManager.instance.PlayShootSound();
                //GameManager.instance.SpendShot();
                //GameManager.instance.SpendAction();
                //return ;
            }
            /*if (tilehit.GetComponent<Tile>().GetIfBullet())
            {

            }*/
            //if theres no player, instantiate the bullet as normal
            if (!b1hit)
            {
                GameObject b = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                //GameManager.instance.SpendShot();
                b.transform.position = tilehit.transform.position;
                b.SetActive(true);
                b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);
                //SoundManager.instance.PlayShootSound();


                //print(dir.ToString()); //debug what dir the bullet is getting
            }
            

            //the second bullet
            if (tilehit2 != null)
            {
                bool b2hit = false;

                /*if (tilehit2.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    Debug.Log("had same dir");
                    return false;
                }*/
                if (tilehit2.GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (tilehit2.GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return false;
                    }
                    tilehit2.GetPlayerOnThis().TakeDamage();
                    b2hit = true;
                    //from bullet destroy
                    GameObject p = PoofPool.Instance.RequestPoolObject();
                    p.transform.position = tilehit2.transform.position;
                    p.GetComponent<SpriteRenderer>().color = sourcePlayer.GetSpriteRend().color;
                    p.GetComponent<Animator>().Play("bulletPoof");

                    //SoundManager.instance.PlayShootSound();
                    //GameManager.instance.SpendShot();
                    //GameManager.instance.SpendAction();
                    //return ;
                }
                /*if (tilehit.GetComponent<Tile>().GetIfBullet())
                {

                }*/
                //if theres no player, instantiate the bullet as normal
                if (!b2hit)
                {
                    GameObject b = BulletPool.Instance.RequestPoolObject();
                    //GameManager.instance.SpendAction();
                    //GameManager.instance.SpendShot();
                    b.transform.position = tilehit2.transform.position;
                    b.SetActive(true);
                    b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit2.GetComponent<Tile>(), sourcePlayer);
                    //SoundManager.instance.PlayShootSound();


                    //print(dir.ToString()); //debug what dir the bullet is getting
                }
            }
            else { return false;}
            return true;
        }
        return false;
    }
    public override void EndSpecial()
    {
        //base.EndSpecial();
    }
}

