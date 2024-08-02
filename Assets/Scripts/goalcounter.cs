using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goalcounter : MonoBehaviour
{
    public int piececount = 0;
    public int goal;
    public GameObject welldone;
    public TextMeshProUGUI text;
    private Board board;

    private void Start()
    {
        board = FindAnyObjectByType<Board>();
    }

    public void updateCount()
    {
        text.text = piececount.ToString() + "/" + goal.ToString();
        if (piececount >= goal)
        {
            Debug.Log("kazandınız");
            welldone.SetActive(true);
            board.currentState = GameState.wait;
            StartCoroutine(switchscene());
        }
    }

    private IEnumerator switchscene()
    {
        yield return new WaitForSeconds(3f);
        //switch scene and overwrite save file
        SceneManager.LoadScene(sceneName: "Levelselect");
        Debug.Log("scene switch");
    }

    public void homebuttonOnClick()
    {
        SceneManager.LoadScene(sceneName: "Levelselect");
    }

}
