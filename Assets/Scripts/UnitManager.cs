using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;

    [SerializeField] private GameObject selectedTile = null;

    public GameObject GetSelectedTile()
    {
        return this.selectedTile;
    }
    public void SetSelectedTile(GameObject tile)
    {
        this.selectedTile = tile;
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
