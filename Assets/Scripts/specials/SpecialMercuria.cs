using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/SpecialMercuria", fileName = "SpecialMercuria")]
public class SpecialMercuria : SpecialAction
{
    public GameObject sword;  //ref to the prefab
    private GameObject Sword; //instance created into the scene
    //private Player parentPlayer;
    public override void SetupSpecial(Player player, GameObject gobj)
    {
        //base.SetupSpecial();
        GameObject s = Instantiate(sword, player.transform);
        s.GetComponent<SpriteRenderer>().color = player.GetSpriteRend().color;
        //s.SetActive(false);
        Sword = s;
    }
    public override bool ActivateSpecial(Vector2 dir, PlayerTurns whichPlayer, Player sourcePlayer)
    {
        //base.ActivateSpecial(dir, whichPlayer, sourcePlayer);

        Collider2D tilehit = Physics2D.Raycast(sourcePlayer.transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

        if (tilehit != null)
        {
            #region The central bullet
            if (tilehit.GetComponent<Tile>().GetIfPlayerOn()) //check if the enemy is on the tile you're shooting
            {
                if (tilehit.GetComponent<Tile>().GetPlayerOnThis() == this)
                {
                    Debug.Log("its the same playu7er");
                    return false;
                }
                tilehit.GetComponent<Tile>().GetPlayerOnThis().Instakill();
                //return;
            }
            if (tilehit.GetComponent<Tile>()
                .GetIfEnemyBullet(sourcePlayer.GetWhichPlayer()))
            {
                List<Bullet> bOnT = tilehit.GetComponent<Tile>().GetBulletsList();
                //Debug.Log("count on tile: " + tilehit.GetComponent<Tile>().GetBulletsList().Count.ToString());
                //Debug.Log("Count on sword: " + bOnT.Count.ToString());
                List<Bullet> bulletsToDelete = new List<Bullet>();
                int bCount = bOnT.Count;
                for (int i = 0; i < bCount; i++)
                {
                    Debug.Log("Loop ran " + i);
                    if (bOnT[i].GetWhose() != sourcePlayer.GetWhichPlayer())
                    {
                        /*Bullet b = bOnT[i];
                        //bOnT.Remove(b);
                        tilehit.GetComponent<Tile>().SetAsBulletOff(b);
                        b.gameObject.SetActive(false);*/
                        /*Debug.Log("Called " + i);
                        Debug.Log("bont count: " + bOnT.Count);
                        Debug.Log("start count: "+ bCount);*/
                        bulletsToDelete.Add(bOnT[i]);
                    }
                    /*else
                    {
                        Debug.Log("its bc theyre misassigned");
                    }*/
                }

                for (int i = 0; i < bCount; i++)
                {
                    tilehit.GetComponent<Tile>().SetAsBulletOff(bulletsToDelete[i]);
                    bulletsToDelete[i].gameObject.SetActive(false);
                }
            }

            //the bullet straight ahead
            /*GameObject b = BulletPool.Instance.RequestPoolObject();
            //GameManager.instance.SpendAction();
            //GameManager.instance.SpendShot();
            b.transform.position = tilehit.transform.position;
            b.SetActive(true);
            b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), sourcePlayer);*/
            #endregion

            //Debug.Log(b.GetComponent<Bullet>().GetDir().ToString());

            //Sword.transform.position = tilehit.transform.position;
            Sword.GetComponent<Animator>().Play("SwordSlash");
            //The "left" and "right" are as clockwise (at 12, 11 is left and 1 is right)
            ////the cardinal direction are gotten from the player's cardinals. Diagonals are gotten from the bullet's
            /*if (dir == Vector2.right)
            {
                //Sword.transform.rotation = Quaternion.identity;
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, Quaternion.identity);

            }
            else if (dir == Vector2.right + Vector2.up)
            {
                //Sword.transform.rotation = new Quaternion(0, 0, 45f, 0);
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, new Quaternion(0, 0, 45f, 0));
            }
            else if (dir == Vector2.up)
            {
                //Sword.transform.rotation = new Quaternion(0, 0, 90f, 0);
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, new Quaternion(0, 0, 90f, 0));
            }
            else if (dir == Vector2.left + Vector2.up)
            {
                //Sword.transform.rotation = new Quaternion(0, 0, 135f, 0);
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, new Quaternion(0, 0, 135f, 0));
            }
            else if (dir == Vector2.left)
            {
                //Sword.transform.rotation = new Quaternion(0, 0, 180f, 0);
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, new Quaternion(0, 0, 180f, 0));
            }
            else if (dir == Vector2.left + Vector2.down)
            {
                //Sword.transform.rotation = new Quaternion(0, 0, 225f, 0);
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, new Quaternion(0, 0, 225f, 0));
            }
            else if (dir == Vector2.down)
            {
                //Sword.transform.rotation = new Quaternion(0, 0, 270f, 0);
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, new Quaternion(0, 0, 270f, 0));

            }
            else if (dir == Vector2.down + Vector2.right)
            {
                //Sword.transform.rotation = new Quaternion(0, 0, 315f, 0);
                Sword.transform.SetPositionAndRotation(tilehit.transform.position, new Quaternion(0, 0, 315f, 0));
            }*/

            Sword.transform.position = tilehit.transform.position;
            Sword.transform.right = sourcePlayer.transform.position - Sword.transform.position;
            EndSpecial();
            UiManager.instance.LockButton(2, whichPlayer);
            return true;
        }
        return false;
    }

    public override void EndSpecial()
    {
        base.EndSpecial();
        GameManager.instance.SpendSpecial();
        
    }

    public override void UndoSetupSpecial()
    {
        //base.UndoSetupSpecial();
        Destroy(Sword);
    }
}
