using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    public PlayerTurns shotByWho = PlayerTurns.Player1;
    private Player shooter;
    [SerializeField] private Vector2 dir;
    private bool hasToMove;
    private WaitForSeconds bulletDelay;
    private bool outOfBounds;
    [SerializeField] private Tile tileOn;
    [SerializeField] private float bulletMoveTimer = 1f;

    void Start()
    {
        bulletDelay = new WaitForSeconds(bulletMoveTimer);
//        tileOn = Physics2D.Raycast(transform.position + (Vector3)dir, dir, 0.3f, LayerMask.GetMask("Grid")).collider.GetComponent<Tile>();
        GameManager.onPlayedMoved += AdvanceBullet;
        GameManager.onMatchEnd += BulletDestroy;
    }

    public void BulletInit(PlayerTurns whose, Vector2 flyTo, Tile on, Player fromWho)
    {        
        tileOn = on;        
        shotByWho = whose;
        on.SetAsBulletOn(shotByWho, this);
        outOfBounds = false;
        shooter = fromWho;
        if (whose == PlayerTurns.Player1)
        {
            sr.color = Color.blue;
        }
        else
        {
            sr.color = Color.red;
        }

        dir = flyTo;
        if (dir == Vector2.right)
        {
            sr.transform.rotation = Quaternion.identity;
        }
        else if (dir == Vector2.right + Vector2.up)
        {
            sr.transform.rotation = new Quaternion(0, 0, 45f, 0);
        }
        else if (dir == Vector2.up)
        {
            sr.transform.rotation = new Quaternion(0, 0, 90f, 0);
        }
        else if (dir == Vector2.left + Vector2.up)
        {
            sr.transform.rotation = new Quaternion(0, 0, 135f, 0);
        }
        else if (dir == Vector2.left)
        {
            sr.transform.rotation = new Quaternion(0, 0, 180f, 0);
        }
        else if (dir == Vector2.left + Vector2.down)
        {
            sr.transform.rotation = new Quaternion(0, 0, 225f, 0);
        }
        else if (dir == Vector2.down)
        {
            sr.transform.rotation = new Quaternion(0, 0, 270f, 0);
        }
        else if (dir == Vector2.down + Vector2.right)
        {
            sr.transform.rotation = new Quaternion(0, 0, 315f, 0);
        }
        //print(transform.rotation.z.ToString());
    }

    public void AdvanceBullet()
    {
        //print("advancing bullet");
        if (GameManager.instance.playerPlaying == shotByWho || outOfBounds)
        {
            return;
        }
        Collider2D tilehit = Physics2D.Raycast(transform.position + (Vector3)dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

        if (tilehit != null)
        {
            //rb.MovePosition(tilehit.transform.position); //replace with tween
            //StartCoroutine(BulletMovement(tilehit.transform.position, false));

            //rb.DOMove(tilehit.transform.position, 0.5f);
            //)
            tileOn.SetAsBulletOff(this);
            tilehit.GetComponent<Tile>().SetAsBulletOn(shotByWho, this);
            tileOn = tilehit.GetComponent<Tile>();

            if (tileOn.GetIfPlayerOn())
            {
                if (tileOn.GetPlayerOnThis() == shooter)
                {
                    Debug.Log("this one doin gamage");
                }
                else
                {
                    print("damage through here");
                    tileOn.GetPlayerOnThis().TakeDamage();
                    gameObject.SetActive(false);
                }
                
            }

            //StartCoroutine(BulletMovement(tilehit.transform.position, false));
            rb.DOMove(tilehit.transform.position, 0.2f);
        }
        else
        {
            tileOn.SetAsBulletOff(this);
            //Destroy(gameObject); //replace with tween into pooling
            //gameObject.SetActive(false); //still needs tween
            //StartCoroutine(BulletMovement(rb.position + dir, true));

            //Tween t = rb.DOMove(tilehit.transform.position, 0.5f);

            Collider2D playerHit = Physics2D.Raycast(transform.position + (Vector3)dir, dir, 0.3f, LayerMask.GetMask("Default")).collider;
            if (playerHit == GameManager.instance.player2 || playerHit == GameManager.instance.player1)
            {
                print("touchedplayer");
                if (playerHit.GetComponent<Player>().GetWhichPlayer() != shotByWho)
                {
                    playerHit.GetComponent<Player>().TakeDamage();
                    //StartCoroutine(BulletMovement(playerHit.transform.position, true));
                    rb.DOMove(tilehit.transform.position, 0.2f).OnComplete(()=> BulletDestroy());
                    print("Hit player");
                }
            }
            else
            {
                //StartCoroutine(BulletMovement(rb.position + dir, true));
                //rb.DOMove(tilehit.transform.position, 0.2f).OnComplete(() => gameObject.SetActive(false));
                outOfBounds = true;
                rb.DOMove(rb.position + dir, 0.2f).OnComplete(() => BulletDestroy());
                print("bullet OoB");
            }
            
            
            //Destroy(gameObject, 0.5f);
        }
    }

    public void BulletDestroy()
    {
        gameObject.SetActive(false);
        //print("by bulletdestroy");
    }

    public PlayerTurns GetWhose()
    {
        return shotByWho;
    }

    private IEnumerator BulletMovement(Vector2 to, bool poolAfter)
    {
        /*while (!hasToMove)
        {
            yield return null;
        }
        Tween t = rb.DOMove(to, 0.2f);
        while (hasToMove)
        {
            while (t.IsActive())
            {
                yield return null;
            }
            if (poolAfter)
            {
                gameObject.SetActive(false);
                hasToMove = false;
                break;
            }
        }*/
        
        //yield return bulletDelay;
        Tween t = rb.DOMove(to, 0.2f);
        //t.OnComplete<Bullet>(BulletDestroy);

        while (t.IsActive())
        {
            yield return null;
        }
        if (poolAfter)
        {
            gameObject.SetActive(false);
        }
        //GameManager.instance.SpendAction();
        yield break;
    }
}
