using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[] pieces;

    private GameObject[,] allPieces;

    void Start()
    {
        allPieces = new GameObject[width, height];
        Setup();
    }

    private void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 tempPosition = new Vector2(x, y);
                int pieceToUse = Random.Range(0, pieces.Length);
                GameObject piece = Instantiate(pieces[pieceToUse], tempPosition, Quaternion.identity);
                piece.transform.parent = this.transform;
                piece.name = "( " + x + ", " + y + " )";
                allPieces[x, y] = piece;
            }
        }
    }
    private bool CheckForMatches()
    {
        bool foundMatch = false;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x > 1 && allPieces[x, y].tag == allPieces[x - 1, y].tag && allPieces[x, y].tag == allPieces[x - 2, y].tag)
                {
                    Destroy(allPieces[x, y]);
                    Destroy(allPieces[x - 1, y]);
                    Destroy(allPieces[x - 2, y]);
                    foundMatch = true;
                }

                if (y > 1 && allPieces[x, y].tag == allPieces[x, y - 1].tag && allPieces[x, y].tag == allPieces[x, y - 2].tag)
                {
                    Destroy(allPieces[x, y]);
                    Destroy(allPieces[x, y - 1]);
                    Destroy(allPieces[x, y - 2]);
                    foundMatch = true;
                }
            }
        }

        return foundMatch;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckForMatches())
            {
                Debug.Log("Match Found");
            }
        }
    }
}
