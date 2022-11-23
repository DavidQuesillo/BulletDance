using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 dir;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerTurns whichPlayer;
    [SerializeField] private Tile tileOn;
    

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

    public void TrackDir(InputAction.CallbackContext ctx)
    {
        dir = ctx.ReadValue<Vector2>();
    
    }

    public void MoveInDir(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (GameManager.instance.playerPlaying != whichPlayer)
            {
                return;
            }
            Collider2D tilehit = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

            if (tilehit != null)
            {
                rb.MovePosition(tilehit.transform.position); //replace with tween
                tileOn.SetAsPlayerOff();
                tilehit.GetComponent<Tile>().SetAsPlayerOn(whichPlayer);

                tileOn = tilehit.GetComponent<Tile>();
                GameManager.instance.SpendAction();
                print("moved to " + tilehit.name);
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
            if (GameManager.instance.playerPlaying != whichPlayer)
            {
                return;
            }
            Collider2D tilehit = Physics2D.Raycast(transform.position, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider;

            if (tilehit != null)
            {
                GameObject b = BulletPool.Instance.RequestPoolObject();
                GameManager.instance.SpendAction();
                b.transform.position = tilehit.transform.position;
                b.GetComponent<Bullet>().BulletInit(whichPlayer, dir);
            }
        }
    }
}
