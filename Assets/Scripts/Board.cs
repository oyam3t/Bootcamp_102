using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    wait,
    move
}

public class Board : MonoBehaviour
{
    public GameState currentState = GameState.move;

    public int height;
    public int width;
    public GameObject tilePrefab;
    public GameObject[] tiles;
    public int offset;

    private BackgroundTile[,] allTiles;
    public matchfinder matchfinder;

    public GameObject[,] allGems;

    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[height, width];
        allGems = new GameObject[width, height];
        matchfinder = FindAnyObjectByType<matchfinder>();
        SetUp();
    }

    private void SetUp()
    {
        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPos = new Vector2(i, j + offset);
                GameObject bgTile = Instantiate(tilePrefab, new Vector3(i,j+offset,0.2f), Quaternion.identity) as GameObject;

                bgTile.transform.parent = this.transform;
                bgTile.name = "(" + i + "," + j + ")";


                int tileToUse = Random.Range(0, tiles.Length);

                while (MatchesAt(i, j, tiles[tileToUse]))
                {
                    tileToUse = Random.Range(0, tiles.Length);
                }

                GameObject tile = Instantiate(tiles[tileToUse], tempPos, Quaternion.identity);
                tile.GetComponent<Gem>().row = j;
                tile.GetComponent<Gem>().col = i;

                tile.transform.parent = this.transform;
                tile.name = "(" + i + "," + j + ")";

                allGems[i,j] = tile;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allGems[column - 1, row] != null && allGems[column - 2, row] != null)
            {
                if (allGems[column - 1, row].tag == piece.tag && allGems[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
            if (allGems[column, row - 1] != null && allGems[column, row - 2] != null)
            {
                if (allGems[column, row - 1].tag == piece.tag && allGems[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }

        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allGems[column, row - 1] != null && allGems[column, row - 2] != null)
                {
                    if (allGems[column, row - 1].tag == piece.tag && allGems[column, row - 2].tag == piece.tag)
                    {
                        return true;
                    }
                }
            }
            if (column > 1)
            {
                if (allGems[column - 1, row] != null && allGems[column - 2, row] != null)
                {
                    if (allGems[column - 1, row].tag == piece.tag && allGems[column - 2, row].tag == piece.tag)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }


    private void DestroyMatchesAt(int column, int row)
    {
        if (allGems[column, row].GetComponent<Gem>().matched)
        {
            //How many elements are in the matched pieces list from findmatches?
            //if (findMatch.currentMatches.Count >= 4)
            //{
            //    CheckToMakeBombs();
            //}

            //Does a tile need to break?
            //if (breakableTiles[column, row] != null)
            //{
            //    //if it does, give one damage.
            //    breakableTiles[column, row].TakeDamage(1);
            //    if (breakableTiles[column, row].hitPoints <= 0)
            //    {
            //        breakableTiles[column, row] = null;
            //    }

            //}
            //Does the sound manager exist?
            //if (soundManager != null)
            //{
            //    soundManager.PlayRandomDestroyNoise();
            //}
            //GameObject particle = Instantiate(destroyParticle,
            //                                  allDots[column, row].transform.position,
            //                                  Quaternion.identity);
            //Destroy(particle, .5f);
            Destroy(allGems[column, row]);
            //scoreManager.IncreaseScore(basePieceValue * streakValue);
            allGems[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        matchfinder.currentMatches.Clear();
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo()
    {
        int nullCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allGems[i, j].GetComponent<Gem>().row -= nullCount;
                    allGems[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FillBoardCo());
    }

    private void RefillBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] == null)
                {
                    Vector2 tempPosition = new Vector2(i, j + offset);
                    int tileToUse = Random.Range(0, tiles.Length);

                    //while (MatchesAt(i, j, tiles[tileToUse]))
                    //{
                    //    tileToUse = Random.Range(0, tiles.Length);
                    //}

                    GameObject piece = Instantiate(tiles[tileToUse], tempPosition, Quaternion.identity);
                    allGems[i, j] = piece;
                    piece.GetComponent<Gem>().row = j;
                    piece.GetComponent<Gem>().col = i;

                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (allGems[i, j] != null)
                {
                    if (allGems[i, j].GetComponent<Gem>().matched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    

    private IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            //streakValue++;
            DestroyMatches();
            yield return new WaitForSeconds(2);

        }
        matchfinder.currentMatches.Clear();
        //currentDot = null;


        //if (IsDeadlocked())
        //{
        //    StartCoroutine(ShuffleBoard());
        //    Debug.Log("Deadlocked!!!");
        //}
        yield return new WaitForSeconds(.5f);
        currentState = GameState.move;
        //streakValue = 1;

    }

}
