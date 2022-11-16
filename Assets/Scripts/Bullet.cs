using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public bool fromPlayer1 = true;
    private Vector2 dir;

    public void BulletInit(bool whose, Vector2 flyTo)
    {
        fromPlayer1 = whose;
        dir = flyTo;
    }

    public void AdvanceBullet()
    {
        Collider2D tilehit = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

        if (tilehit != null)
        {
            rb.MovePosition(tilehit.transform.position); //replace with tween
        }
        else
        {
            Destroy(gameObject); //replace with tween into pooling
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
