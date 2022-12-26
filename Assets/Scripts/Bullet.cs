using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum bulletDirs { L, LU, U, UR, R, DR, D, LD}
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator anim;
    public PlayerTurns shotByWho = PlayerTurns.Player1;
    private Player shooter;
    [SerializeField] private Vector2 dir;
    [SerializeField] private bulletDirs LetterDir;
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
        GameManager.onMatchEnd += BulletDisable;
        //GameManager.onTurnSwitch += BecomeMovable;
    }

    public void BulletInit(PlayerTurns whose, Vector2 flyTo, Tile on, Player fromWho)
    {        
        hasToMove = false;
        tileOn = on;        
        shotByWho = whose;
        
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
            //sr.transform.rotation = Quaternion.identity;
            anim.Play("bulletH");
            sr.flipX = false;
            sr.flipY = false;

            LetterDir = bulletDirs.R;
        }
        else if (dir == Vector2.right + Vector2.up)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 45f, 0);
            anim.Play("bulletD");
            sr.flipX = false;
            sr.flipY = false;

            LetterDir = bulletDirs.UR;
        }
        else if (dir == Vector2.up)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 90f, 0);
            anim.Play("bulletV");
            sr.flipX = false;
            sr.flipY = false;

            LetterDir = bulletDirs.U;
        }
        else if (dir == Vector2.left + Vector2.up)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 135f, 0);
            anim.Play("bulletD");
            sr.flipX = true;
            sr.flipY = false;

            LetterDir = bulletDirs.LU;
        }
        else if (dir == Vector2.left)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 180f, 0);
            anim.Play("bulletH");
            sr.flipX = true;
            sr.flipY = false;

            LetterDir = bulletDirs.L;
        }
        else if (dir == Vector2.left + Vector2.down)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 225f, 0);
            anim.Play("bulletD");
            sr.flipX = true;
            sr.flipY = true;

            LetterDir = bulletDirs.LD;
        }
        else if (dir == Vector2.down)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 270f, 0);
            anim.Play("bulletV");
            sr.flipX = false;
            sr.flipY = true;

            LetterDir = bulletDirs.D;
        }
        else if (dir == Vector2.down + Vector2.right)
        {
            //sr.transform.rotation = new Quaternion(0, 0, 315f, 0);
            anim.Play("bulletD");
            sr.flipX = false;
            sr.flipY = true;

            LetterDir = bulletDirs.DR;
        }
        on.SetAsBulletOn(shotByWho, this, LetterDir);
        BecomeMovable();
        //print(transform.rotation.z.ToString());
    }
    public bulletDirs GetDir()
    {
        return LetterDir;
    }

    private void BecomeMovable()
    {
        if (gameObject.activeInHierarchy)
            hasToMove = true;
    }

    public void AdvanceBullet()
    {
        //print("advancing bullet");
        if (GameManager.instance.playerPlaying == shotByWho || outOfBounds || !hasToMove)
        {
            //hasToMove = true;
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
            tilehit.GetComponent<Tile>().SetAsBulletOn(shotByWho, this, LetterDir);
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
                    //BulletDestroy(true); //old
                    rb.DOMove(rb.position + dir, 0.2f).OnComplete(() => BulletDestroy(true)); //new testing
                }
                
            }

            //StartCoroutine(BulletMovement(tilehit.transform.position, false));
            rb.DOMove(tilehit.transform.position, 0.2f);
        }
        else
        {
            outOfBounds = true;
            rb.DOMove(rb.position + dir, 0.2f).OnComplete(() => BulletDestroy(false));


            //tileOn.SetAsBulletOff(this); //done again in destroy function
            //Destroy(gameObject); //replace with tween into pooling
            //gameObject.SetActive(false); //still needs tween
            //StartCoroutine(BulletMovement(rb.position + dir, true));

            //Tween t = rb.DOMove(tilehit.transform.position, 0.5f);

            /*Collider2D playerHit = Physics2D.Raycast(transform.position + (Vector3)dir, dir, 0.3f, LayerMask.GetMask("Default")).collider;
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
            }*/
            
            
            //Destroy(gameObject, 0.5f);
        }
        //SoundManager.instance.PlayBulletsAdvSound();
    }

    public void BulletDestroy(bool hitEnemy)
    {
        if (hitEnemy)
        {
            GameObject p = PoofPool.Instance.RequestPoolObject();
            p.transform.position = gameObject.transform.position;
            p.GetComponent<SpriteRenderer>().color = sr.color;
            p.GetComponent<Animator>().Play("bulletPoof");

        }

        tileOn.SetAsBulletOff(this);
        gameObject.SetActive(false);
        hasToMove = false;
        //print("by bulletdestroy");
    }
    public void BulletDisable() //this exists literally entirely for onMatchEnd
    {
        tileOn.SetAsBulletOff(this);
        gameObject.SetActive(false);
        hasToMove = false;
    }

    public PlayerTurns GetWhose()
    {
        return shotByWho;
    }

    //replace by tween lambda
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
