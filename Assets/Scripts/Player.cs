using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Vector2 dir;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private PlayerTurns whichPlayer;
    [SerializeField] private Tile tileOn;
    [SerializeField] private float moveDuration = 0.3f;
    [SerializeField] private int hp = 3;
    private bool moving;    
    private bool specialAvailable = true;
    [SerializeField] private PlayerArrows arrows;

    [Header("Character Data")]
    public CharBase charData; //FIX THIS ENCAPSULATION IF AT ALL POSSIBLE
    private int baseActions;
    private int baseShots;
    [SerializeField] private SpecialAction special; //modified, must be SpecialAction


    // Start is called before the first frame update
    void OnEnable()
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            //tileOn = GridManager.instance.grid[GridManager.instance.width / 2 - 1, GridManager.instance.height - 2];
            //tileOn = GridManager.instance.grid[GridManager.instance.width / 2 - 1, 1];

            tileOn = GridManager.instance.grid[1, GridManager.instance.height / 2];
        }
        else
        {
            //tileOn = GridManager.instance.grid[GridManager.instance.width / 2, 1];
            //tileOn = GridManager.instance.grid[GridManager.instance.width / 2, GridManager.instance.height - 2];

            tileOn = GridManager.instance.grid[GridManager.instance.width - 2, GridManager.instance.height / 2];
        }
        rb.position = tileOn.transform.position;
        tileOn.SetAsPlayerOn(this);
        sr.enabled = true;
        //arrows.CheckAvailableMoves(); //moved to coroutine
        arrows.ArrowVisibility(false);

        baseActions = charData.baseActions;
        baseShots = charData.baseShots;
        specialAvailable = true;
        hp = charData.hp;
        special = charData.charSpecial; //intended way, on placeholder //itworked
        special.SetupSpecial(this, gameObject);
        //special = charData.specialScript; //testing //it failed
        anim.runtimeAnimatorController  = charData.animations;
        
        //testing if delay fixes things
        StartCoroutine(SetupRequiredWait());
        //CheckFacing(); //moved to coroutine

        /*print(Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("Grid")).collider.gameObject.name);
        tileOn = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("Grid")).collider.gameObject.GetComponent<Tile>();

        if (whichPlayer == PlayerTurns.Player1)
        {
            tileOn.SetAsPlayerOn(1);
        }
        else
        {
            tileOn.SetAsPlayerOn(2);
        }*/
    }
    private IEnumerator SetupRequiredWait()
    {
        //yield return new WaitForSecondsRealtime(0.01f);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        arrows.CheckAvailableMoves();
        CheckFacing();
    }

    // Update is called once per frame
    void Update()
    {
        arrows.transform.position = transform.position;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rb.position, rb.position + Vector2.up);
    }*/
    public CharBase GetCharData()
    {
        return charData;
    }
    public void SetCharData(CharBase data)
    {
        charData = data;
    }
    public PlayerTurns GetWhichPlayer()
    {
        return whichPlayer;
    }
    public SpriteRenderer GetSpriteRend()
    {
        return sr;
    }
    public PlayerArrows GetArrows()
    {
        return arrows;
    }
    public void TakeDamage()
    {
        print("taking damage");
        hp--;
        UiManager.instance.UpdateHP(whichPlayer, hp);
        if (hp <= 0)
        {
            PlayerDeath();
        }
    }
    public void TakeDamage(Vector3 deathLocation)
    {
        print("taking damage");
        hp--;
        UiManager.instance.UpdateHP(whichPlayer, hp);
        if (hp <= 0)
        {
            PlayerDeath(deathLocation);
        }
    }
    public void Instakill()
    {
        hp = 0;
        PlayerDeath();
    }

    public void PlayerDeath()
    {
        //placeholder
        sr.enabled = false;
        GameManager.instance.MatchEnd(whichPlayer, tileOn.transform.position);
    }
    public void PlayerDeath(Vector3 explodePos)
    {
        sr.enabled = false;
        GameManager.instance.MatchEnd(whichPlayer, explodePos);
    }

    public void CheckFacing()
    {
        Vector3 p;
        if (whichPlayer == PlayerTurns.Player1)
        {
            p = GameManager.instance.player2.transform.position;            
        }
        else
        {
            p = GameManager.instance.player1.transform.position;
        }
        transform.right = p - transform.position;

        /*if (Vector3.Distance(new Vector3(p.x, 0f, 0f), new Vector3(transform.position.x, 0f, 0f)) 
                >= 
                    Vector3.Distance(new Vector3(0f, p.y, 0f), new Vector3(0f, transform.position.y, 0)))
        {
            if (p.x > transform.position.x)
            {
                anim.Play("FaceH");
                sr.flipX = false;
            }
            else
            {
                anim.Play("FaceH");
                sr.flipX = true;
            }
        }
        else
        {
            if (p.y > transform.position.y)
            {
                anim.Play("FaceUp");
                sr.flipX = false;
            }
            else
            {
                anim.Play("FaceDown");
                sr.flipX = false;
            }
        }*/
    }

    public void TrackDir(InputAction.CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
        arrows.HighlightPointingArrow(dir); //currently testing
    }

    public void MoveInDir(InputAction.CallbackContext ctx)
    {
        if (ctx.started && gameObject.activeInHierarchy)
        {
            if (GameManager.instance.playerPlaying != whichPlayer || moving || GameManager.instance.paused
                || dir == Vector2.zero || GameManager.instance.GetActionsInTurn() <= 0)
            { return;    }

            Collider2D tilehit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

            //print(tilehit.gameObject.name);
            if (tilehit != null)
            {
                if (tilehit.GetComponent<Tile>().GetIfPlayerOn())
                {
                    return;
                }
                SoundManager.instance.PlayStepSound();
                //rb.MovePosition(tilehit.transform.position); //replace with tween
                if (tilehit.GetComponent<Tile>().GetIfBullet())
                {
                    if (tilehit.GetComponent<Tile>().GetIfEnemyBullet(whichPlayer))
                    {
                        TakeDamage(tilehit.transform.position);                        
                        print("player stepped on");
                        //if (hp <= 0) { return; }
                        //tilehit.gameObject.SetActive(false);

                        //tilehit.GetComponent<Tile>().GetBulletOnThis().BulletDestroy(true); //the old method

                        //the new testing method
                        #region the thing borrowed from mercuria´s sword
                        /*List<Bullet> bOnT = tilehit.GetComponent<Tile>().GetBulletsList();
                        //Debug.Log("count on tile: " + tilehit.GetComponent<Tile>().GetBulletsList().Count.ToString());
                        //Debug.Log("Count on sword: " + bOnT.Count.ToString());
                        List<Bullet> bulletsToDelete = new List<Bullet>();
                        int bCount = bOnT.Count;
                        for (int i = 0; i < bCount; i++)
                        {
                            Debug.Log("Loop ran " + i);
                            if (bOnT[i].GetWhose() != whichPlayer)
                            {
                                /*Bullet b = bOnT[i];
                                //bOnT.Remove(b);
                                tilehit.GetComponent<Tile>().SetAsBulletOff(b);
                                b.gameObject.SetActive(false);*/
                                /*Debug.Log("Called " + i);
                                Debug.Log("bont count: " + bOnT.Count);
                                Debug.Log("start count: "+ bCount);*/
                               /* bulletsToDelete.Add(bOnT[i]);
                            }*/ //HERE TOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                            /*else
                            {
                                Debug.Log("its bc theyre misassigned");
                            }*/
                        /*} //heres the odd----------------------------------------------------asdcasvddbsdfb

                        for (int i = 0; i < bCount; i++)
                        {
                            print("count: " +i);
                            print("tile count: " +tilehit.GetComponent<Tile>().GetBulletsList().Count);
                            tilehit.GetComponent<Tile>().SetAsBulletOff(bulletsToDelete[i]);
                            //bulletsToDelete[i].gameObject.SetActive(false);
                            bulletsToDelete[i].BulletDestroy(true);
                        }*/
                        #endregion
                        //trying different method using tile function
                        tilehit.GetComponent<Tile>().ClearBulletsOfOpponent(whichPlayer);
                    }
                }
                //StartCoroutine(PlayerMove(tilehit.transform.position)); //here it is
                moving = true;
                rb.DOMove(tilehit.transform.position, 0.2f).OnComplete(() => FinalizeMove());

                tileOn.SetAsPlayerOff();
                //tilehit.GetComponent<Tile>().SetAsPlayerOn(whichPlayer);

                tileOn = tilehit.GetComponent<Tile>();
                tileOn.SetAsPlayerOn(this);

                CheckFacing();
                SoundManager.instance.RefreshBulletAdvSound(); //I really dont know where else to put this
                //GameManager.instance.SpendAction();
                //print("moved to " + tilehit.name);
            }
            /*else
            {
                print("move fail");
                rb.MovePosition(rb.position + dir);
            }*/
        }
    }

    public void ShootInDir(InputAction.CallbackContext ctx)
    {
        if (ctx.started && gameObject.activeInHierarchy)
        {
            if (GameManager.instance.playerPlaying != whichPlayer || moving || GameManager.instance.paused
                || dir == Vector2.zero || GameManager.instance.GetShotsInTurn() <= 0)
            {  return;      }

            Collider2D tilehit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

            if (tilehit != null)
            {
                
                if (tilehit.GetComponent<Tile>().GetIfSameDirBullet(dir))
                {
                    print("had same dir");
                    return;
                }
                if (tilehit.GetComponent<Tile>().GetIfPlayerOn()) // check if the enemy is on the tile you're shooting
                {
                    if (tilehit.GetComponent<Tile>().GetPlayerOnThis() == this)
                    {
                        Debug.Log("its the same playu7er");
                        return;
                    }
                    tilehit.GetComponent<Tile>().GetPlayerOnThis().TakeDamage();

                    //from bullet destroy
                    GameObject p = PoofPool.Instance.RequestPoolObject();
                    p.transform.position = tilehit.transform.position;
                    p.GetComponent<SpriteRenderer>().color = sr.color;
                    p.GetComponent<Animator>().Play("bulletPoof");

                    SoundManager.instance.PlayShootSound();
                    GameManager.instance.SpendShot();
                    //GameManager.instance.SpendAction();
                    return;
                }
                /*if (tilehit.GetComponent<Tile>().GetIfBullet())
                {

                }*/
                //if theres no player, instantiate the bullet as normal
                GameObject b = BulletPool.Instance.RequestPoolObject();
                //GameManager.instance.SpendAction();
                GameManager.instance.SpendShot();
                b.transform.position = tilehit.transform.position;
                b.SetActive(true);
                b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), this);
                SoundManager.instance.PlayShootSound();


                print(dir.ToString()); //debug what dir the bullet is getting
            }
        }
    }

    private void FinalizeMove()
    {
        moving = false;
        arrows.CheckAvailableMoves();
        if (hp <= 0) { return;}
        GameManager.instance.SpendAction();        
        //this func replaces what happens in playermove coroutine
    }

    /*private IEnumerator PlayerMove(Vector2 to)
    {
        Tween t = rb.DOMove(to, moveDuration);
        moving = true;

        /*while (t.IsActive())
        {
            yield return null;
        }/////////*
        yield return t.WaitForCompletion();

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.SpendAction();
        moving = false;
        yield break;
    }*/

    public void UseSpecial(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (GameManager.instance.playerPlaying != whichPlayer || moving ||GameManager.instance.paused
                || dir == Vector2.zero || !specialAvailable)
            { return; }
            //special.ActivateSpecial(dir, whichPlayer, this); //modified for testing
                                                             //special.GetType().GetMethod("ActivateSpecial").Invoke();
                                                             //Invoke(special.GetType().GetMethod("ActivateSpecial").ToString(), 0f);
            
            if (special.ActivateSpecial(dir, whichPlayer, this))
            {
                specialAvailable = false;
                GameManager.instance.SpendSpecial(); //may need to change depending on function of certain specials
                SoundManager.instance.PlaySpecialSound(charData.specialClip);
                //UiManager.instance.LockButton(2, whichPlayer);
            }
            else
            {
                SoundManager.instance.PlayFailSound();
            }
        }        
    }
    public void RefreshSpecial()
    {
        specialAvailable = true;
    }
}
