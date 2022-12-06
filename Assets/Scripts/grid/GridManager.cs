using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    [SerializeField] public int width;
    [SerializeField] public int height;

    [SerializeField] private Tile gridTile;

    [SerializeField] private Transform cam;
    [SerializeField] private GameObject bg;
    public Tile[,] grid;

    public void GenerateGrid()
    {
        grid = new Tile[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile t = Instantiate(gridTile, new Vector3((float)i * 0.5f, j), Quaternion.identity);

                t.name = $"tile {i} {j}";
                grid[i, j] = t;
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

        cam.transform.position = new Vector3((float)width / 4, (float)height / 3 * 2, -10);
        bg.transform.position = new Vector3((float)width / 4, (float)height / 3 * 2, 0);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        GenerateGrid();
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
