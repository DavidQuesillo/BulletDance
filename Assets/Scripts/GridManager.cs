using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Tile tile;

    [SerializeField] private Camera cam;

    void GenerateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile t = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                t.name = $"Tile {x} {y}";

                bool isOffset;
                if (x % 2 == 0 && y % 2 == 0 || (x % 2 != 0 && y % 2 != 0))
                {
                    isOffset = true;
                }
                else
                {
                    isOffset = false;
                }

                t.TileInit(isOffset);
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
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
