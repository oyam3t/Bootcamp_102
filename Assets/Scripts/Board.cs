using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int height;
    public int width;
    public GameObject tilePrefab;

    private BackgroundTile[,] allTiles;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[height, width];
        SetUp();
    }

    private void SetUp()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPos = new Vector2(i, j);
                GameObject bgTile = Instantiate(tilePrefab, tempPos, Quaternion.identity) as GameObject;

                bgTile.transform.parent = this.transform;
                bgTile.name = "(" + i + "," + j + ")";


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
