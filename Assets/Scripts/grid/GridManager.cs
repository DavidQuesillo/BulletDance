using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private Tile gridTile;

    [SerializeField] private Transform cam;

    void GenerateGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile t = Instantiate(gridTile, new Vector3((float)i * 0.5f, j), Quaternion.identity);

                /*if (i % 2 == 0 && j % 2 != 0 || i % 2 != 0 && j % 2 == 0)
                {
                    t.TileInit(true);
                }
                else
                {
                    t.TileInit(false);
                }*/

                if (i % 2 != 0)
                {
                    t.transform.position = t.transform.position + Vector3.up*0.5f;
                }
            }
        }

        cam.transform.position = new Vector3((float)width / 4, (float)height / 2 - 0.5f, -10);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
