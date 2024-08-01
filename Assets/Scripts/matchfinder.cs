using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class matchfinder : MonoBehaviour
{
    public string lookForTag;
    private Board board;
    public goalcounter goalcounter;
    public List<GameObject> currentMatches = new List<GameObject>();
    void Start()
    {
        board = FindObjectOfType<Board>();
        goalcounter = FindObjectOfType<goalcounter>();
    }
    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentGem = board.allGems[i, j];

                if (currentGem != null)
                {
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftGem = board.allGems[i - 1, j];
                        GameObject rightGem = board.allGems[i + 1, j];

                        if (leftGem != null && rightGem != null)
                        {
                            if (leftGem.tag == currentGem.tag && rightGem.tag == currentGem.tag)
                            {
                                if (!currentMatches.Contains(rightGem))
                                {
                                    currentMatches.Add(rightGem);
                                    if(rightGem.tag == lookForTag)
                                        goalcounter.piececount++;
                                }
                                if (!currentMatches.Contains(leftGem))
                                {
                                    currentMatches.Add(leftGem);
                                    if (leftGem.tag == lookForTag)
                                        goalcounter.piececount++;
                                }
                                if (!currentMatches.Contains(currentGem))
                                {
                                    currentMatches.Add(currentGem);
                                    if (currentGem.tag == lookForTag)
                                        goalcounter.piececount++;
                                }
                                rightGem.GetComponent<Gem>().matched = true;
                                leftGem.GetComponent<Gem>().matched = true;
                                currentGem.GetComponent<Gem>().matched = true;
                            }
                        }
                    }

                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upGem = board.allGems[i, j + 1];
                        GameObject downGem = board.allGems[i, j - 1];

                        if (upGem != null && downGem != null)
                        {
                            if (upGem.tag == currentGem.tag && downGem.tag == currentGem.tag)
                            {
                                if (!currentMatches.Contains(upGem))
                                {
                                    currentMatches.Add(upGem);
                                    if (upGem.tag == lookForTag)
                                        goalcounter.piececount++;
                                }
                                if (!currentMatches.Contains(downGem))
                                {
                                    currentMatches.Add(downGem);
                                    if (downGem.tag == lookForTag)
                                        goalcounter.piececount++;
                                }
                                if (!currentMatches.Contains(currentGem))
                                {
                                    currentMatches.Add(currentGem);
                                    if (currentGem.tag == lookForTag)
                                        goalcounter.piececount++;
                                }
                                downGem.GetComponent<Gem>().matched = true;
                                upGem.GetComponent<Gem>().matched = true;
                                currentGem.GetComponent<Gem>().matched = true;
                            }
                        }
                    }
                }
            }
        }

        goalcounter.updateCount();
    }

}
