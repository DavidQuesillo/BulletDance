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

    [Header("Character Data")]
    [SerializeField] private CharBase charData;
    private int baseActions;
    private SpecialAction special;


    // Start is called before the first frame update
    void OnEnable()
    {
        if (whichPlayer == PlayerTurns.Player1)
        {
            tileOn = GridManager.instance.grid[GridManager.instance.width / 2, GridManager.instance.height - 2];
        }
        else
        {
            tileOn = GridManager.instance.grid[GridManager.instance.width / 2 - 1, 1];
        }
        rb.position = tileOn.transform.position;
        tileOn.SetAsPlayerOn(this);

        baseActions = charData.baseActions;
        hp = charData.hp;
        special = charData.charSpecial;

        CheckFacing();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawLine(rb.position, rb.position + Vector2.up);
    }*/
    public CharBase GetCharData()
    {
        return charData;
    }
    public PlayerTurns GetWhichPlayer()
    {
        return whichPlayer;
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
    public void Instakill()
    {
        hp = 0;
        PlayerDeath();
    }

    public void PlayerDeath()
    {
        //placeholder
        GameManager.instance.MatchEnd(whichPlayer);
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

        if (Vector3.Distance(new Vector3(p.x, 0f, 0f), new Vector3(transform.position.x, 0f, 0f)) 
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
        }
    }

    public void TrackDir(InputAction.CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
    
    }

    public void MoveInDir(InputAction.CallbackContext ctx)
    {
        if (ctx.started && gameObject.activeInHierarchy)
        {
            if (GameManager.instance.playerPlaying != whichPlayer || moving || dir == Vector2.zero)
            {
                return;
            }
            Collider2D tilehit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

            //print(tilehit.gameObject.name);
            if (tilehit != null)
            {
                //rb.MovePosition(tilehit.transform.position); //replace with tween
                if (tilehit.GetComponent<Tile>().GetIfBullet())
                {
                    if (tilehit.GetComponent<Tile>().GetIfEnemyBullet(whichPlayer))
                    {
                        TakeDamage();
                        print("player stepped on");
                        //tilehit.gameObject.SetActive(false);
                        tilehit.GetComponent<Tile>().GetBulletOnThis().BulletDestroy();
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
            if (GameManager.instance.playerPlaying != whichPlayer || moving || dir == Vector2.zero)
            {
                return;
            }
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
                    GameManager.instance.SpendAction();
                    return;
                }
                
                //if theres no player, instantiate the bullet as normal
                GameObject b = BulletPool.Instance.RequestPoolObject();
                GameManager.instance.SpendAction();
                b.transform.position = tilehit.transform.position;
                b.SetActive(true);
                b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>(), this);
                
                
                
                print(dir.ToString()); //debug what dir the bullet is getting
            }
        }
    }

    private void FinalizeMove()
    {
        moving = false;
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

    public void UseSpecial()
    {
        special.ActivateSpecial();
    }
}
