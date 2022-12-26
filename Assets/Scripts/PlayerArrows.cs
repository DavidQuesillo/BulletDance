using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerArrows : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color goodColor;
    [SerializeField] private Color unavColor;
    [Header("Arrows")]
    [SerializeField] private SpriteRenderer arrowU;
    [SerializeField] private SpriteRenderer arrowD;
    [SerializeField] private SpriteRenderer arrowL;
    [SerializeField] private SpriteRenderer arrowR;
    [SerializeField] private SpriteRenderer arrowUL;
    [SerializeField] private SpriteRenderer arrowUR;
    [SerializeField] private SpriteRenderer arrowDL;
    [SerializeField] private SpriteRenderer arrowDR;
    [SerializeField] private SpriteRenderer[] arraws = new SpriteRenderer[8];


    public void ArrowVisibility(bool show)
    {
        for (int i = 0; i < arraws.Length; i++)
        {
            arraws[i].gameObject.SetActive(show);
            print("showing arrows");
        }
    }
    public void CheckAvailableMoves()
    {
        //left
        CheckValidArrow(arrowL, Vector3.left);
        //right
        CheckValidArrow(arrowR, Vector3.right);
        //up
        CheckValidArrow(arrowU, Vector3.up);
        //down
        CheckValidArrow(arrowD, Vector3.down);
        //upleft
        CheckValidArrow(arrowUL, Vector3.left + Vector3.up);
        //upright
        CheckValidArrow(arrowUR, Vector3.right + Vector3.up);
        //downleft
        CheckValidArrow(arrowDL, Vector3.left + Vector3.down);
        //downright
        CheckValidArrow(arrowDR, Vector3.right + Vector3.down);
    }

    public void CheckValidArrow(SpriteRenderer whichArrow, Vector3 dir)
    {
        Tile tileL = Physics2D.Raycast(transform.position + dir * 0.5f, dir, 1f, LayerMask.GetMask("Grid"), -0.5f).collider?.GetComponent<Tile>();
        if (tileL != null)
        {
            if (tileL.GetIfPlayerOn() == false)
            {
                whichArrow.color = goodColor;
            }            
        }
        else
        {
            whichArrow.color = unavColor;
        }
    }
    public void HighlightPointingArrow(Vector2 dir)
    {
        #region diagonals
        //Upright
        /*if (dir == Vector2.right + Vector2.up) { arrowUR.transform.localScale = Vector3.one * 1.2f; return;}
        else { arrowUR.transform.localScale = Vector3.one;}*/
        IndependentArrowHighlightCheck(dir, Vector2.right + Vector2.up, arrowUR);

        //upleft
        /*if (dir == Vector2.left + Vector2.up) { arrowUL.transform.localScale = Vector3.one * 1.2f; return;}
        else { arrowUL.transform.localScale = Vector3.one;}*/
        IndependentArrowHighlightCheck(dir, Vector2.left + Vector2.up, arrowUL);

        //downleft
        /*if (dir == Vector2.left + Vector2.down) { arrowDL.transform.localScale = Vector3.one * 1.2f; return; }
        else { arrowDL.transform.localScale = Vector3.one; }*/
        IndependentArrowHighlightCheck(dir, Vector2.left + Vector2.down, arrowDL);

        //downright
        /*if (dir == Vector2.right + Vector2.down) { arrowDR.transform.localScale = Vector3.one * 1.2f; return; }
        else { arrowDR.transform.localScale = Vector3.one; }*/
        IndependentArrowHighlightCheck(dir, Vector2.right + Vector2.down, arrowDR);
        #endregion
        #region cardinals
        //right
        /*if (dir == Vector2.right) { arrowR.transform.localScale = Vector3.one * 1.2f; return;}
        else { arrowR.transform.localScale = Vector3.one;}*/
        IndependentArrowHighlightCheck(dir, Vector2.right, arrowR);

        //left
        /*if (dir == Vector2.left) { arrowL.transform.localScale = Vector3.one * 1.2f; return; }
        else { arrowL.transform.localScale = Vector3.one; }*/
        IndependentArrowHighlightCheck(dir, Vector2.left, arrowL);

        //down
        /*if (dir == Vector2.down) { arrowD.transform.localScale = Vector3.one * 1.2f; return; }
        else { arrowD.transform.localScale = Vector3.one; }*/
        IndependentArrowHighlightCheck(dir, Vector2.down, arrowD);

        //up
        /*if (dir == Vector2.up) { arrowU.transform.localScale = Vector3.one * 1.2f; return; }
        else { arrowU.transform.localScale = Vector3.one; }*/
        IndependentArrowHighlightCheck(dir, Vector2.up, arrowU);
        #endregion
    }
    private void IndependentArrowHighlightCheck(Vector2 dir, Vector2 neededDir, SpriteRenderer whichArrow)
    {
        if (dir == neededDir) { whichArrow.transform.localScale = Vector3.one * 1.4f;}
        else { whichArrow.transform.localScale = Vector3.one; }
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
