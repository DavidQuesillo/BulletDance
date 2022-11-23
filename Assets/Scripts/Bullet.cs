using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    public PlayerTurns shotByWho = PlayerTurns.Player1;
    private Vector2 dir;

    void Start()
    {
        GameManager.onActionPassed += AdvanceBullet;
    }

    public void BulletInit(PlayerTurns whose, Vector2 flyTo)
    {
        shotByWho = whose;
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
    }

    public void AdvanceBullet()
    {
        if (GameManager.instance.playerPlaying == shotByWho)
        {
            return;
        }
        Collider2D tilehit = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

        if (tilehit != null)
        {
            rb.MovePosition(tilehit.transform.position); //replace with tween
        }
        else
        {
            //Destroy(gameObject); //replace with tween into pooling
            gameObject.SetActive(false); //still needs tween
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
