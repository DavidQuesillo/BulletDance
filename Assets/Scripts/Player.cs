using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Vector2 dir;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerTurns whichPlayer;
    [SerializeField] private Tile tileOn;
    [SerializeField] private float moveDuration = 0.3f;
    [SerializeField] private int hp = 3;
    private bool moving;
    

    // Start is called before the first frame update
    void Start()
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
        tileOn.SetAsPlayerOn(whichPlayer);

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

    }

    public void TrackDir(InputAction.CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
    
    }

    public void MoveInDir(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (GameManager.instance.playerPlaying != whichPlayer || moving)
            {
                return;
            }
            Collider2D tilehit = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

            if (tilehit != null)
            {
                //rb.MovePosition(tilehit.transform.position); //replace with tween
                if (tilehit.GetComponent<Tile>().GetIfBullet())
                {
                    if (tilehit.GetComponent<Tile>().GetWhoseBullet() != whichPlayer)
                    {
                        TakeDamage();
                        print("player stepped on");
                        //tilehit.gameObject.SetActive(false);
                        tilehit.GetComponent<Tile>().GetBulletOnThis().BulletDestroy();
                    }
                }
                StartCoroutine(PlayerMove(tilehit.transform.position)); //here it is

                tileOn.SetAsPlayerOff();
                //tilehit.GetComponent<Tile>().SetAsPlayerOn(whichPlayer);

                tileOn = tilehit.GetComponent<Tile>();
                tileOn.SetAsPlayerOn(whichPlayer);
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
        if (ctx.started)
        {
            if (GameManager.instance.playerPlaying != whichPlayer || moving)
            {
                return;
            }
            Collider2D tilehit = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

            if (tilehit != null)
            {
                GameObject b = BulletPool.Instance.RequestPoolObject();
                GameManager.instance.SpendAction();
                b.transform.position = tilehit.transform.position;
                b.SetActive(true);
                b.GetComponent<Bullet>().BulletInit(whichPlayer, dir, tilehit.GetComponent<Tile>());
            }
        }
    }

    private IEnumerator PlayerMove(Vector2 to)
    {
        Tween t = rb.DOMove(to, moveDuration);
        moving = true;

        /*while (t.IsActive())
        {
            yield return null;
        }*/
        yield return t.WaitForCompletion();

        yield return new WaitForSeconds(0.5f);

        GameManager.instance.SpendAction();
        moving = false;
        yield break;
    }
}
