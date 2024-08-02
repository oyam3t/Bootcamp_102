using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goalcounter : MonoBehaviour
{
    public int piececount = 0;
    public int goal;
    public GameObject welldone;
    public TextMeshProUGUI text;
    private Board board;
    private string path;
    public int level_id;
    public AudioSource audiosource;

    private void Start()
    {
        audiosource.Play();
        board = FindAnyObjectByType<Board>();
        path = Path.Combine(Application.persistentDataPath, "savedNumber.txt");
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

        File.WriteAllText(path, (level_id + 1).ToString());

        //switch scene and overwrite save file
        SceneManager.LoadScene(sceneName: "Levelselect");
        Debug.Log("scene switch");
    }

    public void homebuttonOnClick()
    {
        SceneManager.LoadScene(sceneName: "Levelselect");
    }

}
